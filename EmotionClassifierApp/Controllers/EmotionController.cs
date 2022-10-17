using EmotionClassifierApp.Models;
using EmotionClassifierApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmotionClassifierApp.Controllers
{
    [Route("api/[controller]")]
    public class EmotionController : Controller
    {
        private readonly ILogger<EmotionController> _logger;
        private readonly IPredictionService _predictionService;

        public EmotionController(ILogger<EmotionController> logger,
            IPredictionService predictionService)
        {
            _logger = logger;
            _predictionService = predictionService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] MessageData messagedata)
        {            
            Prediction prediction = new Prediction();
            prediction = _predictionService.GeneratePrediction(messagedata);
            _logger.LogInformation("the emotion detected is: {0}", prediction.Emotion);
            return Json(prediction);
        }
    }
}

