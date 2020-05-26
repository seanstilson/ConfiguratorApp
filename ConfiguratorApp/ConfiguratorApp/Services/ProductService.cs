using ConfiguratorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfiguratorApp.Services
{
    class ProductService : IProductService
    {
        List<Product> Products { get; set; }

        private List<Product> CreateProductData()
        {
            return new List<Product>()
            {
                new Product
                {
                    ID = Guid.Parse("0b8271eb-6385-46db-a38b-da78531775bd"),
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
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 5
                },
                 new Product
                {
                    ID = Guid.Parse("0b8271eb-6385-46db-a38b-da78531775bd"),
                    Name = "Catalyst 4",
                    Number = "CAT 4",
                    Title = "Catalyst 4",
                    Type = "Chair",
                    SubType = "Folding Chair",
                    LaunchDate = DateTime.Parse("02/19/2020 4:50 PM"),
                    Status = "Incomplete",
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "4"},
                       new Revision{Number = "3"},
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 4
                },
                 new Product
                {
                    ID = Guid.Parse("0b8271eb-6385-46db-a38b-da78531775bd"),
                    Name = "Catalyst 4",
                    Number = "CAT 4",
                    Title = "Catalyst 4",
                    Type = "Chair",
                    SubType = "Folding Chair",
                    LaunchDate = DateTime.Parse("02/15/2020 4:50 PM"),
                    Status = "Incomplete",
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "3"},
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 3
                },
                 new Product
                {
                    ID = Guid.Parse("0b8271eb-6385-46db-a38b-da78531775bd"),
                    Name = "Catalyst 4",
                    Number = "CAT 4",
                    Title = "Catalyst 4",
                    Type = "Chair",
                    SubType = "Folding Chair",
                    LaunchDate = DateTime.Parse("02/10/2020 4:50 PM"),
                    Status = "Incomplete",
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 2
                },
                new Product
                {
                    ID = Guid.Parse("3ea1523b-bc8f-4dc3-98d6-a2427fe2ca7c"),
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
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 3
                },
                 new Product
                {
                    ID = Guid.Parse("3ea1523b-bc8f-4dc3-98d6-a2427fe2ca7c"),
                    Name = "Squiggles Seat",
                    Number = "LSQS",
                    Title = "Squiggles Seat",
                    Type = "Leckey",
                    SubType = "Sitting",
                    LaunchDate = DateTime.Parse("02/27/2020 10:30 AM"),
                    Status = "Published",
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 2
                },
                 new Product
                {
                    ID = Guid.Parse("3ea1523b-bc8f-4dc3-98d6-a2427fe2ca7c"),
                    Name = "Squiggles Seat",
                    Number = "LSQS",
                    Title = "Squiggles Seat",
                    Type = "Leckey",
                    SubType = "Sitting",
                    LaunchDate = DateTime.Parse("02/25/2020 10:30 AM"),
                    Status = "Published",
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 1
                },
                new Product
                {
                    ID = Guid.Parse("71d1cdc3-86dc-47c1-81e0-4df4654e6c40"),
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
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 4
                },
                new Product
                {
                    ID = Guid.Parse("71d1cdc3-86dc-47c1-81e0-4df4654e6c40"),
                    Name = "Liberty FT",
                    Number = "LIBERTY",
                    Title = "Liberty FT",
                    Type = "Chair",
                    SubType = "Tilt In Space",
                    Status = "Publised",
                    LaunchDate = DateTime.Parse("03/04/2020 3:30 PM"),
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "3"},
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 3
                },
                new Product
                {
                    ID = Guid.Parse("71d1cdc3-86dc-47c1-81e0-4df4654e6c40"),
                    Name = "Liberty FT",
                    Number = "LIBERTY",
                    Title = "Liberty FT",
                    Type = "Chair",
                    SubType = "Tilt In Space",
                    Status = "Publised",
                    LaunchDate = DateTime.Parse("03/02/2020 3:30 PM"),
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "2"},
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 2
                },
                new Product
                {
                    ID = Guid.Parse("71d1cdc3-86dc-47c1-81e0-4df4654e6c40"),
                    Name = "Liberty FT",
                    Number = "LIBERTY",
                    Title = "Liberty FT",
                    Type = "Chair",
                    SubType = "Tilt In Space",
                    Status = "Publised",
                    LaunchDate = DateTime.Parse("03/01/2020 3:30 PM"),
                   Revisions = new List<Revision>()
                   {
                       new Revision{Number = "1"}
                   },
                   ChildrenVisible = false,
                   CurrentRevisionNumber = 1
                }
            };
        }

        public List<Product> GetAllProducts()
        {
            Products = CreateProductData();
            var prodQuery = from p in Products
                          group p by p.ID into reviser
                          select reviser;

            List<Product> currents = new List<Product>();

            if (prodQuery == null)
                return currents;

            foreach(IGrouping<Guid, Product> grp in prodQuery)
            {
                currents.Add(grp.FirstOrDefault());
            }

            return currents;
            
        }

        public Product GetProductByRevision(Guid ID, string revision)
        {
            return Products.SingleOrDefault(p => p.ID == ID && p.CurrentRevisionNumber.ToString() == revision);
        }

       
    }
}
