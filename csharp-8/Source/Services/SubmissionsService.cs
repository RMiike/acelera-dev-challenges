using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly CodenationContext _context;

        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
            => HandleSubmissionSelect(
                s => s.ChallengeId == challengeId &&
                s.User.Candidates
                .Any(x => x.AccelerationId == accelerationId))
                .ToList();

        public decimal FindHigherScoreByChallengeId(int challengeId)
            => HandleSubmissionSelect(s => s.ChallengeId == challengeId)
                  .Max(s => s.Score);

        public Submission Save(Submission submission)
        {
            if (!FindById(submission.UserId, submission.ChallengeId))
                _context.Add(submission);
            else
                _context.Entry(submission).State = EntityState.Modified;

            if (_context.SaveChanges() >= 0)
                return submission;

            throw new ArgumentException("Submission cannot be saved.");
        }
        private bool FindById(int userId, int challengeId)
            => _context
                .Submissions
                .Where(s => s.UserId == userId && s.ChallengeId == challengeId)
                .Any();

        private IQueryable<Submission> HandleSubmissionSelect(Expression<Func<Submission, bool>> condition)
           => _context
                .Submissions
                .Where(condition)
                .AsNoTracking();
    }
}
