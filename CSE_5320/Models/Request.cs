using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSE_5320.Models
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Asset")]
        public int AssetId { get; set; }

        [ForeignKey("User")]
        public int RequestedUser { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int statusId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }
    }
}