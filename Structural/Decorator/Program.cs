using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Decorator
{
    //Decorator is a structural pattern that allows adding new behaviors to objects dynamically by placing them inside special wrapper objects,
    //called decorators.

    //Using decorators you can wrap objects countless number of times since both target objects and decorators follow the same interface. 
    //The resulting object will get a stacking behavior of all wrappers.

    public interface IWorker
        //interface to perform an action
    {
        public void DoWork();
    }

    public class Processor
        //Processor class that initiates the work
    {
        public void Action(IWorker worker)
        {
            worker.DoWork();
        }
    }

    public class WorkerImpl : IWorker
    {
        public void DoWork()
        {
            Console.WriteLine("WorkerImpl, doing the work!");
        }
    }

    public class WorkerDecorator : IWorker
    {
        //Decorator class that implements IWorker so it can be used with the common interface of IWorker
        private readonly IWorker _worker;

        public WorkerDecorator(IWorker worker)
        {
            this._worker = worker;
        }

        public void DoWork()
        {
            Console.WriteLine("WorkerDecorator, doing somthing extra!");
            this._worker.DoWork();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Processor processor = new();

            IWorker worker = new WorkerImpl();
            WorkerDecorator decorator = new(worker);

            processor.Action(decorator);
        }
    }
}
