namespace KitNugs.Configuration
{
    public enum ConfigurationVariables
    {
        TEST_VAR,
    }

    public interface IServiceConfiguration
    {
        string GetConfigurationValue(ConfigurationVariables configurationVariable);
    }
}
