using Microsoft.ML.Data;

namespace EmotionClassifierApp.Models
{
    public class Prediction
    {
        [ColumnName("PredictedLabel")]
        public string Emotion;
    }
}
