namespace KitNugs.Configuration
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        private readonly IDictionary<string, string> _values = new Dictionary<string, string>();

        public ServiceConfiguration(IConfiguration configuration)
        {
            foreach(string name in Enum.GetNames(typeof(ConfigurationVariables)))
            {
                _values[name] = configuration.GetValue<string>(name) ?? throw new UnsetEnvironmentVariableException(name); ;
            }
        }

        public string GetConfigurationValue(ConfigurationVariables configurationVariable)
        {
            return _values[configurationVariable.ToString()];
        }
    }
}
