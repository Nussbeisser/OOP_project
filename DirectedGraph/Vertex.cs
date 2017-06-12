using System.Collections.Generic;

namespace GraphClass
{
    /// <summary>
    /// Data structure which contains data about a single vertex.
    /// </summary>
    public class Vertex
    {

        private string name;
        private List<string> adjacentVertices;
        private List<int> weights;

        /// <summary>
        /// Initialises object of <typeparamref name="Vertex"/> type.
        /// </summary>
        /// <param name="name">Vertex name.</param>
        /// <param name="adjacentVertices">Names of adjacent verticies.</param>
        /// <param name="weights">Weights that correpond between this vertex and its adjacent verticies.</param>
        public Vertex(string name, List<string> adjacentVertices, List<int> weights)
        {
            this.name = name;
            this.adjacentVertices = new List<string>(adjacentVertices);
            this.weights = new List<int>(weights);
        }

        /// <summary>
        /// Copies object of <typeparamref name="Vertex"/> type.
        /// </summary>
        /// <param name="ver">Object to be copied.</param>
        public Vertex(Vertex ver)
        {
            name = ver.Name;
            adjacentVertices = new List<string>(ver.AdjacentVerticies);
            weights = new List<int>(ver.weights);
        }
        
        /// <summary>
        /// Gets the vertex name.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the names of adjacent vertices.
        /// </summary>
        public List<string> AdjacentVerticies
        {
            get { return adjacentVertices; }
        }

        /// <summary>
        /// Gets correspoding weights between this vertex and its adjacent verticies.
        /// </summary>
        public List<int> Weights
        {
            get { return weights; }
        }
    }
}
