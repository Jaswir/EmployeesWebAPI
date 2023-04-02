using Microsoft.EntityFrameworkCore;
using EmployeesWebAPI.Models.Domain;

namespace EmployeesWebAPI.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
