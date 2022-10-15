using Microsoft.ML.Data;

namespace EmotionClassifierApp.Models
{
    public class Prediction
    {
        [ColumnName("PredictedLabel")]
        public string Emotion { get; set; }
        [ColumnName("BackgroundColor")]
        public string BackgroundColor { get; set; }

        [ColumnName("FontColor")]
        public string FontColor { get; set; }
    }
}
