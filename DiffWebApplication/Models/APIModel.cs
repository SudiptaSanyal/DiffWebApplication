using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiffWebApplication.Models
{
    public class APIModel
    {
        public int ID { get; set; }
        public string left { get; set; }
        public string right { get; set; }
        public string diffResponse { get; set; }
        public string diffContent { get; set; }
    }
}