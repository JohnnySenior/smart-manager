//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace SmartManager.Models.Applicants.Exceptions
{
    public class NullApplicantException : Xeption
    {
        public NullApplicantException()
           : base(message: "Applicant is null.")
        { }
    }
}
