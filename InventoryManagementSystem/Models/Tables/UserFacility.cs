using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models.Tables
{
    public class UserFacility
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Facility")]
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }

        public virtual User User { get; set; }
    }
}