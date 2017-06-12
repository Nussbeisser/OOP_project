using System.Collections.Generic;
using System.Linq;
using GraphClass;

namespace TSP
{
    /// <summary>
    /// Implements a brute force algorithm that finds solution to Travelling Salesman Problem (TSP).
    /// </summary>
    public class BruteForce
    {
        private Graph graph;
        private Vertex root;
        private Path currentPath;
        private Path solution;
        private int totalCost = 0;

        /// <summary>
        /// Initialises object of <typeparamref name="BruteForce"/> type.
        /// </summary>
        /// <param name="graph">Graph that represents TSP problem.</param>
        /// <param name="root">Initial point (vertex).</param>
        public BruteForce(Graph graph, string root)
        {
            this.graph = graph;
            this.root = isRootValid(stringToVertex(root));
            currentPath = new Path();
        }

        /// <summary>
        /// Ininitialise algorithm.
        /// </summary>
        /// <param name="ver">Initial vertex (root).</param>
        public void Initialise(Vertex ver)
        {
            if (promising(ver))
            {
                currentPath.Add(ver);
                if (isSolution())
                {
                    setSolution();
                    currentPath.Remove();
                }
                else
                {
                    foreach (string child in ver.AdjacentVerticies)
                        Initialise(stringToVertex(child));

                    currentPath.Remove();
                }
            }
        }

        /// <summary>
        /// Gets the initial vertex.
        /// </summary>
        public Vertex Root
        {
            get { return root; }
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        public List<string> Solution
        {
            get { return solution.getPath(); }
        }

        /// <summary>
        /// Gets the total cost of the solution
        /// </summary>
        public int TotalCost
        {
            get { return totalCost; }
        }



        private bool promising(Vertex ver)
        {
            //getLevel() + 1 - check in advance whether given vertex will be the last vertex
            return !currentPath.Contains(ver) || (isRoot(ver) && isLeaf(getLevel() + 1));
        }

        private bool isSolution()
        {
            return isLeaf(getLevel());
        }

        private void setSolution()
        {
            if (solution == null)
            {
                solution = new Path(currentPath);
                totalCost = solution.calculatePathCost();
            }
            else
                if (totalCost > currentPath.calculatePathCost())
            {
                solution = new Path(currentPath);
                totalCost = currentPath.calculatePathCost();
            }

        }

        private bool isRoot(Vertex ver)
        {
            return ver == root;
        }

        private bool isLeaf(int level)
        {
            return level == graph.NoOfVertices + 1;
        }

        private int getLevel()
        {
            return currentPath.Verticies.Count;
        }

        private Vertex stringToVertex(string vertexName)
        {
            foreach (Vertex vertex in graph.Vertices)
                if (vertexName == vertex.Name)
                    return vertex;

            return null;
        }

        private Vertex isRootValid(Vertex root)
        {
            if (graph.Vertices.Contains(root))
                return root;
            else
                throw new System.Exception("Such a root does not exist");
        }
    }
}

