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

        [ForeignKey("User")]
        public int RequestedUser { get; set; }

        [ForeignKey("Status")]
        public int statusId { get; set; }

        public virtual User User { get; set; }

        public virtual Status Status { get; set; }
    }
}