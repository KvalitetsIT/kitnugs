namespace KitNugs.Configuration
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        private IDictionary<string, string> values = new Dictionary<string, string>();
        public ServiceConfiguration(IConfiguration configuration)
        {
            foreach(string name in Enum.GetNames(typeof(ConfigurationVariables)))
            {
                values[name] = configuration.GetValue<string>(name) ?? throw new UnsetEnvironmentVariableException(name); ;
            }
        }

        public string GetConfigurationValue(ConfigurationVariables configurationVariable)
        {
            return values[configurationVariable.ToString()];
        }
    }
}
