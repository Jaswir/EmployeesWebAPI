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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Employee>>> PostEmployee(Employee employee)
        {
            ApplicationDbContext.Employees.Add(employee);
            await ApplicationDbContext.SaveChangesAsync();

            //Give 201 Status code if succesfully created
            return CreatedAtAction("Get", new { id = employee.Id }, employee);
        }

        // GET api/<EmployeesAPIController>/5
        [HttpGet("dog")]
        public string Get(int id)
        {
            return "dog";
        }

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
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
