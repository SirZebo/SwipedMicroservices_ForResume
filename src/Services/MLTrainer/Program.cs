using System;
using MLService.ML;

namespace MLTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Training the product categorization model...");
            var trainer = new ProductCategorizationTrainer();
            trainer.TrainModel();
            Console.WriteLine("Model training complete!");
        }
    }
}
