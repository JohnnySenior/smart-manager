//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Orchestrations.Exceptions;
using SmartManager.Models.Spreadsheets.Exceptions;
using System.Threading.Tasks;
using Xeptions;

namespace SmartManager.Services.Orchestrations
{
    public partial class OrchestrationService
    {
        private delegate Task ReturningTaskFunction();

        private async Task TryCatch(ReturningTaskFunction returningTaskFunction)
        {
            try
            {
                await returningTaskFunction();
            }
            catch (ExternalApplicantsProcessingValidationException externalApplicantsProcessingValidationException)
            {
                throw CreateAndLogValidationException(
                    externalApplicantsProcessingValidationException.InnerException as Xeption);
            }
        }

        private ExternalApplicantsOrchestrationValidationException CreateAndLogValidationException(Xeption exception)
        {
            var externalApplicantsOrchestrationValidationException =
                new ExternalApplicantsOrchestrationValidationException(exception);

            this.loggingBroker.LogError(externalApplicantsOrchestrationValidationException);

            return externalApplicantsOrchestrationValidationException;
        }
    }
}
