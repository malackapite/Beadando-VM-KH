namespace SimpleLocalDB
{
    /// <summary>
    /// Instantiates a <see cref="InvalidOrMissingConfigFileFieldException"/> object with a specified field name in the configuration file.
    /// </summary>
    /// <param name="fieldName"></param>
    /// <author>KeresztesHunor</author>
    public class InvalidOrMissingConfigFileFieldException(string fieldName) : InvalidOperationException($"Field '{fieldName}' is missing from the config file.")
    {

    }
}
