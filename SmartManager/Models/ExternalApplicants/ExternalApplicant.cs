//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;

namespace SmartManager.Models.ExternalApplicants
{
    public class ExternalApplicant
    {
        public Guid ExternalApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string GroupName { get; set; }
    }
}
