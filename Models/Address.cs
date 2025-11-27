using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codebase_Assignment.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string house_no { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string area { get; set; }


        [ForeignKey("user")]
        public int user_id { get; set; }

        public User? user { get; set; }
    }

}
