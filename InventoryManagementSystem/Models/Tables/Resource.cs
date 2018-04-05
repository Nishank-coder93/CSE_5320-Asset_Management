using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models.Tables
{
    public class Resource
    {
        public Resource()
        {
            Status = true;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public bool Verfied { get; set; }

        public bool Status { get; set; }

        [ForeignKey("Facility")]
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
    }
}