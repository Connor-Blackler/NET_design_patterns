namespace Lazy_Initialization
{
    // Lazy initialization of an object means that its creation is deferred until it is first used
    // Lazy initialization is primarily used to improve performance, avoid wasteful computation, and reduce program memory requirements

    // Although you can write your own custom code to implement lazy initialization, Microsoft recommends
    // using the Lazy<T> class instead. The Lazy<T> class in the System namespace in C# was
    // introduced as part of .Net Framework 4.0 to provide a thread-safe way to implement lazy initialization. 


    internal class Program
    {
        public sealed class MarketManager
        {
            private readonly Dictionary<string, List<House>> _HouseRegistry = new Dictionary<string, List<House>>();
            private static readonly MarketManager instance = new MarketManager();
            public static MarketManager Instance { get { return instance; } }
            static MarketManager() { }
            private MarketManager()
            {
                Console.WriteLine("Market Manager is constructing");
                // dummy construction of houses
                // person 1
                List<House> BobHouses = [];
                BobHouses.Add(new House("11 bob street"));
                BobHouses.Add(new House("12 test ave"));
                _HouseRegistry.Add("Bob the builder", BobHouses);

                // person 2
                List<House> BranHouses = [];
                BranHouses.Add(new House("1234 hj lane"));
                BranHouses.Add(new House("263 xd road"));
                _HouseRegistry.Add("Brans the best", BranHouses);
            }

            public List<House> GetHouses(string name)
            {
                return _HouseRegistry[name];
            }
        }

        public class House(string address)
        {
            private readonly string _address = address;
            public string Address() { return _address; }
        }

        private class Human
        {
            private readonly string _name;
            private readonly Lazy<List<House>> _house;

            public Human(string name)
            {
                _name = name;
                // to initalize the lazy, we must past through a delegate which is called when the object needs initalizating
                _house = new Lazy<List<House>>(InitializeHouses);
            }

            private List<House> InitializeHouses()
            {
                //this method is used as a delegate for instantiation of _house lazy object
                Console.WriteLine($"InitializeHouses is called for {_name}");
                return MarketManager.Instance.GetHouses(_name);
            }

            public string Name() { return _name; }

            public List<House> GetHouses()
            {
                Console.WriteLine($"GetHouse is called for {_name}");
                return _house.Value;
            }
        }

        static void Main(string[] args)
        {
            Human bob = new("Bob the builder");
            Human brans = new("Brans the best");

            Console.WriteLine("Houses created");
            Console.WriteLine($"Name of bob: {bob.Name()}");
            Console.WriteLine($"Name of brans: {brans.Name()}");

            // at this point, the singleton and the Human.house list is not constructed

            //lets get bobs houses
            Console.WriteLine("\n");
            Console.WriteLine("Retrieving Bobs houses");
            List<House> BobHouses = bob.GetHouses();
            foreach (House house in BobHouses)
            {
                Console.WriteLine($"\t {house.Address()}");
            }

            //lets get brans houses
            Console.WriteLine("\n");
            Console.WriteLine("Retrieving Brans houses");
            List<House> BransHouses = brans.GetHouses();
            foreach (House house in BransHouses)
            {
                Console.WriteLine($"\t {house.Address()}");
            }
        }
    }
}
