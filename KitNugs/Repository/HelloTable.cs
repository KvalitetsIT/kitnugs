using System.ComponentModel.DataAnnotations.Schema;

namespace KitNugs.Repository
{
    public class HelloTable
    {
        public int HelloTableId { get; set; }
        public DateTimeOffset Created { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? AnotherColumn { get; set; }
    }
}
