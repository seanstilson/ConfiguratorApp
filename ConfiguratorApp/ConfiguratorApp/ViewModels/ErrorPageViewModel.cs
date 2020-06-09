using ConfiguratorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.ViewModels
{
    public class ErrorPageViewModel : BasePageViewModel
    {
        public List<Error> Errors { get; set; }

        public ErrorPageViewModel()
        {
            Errors = new List<Error>()
            {
                new Error
                {
                    Description = "Names cannot be used in configuration names or sub items.",
                    Severity = "Standard"
                },
                new Error
                {
                    Description = "Feature Names have a length limit of  32 characters",
                    Severity = "High"
                }
            };
        }
    }
}
