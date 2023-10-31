//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SmartManager.Models.Applicants;
using System.Threading.Tasks;
using Xunit;

namespace SmartManagerTestsUnit.Services.Foundations
{
    public partial class ApplicantServiceTests
    {

        [Fact]
        public async Task ShouldAddApplicantAsync()
        {
            // given
            Applicant randomApplicant = CreateRandomApplicant();
            Applicant inputApplicant = randomApplicant;
            Applicant persistedApplicant = inputApplicant;
            Applicant expectedApplicant = persistedApplicant.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertApplicantAsync(inputApplicant))
                .ReturnsAsync(persistedApplicant);

            // when
            Applicant actualApplicant = await this.applicantService
                .AddApplicantAsync(inputApplicant);

            // then
            actualApplicant.Should().BeEquivalentTo(expectedApplicant);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertApplicantAsync(inputApplicant), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
