//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class NotFoundGroupException : Xeption
    {

        public NotFoundGroupException(Guid accountId)
            : base(message: $"Group is not found with id: {accountId}.")
        { }
    }
}
