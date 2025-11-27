using codebase_Assignment.Models;

namespace codebase_Assignment.Interface
{
    public interface IUserpin
    {
        Task<(bool success, string message)> SetPinAsync(UserPin up, int id);
        Task<(bool success, string message, int? id , UserPin? data)> getPinAsync(string pin);
    }
}
