using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TSP;
using GraphClass;
using TSPTest;
using System.Collections.Generic;

namespace TSPTest
{
    [TestClass]
    public class BruteForceTest
    {
        BruteForce bruteForce;

        [TestInitialize]
        public void TestSetUp()
        {
            Graph exampleGraph = new Graph("myGraph.txt");
            bruteForce = new BruteForce(exampleGraph, "v1");
        }

        [TestMethod]
        public void BFShouldReturnValidStringList()
        {
            List<string> expected = new List<string> { "v1", "v3", "v4", "v2", "v1" };

            bruteForce.Initialise(bruteForce.Root);
            List<string> result = bruteForce.Solution;


            CollectionAssert.AreEqual(expected, result);
        }
    }
}
