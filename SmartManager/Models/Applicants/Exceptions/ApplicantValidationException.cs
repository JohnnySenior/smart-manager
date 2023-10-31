//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace SmartManager.Models.Applicants.Exceptions
{
    public class ApplicantValidationException : Xeption
    {
        public ApplicantValidationException(Xeption innerException)
            : base(message: "Applicant validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
