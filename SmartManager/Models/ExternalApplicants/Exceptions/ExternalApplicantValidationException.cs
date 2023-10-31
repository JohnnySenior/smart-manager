//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace SmartManager.Models.ExternalApplicants.Exceptions
{
    public class ExternalApplicantValidationException : Xeption
    {
        public ExternalApplicantValidationException(Xeption innerException)
            : base(message: "External applicant validation error occurred, fix the error and try again",
                  innerException)
        { }
    }
}
