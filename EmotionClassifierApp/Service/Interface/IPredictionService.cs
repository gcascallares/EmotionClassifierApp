using EmotionClassifierApp.Models;

namespace EmotionClassifierApp.Service.Interface
{
    public interface IPredictionService
    {
        Prediction GeneratePrediction(MessageData messageData);
    }
}
