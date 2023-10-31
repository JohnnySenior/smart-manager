//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace SmartManager.Models.Spreadsheets.Exceptions
{
    public class ExternalApplicantsProcessingValidationException : Xeption
    {
        public ExternalApplicantsProcessingValidationException(Xeption innerException)
            : base(message: "External applicant validation error occurred, fix the error and try again",
                  innerException)
        { }
    }
}
