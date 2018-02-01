using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_5320.Models
{
    public class Maintainance : Base
    {
        [ForeignKey("User")]
        public int AssignedUserId { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual User User { get; set; }
        public virtual Department Department { get;set;}
    }
}