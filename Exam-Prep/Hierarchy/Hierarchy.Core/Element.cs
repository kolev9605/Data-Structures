namespace Hierarchy.Core
{
    using System.Collections.Generic;

    public class Element<T>
    {
        public Element()
        {
            this.Children = new Dictionary<T, T>();
            this.Parent = default(T);
        }

        public Dictionary<T,T> Children { get; set; }

        public T Parent { get; set; }
    }
}
