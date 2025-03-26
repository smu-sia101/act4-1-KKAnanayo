using Microsoft.AspNetCore.Mvc;
using System;

namespace PetNameGenerator.Controllers
{
    [ApiController]
    [Route("api/PetType")]
    public class PetnameController : ControllerBase
    {
        private string[] DogNames = new string[] { "Buddy", "Max", "Charlie", "Rocky", "Rex" };
        private string[] CatNames = new string[] { "Whiskers", "Mittens", "Luna", "Simba", "Tiger" };
        private string[] BirdNames = new string[] { "Tweety", "Sky", "Chirpy", "Raven", "Sunny" };

        public class GeneratePetNameRequest
        {
            public string AnimalType { get; set; }
            public bool? TwoPart { get; set; }
        }

        [HttpPost("generate")]
        public IActionResult Post([FromBody] GeneratePetNameRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.AnimalType))
            {
                return BadRequest(new { error = "AnimalType is required." });
            }

            string[] names;
            switch (request.AnimalType.ToLower())
            {
                case "dog":
                    names = DogNames;
                    break;
                case "cat":
                    names = CatNames;
                    break;
                case "bird":
                    names = BirdNames;
                    break;
                default:
                    return BadRequest(new { error = "Invalid AnimalType. Must be 'dog', 'cat', or 'bird'." });
            }

            var random = new Random();
            string generatedName;

            if (request.TwoPart.HasValue && request.TwoPart.Value)
            {
                var firstPart = names[random.Next(names.Length)];
                var secondPart = names[random.Next(names.Length)];
                generatedName = $"{firstPart} {secondPart}";
            }
            else
            {
                generatedName = names[random.Next(names.Length)];
            }

            return Ok(new { petName = generatedName });
        }
    }
}
