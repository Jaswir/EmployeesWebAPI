using EmployeesWebAPI.Models.Domain;

namespace EmployeesWebAPI.Data
{
    public class Seeder
    {
        public void Seed_Employees(ApplicationDbContext context)
        {
            //Seeds Employees Data incase table is empty
            var testEmployee = context.Employees.FirstOrDefault(b => b.Name == "Bill Gates");
            if (testEmployee == null)
            {
                AddEmployee(context, "Mark Zuckerberg", "markyzuckerberg@gmail.com", "IT", 3536000000, new DateTime(1984, 1, 7));
                AddEmployee(context, "Elon Musk", "elonmusk@gmail.com", "Electric Vehicles", 7355600000, new DateTime(1971, 1, 7));
                AddEmployee(context, "Bill Gates", "billgates@gmail.com", "IT", 2500000000, new DateTime(1956, 1, 7));
            }

            context.SaveChanges();
        }

        private void AddEmployee(ApplicationDbContext context, string name, string email, string department, long salary, DateTime dateOfBirth)
        {
            context.Employees.Add(
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Department = department,
                    Salary = salary,
                    DateOfBirth = dateOfBirth

                });
        }
    }
}
