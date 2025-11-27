using codebase_Assignment.Interface;
using codebase_Assignment.Models;

namespace codebase_Assignment.Services
{
    public class UserService : IUserInterface
    {
        private CodebaseContext _context;
        public UserService(CodebaseContext context) 
        {
            _context=context;
        }
        async public Task<(bool success, string message,int? id)> SetUserAsync(User u,int code)
        {
            var query = _context.Users.Where(x => x.otp_num == code || x.email==u.email).FirstOrDefault();
            var otp_data = _context.OTP.Where(x=>x.otp==code).FirstOrDefault();
            if (query != null)
            { 
                return (false,"data already inserted",null);
            }

            if (u.otp_num != code)
            {
                return (false, "invalid otp",null);
            }

            await _context.Users.AddAsync(u);
            otp_data.is_used = "yes";
            u.otp_id = otp_data.id;
            await _context.SaveChangesAsync();

            return (true, "userdata registered successfully",u.id);
        }
    }
}
