//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.Applicants.Exceptions
{
    public class ApplicantProcessingValidationException : Xeption
    {
        public ApplicantProcessingValidationException(Xeption innerException)
            : base(message: "Applicant validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
