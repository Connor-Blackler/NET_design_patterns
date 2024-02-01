namespace Mediator
{
    internal class Program
    {
        // Abstract Classes
        public interface IFood
        {
            public int Price();
            public string Name();
        }

        public class AbstractBuyer(IFoodProcessor processor)
        {
            protected IFoodProcessor _processor = processor;
        }

        public interface IFoodProcessor
        {
            public int Price(IFood food);
            public void Purchase(IFood food);
        }

        // Concrete Classes
        public class Shopper(IFoodProcessor processor) : AbstractBuyer(processor)
        {
            public void Purchase(IFood food)
            {
                this._processor.Purchase(food);
            }
        }

        public class Apple : IFood
        {
            public int Price()
            {
                return 199;
            }

            public string Name()
            {
                return "Apple";
            }
        }

        public class Chicken : IFood
        {
            public int Price()
            {
                return 599;
            }

            public string Name()
            {
                return "Chicken";
            }
        }

        // Concrete mediator
        public class Cashier : IFoodProcessor
        {
            private readonly List<IFood> _shoppingList = new();

            public int Price(IFood food)
            {
                return food.Price();
            }

            public void Purchase(IFood food)
            {
                this._shoppingList.Add(food);
            }

            public int TotalPrice()
            {
                int ret = 0;

                foreach (IFood thisFood in _shoppingList)
                {
                    ret += thisFood.Price();
                }

                return ret;
            }
        }

        static void Main(string[] args)
        {
            Cashier shopAssistant = new();
            Shopper Customer = new(shopAssistant);

            List<IFood> FoodToBuy = [new Apple(), new Apple(), new Chicken(), new Chicken()];

            foreach (IFood food in FoodToBuy)
            {
                Customer.Purchase(food);
            }

            Console.WriteLine(shopAssistant.TotalPrice());
        }
    }
}
