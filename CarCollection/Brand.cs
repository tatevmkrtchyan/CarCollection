using System.Collections.Generic;

namespace CarCollection
{
    public class Brand
    {
        public string Name { get; set; }

        public List<Model> Models=new List<Model>();

        public Brand()
        {

        }

        public Brand(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
