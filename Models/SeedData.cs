using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HomeFinder.Data;
using HomeFinder.Models;
using System;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Generic;

namespace HomeFinder.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HomeFinderContext(
                serviceProvider.GetRequiredService<DbContextOptions<HomeFinderContext>>()))
            {
                //// Look for any movies.
                //if (context.Item.Any())
                //{
                //    return;   // DB has been seeded
                //}

                context.Database.EnsureDeleted();
                context.Database.Migrate();

                var itemType1 = new ItemType() { Name = "Fritidsboende" };
                var itemType2 = new ItemType() { Name = "Gård" }; 
                var itemType3 = new ItemType() { Name = "Lägenhet" };
                var itemType4 = new ItemType() { Name = "Radhus" }; 
                var itemType5 = new ItemType() { Name = "Villa" }; 

                context.AddRange(itemType1, itemType2, itemType3, itemType4, itemType5);
                context.SaveChanges();
                Generator.GenContext = context;


                //========================< INFO >=============================
                // Ange hur många objekt du vill skapa i numberOfItems
                int numberOfItems = 50;

                for (int i = 0; i < numberOfItems; i++)
                {
                    var newItem = CreateNewItem();
                    context.Items.AddRange(newItem);
                }

                var newUser = CreateNewApplicationUser();
                context.SaveChanges();
            }
        }

        public static ApplicationUser CreateNewApplicationUser()
        {
            return new ApplicationUser
            {
                UserName = "",
                Email = "petter.berelin@gmail.com",
                PasswordHash = "Superpassword-2022"
            };
        }
        public static Item CreateNewItem()
        {
            return new Item
            {
                FormOfLease = Generator.SetFormOfLease(),
                ItemType = Generator.SetItemType(),
                Address = Generator.SetAddress(),
                NrOfRoom = Generator.SetNrOfRooms(),
                City = Generator.SetCity(),
                Description = Generator.SetDescription(),
                LivingArea = Generator.SetLivingArea(),
                GrossFloorArea = Generator.SetGrosFloorArea(),
                PlotArea = Generator.SetPlotArea(),
                Price = Generator.SetPrice(),
                MainImageUrl = Generator.SetImage(),
                ConstructionYear = DateTime.Now,
                ListingDate = DateTime.Now,
                Lat = "59.4112",
                Lng = "17.9153"
            };
        }
    }

    class Generator
    {
        public static Random Random { get; set; } = new Random();

        public static string GenItemType { get; set; }
        public static int GenNrOfRooms { get; set; }
        public static double GenLivingArea { get; set; }
        public static double? GenGrosFloorArea { get; set; }
        public static double? GenPlotArea { get; set; }
        public static HomeFinderContext GenContext { get; set; }


        //=====properties i klassen Item=======
        // int Id, string ItemType, string Address, string City, decimal Price, int NrOfRoom, string Description,
        // double LivingArea, double? GrosFloorArea, double? PlotArea, DataType.Date ConstructionYear, DataType.Date ListingDate
        //public static string SetItemType()
        //{
        //    int num = Random.Next(1, 6);

        //    return GenItemType = num == 1 ? "Villa" : num == 2 ? "Lägenhet" : num == 3 ? "Radhus" : num == 4 ? "Gård" : "Fritidsboende";
        //}

        public static ItemType SetItemType()
        {
            int num = Random.Next(1, 6);
            var itemTypes = GenContext.ItemTypes.FirstOrDefault(i => i.Id == num);
            GenItemType = itemTypes.Name;

            return itemTypes;
        }

        public static string SetFormOfLease()
        {
            if (GenItemType == "Lägenhet" || GenItemType == "Radhus")
            {
                int num = Random.Next(1, 3);
                return num == 1 ? "Hyresrätt" : "Bostadsrätt";
            }
            else
            {
                return "Äganderätt";
            }

        }

        public static string SetAddress()
        {
            int num = Random.Next(1, 6);
            var prefix = num == 1 ? "Äppel" : num == 2 ? "Päron" : num == 3 ? "Körsbärs" : num == 4 ? "Melon" : "Banan";

            num = Random.Next(1, 6);
            var sufix = num == 1 ? "vägen " : num == 2 ? "gatan " : num == 3 ? "stigen " : num == 4 ? "plan " : "allé ";

            num = Random.Next(1, 101);
            
            return prefix + sufix + num;
        }

        public static string SetCity()
        {
            int num = Random.Next(1, 6);
            return num == 1 ? "Stockholm" : num == 2 ? "Göteborg" : num == 3 ? "Malmö" : num == 4 ? "Finspång" : "Korpilombolo";
        }

        public static int SetNrOfRooms()
        {
            return GenNrOfRooms = Random.Next(1, 11);
        }

        public static string SetDescription()
        {
            int num = Random.Next(1, 6);
            var part1 = num == 1 ? "Otroligt " : num == 2 ? "Fantastiskt " : num == 3 ? "Ovanligt " : num == 4 ? "Häpnadsväckande " : "Perfekt ";

            num = Random.Next(1, 6);
            var part2 = "";
            if (GenItemType == "Radhus" || GenItemType == "Fritidsboende")
            {
                part2 = num == 1 ? "litet " : num == 2 ? "stort " : num == 3 ? "rymligt " : num == 4 ? "nedgånget " : "dyrt ";
            }

            else
            {
                part2 = num == 1 ? "liten " : num == 2 ? "stor " : num == 3 ? "rymlig " : num == 4 ? "nedgången " : "dyr ";
            }

            return part1 + part2 + GenItemType.ToLower();
        }

        public static double SetLivingArea()
        {
            return GenLivingArea = Random.Next(20, 41) * GenNrOfRooms;
        }

        public static double? SetGrosFloorArea()
        {
            if (GenItemType != "Lägenhet")
            {
               return GenGrosFloorArea = Random.Next(10, 31);
            }
            return 0;
        }

        public static double? SetPlotArea()
        {
            if (GenItemType != "Lägenhet")
            {
                if (GenItemType != "Radhus")
                {
                 return GenPlotArea = Random.Next(100, 1001);

                }

                return GenPlotArea = Random.Next(10, 31);
            }
            return 0;
        }

        public static decimal SetPrice()
        {
            int price = ((int)GenNrOfRooms * 500000); 
                //* ((int)GenLivingArea / 100);

            if (GenPlotArea != null)
            {
                price += ((int)GenGrosFloorArea + (int)GenPlotArea) * 1000;
            }

            return price;
        }

        public static DateTime SetConstructionYear()
        {
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(Random.Next(range));
        }
        public static DateTime SetListingDate()
        {
            DateTime start = DateTime.Now.AddDays(-30);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(Random.Next(range));
        }
        
        public static string SetImage()
        {
            if (Generator.GenItemType == "Lägenhet")
            {
                return "~/placeholderimg/lägenhet.jpg";
            }
            else if (Generator.GenItemType == "Radhus")
            {
                return "~/placeholderimg/radhus.jpg";
            }

            else if (Generator.GenItemType == "Villa")
            {
                return "~/placeholderimg/villa.jpg";
            }

            else if (Generator.GenItemType == "Gård")
            {
                return "~/placeholderimg/gård.jpg";
            }
            else if (Generator.GenItemType == "Fritidsboende")
            {
                return "~/placeholderimg/fritidsboende.jpg";
            }
            return "~/placeholderimg/fritidsboende.jpg";

        }
    }
}