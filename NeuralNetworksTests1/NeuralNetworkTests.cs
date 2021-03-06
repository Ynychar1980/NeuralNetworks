using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetworks.Tests
{
   [TestClass()]
   public class NeuralNetworkTests
   {
      [TestMethod()]
      public void FeedForwardTest()
      {
         var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
         var inputs = new double[,]
         {
            {0, 0, 0, 0},
            {0, 0, 0, 1},
            {0, 0, 1, 0},
            {0, 0, 1, 1},
            {0, 1, 0, 0},
            {0, 1, 0, 1},
            {0, 1, 1, 0},
            {0, 1, 1, 1},
            {1, 0, 0, 0},
            {1, 0, 0, 1},
            {1, 0, 1, 0},
            {1, 0, 1, 1},
            {1, 1, 0, 0},
            {1, 1, 0, 1},
            {1, 1, 1, 0},
            {1, 1, 1, 1}
         };

         var topology = new Topology(4, 1, 0.1, 2);
         var neuralNetwork = new NeuralNetwork(topology);
         var difference = neuralNetwork.Learn(outputs, inputs, 10000);

         var results = new List<double>();
         for (int i = 0; i < outputs.Length; i++)
         {
            var row = NeuralNetwork.GetRow(inputs, i);
            var res = neuralNetwork.Predict(row).Output;
            results.Add(res);
         }

         for (int i = 0; i < results.Count; i++)
         {
            var expected = Math.Round(outputs[i], 2);
            var actual = Math.Round(results[i], 2);
            Assert.AreEqual(expected, actual);
         }
      }

      [TestMethod()]
      public void RecognizeImages()
      {
         var size = 500;
         var parasitizedPath = @"F:\c#\cell_images\Parasitized\";
         var unparasitizedPath = @"F:\c#\cell_images\Uninfected\";

         var converter = new PictureConverter();
         var testParasitizedImageInput = converter.Convert(@"F:\c#\NeuralNetworks\NeuralNetworksTests1\images\Parasitized.png");
         var testUnparasitizedImageInput = converter.Convert(@"F:\c#\NeuralNetworks\NeuralNetworksTests1\images\Unparasitized.png");

         var topology = new Topology(testParasitizedImageInput.Length, 1, 0.1, testParasitizedImageInput.Length / 2);
         var neuralNetwork = new NeuralNetwork(topology);

         double[,] parasitizedInputs = GetData(parasitizedPath, converter, testParasitizedImageInput, size);
         neuralNetwork.Learn(new double[] { 1.0 }, parasitizedInputs, 1);

         double[,] unparasitizedInputs = GetData(unparasitizedPath, converter, testParasitizedImageInput, size);
         neuralNetwork.Learn(new double[] { 0.0 }, unparasitizedInputs, 1);

         var par = neuralNetwork.Predict(testParasitizedImageInput.Select(t => (double)t).ToArray());
         var unpar = neuralNetwork.Predict(testUnparasitizedImageInput.Select(t => (double)t).ToArray());

         Assert.AreEqual(1, Math.Round(par.Output, 2));
         Assert.AreEqual(0, Math.Round(unpar.Output, 2));
      }

      private static double[,] GetData(string parasitizedPath, PictureConverter converter, double[] testImageInput, int size)
      {
         var images = Directory.GetFiles(parasitizedPath);
         var result = new double[size, testImageInput.Length];
         for (int i = 0; i < size; i++)
         {
            var image = converter.Convert(images[i]);
            for (int j = 0; j < image.Length; j++)
            {
               result[i, j] = image[j];
            }
         }

         return result;
      }
   }
}