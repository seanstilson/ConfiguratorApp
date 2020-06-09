using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.Models
{
    public enum SearchParam
    {
        OptionGroup,
        FeatureName,
        OptionName,
        OptionCode
    }

    public enum SearchType
    {
        Equals,
        NotEqualTo,
        StartsWith,
        Contains,
        EndsWith
    }

    
}
