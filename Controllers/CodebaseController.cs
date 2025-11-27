using codebase_Assignment.Interface;
using codebase_Assignment.Models;
using codebase_Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace codebase_Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodebaseController : Controller
    {
        private IotpInterface _otp;
        private IUserInterface _user;
        private IAddress _address;
        private IUserpin _userpin;
        public CodebaseController(IotpInterface otp, IUserInterface user, IAddress address,IUserpin userpin)
        { 
            _otp = otp;
            _user = user;
            _address = address;
            _userpin = userpin;
        }
        [HttpPost("sendotp")]
        public async Task<IActionResult> sendotp([FromBody] OTP o)
        {
            try
            {
                var res = await _otp.SetOtpAsync(o);
                if (!res.success)
                {
                    return BadRequest(new {error=res.message});
                }


                SetCookieCode("code", res.otp.ToString());
                return Ok(new {message=res.message , otp=res.otp , mobile_number=o.mobile_number});
            }
            catch (Exception ex) {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("adduser")]
        public async Task<IActionResult> userregistered([FromBody] User u)
        {
            try
            {
                var code = HttpContext.Request.Cookies["code"];
                var code_int = Convert.ToInt32(code);
                var res = await _user.SetUserAsync(u,code_int);
                if (!res.success)
                {
                    return BadRequest(new { error = res.message });
                }
                SetCookieUser("user_address",res.id.ToString());
                return Ok(new { message = res.message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("address")]
        public async Task<IActionResult> insertaddress([FromBody] Address a)
        {
            try
            {
                var id = HttpContext.Request.Cookies["user_address"];
                var user_id = Convert.ToInt32(id);
                var res = await _address.SetAddressAsync(a, user_id);
                if (!res.success)
                {
                    return BadRequest(new { error = res.message });
                }
                return Ok(new { message = res.message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("setpin")]
        public async Task<IActionResult> pin([FromBody] UserPin up)
        {
            try
            {
                var id = HttpContext.Request.Cookies["user_address"];
                var user_id = Convert.ToInt32(id);
                var res = await _userpin.SetPinAsync(up, user_id);
                if (!res.success)
                {
                    return BadRequest(new { error = res.message });
                }
                return Ok(new { message = res.message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("verifypin")]
        public async Task<IActionResult> verifypin([FromBody] string pin)
        {
            try
            {
                var res = await _userpin.getPinAsync(pin);
                if (!res.success)
                    return BadRequest(new { error = res.message });

                var json = System.Text.Json.JsonSerializer.Serialize(res.data, new System.Text.Json.JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                });

                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPost("setcookie")]
        public IActionResult SetCookieCode(string key, string value)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(5),
                HttpOnly = true,
                Secure = true
            };

            HttpContext.Response.Cookies.Append(key, value, options);

            return Ok(new { message = "Cookie set successfully" });
        }
        [HttpPost("setcookieuser")]
        public IActionResult SetCookieUser(string key, string value)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(15),
                HttpOnly = true,
                Secure = true
            };

            HttpContext.Response.Cookies.Append(key, value, options);

            return Ok(new { message = "Cookie set successfully" });
        }
    }
}
