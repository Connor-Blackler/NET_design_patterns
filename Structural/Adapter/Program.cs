namespace Adapter
{
    internal class Program
    {
        // The Adapter pattern converts an interface into another interface that clients expect
        // The Adapter pattern allows classes of incompatible interfaces to work together.

        // Concrete Employee system
        // !! Importrant: This system knows nothing about PaymentProcessor systems
        public class CompanyEmployee(string name, string address, string number, string accountnumber, string sortcode, int salary)
        {
            public string Name { get; } = name;
            public string Address { get; } = address;
            public string Number { get; } = number;
            public string AccountNumber { get; } = accountnumber;
            public string SortCode { get; } = sortcode;
            public int Salary { get; } = salary;
        }

        public class CompanyEmployees
        {
            private readonly List<CompanyEmployee> _employees = [];
            public CompanyEmployees()
            {
                _employees.Add(new CompanyEmployee("Connor", "Connor address", "12312213", "Connor_MYACCOUNTNUMBER", "Connor_MYSORTCODE", 25));
                _employees.Add(new CompanyEmployee("Brandy", "Brandy address", "88956776", "Brandy_MYACCOUNTNUMBER", "Brandy_MYSORTCODE", 15));
            }

            public List<CompanyEmployee> GetEmployees()
            {
                // Clone the list to prevent callers from adding, this should be accessed through the iterator design pattern but lets keep it simple
                return new List<CompanyEmployee>(_employees);
            }
        }

        // Payment Processor system
        // !! Important: Payment Processor knows nothing about CompanyEmployee / CompanyEmployees system
        public interface IPayment
        {
            public void MakePayment();
        }

        public class PaymentRecipient(string AccountNumber, string SortCode)
        {
            public string AccountNumber { get; private set; } = AccountNumber;
            public string SortCode { get; private set; } = SortCode;
        }

        public class Payment(PaymentRecipient recipient, int amount) : IPayment
        {
            private readonly PaymentRecipient _recipient = recipient;
            private readonly int _amount = amount;

            public void MakePayment()
            {
                Console.WriteLine($"Making payment: \n" +
                    $"\t Account number: {_recipient.AccountNumber}) \n" +
                    $"\t Sort Code: {_recipient.SortCode}");
            }
        }

        public class PaymentProcessor(List<IPayment> PaymentActions)
        {
            private readonly List<IPayment> Payments = PaymentActions;

            public void Pay()
            {
                foreach (Payment payment in Payments)
                {
                    payment.MakePayment();
                }
            }
        }

        // Adapt the CompanyEmployees to be usable with the PaymentProcessor
        // Adapter knows both CompanyEmployee/s and PaymentProcessor systems
        public class CompanyEmployeePaymentAdapter(CompanyEmployees employees)
        {
            public List<CompanyEmployee> Employees { get; } = employees.GetEmployees();

            //Much better ways to do this, but lets keep it simple
            public List<IPayment> GetPayments()
            {
                List<IPayment> ret = [];

                // Iterate over each employee, and convert it to a Payment to be usable in the PaymentProcessor
                foreach (CompanyEmployee employee in Employees)
                {
                    ret.Add(new Payment(new PaymentRecipient(employee.AccountNumber, employee.SortCode), employee.Salary));
                }

                return ret;
            }
        }

        static void Main(string[] args)
        {
            // Construct our CompanyEmployees
            CompanyEmployees employees = new();

            // Create the adapter and get a list of IPayments for the PaymentProcessor
            CompanyEmployeePaymentAdapter employeeAdapter = new(employees);
            List<IPayment> payments = employeeAdapter.GetPayments();

            // Pass the payments through to the payment processor to make the payments
            PaymentProcessor processor = new(payments);
            processor.Pay();
        }
    }
}
