using Microsoft.ML.Data;
using System.ComponentModel.DataAnnotations;

namespace EmotionClassifierApp.Models
{
    public class MessageData
    {
        [LoadColumn(0)]
        public string? Emotion { get; set; }

        [Required(ErrorMessage = "please enter the text to evaluate")]
        [LoadColumn(1)]
        public string Message { get; set; }
    }
}
