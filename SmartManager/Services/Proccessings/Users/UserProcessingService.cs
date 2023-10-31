//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Users;
using SmartManager.Services.Foundations.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Proccessings.Users
{
    public class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;

        public UserProcessingService(IUserService userService)
        {
            this.userService = userService;
        }

        public async ValueTask<User> AddUserAsync(User user) =>
           await this.userService.AddUserAsync(user);

        public async ValueTask<User> RetrieveUserByIdAsync(Guid userid) =>
            await this.userService.RetrieveUserByIdAsync(userid);

        public async ValueTask<User> RetrieveUserByEmailAndPasswordAsync(string email, string password) =>
            await this.userService.RetrieveUserByEmailAndPasswordAsync(email, password);

        public IQueryable RetrieveAllUsers() =>
            this.userService.RetrieveAllUsers();

        public async ValueTask<User> ModifyUserAsync(User user) =>
            await this.userService.ModifyUserAsync(user);

        public async ValueTask<User> RemoveUserAsync(Guid userid) =>
            await this.userService.RemoveUserAsync(userid);
    }
}

