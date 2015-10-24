using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace StronglyConnectedComponents.Client
{
    public class LineFeeder : IEnumerable<string>
    {
        private readonly string filePath;

        public LineFeeder(string filePath)
        {
            this.filePath = filePath;
        }

        public IEnumerator<string> GetEnumerator()
        {
            var reader = new StreamReader(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
            reader.Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}