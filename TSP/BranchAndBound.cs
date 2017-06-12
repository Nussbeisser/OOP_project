using System;
using System.Collections.Generic;
using System.Linq;
using GraphClass;

/*
1. state space tree - expanding new verticies needs optimization (redundant verticies are unneccessay expanded)
2. getBound method - bound computing can be optimized
*/

namespace TSP
{
    /// <summary>
    /// Implements a branch and bound algorithm with the best first search that finds solution to Travelling Salesman Problem (TSP).
    /// </summary>
    public class BranchAndBound
    {
        private Graph graph;
        private Vertex root;
        private List<Tuple<int, List<Vertex>>> pathAndBound;
        private int minimum;
        private List<Vertex> solution;
        //private bool minimumIsSet;

        /// <summary>
        /// Initialises obejct of <typeparamref name="BranchAndBound"/> type.
        /// </summary>
        /// <param name="graph">Graph that represents TSP problem.</param>
        /// <param name="root">Initial point (vertex).</param>
        public BranchAndBound(Graph graph, string root)
        {
            this.graph = graph;
            this.root = stringToVertex(root);
            pathAndBound = new List<Tuple<int, List<Vertex>>>();
            solution = new List<Vertex>();
            //minimumIsSet = false;
        }

        /// <summary>
        /// Gets the initial vertex.
        /// </summary>
        public Vertex Root
        {
            get { return root; }
        }

        /// <summary>
        /// Gets the cost of the soluton.
        /// </summary>
        public int Minimum
        {
            get { return minimum; }
        }

        /// <summary>
        /// Gets solution.
        /// </summary>
        public List<string> Solution
        {
            get
            {
                List<string> stringList = new List<string>();

                foreach (Vertex vertex in solution)
                    stringList.Add(vertex.Name);

                return stringList;
            }
        }

        /// <summary>
        /// Initialise algorithm.
        /// </summary>
        /// <param name="ver">Initial vertex (root).</param>
        public void Initialise(Vertex ver)
        {
            List<Vertex> tempList = new List<Vertex>();
            tempList.Add(ver);
            pathAndBound.Add(new Tuple<int, List<Vertex>>(getBound(tempList), tempList));

            while (pathAndBound.Count != 0)
            {
                int index = getLowestBound();
                List<List<Vertex>> possiblePaths = new List<List<Vertex>>(expandPath(pathAndBound[index].Item2));
                pathAndBound.RemoveAt(index);

                foreach (List<Vertex> path in possiblePaths)
                {
                    pathAndBound.Add(new Tuple<int, List<Vertex>>(getBound(path), path));
                    if (isLeaf(pathAndBound[pathAndBound.Count - 1]))
                        if (isSolution(pathAndBound[pathAndBound.Count - 1]))
                        {
                            getSolution();
                            updatePathAndBound();

                        }
                }
            }
        }



        private void getSolution() //NOTE change to take Tuple<> param? 
        {
            pathAndBound[pathAndBound.Count - 1].Item2.Add(root);
            solution = new List<Vertex>(pathAndBound[pathAndBound.Count - 1].Item2);
            minimum = getCost(solution);
            //minimumIsSet = true;
        }

        private int getCost(List<Vertex> vers)
        {
            int total = 0;

            //adds up cost of fixed verticies (already chosen)
            for (int index = 0; index < vers.Count - 1; index++)
                total += getEdgeWeight(vers[index], vers[index + 1]);

            return total;
        }

        private bool isSolution(Tuple<int, List<Vertex>> path) //NOTE change to take no params?
        {
            if (isContaining(root, path.Item2[path.Item2.Count - 1].AdjacentVerticies))
                return true;

            return false;
        }

        private bool isLeaf(Tuple<int, List<Vertex>> path) //NOTE change to take no params?
        {
            if (path.Item2.Count == graph.NoOfVertices)
                return true;

            return false;
        }

        private int getLowestBound()
        {
            int index = 0;
            int min = pathAndBound[0].Item1;

            for (int tupleIndex = 1; tupleIndex < pathAndBound.Count; tupleIndex++)
                if (pathAndBound[tupleIndex].Item1 < min)
                {
                    min = pathAndBound[tupleIndex].Item1;
                    index = tupleIndex;
                }

            return index;
        }

        private bool promising(Tuple<int, List<Vertex>> path)
        {
            return path.Item1 < minimum;
        }

        private void updatePathAndBound()
        {
            for (int tupleIndex = 0; tupleIndex < pathAndBound.Count; tupleIndex++)
                if (!promising(pathAndBound[tupleIndex]))
                {
                    pathAndBound.RemoveAt(tupleIndex);
                    tupleIndex--;
                }
        }

        private List<List<Vertex>> expandPath(List<Vertex> verticiesInPath)
        {
            List<List<Vertex>> possiblePaths = new List<List<Vertex>>();
            List<Vertex> path = new List<Vertex>(verticiesInPath);

            foreach (string vertexName in verticiesInPath[verticiesInPath.Count - 1].AdjacentVerticies)
                if (!isContaining(vertexName, path))
                {
                    path.Add(stringToVertex(vertexName));
                    possiblePaths.Add(path);
                    path = new List<Vertex>(verticiesInPath);
                }

            return possiblePaths;
        }

        private int getBound(List<Vertex> vers)
        {
            int total = 0;

            //adds up cost of fixed verticies (already chosen)
            for (int index = 0; index < vers.Count - 1; index++)
                total += getEdgeWeight(vers[index], vers[index + 1]);

            //NOTE! this part could be optimized, or not?, loop doesn't have to go through all of the graph verticies?
            foreach (Vertex possibleVertex in graph.Vertices)
                if (!isContaining(possibleVertex, vers) || possibleVertex == vers[vers.Count - 1])
                    total += getMinimum(possibleVertex, vers);

            return total;
        }

        private int getMinimum(Vertex ver, List<Vertex> verticies)
        {
            List<Vertex> visitedVerticies = new List<Vertex>(verticies);
            int min = 0;
            bool isSet = false;

            //does the given vertex "ver" equals to the last vertex in the given list of verticies "verticies"
            if (ver != visitedVerticies[visitedVerticies.Count - 1])
                visitedVerticies.RemoveAt(0); //remove root
            for (int i = 0; i < ver.AdjacentVerticies.Count; i++)
                if (!isContaining(ver.AdjacentVerticies[i], visitedVerticies) && isSet == false)
                {
                    min = ver.Weights[i];
                    isSet = true;
                }
                else if (!isContaining(ver.AdjacentVerticies[i], visitedVerticies) && ver.Weights[i] < min)
                    min = ver.Weights[i];

            return min;
        }

        private bool isContaining(string vertexName, List<Vertex> visitedVerticies)
        {
            foreach (Vertex ver in visitedVerticies)
                if (ver.Name == vertexName)
                    return true;

            return false;
        }

        private bool isContaining(Vertex vertex, List<Vertex> visitedVerticies)
        {
            foreach (Vertex ver in visitedVerticies)
                if (ver == vertex)
                    return true;

            return false;
        }

        private bool isContaining(Vertex vertex, List<string> visitedVerticies)
        {
            foreach (string ver in visitedVerticies)
                if (ver == vertex.Name)
                    return true;

            return false;
        }

        private int getEdgeWeight(Vertex ver1, Vertex ver2)
        {
            //get weight value from list of weights (from ver1) that correponds with the name of next vertex (from ver2)
            return ver1.Weights[ver1.AdjacentVerticies.IndexOf(ver2.Name)];
        }

        private Vertex stringToVertex(string vertexName)
        {
            foreach (Vertex vertex in graph.Vertices)
                if (vertexName == vertex.Name)
                    return vertex;

            return null;
        }
    }
}


