namespace Quiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Quiz.Data.Common.Models;

    public class Answer : BaseDeletableModel<int>
    {
        public Answer()
        {
            this.UserAnswers = new HashSet<UserAnswer>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
