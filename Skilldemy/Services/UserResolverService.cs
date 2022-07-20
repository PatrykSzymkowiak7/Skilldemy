using Skilldemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skilldemy.Services
{
    public class UserResolverService
    {
        private ApplicationDbContext _context;

        public UserResolverService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUserById(string id)
        {
            var user = _context.Users.FirstOrDefault(u => String.Equals(u.Id, id));

            return user;
        }

        public ApplicationUser GetCurrentUser()
        {
            HttpContext currentContext = HttpContext.Current;
            var userName = currentContext.User.Identity.Name;

            var user = _context.Users.FirstOrDefault(u => String.Equals(u.UserName, userName));

            return user;
        }
    }
}