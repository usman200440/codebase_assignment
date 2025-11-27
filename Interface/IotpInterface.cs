using codebase_Assignment.Models;

namespace codebase_Assignment.Interface
{
    public interface IotpInterface
    {
        Task<(bool success, string message, int? otp)> SetOtpAsync(OTP o);
    }
}
