using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_5320.Models
{
    public class Asset : Base
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}