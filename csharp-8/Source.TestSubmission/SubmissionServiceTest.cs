using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;

namespace Codenation.Challenge
{
    public class SubmissionServiceTest
    {

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Be_Higher_Score_When_Get_By_Challenge_Id(int challengeId)
        {
            var fakeContext = new FakeContext("HigherSubmissionByChallenge");
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                 var expected = fakeContext.GetFakeData<Submission>().
                    Where(challenge => challenge.ChallengeId == challengeId).
                    Max(challenge => challenge.Score);                    

                var service = new SubmissionService(context);
                var actual = service.FindHigherScoreByChallengeId(challengeId);

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(3,3)]
        public void Should_Be_Right_Submissions_When_Find_By_Challenge_And_Acceleration(int challengeId, int accelerationId)
        {
            var fakeContext = new FakeContext("SubmissionByChallengeAndAcceleration");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                 var expected = fakeContext.GetFakeData<Candidate>().
                    Where(candidate => candidate.AccelerationId == accelerationId).
                    Join(fakeContext.GetFakeData<User>(),
                        candidate => candidate.UserId,
                        user => user.Id,
                        (candidate, user) => user).
                    Join(fakeContext.GetFakeData<Submission>(),
                        user => user.Id,
                        submission => submission.UserId,
                        (user, submission) => submission).
                    Where(submission => submission.ChallengeId == challengeId).
                    Distinct().
                    ToList();

                var service = new SubmissionService(context);
                var actual = service.FindByChallengeIdAndAccelerationId(accelerationId, challengeId);

                Assert.Equal(expected, actual, new SubmissionIdComparer());
            }
        }
    
        [Fact]
        public void Should_Add_New_Submission_When_Save()
        {
            var fakeContext = new FakeContext("SaveNewSubmission");
            // precisa ser uma tupla que não está no arquivo submissions.json
            var expected = new Submission() {
                UserId = 2, 
                ChallengeId = 6,
                Score = 90
            };

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new SubmissionService(context);
                var actual = service.Save(expected);

                Assert.Equal(expected, actual, new SubmissionIdComparer());
            }
        }

        [Fact]
        public void Should_Update_The_Sumission_When_Save()
        {
            var fakeContext = new FakeContext("SaveSubmission");
            fakeContext.FillWith<Submission>();

            var expected = fakeContext.GetFakeData<Submission>().First();
            expected.Score = 99.9M;
            
            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new SubmissionService(context);
                var actual = service.Save(expected);

                Assert.Equal(expected, actual, new SubmissionIdComparer());
                Assert.Equal(expected.Score, actual.Score);                
            }
        }

    }
}
