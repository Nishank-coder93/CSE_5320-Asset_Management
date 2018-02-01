using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSE_5320.Models
{
    public class DepartmentAsset : Base
    {
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [ForeignKey("Asset")]
        public int AssetId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Asset Asset { get; set; }
    }
}