using static Animals.Animal;

namespace Animals
{
    public abstract class Animal
    {
        private string _name = "";

        public Animal(string name)
        {
            this._name = name;
        }

        protected abstract string species();
        protected abstract string race();

        public override String ToString()
        {
            return $"Aniamal ====================== \n" +
                $"\t Name: {this._name}, \n" +
                $"\t Species: {this.species()}, \n" +
                $"\t Race: {this.race()} \n";

        }
    }
    public abstract class Dog : Animal
    {
        protected Dog(string name) : base(name)
        {
            // Call Animal constructor
        }

        protected override string species()
        {
            return "Dog";
        }
    }

    public class Boxer : Dog
    {
        public Boxer(string name) : base(name)
        {
            // Call Dog constructor
        }

        protected override string race()
        {
            return "Boxer";
        }
    }

    public class Sausage : Dog
    {
        public Sausage(string name) : base(name)
        {
            // Call Dog constructor
        }

        protected override string race()
        {
            return "Sausage";
        }
    }

    public abstract class Snake : Animal
    {
        protected Snake(string name) : base(name)
        {
            // Call Animal constructor
        }

        protected override string species()
        {
            return "Reptile";
        }
    }

    public class KingCobra : Snake
    {
        public KingCobra(string name) : base(name)
        {
            // Call Snake constructor
        }

        protected override string race()
        {
            return "King Cobra";
        }
    }

    public class Adder : Snake
    {
        public Adder(string name) : base(name)
        {
            // Call Snake constructor
        }

        protected override string race()
        {
            return "Adder";
        }
    }
}
