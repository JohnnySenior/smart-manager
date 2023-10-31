//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Applicants.Exceptions
{
    public class AlreadyExistsApplicantException : Xeption
    {
        public AlreadyExistsApplicantException(Exception innerException)
         : base(message: "Appliacant already exists.", innerException)
        { }
    }
}
