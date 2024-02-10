namespace Factory_Method
{
    internal class Program
    {
        public interface ISpeakable
        {
            public string Speak();
        }

        class Person(string name) : ISpeakable
        {
            private readonly string _name = name;

            public string Speak()
            {
                return $"Hello, My name is {this._name}";
            }
        }

        abstract class SpeakOperator
        {
            // Note that the SpeakOperator may also provide some default implementation of
            // the factory method.
            public abstract ISpeakable Speaker();
            public string Perform()
            {
                ISpeakable MySpeaker = Speaker();
                return $"SpeakOperator: {MySpeaker.Speak()}";
            }
        }

        class ConcreteSpeakOperatorA : SpeakOperator
        {
            public override ISpeakable Speaker()
            {
                return new Person("Bob");
            }
        }

        class ConcreteSpeakOperatorB : SpeakOperator
        {
            public override ISpeakable Speaker()
            {
                return new Person("Connor");
            }
        }

        static void Main(string[] args)
        {
            void ConductTheSpeech(SpeakOperator speaker)
            {
                Console.WriteLine(speaker.Perform());
            }

            SpeakOperator speakerA = new ConcreteSpeakOperatorA();
            ConductTheSpeech(speakerA);

            SpeakOperator speakerB = new ConcreteSpeakOperatorB();
            ConductTheSpeech(speakerB);
        }
    }
}
