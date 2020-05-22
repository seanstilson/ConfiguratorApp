using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string DisplayOrder { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public DateTime LaunchDate { get; set; }
        public string LaunchDateString => LaunchDate.ToString("MM/dd/yyyy h:mm tt");
        public List<Revision> Revisions { get; set; }
        public bool ChildrenVisible { get; set; }
    }
}
