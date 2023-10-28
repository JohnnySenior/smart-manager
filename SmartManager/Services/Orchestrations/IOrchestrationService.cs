//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.IO;
using System.Threading.Tasks;

namespace SmartManager.Services.Orchestrations
{
    public interface IOrchestrationService
    {
        Task ProcessImportRequest(MemoryStream stream);
    }
}
