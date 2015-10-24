using System.Collections.Generic;
using System.Linq;

namespace StronglyConnectedComponents.Client
{
    public class VertexFactory <T>
    {
        public readonly Dictionary<T,Vertex<T>>  VertexDictionary = new Dictionary<T, Vertex<T>>();

        public void Add(T vertex, T dependency)
        {
            // Add new vertex
            if (!VertexDictionary.ContainsKey(vertex))
            {
                VertexDictionary.Add(vertex,new Vertex<T>(vertex));
            }
            // Add dependency vertex to vertex list
            if (!VertexDictionary.ContainsKey(dependency))
            {
                VertexDictionary.Add(dependency, new Vertex<T>(dependency));
            }
            // Add dependency to vertex
            if (VertexDictionary[vertex].Dependencies.All(dep => !dep.Value.Equals(vertex)))
            {
                VertexDictionary[vertex].Dependencies.Add(VertexDictionary[dependency]);
            }
        }
    }
}