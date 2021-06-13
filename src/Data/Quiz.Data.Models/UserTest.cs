namespace Quiz.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserTest
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
