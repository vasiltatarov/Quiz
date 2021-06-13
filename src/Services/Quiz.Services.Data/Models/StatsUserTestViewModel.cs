namespace Quiz.Services.Data.Models
{
    using System;

    public class StatsUserTestViewModel
    {
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Result { get; set; }

        public int Questions { get; set; }
    }
}
