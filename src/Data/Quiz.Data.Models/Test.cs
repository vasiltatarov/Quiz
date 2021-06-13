namespace Quiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Quiz.Data.Common.Models;

    public class Test : BaseDeletableModel<int>
    {
        public Test()
        {
            this.Questions = new HashSet<Question>();
            this.UserTests = new HashSet<UserTest>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<UserTest> UserTests { get; set; }
    }
}
