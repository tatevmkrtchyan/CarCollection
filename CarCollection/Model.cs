namespace CarCollection
{
    public class Model
    {
        public string Name { get; set; }

        public Model()
        {
            Name = "Unknown";
        }

        public Model(string name)
        {
            Name = name;              
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
