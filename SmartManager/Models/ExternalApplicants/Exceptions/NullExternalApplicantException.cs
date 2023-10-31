//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace SmartManager.Models.ExternalApplicants.Exceptions
{
    public class NullExternalApplicantException : Xeption
    {
        public NullExternalApplicantException()
            : base(message: "External applicant is null")
        { }
    }
}
