namespace Quiz.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    using Quiz.Services.Data.Models;

    public class JsonImportService : IJsonImportService
    {
        private readonly ITestService testService;
        private readonly IQuestionService questionService;
        private readonly IAnswerService answerService;

        public JsonImportService(
            ITestService testService,
            IQuestionService questionService,
            IAnswerService answerService)
        {
            this.testService = testService;
            this.questionService = questionService;
            this.answerService = answerService;
        }

        public async Task Import(string fileName, string quizName)
        {
            var json = File.ReadAllText(fileName);
            var questions = JsonConvert.DeserializeObject<IEnumerable<JsonQuestion>>(json);

            var testId = await this.testService.Add(quizName);

            if (testId == -1)
            {
                throw new InvalidOperationException("This Exam already exist!");
            }

            foreach (var question in questions)
            {
                var questionId = await this.questionService.Add(question.Question, 1);

                foreach (var answer in question.Answers)
                {
                    await this.answerService.Add(answer.Answer, answer.Correct, answer.Correct ? 1 : 0, questionId);
                }
            }
        }
    }
}
