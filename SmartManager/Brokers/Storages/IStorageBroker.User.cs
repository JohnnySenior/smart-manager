//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Applicants;
using System.Linq;
using System.Threading.Tasks;
using System;
using SmartManager.Models.Users;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<User> InsertUserAsync(User user);
        IQueryable<User> SelectAllUsers();
        ValueTask<User> SelectUserByIdAsync(Guid userId);
        ValueTask<User> SelectUserByEmailAndPasswordAsync(string email, string password);
        ValueTask<User> UpdateAppolicantAsync(User user);
        ValueTask<User> DeleteUserAsync(User user);
    }
}
