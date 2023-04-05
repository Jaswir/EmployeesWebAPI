using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesWebAPI.Models.Domain;
using EmployeesWebAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesAPIController : ControllerBase
    {
        public ApplicationDbContext ApplicationDbContext { get; }

        public EmployeesAPIController(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        // GET: api/EmployeesAPI
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            var employees = await ApplicationDbContext.Employees.ToListAsync();
            return employees;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Employee>>> PostEmployee(Employee employee)
        {
            ApplicationDbContext.Employees.Add(employee);
            await ApplicationDbContext.SaveChangesAsync();

            //Give 201 Status code if succesfully created
            return CreatedAtAction("Get", new { id = employee.Id }, employee);
        }

        // GET: api/EmployeesAPI/firstname
        [HttpGet("{firstname}")]
        public async Task<ActionResult<Employee>> GetFirstName(string firstName)
        {
            //First or Default Async cause there can be more that match the condition
            var employee = await ApplicationDbContext.Employees.FirstOrDefaultAsync(x => x.FirstName == firstName);

            if (employee == null)
            {
                //404
                return NotFound();
            }

            return employee;
        }


        //[HttpDelete("firstname/{firstname}")]
        //public async Task<ActionResult<IEnumerable<Employee>>> DeleteEmployee(string firstName)
        //{
        //    //Find Async : get item given its primary key,
        //    // Optimized for finding an entity with a primary key,
        //    // by returning the entity without hitting the database if the entity was tracked.
        //    //
        //    var employee = await ApplicationDbContext.Employees.FirstOrDefaultAsync(x => x.FirstName == firstName);
        //    if (employee == null)
        //    {
        //        //404
        //        return NotFound();
        //    }

        //    ApplicationDbContext.Employees.Remove(employee);
        //    await ApplicationDbContext.SaveChangesAsync();


        //    //204 No content to return 
        //    return NoContent();
        //}

        // GET: api/EmployeesAPI/department
        [HttpGet("department")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetDepartment(string department)
        {
            var employees = await ApplicationDbContext.Employees.Where(x => x.Department == department).ToListAsync();

            if (employees == null)
            {
                //404
                return NotFound();
            }

            return employees;
        }

     

        //[HttpPut("firstname/{firstname}")]
        //public async Task<ActionResult<IEnumerable<Employee>>> PutEmployee(string firstName, Employee employee)
        //{
        //    if (firstName != employee.FirstName)
        //    {
        //        //Error 400 => Client error, client does something wrong
        //        return BadRequest();
        //    }

        //    ApplicationDbContext.Entry(employee).State = EntityState.Modified;

        //    await ApplicationDbContext.SaveChangesAsync();

        //    //Give 201 Status code if succesfully created
        //    return CreatedAtAction("Get", new { id = employee.Id }, employee);
        //}



    }
}
