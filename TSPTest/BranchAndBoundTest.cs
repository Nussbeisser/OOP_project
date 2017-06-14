using System;
using TSP;
using GraphClass;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TSPTest
{
    [TestClass]
    public class BranchAndBoundTest
    {
        BranchAndBound branchAndBound;

        [TestInitialize]
        public void TestSetUp()
        {
            Graph exampleGraph = new Graph("myGraph.txt");
            branchAndBound = new BranchAndBound(exampleGraph, "v1");
        }

        [TestMethod]
        public void BBShouldReturnValidStringList()
        {
            List<string> expected = new List<string> { "v1", "v3", "v4", "v2", "v1" };

            branchAndBound.Initialise(branchAndBound.Root);
            List<String> result = branchAndBound.Solution;

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
