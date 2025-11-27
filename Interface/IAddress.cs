using codebase_Assignment.Models;

namespace codebase_Assignment.Interface
{
    public interface IAddress
    {
        Task<(bool success, string message)> SetAddressAsync(Address d, int id);
    }
}
