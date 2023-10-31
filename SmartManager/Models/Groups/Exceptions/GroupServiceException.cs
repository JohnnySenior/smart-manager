//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class GroupServiceException : Xeption
    {
        public GroupServiceException(Xeption innerException)
            : base(message: "Group dependency validation error occurred, fix the error and try again.",
                  innerException)
        { }
    }
}
