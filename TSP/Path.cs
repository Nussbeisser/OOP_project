using System.Collections.Generic;
using GraphClass;

namespace TSP
{
    /// <summary>
    /// Data structure that represents path of verticies.
    /// </summary>
    public class Path
    {
        private List<Vertex> verticies;

        /// <summary>
        /// Initialises object of <typeparamref name="Path"/> type.
        /// </summary>
        public Path()
        {
            verticies = new List<Vertex>();
        }

        /// <summary>
        /// Copies object of <typeparamref name="Path"/> type.
        /// </summary>
        /// <param name="p">Object of <typeparamref name="Path"/> type.</param>
        public Path(Path p)
        {
            this.verticies = new List<Vertex>(p.Verticies);
        }
        
        /// <summary>
        /// Gets the verticies in the path.
        /// </summary>
        public List<Vertex> Verticies
        {
            get { return verticies; }
            //set { verticies = value; }
        }

        /// <summary>
        /// Adds vertex to the end of the path.
        /// </summary>
        /// <param name="ver">Vertex to be added at the end of the path.</param>
        public void Add(Vertex ver)
        {
            verticies.Add(ver);   
        }

        /// <summary>
        /// Removes last vertex in the path.
        /// </summary>
        public void Remove()
        {
            if (verticies.Count != 0)
                verticies.RemoveAt(verticies.Count - 1);
        }

        /// <summary>
        /// Check if the path contains given vertex.
        /// </summary>
        /// <param name="ver">Vertex to be checked in the path.</param>
        /// <returns>Returns true if given vertex occurs in the path, otherwise returns false.</returns>
        public bool Contains(Vertex ver)
        {
            foreach (Vertex vertex in verticies)
                if (vertex == ver)
                    return true;
            return false;
        }
        
        /// <summary>
        /// Calculates cost of the path.
        /// </summary>
        /// <returns>Returns cost of the path</returns>
        public int calculatePathCost()
        {
            int cost = 0;

            //go through every but last vertex in verticies list
            for (int index = 0; index < verticies.Count - 1; index++)
                cost += getEdgeWeight(verticies[index], verticies[index + 1]); //add up total cost

            return cost;
        }

        /// <summary>
        /// Gets names of the verticies in the path.
        /// </summary>
        /// <returns>Returns list of verticies names in the path.</returns>
        public List<string> getPath()
        {
            List<string> verticiesNames = new List<string>();
            foreach (Vertex vertex in verticies)
                verticiesNames.Add(vertex.Name);

            return verticiesNames;
        }



        private int getEdgeWeight(Vertex ver1, Vertex ver2)
        {
            //get weight value from list of weights (from ver1) that correponds with the name of next vertex (from ver2)
            return ver1.Weights[ ver1.AdjacentVerticies.IndexOf(ver2.Name) ];
        }
    }
}
