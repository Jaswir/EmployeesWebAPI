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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            var employees = await ApplicationDbContext.Employees.ToListAsync();
            return employees;
        }

        // GET: api/EmployeesAPI/firstname
        [HttpGet("firstname/{firstname}")]
        public async Task<ActionResult<Employee>> GetFirstName(string firstName)
        {
            var employee = await ApplicationDbContext.Employees.FirstOrDefaultAsync(x => x.FirstName == firstName);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // GET: api/EmployeesAPI/department
        [HttpGet("department/{department}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetDepartment(string department)
        {
            var employees = await ApplicationDbContext.Employees.Where(x => x.Department == department).ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

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

        //[HttpDelete("{firstname}")]
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



        // POST api/<EmployeesAPIController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<EmployeesAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesAPIController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
