using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_5320.Models
{
    public class Department : Base
    {
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}