using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codebase_Assignment.Models
{
    public class OTP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int otp { get; set; }
        public string mobile_number { get; set; }
        public string is_used { get; set; } = "no";
        public DateTime is_created { get; set; } = DateTime.Now;
        public User? user { get; set; }

    }
}
