//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================
using Microsoft.EntityFrameworkCore;
using SmartManager.Models.Applicants;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Applicant> Applicants { get; set; }
    }
}
