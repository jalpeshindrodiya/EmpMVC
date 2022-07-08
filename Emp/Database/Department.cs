﻿namespace Emp.Database
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
