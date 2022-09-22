using EmotionClassifierApp.Models;
using EmotionClassifierApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmotionClassifierApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPredictionService _predictionService;

        public HomeController(ILogger<HomeController> logger,
            IPredictionService predictionService)
        {
            _logger = logger;
            _predictionService = predictionService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Emotionclassifier()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmotionProcessing(MessageData messagedata)
        {
            Prediction prediction = new Prediction();
            if (ModelState.IsValid)
            {
                prediction = _predictionService.GeneratePrediction(messagedata);
                _logger.LogInformation("the emotion detected is: {0}", prediction.Emotion);
            }
            return View("ProcessedEmotion", prediction);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}