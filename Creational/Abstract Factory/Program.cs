using System.Drawing;

namespace Abstract_Factory
{
    internal class Program
    {
        //The Abstract Factory Design Pattern provides a way to encapsulate a group of factories with a common theme
        // without specifying their concrete classes

        //Abstract Factory defines an interface for creating all distinct products but leaves the actual
        //product creation to concrete factory classes. Each factory type corresponds to a certain product variety

        //The client code calls the creation methods of a factory object instead of creating products directly with a constructor call (new operator).
        //Since a factory corresponds to a single product variant, all its products will be compatible.

        //Client code works with factories and products only through their abstract interfaces.
        //This lets the client code work with any product variants, created by the factory object.
        //You just create a new concrete factory class and pass it to the client code.


        // Declare Product interface "A"
        public interface IHouseDetails
        {
            public string Number { get; }
            public string PostCode { get; }
        }

        // Declare Product interface "B"
        public interface IPersonDetails
        {
            public string Name { get; }
        }

        // Declare the Factory interface
        public interface IPersonFactory
        {
            public IHouseDetails HouseDetails { get; }
            public IPersonDetails PersonDetails { get; }
        }

        // Declare the concrete class for PersonDetails
        public class Person(string name) : IPersonDetails
        {
            private readonly string _name = name;

            public string Name {
                get
                { return _name; }
            }

            public override string ToString()
            {
                return $"\t \t Person ========== \n" +
                    $"\t \t \t Name: {this.Name} \n";
            }
        }

        // Declare the concrete class for House
        public class House(string Number, string PostCode) : IHouseDetails
        {
            private readonly string _number = Number;
            private readonly string _postCode = PostCode;

            public string Number {
                get
                { return _number; }
            }

            public string PostCode
            {
                get
                { return _postCode; }
            }

            public override string ToString()
            {
                return $"\t \t House ========== \n" +
                    $"\t \t \t Number: {this.Number} \n" +
                    $"\t \t \t PostCode: {this.PostCode} \n";
            }
        }

        // Wrap everything together with the different concrete factories
        // First make an abstract factory to isolate all boiler plate code
        public abstract class AbstractPersonFactory(IPersonDetails person, IHouseDetails house) : IPersonFactory
        {
            protected readonly IPersonDetails _personDetails = person;
            protected readonly IHouseDetails _houseDetails = house;

            public IHouseDetails HouseDetails
            {
                get { return this._houseDetails; }
            }

            public IPersonDetails PersonDetails
            {
                get { return this._personDetails; }
            }

            public override string ToString()
            {
                string ret = "\t PersonFactory \n";

                ret += this._personDetails;
                ret += this._houseDetails;

                return ret;
            }
        }

        // Declare concrete factory "A" (use a better name as A indicates an array like object)
        public class PersonFactoryA : AbstractPersonFactory
        {
            public PersonFactoryA() : base(new Person("Connor"), new House("11", "PL12XYZ")) { }
        }

        // Declare concrete factory "B"
        public class PersonFactoryB : AbstractPersonFactory
        {
            public PersonFactoryB() : base(new Person("Brandy"), new House("33", "PL44BNH")) { }
        }


        static void Main(string[] args)
        {
            void ReportPersonFactory(IPersonFactory personFactory)
            {
                if (personFactory == null) { return; }
                Console.WriteLine(personFactory);
            }

            Console.WriteLine("Initiate first factory A");
            ReportPersonFactory(new PersonFactoryA());

            Console.WriteLine("Initiate second factory B");
            ReportPersonFactory(new PersonFactoryB());
        }
    }
}
