using System;
using System.Collections.Generic;
using System.Linq;

namespace StronglyConnectedComponents.Client
{
    class Program
    {
        // B→D
        // D→C
        // A→B→C
        // A→B→D→C
        static void Main()
        {
            var feeder = new LineFeeder("data.txt");
            var factory = new VertexFactory<string>();
            foreach (var line in feeder)
            {
                var splittedLine = line.Split(',');
                factory.Add(splittedLine[0],splittedLine[1]);
            }
            //var path = @"C:\Users\Eduard\Source\Repos\CycleDetection\data.txt";
            var graph = new List<Vertex<string>>();
            graph.AddRange(factory.VertexDictionary.Select(vde=>vde.Value));

            var detector = new StronglyConnectedComponentFinder<string>();
            var components = detector.DetectCycle(graph);
            int index = 0;
            foreach (var component in components)
            {
                Console.Write($"component {index++} : ");
                foreach (var vertex in component)
                {
                    Console.Write(vertex.Value + " ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
