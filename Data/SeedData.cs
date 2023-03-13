using System.Text.Json;
using BirdsSweden300.web.Data;
using BirdsSweden300.web.Models;

namespace BirdsSweden300.web.Data
{
    public static class SeedData
    {
        public static async Task LoadBirdData(BirdsContext context)
        {
            //steg 1
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            //steg 2 endast ladda data om vehicles tab är tom
            if(context.Birds.Any()) return;

            //steg 3 läsa in Json info från vehicles.json fil
            var json = System.IO.File.ReadAllText("Data/json/birds.json");
            
            // steg 4 omvandla json obj till lista av vechicle model obj
            var birds = JsonSerializer.Deserialize<List<BirdModel>>(json,options);

            //steg 5 skicka listan med vehiclemodel obj till db
            if(birds is not null && birds.Count >0){
                await context.Birds.AddRangeAsync(birds);
                await context.SaveChangesAsync();
            }
        }
    }
}