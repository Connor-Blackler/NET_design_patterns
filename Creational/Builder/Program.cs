﻿namespace Builder
{
    public interface IBuilder
    {
        //Builder
        //  This is an interface which is used to define all the steps required to create a product.
        public void BuildPartA();
        public void BuildPartB();
        public void BuildPartC();
    }

    public interface IAnimalProvider
    {
        //Seperate out the Animal component from IBuilder, keep IBuilder generic
        public Animal GetAnimal();
    }

    public class Animal
    {
        //Product
        //  This is a class which defines the parts of the complex object which are to be generated by the Builder Pattern.
        public string Name { get; set; }
        public string Species { get; set; }
        public UInt16 LegCount { get; set; }

        public override string ToString()
        {
            return $"Animal ======================== \n" +
                $"\t Name: {this.Name}, \n" +
                $"\t Species: {this.Species}, \n" +
                $"\t Leg Count: {this.LegCount} \n";
        }
    }

    public class AnimalSpec
    //Lets create a spec, since for more complex items, we can send in information using a spec object and let the builder
    // determine the ordering of construction / building
    {
        public string Name = "";
        public string Species = "";
        public UInt16 LegCount = 0;

        public AnimalSpec(string name, string species, UInt16 LegCount)
        {
            this.Name = name;
            this.Species = species;
            this.LegCount = LegCount;
        }

}

public class AnimalBuilder : IBuilder, IAnimalProvider
    {
        //ConcreteBuilder
        //  This is a class which implements the IBuilder / IAnimalProvider interface to create a complex product.
        private readonly Animal _animal;
        private readonly AnimalSpec _spec;

        public AnimalBuilder(AnimalSpec spec)
        {
            this._spec = spec;
            this._animal = new();
        }

        public void BuildPartA()
        {
            this._animal.Name = this._spec.Name;
        }

        public void BuildPartB()
        {
            this._animal.Species = this._spec.Species;
        }

        public void BuildPartC()
        {
            this._animal.LegCount = this._spec.LegCount;
        }

        public Animal GetAnimal()
        {
            return this._animal;
        }
    }

    public class ConcreteAnimalBuilder
    {
        private readonly AnimalBuilder _builder;

        public ConcreteAnimalBuilder(AnimalBuilder builder)
        {
            this._builder = builder;
        }

        public Animal GetAnimal()
        {
            this._builder.BuildPartA();
            this._builder.BuildPartB();
            this._builder.BuildPartC();
            return this._builder.GetAnimal();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ConcreteAnimalBuilder MyAnimalBuilder = new(new AnimalBuilder(new AnimalSpec("Brandy","Snake",0)));
            Animal MyAnimal = MyAnimalBuilder.GetAnimal();

            Console.WriteLine(MyAnimal);

            ConcreteAnimalBuilder MyAnimalBuilder2 = new(new AnimalBuilder(new AnimalSpec("Shannon", "spider", 8)));
            Animal MyAnimal2 = MyAnimalBuilder2.GetAnimal();

            Console.WriteLine(MyAnimal2);
        }
    }
}
