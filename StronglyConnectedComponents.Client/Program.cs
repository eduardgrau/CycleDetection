using System;
using System.Collections.Generic;

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
            var graph = new List<Vertex<string>>();
            var vA = new Vertex<string>("A");
            var vB = new Vertex<string>("B");
            var vC = new Vertex<string>("C");
            var vD = new Vertex<string>("D");
            vB.Dependencies.Add(vD);
            vD.Dependencies.Add(vC);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);

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
