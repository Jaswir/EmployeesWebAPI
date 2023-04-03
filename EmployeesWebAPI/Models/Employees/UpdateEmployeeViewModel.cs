namespace EmployeesWebAPI.Models.Employees
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
