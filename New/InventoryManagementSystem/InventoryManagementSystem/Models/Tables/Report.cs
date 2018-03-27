using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models.Tables
{
    public class Report
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Resource")]
        public int ResourceId { get; set; }

        public bool Verify { get; set; }

        public bool Missing { get; set; }

        public int? QuantityChange { get; set; }

        public int UpdatedBy { get; set; }

        public Resource Resource { get; set; }
        
    }
}