using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTest")]
namespace KitNugs.Configuration
{
    [Serializable]
    internal class UnsetEnvironmentVariableException : Exception
    {
        public UnsetEnvironmentVariableException(string message) : base($"Environment variable '{message}' not set.")
        {
        }
    }
}
