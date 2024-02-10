namespace Dependency_Injection
{
    internal class Program
    {
        public class NumberOperator(Int128 x, Int128 y)
        {
            private readonly Int128 _x = x;
            private readonly Int128 _y = y;

            public Int128 Operate(Func<Int128, Int128, Int128> operation)
            {
               return operation(_x, _y);
            }
        }

        static void Main(string[] args)
        {
            // Define a lambda expression for addition
            Func<Int128, Int128, Int128> addition = (a, b) => a + b;

            // Define a lambda expression for multiply
            Func<Int128, Int128, Int128> multiply = (a, b) => a * b;

            // Define a lambda expression for subtract
            Func<Int128, Int128, Int128> subtract = (a, b) => a - b;

            // Define a lambda expression for divide
            Func<Int128, Int128, Int128> divide = (a, b) => a / b;

            NumberOperator handler = new(100, 100);
            Console.WriteLine($"Lets try addition: \n" +
                $"\t {handler.Operate(addition)}");

            Console.WriteLine($"Lets try multiply: \n" +
                $"\t {handler.Operate(multiply)}");

            Console.WriteLine($"Lets try subtract: \n" +
                $"\t {handler.Operate(subtract)}");

            Console.WriteLine($"Lets try divide: \n" +
                $"\t {handler.Operate(divide)}");
        }
    }
}
