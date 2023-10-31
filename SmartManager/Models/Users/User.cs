//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================
using System;

namespace SmartManager.Models.Users
{
    public class User
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
