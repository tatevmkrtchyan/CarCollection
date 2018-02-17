
namespace CarCollection
{
    public class Car
    {
        public Model Model = new Model();

        public string Color { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        public bool Sold;
        public bool Deleted;

        public Car()
        {
            Sold = false;
            Deleted = false;
        }

        public override string ToString()
        {
            return $"{Model.Name}~{Color}~{Year}~{Price}";
        }
    }
}
