using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_5320.Models
{
    public class Asset : Base
    {
        public int TimesUsed { get; set; }

        [ForeignKey("Computer")]
        public int? ComputerId { get; set; }

        [ForeignKey("Software")]
        public int? SoftwareId { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }

        public virtual Computer Computer { get;set;}

        public virtual Software Software { get;set; }

        public virtual Status Status { get; set; }
    }
}