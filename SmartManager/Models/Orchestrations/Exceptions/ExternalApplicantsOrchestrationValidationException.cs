//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace SmartManager.Models.Orchestrations.Exceptions
{
    public class ExternalApplicantsOrchestrationValidationException : Xeption
    {
        public ExternalApplicantsOrchestrationValidationException(Xeption innerException)
            : base(message: "External applicant validation error occurred, fix the error and try again",
                  innerException)
        { }
    }
}
