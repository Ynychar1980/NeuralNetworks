using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeuralNetworks.Tests
{
   [TestClass()]
   public class PictureConverterTests
   {
      [TestMethod()]
      public void ConvertTest()
      {
         var converter = new PictureConverter();
         var inputs = converter.Convert(@"F:\c#\NeuralNetworks\NeuralNetworksTests1\images\Parasitized.png");
         converter.Save("d:\\image.png", inputs);
      }
   }
}