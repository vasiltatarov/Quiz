﻿namespace Quiz.Services.Data
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

    public class TestService : ITestService
    {
        private readonly IDeletableEntityRepository<Test> tests;
        private readonly IDeletableEntityRepository<UserAnswer> userAnswers;
        private readonly IDeletableEntityRepository<Question> questions;

        public TestService(
            IDeletableEntityRepository<Test> tests,
            IDeletableEntityRepository<UserAnswer> userAnswers,
            IDeletableEntityRepository<Question> questions)
        {
            this.tests = tests;
            this.userAnswers = userAnswers;
            this.questions = questions;
        }

        public async Task<int> Add(string title)
        {
            var test = new Test { Title = title };
            await this.tests.AddAsync(test);
            await this.tests.SaveChangesAsync();

            return test.Id;
        }

        public QuizViewModel GetQuizById(int testId)
        {
            var test = this.tests.All()
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefault(x => x.Id == testId);

            var testViewModel = new QuizViewModel
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