using System.ComponentModel.DataAnnotations;

namespace WebApi.TestProject.Models
{
    public class Person
    {
        [Range(1,100)]
        [Required]
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 10)] // максимум + минимум
        public string FirstName { get; set; }
        [StringLength(30)] // максимум
        public string LastName { get; set; }
        [Range(1, 10)]
        public int Age { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email {get;set;}
        public Person()
        {

        }
    }
}
