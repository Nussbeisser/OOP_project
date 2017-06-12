using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GraphClass
{
    /// <summary>
    /// Data structure that represents directed graph.
    /// </summary>
    public class Graph
    {
        private List<Vertex> vertices;
        private int noOfVertices;

        /// <summary>
        /// Initialises object of <typeparamref name="Graph"/> type.
        /// </summary>
        /// <param name="filePath">File location.</param>
        public Graph(string filePath)
        {
            vertices = new List<Vertex>();
            List<string> edges = new List<string>();
            List<int> weightsOfEdges = new List<int>();

            readFromFile(filePath, ref edges, ref weightsOfEdges);
            extractVertices(edges, weightsOfEdges);

            noOfVertices = vertices.Count;
        }

        /// <summary>
        /// Gets the number of verticies in the graph.
        /// </summary>
        public int NoOfVertices
        {
            get { return noOfVertices; }
        }

        /// <summary>
        /// Gets the graph verticies.
        /// </summary>
        public List<Vertex> Vertices
        {
            get { return vertices; }
        }



        private void readFromFile(string filePath, ref List<string> edges, ref List<int> weightsOfEdges)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader file = new StreamReader(stream);

            string fileLine;
            char[] separator = { ',', ' ' };

            //read the sets of edges
            fileLine = file.ReadLine();
            edges = fileLine.Split(separator, StringSplitOptions.RemoveEmptyEntries).
                AsEnumerable().ToList(); //list = table -> enumerable -> list

            //read the set of weights
            fileLine = file.ReadLine();
            weightsOfEdges = Array.ConvertAll(fileLine.Split(separator, StringSplitOptions.RemoveEmptyEntries), int.Parse).
                AsEnumerable().ToList(); //list = table<string> -> table<int> -> enumerable -> list

            file.Close();
        }

        private void extractVertices(List<string> edges, List<int> weightsOfEdges)
        {
            string vertexName;
            List<string> adjacentVertices = new List<string>();
            List<int> weights = new List<int>();

            vertexName = getVertexName(edges[0]);

            for (int i = 0; i < edges.Count; i++)
            {
                if (vertexName != getVertexName(edges[i]))
                {
                    vertices.Add(new Vertex(vertexName, adjacentVertices, weights));
                    adjacentVertices.Clear();
                    weights.Clear();

                    vertexName = getVertexName(edges[i]);
                    adjacentVertices.Add(getAdjVertex(edges[i]));
                    weights.Add(weightsOfEdges[i]);
                }
                else
                {
                    adjacentVertices.Add(getAdjVertex(edges[i]));
                    weights.Add(weightsOfEdges[i]);
                }
            }
            vertices.Add(new Vertex(vertexName, adjacentVertices, weights));
        }

        private string getVertexName(string rawEdge)
        {
            return rawEdge.Split('-')[0];
        }

        private string getAdjVertex(string rawEdge)
        {
            return rawEdge.Split('-')[1];
        }
    }
}
