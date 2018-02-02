using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_5320.Models
{
    public class Location : Base
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }

        public virtual State State { get; set; }
    }
}