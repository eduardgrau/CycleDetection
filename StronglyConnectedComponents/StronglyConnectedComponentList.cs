﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StronglyConnectedComponents
{
    public class StronglyConnectedComponentList<T> : IEnumerable<StronglyConnectedComponent<T>>
    {
        private readonly LinkedList<StronglyConnectedComponent<T>> collection;

        public StronglyConnectedComponentList()
        {
            this.collection = new LinkedList<StronglyConnectedComponent<T>>();
        }

        public StronglyConnectedComponentList(IEnumerable<StronglyConnectedComponent<T>> collection)
        {
            this.collection = new LinkedList<StronglyConnectedComponent<T>>(collection);
        }

        public void Add(StronglyConnectedComponent<T> scc)
        {
            this.collection.AddLast(scc);
        }

        public int Count => this.collection.Count;

        public IEnumerator<StronglyConnectedComponent<T>> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        public IEnumerable<StronglyConnectedComponent<T>> IndependentComponents()
        {
            return this.Where(c => !c.IsCycle);
        }

        public IEnumerable<StronglyConnectedComponent<T>> Cycles()
        {
            return this.Where(c => c.IsCycle);
        }
    }
}
