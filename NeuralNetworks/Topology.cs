using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
   public class Topology
   {
      public int InputCount { get; }
      public int OutputCount { get; }
      public double LearnRate { get; }
      public List<int> HiddenLayers { get; }

      public Topology(int inputCount, int outputCount, double learnRate, params int[] layers)
      {
         InputCount = inputCount;
         OutputCount = outputCount;
         LearnRate = learnRate;
         HiddenLayers = new List<int>();
         HiddenLayers.AddRange(layers);
      }
   }
}
