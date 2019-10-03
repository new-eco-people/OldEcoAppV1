using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using API.Core.Domain.Models.Defaults;
using API.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace API.Helper.Extension
{
    
    public static class Extension
    {
        public static readonly int allCountryId = 247;
        public static void AddApplicationError(this HttpResponse response, string message) {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app) { 
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider; 
            try 
            { 
                var context = serviceProvider.GetService<DataContext>(); 
                InsertSeedData(context); 
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
                // var logger = serviceProvider.GetRequiredService<ILogger<Program>>(); 
                // logger.LogError(ex, "An error occurred while seeding the database."); 
            } 
            return app; 
        } 

        private static void InsertSeedData(DataContext context) { 

            // AllRegion(context);

            if (!context.Countries.Any()) { 

                using (StreamReader jsonData = new StreamReader("Helper/SeedData/Countries.json")){
                    List<Country> Countries = JsonConvert.DeserializeObject<List<Country>>(jsonData.ReadToEnd());
                    Countries.ForEach(s => context.Countries.Add(s));
                    
                }

                using (StreamReader jsonData = new StreamReader("Helper/SeedData/States.json")){
                    List<State> States = JsonConvert.DeserializeObject<List<State>>(jsonData.ReadToEnd());
                    States.ForEach(s => context.States.Add(s));

                }

                context.SaveChanges();

            } 

            // context.SaveChanges();
        }

        // private static void AllRegion(DataContext context) {
        //     var state = context.States.FirstOrDefault(s => s.Id == 4122);

        //     if (state == null){

        //         context.Countries.Add(new Country() {
        //             Id = 247, SortName = "--", Name = "---", PhoneCode = "260"
        //         });

        //         context.States.Add(new State() {
        //             Id = 4122, Name = "All Regions", CountryId = 247
        //         });
        //     }

            
        // }
    }
}