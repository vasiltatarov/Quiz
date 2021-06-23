namespace Quiz.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Quiz.Data.Common.Repositories;
    using Quiz.Data.Models;
    using Quiz.Services.Data.Models;
    using Quiz.Services.Mapping;
    using Quiz.Web.ViewModels.Answers;
    using Quiz.Web.ViewModels.Questions;
    using Quiz.Web.ViewModels.Tests;

    public class TestService : ITestService
    {
        private readonly IDeletableEntityRepository<Test> tests;
        private readonly IDeletableEntityRepository<UserAnswer> userAnswers;
        private readonly IDeletableEntityRepository<Question> questions;
        private readonly IRepository<UserTest> userTestsRepository;
        private readonly IQuestionService questionService;
        private readonly IUserAnswerService userAnswerService;

        public TestService(
            IDeletableEntityRepository<Test> tests,
            IDeletableEntityRepository<UserAnswer> userAnswers,
            IDeletableEntityRepository<Question> questions,
            IRepository<UserTest> userTestsRepository,
            IQuestionService questionService,
            IUserAnswerService userAnswerService)
        {
            this.tests = tests;
            this.userAnswers = userAnswers;
            this.questions = questions;
            this.userTestsRepository = userTestsRepository;
            this.questionService = questionService;
            this.userAnswerService = userAnswerService;
        }

        public async Task<int> Add(string title, string creatorId)
        {
            if (await this.tests.All()
                .AnyAsync(x => x.Title == title))
            {
                return -1;
            }

            var test = new Test
            {
                Title = title,
                CreatorId = creatorId,
            };
            await this.tests.AddAsync(test);
            await this.tests.SaveChangesAsync();

            return test.Id;
        }

        public TestViewModel GetTestById(int testId)
        {
            var test = this.tests.All()
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefault(x => x.Id == testId);

            var testViewModel = new TestViewModel
            {
                Id = testId,
                Title = test.Title,
                Questions = test.Questions
                    .OrderBy(r => Guid.NewGuid())
                    .Select(x => new QuestionViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Answers = x.Answers
                            .OrderBy(r => Guid.NewGuid())
                            .Select(a => new AnswerViewModel
                            {
                                Id = a.Id,
                                Title = a.Title,
                            })
                            .ToList(),
                    })
                    .ToList(),
            };

            return testViewModel;
        }

        public TestViewModel GetTestResult(string userId, int testId)
        {
            var test = this.userTestsRepository.AllAsNoTracking()
                .Include(x => x.Test.Questions)
                .ThenInclude(x => x.UserAnswers)
                .ThenInclude(x => x.Answer)
                .FirstOrDefault(x => x.TestId == testId && x.UserId == userId);

            var testViewModel = new TestViewModel
            {
                Id = testId,
                Title = test.Test.Title,
                Questions = test.Test.Questions
                    .OrderBy(q => Guid.NewGuid())
                    .Select(q => new QuestionViewModel
                    {
                        Id = q.Id,
                        Title = q.Title,
                        Answers = q.UserAnswers
                            .Where(ua => ua.UserId == userId)
                            .OrderBy(ua => Guid.NewGuid())
                            .Select(ua => new AnswerViewModel
                            {
                                Id = ua.AnswerId.Value,
                                Title = ua.Answer.Title,
                                IsCorrect = ua.Answer.IsCorrect,
                            })
                            .ToList(),
                    })
                    .ToList(),
            };

            return testViewModel;
        }

        public async Task<T> GetTestById<T>(int id)
            => await this.tests.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public IEnumerable<UserTestViewModel> GetTestsByUserId(string userId)
        {
            var tests = this.tests.All()
                .Select(x => new UserTestViewModel
                {
                    TestId = x.Id,
                    Title = x.Title,
                    Participants = x.UserTests.Count(ut => ut.TestId == x.Id),
                    Questions = this.questionService.GetQuestionCountByTestId(x.Id).Result,
                    Result = this.userAnswerService.GetUserPoints(userId, x.Id).Result,
                })
                .ToList();

            foreach (var test in tests)
            {
                var questionsCount = this.userAnswers.All()
                    .Count(x => x.UserId == userId && x.Question.TestId == test.TestId);

                if (questionsCount == 0)
                {
                    test.Status = TestStatus.NotStarted;
                    continue;
                }

                var answeredQuestions = this.userAnswers.All()
                    .Count(x => x.UserId == userId && x.Question.TestId == test.TestId && x.AnswerId.HasValue);

                _ = answeredQuestions == questionsCount
                    ? test.Status = TestStatus.Finished
                    : test.Status = TestStatus.InProgress;
            }

            return tests;
        }

        public IEnumerable<T> GetTestsByUserId<T>(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task StartTest(string userId, int testId)
        {
            if (await this.userAnswers.AllAsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.Question.TestId == testId))
            {
                return;
            }

            var questions = this.questions.All()
                .Where(x => x.TestId == testId)
                .Select(x => new
                {
                    x.Id,
                })
                .ToList();

            foreach (var question in questions)
            {
                var userAnswer = new UserAnswer
                {
                    AnswerId = null,
                    UserId = userId,
                    QuestionId = question.Id,
                };
                await this.userAnswers.AddAsync(userAnswer);
            }

            await this.userAnswers.SaveChangesAsync();
        }
    }
}
