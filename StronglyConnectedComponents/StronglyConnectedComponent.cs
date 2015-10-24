﻿using System.Collections.Generic;

namespace StronglyConnectedComponents
{
    public class StronglyConnectedComponent<T> : IEnumerable<Vertex<T>>
    {
        private readonly LinkedList<Vertex<T>> list;

        public StronglyConnectedComponent()
        {
            this.list = new LinkedList<Vertex<T>>();
        }

        public StronglyConnectedComponent(IEnumerable<Vertex<T>> collection)
        {
            this.list = new LinkedList<Vertex<T>>(collection);
        }

        public void Add(Vertex<T> vertex)
        {
            this.list.AddLast(vertex);
        }

        public IEnumerator<Vertex<T>> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public int Count => this.list.Count;

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public bool IsCycle => list.Count > 1;
    }
}
