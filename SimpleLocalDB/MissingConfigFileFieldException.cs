namespace SimpleLocalDB
{
    internal class MissingConfigFileFieldException(string fieldName) : InvalidOperationException($"Field '{fieldName}' is missing from the config file.")
    {

    }
}
