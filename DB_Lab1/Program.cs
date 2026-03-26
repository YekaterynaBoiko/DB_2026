namespace DB_Lab1
{
    internal class Program
    {
        class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public double Salary { get; set; }
            public int Age { get; set; }
        }

        class EmployeeService
        {
            private List<Employee> employees = new List<Employee>();
            private int nextId = 1;

            public void Create(string name, string position, double salary, int age)
            {
                employees.Add(new Employee
                {
                    Id = nextId++,
                    Name = name,
                    Position = position,
                    Salary = salary,
                    Age = age
                });
            }

            public bool Update(int id, string name, string position, double salary, int age)
            {
                var emp = employees.FirstOrDefault(e => e.Id == id);
                if (emp == null) return false;

                emp.Name = name;
                emp.Position = position;
                emp.Salary = salary;
                emp.Age = age;

                return true;
            }

            public bool Delete(int id)
            {
                var emp = employees.FirstOrDefault(e => e.Id == id);
                if (emp == null) return false;

                employees.Remove(emp);
                return true;
            }

            public List<Employee> GetAll(int page = 1, int pageSize = 10)
            {
                return employees
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            public int Count()
            {
                return employees.Count;
            }
        }

        static void Create(EmployeeService service)
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Position: ");
            var position = Console.ReadLine();

            Console.Write("Salary: ");
            double salary = double.Parse(Console.ReadLine());

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());

            service.Create(name, position, salary, age);
            Console.WriteLine("Employee added!");
        }


        static void Update(EmployeeService service)
        {
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("New Name: ");
            var name = Console.ReadLine();

            Console.Write("New Position: ");
            var position = Console.ReadLine();

            Console.Write("New Salary: ");
            double salary = double.Parse(Console.ReadLine());

            Console.Write("New Age: ");
            int age = int.Parse(Console.ReadLine());

            if (service.Update(id, name, position, salary, age))
                Console.WriteLine("Updated!");
            else
                Console.WriteLine("Employee not found");
        }

        static void Delete(EmployeeService service)
        {
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            if (service.Delete(id))
                Console.WriteLine("Deleted!");
            else
                Console.WriteLine("Employee not found");
        }

        static void Show(EmployeeService service)
        {
            int page = 1;
            int pageSize = 10;

            while (true)
            {
                var employees = service.GetAll(page, pageSize);

                Console.Clear();
                Console.WriteLine($"--- Page {page} ---");

                if (employees.Count == 0)
                {
                    Console.WriteLine("No data");
                }
                else
                {
                    foreach (var e in employees)
                    {
                        Console.WriteLine($"ID: {e.Id} | Name: {e.Name} | Position: {e.Position} | Salary: {e.Salary} | Age: {e.Age}");
                    }
                }

                Console.WriteLine("\nN - next page");
                Console.WriteLine("P - previous page");
                Console.WriteLine("Q - quit");

                Console.Write("Choose: ");
                var input = Console.ReadLine().ToLower();

                if (input == "n")
                {
                    if (page * pageSize < service.Count())
                        page++;
                }
                else if (input == "p")
                {
                    if (page > 1)
                        page--;
                }
                else if (input == "q")
                {
                    break;
                }
            }
        }
        static void Main(string[] args)
        {
            var service = new EmployeeService();

            while (true)
            {
                Console.WriteLine("\n1. Create");
                Console.WriteLine("2. Update");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Show (paged)");
                Console.WriteLine("0. Exit");

                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Create(service);
                        break;

                    case "2":
                        Update(service);
                        break;

                    case "3":
                        Delete(service);
                        break;

                    case "4":
                        Show(service);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
