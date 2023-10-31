//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Applicants.Exceptions
{
    public class NotFoundApplicantException : Xeption
    {
        public NotFoundApplicantException(Guid applicantId)
            : base(message: $"Applicant is not found with id: {applicantId}.")
        { }
    }
}
