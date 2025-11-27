using Microsoft.EntityFrameworkCore;

namespace codebase_Assignment.Models
{
    public class CodebaseContext:DbContext
    {
        public CodebaseContext(DbContextOptions<CodebaseContext> options):base(options) { 
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPin> UsersPin { get; set; }
        public DbSet<OTP> OTP { get; set; }
        public DbSet<Address> Address { get; set; }
   
    }
}
