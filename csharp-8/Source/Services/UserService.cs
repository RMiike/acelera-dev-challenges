using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private readonly CodenationContext _context;

        public UserService(CodenationContext context)
        {
            _context = context;
        }

        public IList<User> FindByAccelerationName(string name)
            => HandleCandidateSelect(c => c.Acceleration.Name == name)
               .ToList();

        public IList<User> FindByCompanyId(int companyId)
            => HandleCandidateSelect(c => c.CompanyId == companyId)
                .Distinct()
                .ToList();

        public User FindById(int id)
             => _context
                .Users
                .Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

        public User Save(User user)
        {
            if (user.Id == 0)
                _context.Add(user);
            else
                _context.Entry(user).State = EntityState.Modified;

            if (_context.SaveChanges() >= 0)
                return user;

            throw new ArgumentException("User cannot be saved.");
        }

        private IQueryable<User> HandleCandidateSelect(Expression<Func<Candidate, bool>> condition)
              => _context
                      .Candidates
                      .Where(condition)
                      .Select(u => u.User)
                      .AsNoTracking();
    }
}
