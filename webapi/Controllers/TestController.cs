using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace webapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class Values2Controller : ControllerBase
    {
        // /api/values2/divide/1/2
        [HttpGet("{Numerator}/{Denominator}")]
        public IActionResult Divide(double Numerator, double Denominator)
        {
            if (Denominator == 0)
            {
                return BadRequest();
            }

            return Ok(Numerator / Denominator);
        }

        // /api/values2 /squareroot/4
        [HttpGet("{radicand}")]
        public IActionResult Squareroot(double radicand)
        {
            if (radicand < 0)
            {
                return BadRequest();
            }

            return Ok(Math.Sqrt(radicand));
        }

        [HttpGet]
        public IActionResult ArrowFunc()
        {
            List<Person> list = new List<Person>()
            {
                new Person() { Name = "Bartosz", Age = 23},
                new Person() { Name = "Jakub", Age = 25},
                new Person() { Name = "Marcelin", Age = 27}
            };

            Random rand = new Random();
            int stringLength = rand.Next(1, 10);
            int randValue;
            char letter;
            string str = "";
            for(int i = 0; i<20; i++)
            {
                for (int j = 0; j < stringLength; j++)
                {
                    randValue = rand.Next(0, 26);
                    letter = Convert.ToChar(randValue + 65);
                    str = str + letter;
                }
                list.Add(new Person { Name = str, Age = rand.Next(10, 91) });
                str = "";
                stringLength = rand.Next(1, 10);
            }

            var sortedList = list.OrderBy(p => p.Age).Where(p => p.Name.Length > 5);

            return Ok(sortedList);
        }

    }

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}
