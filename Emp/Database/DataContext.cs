using Microsoft.EntityFrameworkCore;
using Emp.Database;

namespace Emp.Database
{
    public class DataContext : DbContext
    {
        public DbSet<EmployeeDTO> Employees { get; set; }
        //public DbSet<TaskDTO> Tasks { get; set; }

        public DbSet<Department> Department { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-HLBCC6R\SQLEXPRESS; Database=Task; Integrated Security=True;");
        }
      

        

        public DbSet<Emp.Database.TaskDTO>? TaskDTO { get; set; }
    }
}
