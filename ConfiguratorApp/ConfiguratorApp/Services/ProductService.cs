using ConfiguratorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.Services
{
    class ProductService : IProductService
    {
        public List<Product> GetAllProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Name = "Catalyst 4",
                    Number = "CAT 4",
                    Title = "Catalyst 4",
                    Type = "Chair",
                    SubType = "Folding Chair",
                    LaunchDate = DateTime.Parse("02/21/2020 4:50 PM"),
                    Status = "Incomplete",
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "5"},
                       new Revision{Number = "4"},
                       new Revision{Number = "3"},
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false
                },
                new Product
                {
                    Name = "Squiggles Seat",
                    Number = "LSQS",
                    Title = "Squiggles Seat",
                    Type = "Leckey",
                    SubType = "Sitting",
                    LaunchDate = DateTime.Parse("03/01/2020 10:30 AM"),
                    Status = "Published",
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "3"},
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false
                },
                new Product
                {
                    Name = "Liberty FT",
                    Number = "LIBERTY",
                    Title = "Liberty FT",
                    Type = "Chair",
                    SubType = "Tilt In Space",
                    Status = "Publised",
                    LaunchDate = DateTime.Parse("03/05/2020 3:30 PM"),
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "4"},
                       new Revision{Number = "3"},
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false
                }
            };
        }
    }
}
