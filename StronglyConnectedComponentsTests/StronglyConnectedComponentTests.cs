﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StronglyConnectedComponents.Tests
{
    [TestClass]
    public class StronglyConnectedComponentTests
    {
        [TestMethod]
        public void EmptyGraph()
        {
            var detector = new StronglyConnectedComponentFinder<int>();
            var cycles = detector.DetectCycle(new List<Vertex<int>>());
            Assert.AreEqual(0, cycles.Count);
        }

        // A
        [TestMethod]
        public void SingleVertex()
        {
            var graph = new List<Vertex<int>>
            {
                new Vertex<int>(1)
            };
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.AreEqual(1, components.Count);
            Assert.AreEqual(1, components.IndependentComponents().Count());
            Assert.AreEqual(0, components.Cycles().Count());
        }

        // A→B
        [TestMethod]
        public void Linear2()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            vA.Dependencies.Add(vB);
            graph.Add(vA);
            graph.Add(vB);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.AreEqual(2, components.Count);
            Assert.AreEqual(2, components.IndependentComponents().Count());
            Assert.AreEqual(0, components.Cycles().Count());
        }

        // A→B→C
        [TestMethod]
        public void Linear3()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            var vC = new Vertex<int>(3);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.AreEqual(3, components.Count);
            Assert.AreEqual(3, components.IndependentComponents().Count());
            Assert.AreEqual(0, components.Cycles().Count());
        }

        // A↔B
        [TestMethod]
        public void Cycle2()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vA);
            graph.Add(vA);
            graph.Add(vB);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.AreEqual(1, components.Count);
            Assert.AreEqual(0, components.IndependentComponents().Count());
            Assert.AreEqual(1, components.Cycles().Count());
            Assert.AreEqual(2, components.First().Count);
        }

        // A→B
        // ↑ ↓
        // └─C
        [TestMethod]
        public void Cycle3()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            var vC = new Vertex<int>(3);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            vC.Dependencies.Add(vA);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.AreEqual(1, components.Count);
            Assert.AreEqual(0, components.IndependentComponents().Count());
            Assert.AreEqual(1, components.Cycles().Count());
            Assert.AreEqual(3, components.Single().Count);
        }

        // A→B   D→E
        // ↑ ↓   ↑ ↓
        // └─C   └─F
        [TestMethod]
        public void TwoIsolated3Cycles()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            var vC = new Vertex<int>(3);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            vC.Dependencies.Add(vA);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);

            var vD = new Vertex<int>(4);
            var vE = new Vertex<int>(5);
            var vF = new Vertex<int>(6);
            vD.Dependencies.Add(vE);
            vE.Dependencies.Add(vF);
            vF.Dependencies.Add(vD);
            graph.Add(vD);
            graph.Add(vE);
            graph.Add(vF);

            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.AreEqual(2, components.Count);
            Assert.AreEqual(0, components.IndependentComponents().Count());
            Assert.AreEqual(2, components.Cycles().Count());
            Assert.IsTrue(components.All(c => c.Count == 3));
        }

        // A→B
        // ↑ ↓
        // └─C-→D
        [TestMethod]
        public void Cycle3WithStub()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            var vC = new Vertex<int>(3);
            var vD = new Vertex<int>(4);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            vC.Dependencies.Add(vA);
            vC.Dependencies.Add(vD);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);
            graph.Add(vD);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.AreEqual(2, components.Count);
            Assert.AreEqual(1, components.IndependentComponents().Count());
            Assert.AreEqual(1, components.Cycles().Count());
            Assert.AreEqual(1, components.Count(c => c.Count == 3));
            Assert.AreEqual(1, components.Count(c => c.Count == 1));
            Assert.IsTrue(components.Single(c => c.Count == 1).Single() == vD);
        }

        // B→D
        // D→C
        // A→B→C
        // A→B→D→C
        [TestMethod]
        public void LinearTwoLines()
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
            StronglyConnectedComponentList<string> components = detector.DetectCycle(graph);

            Assert.AreEqual(4, components.Count);
            Assert.AreEqual(4, components.IndependentComponents().Count());
            Assert.AreEqual(0, components.Cycles().Count());
            Assert.AreEqual("C", components.First().First().Value);
            Assert.AreEqual("D", components.Skip(1).First().First().Value);
            Assert.AreEqual("B", components.Skip(2).First().First().Value);
            Assert.AreEqual("A", components.Skip(3).First().First().Value);
        }
    }
}
