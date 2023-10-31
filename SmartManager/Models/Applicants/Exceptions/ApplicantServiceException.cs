//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace SmartManager.Models.Applicants.Exceptions
{
    public class ApplicantServiceException : Xeption
    {
        public ApplicantServiceException(Xeption innerException)
            : base(message: "Appplicant service error occurred, contact support",
                  innerException)
        { }
    }
}
