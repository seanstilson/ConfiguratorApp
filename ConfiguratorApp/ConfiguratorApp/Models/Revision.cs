using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.Models
{
    public class Revision
    {
        public string Number { get; set; }
        public string RevisionNumber => $"Revision {Number}";
    }
}
