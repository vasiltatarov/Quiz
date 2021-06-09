namespace Quiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Quiz.Data.Common.Models;

    public class UserAnswer : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int? AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}
