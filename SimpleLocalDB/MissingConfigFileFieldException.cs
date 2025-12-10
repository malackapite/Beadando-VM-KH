namespace SimpleLocalDB
{
    // Kötelezően megadandó konfigurációs file mezőjének hiányára dobandó kivétel típus.
    public class MissingConfigFileFieldException(string fieldName) : InvalidOperationException($"Field '{fieldName}' is missing from the config file.")
    {

    }
}
