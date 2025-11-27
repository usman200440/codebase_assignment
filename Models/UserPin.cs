using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codebase_Assignment.Models
{
    public class UserPin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string pin { get; set; }

        [ForeignKey("user")]
        public int user_id { get; set; }

        public User? user { get; set; }
    }
}
