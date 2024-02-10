using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace Singleton
{
    internal class Program
    {
        // A singleton is a class which only allows a single instance of itself to be created,
        // and usually gives simple access to that instance.

        // This example is a thread-safe approach to a singleton given the proceedings of the singleton initialization
        public sealed class PersonManager
        {
            private static readonly PersonManager instance = new PersonManager();

            // Explicit static constructor to tell C# compiler not to mark type as "beforefieldinit"
            //      If marked "BeforeFieldInit" then the type's initializer method is executed at, or sometime before, first access to any
            //          static field defined for that type
            //      If not marked "BeforeFieldInit" then that type's initializer method is executed at:
            //          - first access to any static or instance field of that type, or
            //          - first invocation of any static, instance or virtual method of that type
            static PersonManager()
            {
                // If this static method is removed:
                //      the "public static PersonManager Instance" getter would get called first
                //      then the constructor would fire
                // If this static is included:
                //      The constructor would fire first
                //      Then "public static PersonManager Instance" would get called second
            }

            // Privatise the constructor to prevent callers using "new" to instantiate this object
            private PersonManager()
            {
                Console.WriteLine("constructing");
            }

            public static PersonManager Instance
            {
                get
                {
                    Console.WriteLine("instance getter has gotten called");
                    return instance;
                }
            }
        }

        static void Main(string[] args)
        {
            PersonManager mgr = PersonManager.Instance;
        }
    }
}
