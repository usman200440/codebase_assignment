using codebase_Assignment.Models;

namespace codebase_Assignment.Interface
{
    public interface IUserInterface
    {
        Task<(bool success, string message,int? id)> SetUserAsync(User u,int code);
    }
}
