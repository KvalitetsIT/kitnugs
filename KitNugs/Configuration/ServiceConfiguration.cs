using System.ComponentModel.DataAnnotations;

namespace KitNugs.Configuration
{
    public class ServiceConfiguration
    {
        [Required]
        public string TEST_VAR { get; set; }
    }
}
