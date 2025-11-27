using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codebase_Assignment.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int otp_num { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public DateOnly Dob { get; set; }

        [ForeignKey("otp")]
        public int otp_id { get; set; }

        public OTP? otp { get; set; }
        public Address? address { get; set; }
        public UserPin? userpin { get; set; }
    }
}
