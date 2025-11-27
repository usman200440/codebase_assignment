using codebase_Assignment.Interface;
using codebase_Assignment.Models;

namespace codebase_Assignment.Services
{
    public class AddressService : IAddress
    {
        private CodebaseContext _context;
        public AddressService(CodebaseContext context)
        {
            _context = context;
        }
        async public Task<(bool success, string message)> SetAddressAsync(Address d, int id)
        {
            var query = _context.Address.Where(x=>x.user_id == id).FirstOrDefault();
            if (query != null)
            {
                return (false, "data already inserted");
            }
            if (d == null) {
                return (false, "null data inserted");
            }
            if(id == 0)
            {
                return (false, "userdata not found");
            }
            await _context.Address.AddAsync(d);
            d.user_id = id;
            await _context.SaveChangesAsync();
            return (true, "address registered");
        }
    }
}
