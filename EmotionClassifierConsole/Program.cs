//instanciamos el contexto, clase principal para trabajar con ML
using EmotionClassifierApp.Models;
using Microsoft.ML;

var context = new MLContext();

//Obtener los datos desde archivo
//ruta de donde se obtienen los datos
var dataFile = context.Data.LoadFromTextFile<MessageData>(
    "C:\\Users\\gcasc\\source\\repos\\EmotionClassifierApp\\Data\\emotions.txt",
    separatorChar: ',',
    hasHeader: true
);
//Carga de datos y separacion de datos de test
var sets = context.Data.TrainTestSplit(
    data: dataFile,
    testFraction: 0.1
);
//ML no analiza texto, por eso se deben convertir a numeros
//Se convierte el texto a valores numericos a traves del metodo Transforms de ML
var textTransformation = context.Transforms.Text.FeaturizeText(
    outputColumnName: "Features",//nombre de columna de salida(convertida a numeros)
    inputColumnName: nameof(MessageData.Message)
);

var labelTransformation = context.Transforms.Conversion.MapValueToKey(inputColumnName: nameof(MessageData.Emotion), outputColumnName: "Label");

//seleccion del algoritmo
//Modelo de clasificacion multi-clases
var classifier = context.MulticlassClassification.Trainers.SdcaMaximumEntropy();

//creacion de flujo de trabajo (pipeline)
//Al texto transformado le agregamos la converison de etiquetas y el modelo seleccionado
//si existen mas transformaciones de etiquetas(columnas en el excel con mas informacion) se deben agregar con el metodo append
var pipeline = textTransformation//leer datos
    .Append(labelTransformation)//transformar etiqueta
    .Append(classifier)
    .Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel")); ;//utilizar resultado de transformaciones en el clasificador

//Entrenamiento del modelo
//Se pasan datos de entrenamiento, TrainSet

Console.WriteLine($"*       Training for Multi-class Classification model        *");
var modeloEntrenado = pipeline.Fit(sets.TrainSet);

//Evaluacion del modelo, se pasa conjunto de datos de Test, TestSet
var evaluacion = context.MulticlassClassification.Evaluate(
              modeloEntrenado.Transform(sets.TestSet)
          );

Console.WriteLine($"*************************************************************************************************************");
Console.WriteLine($"*       Metrics for Multi-class Classification model - Test Data     ");
Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
Console.WriteLine($"*       MicroAccuracy:    {evaluacion.MicroAccuracy:0.###}");
Console.WriteLine($"*       MacroAccuracy:    {evaluacion.MacroAccuracy:0.###}");
Console.WriteLine($"*       LogLoss:          {evaluacion.LogLoss:#.###}");
Console.WriteLine($"*       LogLossReduction: {evaluacion.LogLossReduction:#.###}");
Console.WriteLine($"*************************************************************************************************************");

//Guardar el modelo entrenado
//ruta donde se guardara el modelo entrenado
context.Model.Save(modeloEntrenado, dataFile.Schema, "C:\\Users\\gcasc\\source\\repos\\EmotionClassifierApp\\ModelClassifier\\emotionClassifierModel.zip");
