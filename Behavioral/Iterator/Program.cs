using Animals;
using System;
using System.Collections;

namespace Iterator
{
    internal class Program
    {
        public class Animals : IEnumerable
        {
            private List<Animal> _animals;

            public Animals()
            {
                _animals = new List<Animal>();
            }

            public Animals(Animal[] animals) : this()
            {
                foreach (Animal thisAnimal in animals)
                {
                    this.Append(thisAnimal);
                }
            }

            public void Append(Animal animal)
            {
                this._animals.Add(animal);
            }

            public AnimalsEnum GetEnumerator()
            {
                return new AnimalsEnum(this._animals);
            }

            // Implementation for the GetEnumerator method.
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override String ToString()
            {
                String ret = "";

                foreach (Animal thisAnimal in this._animals)
                {
                    ret += thisAnimal.ToString();
                }

                return ret;
            }
        }

        // When you implement IEnumerable, you must also implement IEnumerator.
        public class AnimalsEnum : IEnumerator
        {
            public List<Animal> _myAnimals;
            int currentPos = -1;

            public AnimalsEnum(List<Animal> animals)
            {
                _myAnimals = animals;
            }

            public bool MoveNext()
            {
                currentPos++;
                return (currentPos < _myAnimals.Count -1);
            }

            public void Reset()
            {
                currentPos = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Animal Current
            {
                get
                {
                    try
                    {
                        return _myAnimals[currentPos];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Animals allAnimals = new(
                new Animal[]
                {
                    new Boxer("Harry"),
                    new Sausage("Ralph")
                }
            );

            allAnimals.Append(new KingCobra("Shannon"));
            allAnimals.Append(new Adder("Brandy"));

            Console.WriteLine(allAnimals);
        }
    }
 }