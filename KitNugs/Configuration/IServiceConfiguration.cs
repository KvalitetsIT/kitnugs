namespace KitNugs.Configuration
{
    public interface IServiceConfiguration
    {
        enum ConfigurationVariables
        {
            TEST_VAR,
        }

        string GetConfigurationValue(ConfigurationVariables configurationVariable);
    }
}
