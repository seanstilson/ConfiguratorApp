using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.Models
{
    public class ExcelProduct
    {
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string FeatureName { get; set; }
        public string FeatureRequired { get; set; }
        public string FeatureCSROnly { get; set; }
        public string OptionGroupName { get; set; }
        public string OptionGroupRequired { get; set; }
        public string OptionGroupCSRonly { get; set; }
        public string SubOptionGroupName { get; set; }
        public string SubOptionGroupRequired { get; set; }
        public string OptionName { get; set; }
        public string OptionCode { get; set; }
        public string HCPCS { get; set; }
        public string OptionRequired { get; set; }
        public string OptionCSROnly { get; set; }
        public string WorkTicketInput { get; set; }

    }
}
