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
            MLContext context = new MLContext();
            //string dir = "C:\\Users\\Usuario\\Documents\\LicGestionTecnologia\\ProgramacionAvanzada-2\\EmotionClassifierApp\\EmotionClassifierApp\\ModelClassifier\\emotionClassifierModel.zip";
            string dir = "/Users/nicolaslucero/development/personal/emotionClassifierModel.zip";

            ITransformer savedModel = context.Model.Load(dir, out var schema);
            PredictionEngine<MessageData, Prediction> predictionEngine = context.Model.CreatePredictionEngine<MessageData, Prediction>(savedModel);
            Prediction prediction = predictionEngine.Predict(messageData);
            return prediction;
        }
    }
}

