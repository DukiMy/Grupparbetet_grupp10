using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HomeFinder.Data;
using HomeFinder.Models;
using System;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Generic;
//using HomeFinder.Migrations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HomeFinder.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HomeFinderContext(
                serviceProvider.GetRequiredService<DbContextOptions<HomeFinderContext>>()))
            {
                // Look for any movies.
                //if (context.Items.Any())
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
                int numberOfItems = 250;

                for (int i = 0; i < numberOfItems; i++)
                {
                    var newItem = CreateNewItem();
                    context.Items.AddRange(newItem);
                }
                //var newUser = CreateNewApplicationUser();
                //var user = new ApplicationUser { UserName = "testmail@gmail.com", Email = "testmail@gmail.com" };
                //var result = using (var userManager new UserManager()) _userManager.CreateAsync(user, "Superpassword-2022");
                context.SaveChanges();
            }
        }

        public static ApplicationUser CreateNewApplicationUser()
        {
            return new ApplicationUser
            {
                UserName = "",
                Email = "testmail@gmail.com",
                PasswordHash = "Superpassword-2022",
                EmailConfirmed = true
                
            };
        }
        public static Item CreateNewItem()
        {
            return new Item
            {
                ItemType = Generator.SetItemType(),
                FormOfLease = Generator.SetFormOfLease(),
                City = Generator.SetCity(),
                Address = Generator.SetAddress(),
                NrOfRoom = Generator.SetNrOfRooms(),
                Description = Generator.SetDescription(),
                LivingArea = Generator.SetLivingArea(),
                GrossFloorArea = Generator.SetGrosFloorArea(),
                PlotArea = Generator.SetPlotArea(),
                Price = Generator.SetPrice(),
                MainImageUrl = Generator.SetImage(),
                ConstructionYear = Generator.SetConstructionYear(),
                //ListingDate = DateTime.Now,
                ListingDate = Generator.SetListingDate(),
                ShowingDate = Generator.SetShowingDate()
            };
        }
    }

    class Generator
    {
        public static ApplicationUser CurrentUser { get; set; } = new();
        public static Random Random { get; set; } = new Random();

        public static string GenCity { get; set; }
        public static string GenAddress { get; set; }
        public static string GenItemType { get; set; }
        public static int GenNrOfRooms { get; set; }
        public static double GenLivingArea { get; set; }
        public static double? GenGrosFloorArea { get; set; }
        public static double? GenPlotArea { get; set; }
        public static DateTime GenConstructionYear { get; set; }
        public static DateTime GenListingDate { get; set; }
        public static DateTime GenShowingDate { get; set; }
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
       
        public static string SetCity()
        {
            int num = Random.Next(0, 5);
            return GenCity = Enum.GetName(typeof(City), num);
            //return GenCity = num == 1 ? "Stockholm" : num == 2 ? "Göteborg" : num == 3 ? "Malmö" : num == 4 ? "Uppsala" : "Umeå";
        }
        public static string SetAddress()
        {
            int num = Random.Next(0, 5);
           
            if (GenCity == "Stockholm")
            {
                GenAddress = Enum.GetName(typeof(Stockholm), num);
            }

           else if (GenCity == "Malmö")
            {
                GenAddress = Enum.GetName(typeof(Malmö), num);
            }

            else if(GenCity == "Göteborg")
            {
                GenAddress = Enum.GetName(typeof(Göteborg), num);
            }

            else if (GenCity == "Umeå")
            {
                GenAddress = Enum.GetName(typeof(Umeå), num);
            }

            else if (GenCity == "Uppsala")
            {
                GenAddress = Enum.GetName(typeof(Uppsala), num);
            }

            int addressNum = Random.Next(1, 51);
            return GenAddress + " " + addressNum.ToString();
        }

        //public static string SetZipCode()
        //{

        //} 
       
      

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

            return @"Lorem, ipsum dolor sit amet consectetur adipisicing elit.Ut, tempora, illum deleniti in voluptatum 
            non veritatis sed fuga culpa consectetur atque accusamus tempore et vero veniam hic nulla laborum
            aperiam sapiente repudiandae praesentium? Possimus, distinctio ut quis assumenda asperiores ea sequi
            reiciendis accusantium vitae dignissimos est excepturi veniam magni repellat, aliquam quidem provident illum
            repellendus eos architecto deleniti? Illum consequatur tempora sit, mollitia beatae atque? Inventore in
            aliquam quibusdam neque? Consequuntur, harum? Molestiae ullam, obcaecati delectus sunt deserunt ipsum non
            repellat dolorem facere, quod nostrum atque repellendus tempore cupiditate mollitia, culpa vitae sit.
            Corrupti at reprehenderit nostrum impedit necessitatibus quasi a officia excepturi, provident veniam
            voluptatibus aut vitae. Optio id nostrum nobis possimus praesentium provident ad odit ullam sequi sunt
            facilis iusto nulla adipisci ipsam, quos delectus corrupti incidunt. Maiores mollitia obcaecati autem, omnis
            quam, iure itaque hic officia, exercitationem non voluptate voluptatum.Aspernatur hic, quo fuga nobis
            totam, odit officiis nisi soluta, dolorum quae ratione animi vel non rerum autem dolores quia cupiditate
            mollitia.Nesciunt ipsa maiores repellat obcaecati, rem reprehenderit ad veniam accusantium aliquam,
           inventore, hic facilis amet numquam accusamus!Adipisci fugiat nemo animi ex enim amet aliquam nisi omnis
          temporibus consequatur, tempore voluptatem illum quas quaerat nihil.";
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

        public static int SetPrice()
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
            //DateTime end = DateTime.Now;
            int range = (DateTime.Now - start).Days;
            return start.AddDays(Random.Next(range));
        }
        public static DateTime SetListingDate()
        {
            DateTime start = DateTime.Now.AddDays(-30);
            int range = (DateTime.Now - start).Days;
            return GenListingDate = start.AddDays(Random.Next( range +1));
        }
        public static DateTime SetShowingDate()
        {

            //// DateTime end = new DateTime(2022, 1, 1);
            // DateTime end = GenListingDate.AddDays(14);
            // int range = (end - GenListingDate).Days;
            // //return GenListingDate.AddDays(Random.Next(range));

           
            //return GenListingDate.AddDays(Random.Next(7,21));
            return GenListingDate.AddDays(Random.Next(7, 21));
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
    public enum City
    {
        Stockholm,
        Malmö,
        Göteborg,
        Uppsala,
        Umeå
    }

    enum Stockholm
    {
        Drottninggatan,
        Sveavägen,
        Storgatan,
        Vasagatan,
        Odengatan
    }

    enum Malmö
    {
        Drottninggatan,
        Storgatan,
        Vasagatan,
        Vaktgatan,
        Auravägen
    }

    enum Göteborg
    {
        Ullevigatan,
        Fiskhamnsgatan,
        Byalagsgatan,
        Önskevädersgatan,
        Smögengatan
    }

    enum Umeå
    {
        Bölevägen,
        Backenvägen,
        Dirigentstråket,
        Fläktvägen,
        Klintvägen
    }

    enum Uppsala
    {
        Storgatan,
        Drottninggatan,
        Kungsgatan,
        Skolgatan,
        Götgatan
    }
}