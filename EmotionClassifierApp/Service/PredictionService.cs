using EmotionClassifierApp.Models;
using EmotionClassifierApp.Service.Interface;
using Microsoft.ML;

namespace EmotionClassifierApp.Service
{
    public class PredictionService : IPredictionService
    {
        public Prediction GeneratePrediction(MessageData messageData)
        {
            var context = new MLContext();
            //ruta donde se encuentra el modelo entrenado
            var savedModel = context.Model.Load("C:\\Users\\Usuario\\Documents\\LicGestionTecnologia\\ProgramacionAvanzada-2\\EmotionClassifierApp\\EmotionClassifierApp\\ModelClassifier\\emotionClassifierModel.zip", out var schema);
            var predictionEngine = context.Model.CreatePredictionEngine<MessageData, Prediction>(savedModel);
            Prediction prediction = predictionEngine.Predict(messageData);
            return prediction;
        }
    }
}
