using codebase_Assignment.Interface;
using codebase_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace codebase_Assignment.Services
{
    public class UserPinService : IUserpin
    {
        private CodebaseContext _context;
        public UserPinService(CodebaseContext context)
        {
            _context = context;
        }
        async public Task<(bool success, string message)> SetPinAsync(UserPin up, int id)
        {
            var query = _context.UsersPin.Where(x => x.user_id == id).FirstOrDefault();
            if (query != null)
            {
                return (false, "data already inserted");
            }
            if (up == null)
            {
                return (false, "null data inserted");
            }
            if (id == 0)
            {
                return (false, "userdata not found");
            }
            if(up.pin.Length != 4)
            {
                return (false, "pin must be 4 digits");
            }
            string hashed_pin = BCrypt.Net.BCrypt.HashPassword(up.pin);
           await  _context.UsersPin.AddAsync(up);
            up.user_id = id;
            up.pin = hashed_pin;
            await _context.SaveChangesAsync();
            return (true, "user pin created");
        }

        async public Task<(bool success, string message, int? id , UserPin? data)> getPinAsync(string pin)
        {
            var hashedpins =await _context.UsersPin.Include(x => x.user).ThenInclude(u => u.address).Include(up => up.user).ThenInclude(o => o.otp).ToListAsync();
           
            if (pin == null)
            {
                return (false, "pin not found",null,null);
            }
            foreach (var pins in hashedpins) 
            {
                if (BCrypt.Net.BCrypt.Verify(pin, pins.pin))
                {
                    return (true, "user pin created",pins.id,pins);
                }
            }
            return (false, "Invalid pin",null,null);
        }
    }
}
