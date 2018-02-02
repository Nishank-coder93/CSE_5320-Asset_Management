using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_5320.Models
{
    public class Location : Base
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }

        public virtual State State { get; set; }
    }
}