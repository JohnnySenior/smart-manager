//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Applicants;
using System;
using System.Collections.Generic;

namespace SmartManager.Models.Groups
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public List<Applicant> Applicants { get; set; }
    }
}
