using System.ComponentModel.DataAnnotations;

namespace UserDetails_CRUD.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Age { get; set; }
        public bool IsActive { get; set; }
    }
}
