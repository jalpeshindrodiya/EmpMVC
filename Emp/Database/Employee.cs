namespace Emp.Database
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<double> Salary { get; set; }
        public string ImageName { get; set; }
        public Nullable<int> DepID { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
