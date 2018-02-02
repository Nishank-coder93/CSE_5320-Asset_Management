using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSE_5320.Models
{
    public class State
    {
        public State()
        {
            Value = 0;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Value { get; set; }
    }
}