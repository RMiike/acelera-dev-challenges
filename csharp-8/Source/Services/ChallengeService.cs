using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly CodenationContext _context;
        public ChallengeService(CodenationContext context)
        {
            _context = context;

        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
            => _context
                .Candidates
                .Where(x => x.AccelerationId == accelerationId && x.UserId == userId)
                .Select(x => x.Acceleration.Challenge)
                .AsNoTracking()
                .Distinct()
                .ToList();

        public Models.Challenge Save(Models.Challenge challenge)
        {
            if (challenge.Id == 0)
                _context.Add(challenge);
            else
                _context.Entry(challenge).State = EntityState.Modified;

            if (_context.SaveChanges() >= 0)
                return challenge;

            throw new ArgumentException("Challenge cannot be saved!");
        }
    }
}