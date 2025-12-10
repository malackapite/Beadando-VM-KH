using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SimpleLocalDB
{
    public abstract class AppDbContext(IConfiguration config) : DbContext()
    {
        /*
         * Ez a teljes elérési útvonala a solution-t tartalmazó mappának.
         * Az AppContext.BaseDirectory a {solution-t tartalmazó mappa útvonala}/bin/Debug/net9.0/ mappájának az elérési útvonalát adja,
         * a "!.Parent!.Parent!.Parent"-tel pedig eljutunk belőle a solution-t tartalmazó mappához,
         * aminek lekérjük a teljes elérési útvonalát a "!.FullName"-mel.
         */
        public static readonly string BasePath = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

        // Konfigurációs file-ból való adatlekérdezésre szolgáló kezelő.
        IConfiguration config { get; } = config;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
             * Általában az appconfig.json ConnectionString mezőjében tárolt connection string-ek közül használandó mező nevét direktben itt a kódban szokták megadni,
             * de mivel a DbContext osztály a dll-ben van benne, ezért az őt használó projekt nem tudja megváltoztatni,
             * ezért azt is az appconfig.json file-ból határozzuk meg a "UseConnectionString" mezőben.
             */
            const string UseConnectionString = nameof(UseConnectionString);
            string? connectionString = config.GetConnectionString(config[UseConnectionString] ?? throw new MissingConfigFileFieldException(UseConnectionString));
            /*
             * Az appconfig.json file-ban a connection string-ben az "AttachDbFilename" paraméternek a file teljes elérési útvonalát kell megadni,
             * amit a program saját maga keres ki a kódban,
             * tehát a connection string-ben egy {0} mezővel jeleztük az beszúrandó elérési útvonal helyét,
             * hogy be lehessen szúrni egy string.Format() segítségével.
             */
            optionsBuilder.UseSqlServer(connectionString is not null ? string.Format(connectionString, AppContext.BaseDirectory) : null);
        }
    }

    /*
     * Leszármazott generikus osztály, ami azért van elkülönítve a nem generikus ősosztályától,
     * mert a különböző típusokkal használt generikus osztályok (illetve struct-ok és metódusok) különbözőnek számítanak
     * és ezért a bennük lévő statikus mezők is azokhoz a meghatározott típusokhoz tartoznak.
     * Mivel az ősosztályban lévő BasePath mezőnek nem kell egyedinek lennie a különböző típusokkal megadott AppDbContext<T> típusok példányai között,
     * a nem generikus ősosztályban van, így az összes különböző típussal megadott leszármazott AppDbContext<T> osztálypéldány között is ugyanaz,
     * de az alreadyWiped mezőnek pedig a különböző típusokkal használt AppDbContext<T> osztálypédányok között kell megegyeznie,
     * ezért a leszármazott generikus típusú osztályban van.
     */
    public class AppDbContext<T> : AppDbContext where T : class
    {
        /*
         * Ez egy flag, ami azt jelöli az AppDbContext<T> osztály példányosításakor,
         * hogy volt-e már a ezelőtt a példányosítás előtt lenullázva a hozzátartozó adatbázis.
         */
        static bool alreadyWiped = false;

        public DbSet<T> values { get; private set; }

        public AppDbContext(IConfiguration config) : base(config)
        {
            if (!alreadyWiped)
            {
                /*
                 * Az appconfig.json file-ban van egy "RunInDevEnvironment" mező,
                 * ami arra szolgál, hogy ha igazra van állítva az értéke (és nem hiányzik a mező és helyesen boolean értéket kap),
                 * akkor "dev mód"-ban fut a program, ami azt jelenti,
                 * hogy az adatbázis minden futtatásnál újra lesz generálva (az összes adat elvesztésével együtt).
                 * Ez arra kellett, hogy ha fejlesztés közben megváltozott a modell szerkezete,
                 * akkor a belőle generált adatbázist frissíteni kell,
                 * amit csak az adatbázis kitörlésével és újragenerálásával lehet elérni,
                 * mert a migrációk használata nem volt opció (dokumentációban részletezve).
                 */
                string? runInDevEnvironment = config["RunInDevEnvironment"];
                if (runInDevEnvironment is not null && (!bool.TryParse(runInDevEnvironment, out bool result) || result))
                {
                    // Ha létezik az adatbázis, törölje ki.
                    Database.EnsureDeleted();
                }
                alreadyWiped = true;
            }
            // Ha nem létezik az adatbázis, generálja le.
            Database.EnsureCreated();
        }
    }
}
