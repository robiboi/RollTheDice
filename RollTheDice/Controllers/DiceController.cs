using Microsoft.AspNetCore.Mvc;
using RollTheDice.Interface;
using RollTheDice.Model;
using RollTheDice.Services;

namespace RollTheDice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiceController : Controller
    {
        private readonly IDieService _dieService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public DiceController(IDieService dieService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _dieService = dieService;
            _environment = hostingEnvironment;
        }

        [HttpPost]
        [Route("RollDice")]
        public IEnumerable<FaceResults> RollDice(DiceInput diceInput)
        {
            List<Die> inputDie1 = PopulateFactor(diceInput.FaceValue1, diceInput.FaceFactor1);

            List<Die> inputDie2 = PopulateFactor(diceInput.FaceValue2, diceInput.FaceFactor2);

            var numberOfRolls = _dieService.Roll(inputDie1, inputDie2, diceInput.NumberOfRolls);

            var faceResults = numberOfRolls.Select(n => new FaceResults { FaceValue1 = n.FirstDieResult.FaceValue, FaceValue2 = n.SecondDieResult.FaceValue });
            return faceResults;
        }

        [HttpGet]
        public IActionResult Show()
        {
            var path = Path.Combine(_environment.WebRootPath, "Content", "Dice.html");
            var fileStream = System.IO.File.OpenRead(path);
            return File(fileStream, "text/html");
        }


        private List<Die> PopulateFactor(int faceValue, int faceFactor)
        {
            List<Die> inputDie = new List<Die>();
            for (int i = 1; i <= 6; i++)
            {
                if (i == faceValue)
                    inputDie.Add(new Die(faceValue, faceFactor));
                else
                    inputDie.Add(new Die(i, 1));
            }
            return inputDie;
        }
    }
}
