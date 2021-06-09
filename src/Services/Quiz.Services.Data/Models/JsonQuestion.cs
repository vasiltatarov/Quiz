namespace Quiz.Services.Data.Models
{
    using System.Collections.Generic;

    public class JsonQuestion
    {
        public string Question { get; set; }

        public IEnumerable<JsonAnswer> Answers { get; set; }
    }
}
