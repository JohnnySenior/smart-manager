//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }

        public async ValueTask<User> InsertUserAsync(User user) =>
            await InsertAsync(user);

        public IQueryable<User> SelectAllUsers() =>
            SelectAll<User>().AsQueryable();

        public async ValueTask<User> SelectUserByIdAsync(Guid userId) =>
            await SelectAsync<User>(userId);

        public async ValueTask<User> SelectUserByEmailAndPasswordAsync(string email, string password) =>
            await this.Users.FirstOrDefaultAsync(user => user.Email ==  email && user.Password == password);

        public async ValueTask<User> UpdateAppolicantAsync(User user) =>
            await UpdateAsync(user);

        public async ValueTask<User> DeleteUserAsync(User user) =>
            await DeleteAsync(user);
    }
}
