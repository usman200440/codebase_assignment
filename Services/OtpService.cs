using codebase_Assignment.Interface;
using codebase_Assignment.Models;

namespace codebase_Assignment.Services
{
    public class OtpService:IotpInterface
    {
        private CodebaseContext _context;
        public OtpService(CodebaseContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string message, int? otp)> SetOtpAsync(OTP o)
        {
            Random random = new Random();
            int code = random.Next(1000, 10000);
            o.otp = code;
            var query= _context.OTP.Where(x=>x.mobile_number==o.mobile_number).FirstOrDefault();
            if (query != null && query.is_used == "yes")
            {
                return (false, "mobile number already inserted", null);
            }

            if (query != null && query.is_used == "no")
            {
                query.otp = code;
                await _context.SaveChangesAsync();
                return (true, "code resend successfuly", code);
            }

            if (o.mobile_number == null)
            {
                return (false, "null data found in otp", null);
            }
            if (!o.mobile_number.All(char.IsDigit))
            {
                return (false, "Mobile number must contain only digits", null);
            }
            if (!o.mobile_number.StartsWith("0"))
            {
                return (false, "Mobile number must start with 0", null);
            }
            if (o.mobile_number.Length != 11)
            {
                return (false, "Mobile number must be 11 digits", null);
            }

          
            await _context.OTP.AddAsync(o);
            await _context.SaveChangesAsync();
            return (true, "otp sent successfully", code);
        }
    }
}
