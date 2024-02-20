using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TravelApp_API.Context;
using TravelApp_API.Models; 

namespace TravelApp_API.Repository
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<UserModel> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<UserModel>(new UserStore<UserModel>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel user)
        {
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            return result;
        }

        public async Task<UserModel> FindUser(string UserName)
        {
            UserModel user = await _userManager.FindByNameAsync(UserName);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}