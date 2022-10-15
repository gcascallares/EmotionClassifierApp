using EmotionClassifierApp.Models;
using EmotionClassifierApp.Service.Interface;
using Microsoft.ML;

namespace EmotionClassifierApp.Service
{

    public enum Emotion
    {
        
        
        miedo,
        sorpresa
    }

    public class PredictionService : IPredictionService
    {
        public Prediction GeneratePrediction(MessageData messageData)
        {
            var context = new MLContext();
            //string dir = "C:\\Users\\Usuario\\Documents\\LicGestionTecnologia\\ProgramacionAvanzada-2\\EmotionClassifierApp\\EmotionClassifierApp\\ModelClassifier\\emotionClassifierModel.zip";
            string dir = "/Users/nicolaslucero/development/personal/emotionClassifierModel.zip";

            //ruta donde se encuentra el modelo entrenado
            var savedModel = context.Model.Load(dir, out var schema);
            var predictionEngine = context.Model.CreatePredictionEngine<MessageData, Prediction>(savedModel);
            Prediction prediction = predictionEngine.Predict(messageData);
            return prediction;
        }
    }
}

