using static Object_Pool.Program;

namespace Object_Pool
{
    internal class Program
    {
        // Allows keeping a group of objects in memory for reuse rather than allowing the objects to be garbage collected
        // Apps might want to use the object pool if the objects that are being managed are:
        //      Expensive to allocate/initialize.
        //      Represent a limited resource.
        //      Used predictably and frequently.


        // Construct a generic item that will be pooled
        public class PoolItem<T>(T wrapped) : IDisposable
        {
            public event EventHandler<EventArgs> DisposedMethod;
            public T WrappedObject { get; private set; } = wrapped;
            public void Dispose() => DisposedMethod(this, EventArgs.Empty);
        }

        // Construct the generic pool holder, enforcing class
        public class Pool<T>(Func<T> initializer) where T : class
        {
            // An object that can be used to synchronize access
            private static readonly object _SyncRoot = new object();

            // initialization method used to construct the object to pool
            private readonly Func<T> _InitializationMethod = initializer;

            // List of currently pooled items
            private List<T> _PooledItems = new List<T>();

            //Method to get the next available pool item

            void DisposeWrapper(object sender, EventArgs e)
            {
                PoolItem<T>? wrapper = sender as PoolItem<T>;

                lock (_SyncRoot)
                {
                    _PooledItems.Add(wrapper.WrappedObject);
                }
            }

            public PoolItem<T> Get()
            {
                T? ret = null;

                lock (_SyncRoot)
                {
                    if (_PooledItems.Count > 0)
                    {
                        ret = _PooledItems[0];
                        _PooledItems.RemoveAt(0);
                    }
                }

                ret ??= _InitializationMethod();

                PoolItem<T> wrapper = new PoolItem<T>(ret);
                wrapper.DisposedMethod += DisposeWrapper;

                return wrapper;
            }
        }

        //Construct a concrete poolable object
        class ProcessorWithComplexInit
        {
            public ProcessorWithComplexInit()
            {
                Console.WriteLine("Complex constructor is being performed");
            }

            public void Process(string work)
            {
                Console.WriteLine($"Processing: {work}");
            }
        }

        static void Main(string[] args)
        {
            var objectPool = new Pool<ProcessorWithComplexInit>(() => new ProcessorWithComplexInit());

            // We declare the first object in the same scope as the others. This object has not been disposed therefore
            // it will not be available by the next two calls
            PoolItem<ProcessorWithComplexInit> processor1 = objectPool.Get();
            processor1.WrappedObject.Process("1: the first work!");

            // Use using here to scope the next processor object for automatic cleanup, this will allow the object to be reused and placed back
            // into the object pool
            using (PoolItem<ProcessorWithComplexInit> processor2 = objectPool.Get())
                processor2.WrappedObject.Process("2: the Second work!");

            // We will reuse the same object as processor2 here, without the caller knowing
            using (PoolItem<ProcessorWithComplexInit> processor3 = objectPool.Get())
                processor3.WrappedObject.Process("3: the third work!");

            //shifting this line above the processor2 or processor 3 will cause the next call to use processor1's object without the caller knowing
            processor1.Dispose();
        }
    }
}
