//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using FluentAssertions;
using Moq;
using SmartManager.Models.Applicants;
using System.Linq;
using Xunit;

namespace SmartManagerTestsUnit.Services.Foundations
{
    public partial class ApplicantServiceTests
    {

        [Fact]
        public void ShouldRetrieveAllApplicants()
        {
            //given
            IQueryable<Applicant> randomApplicants = GetRandomApplicants();
            IQueryable<Applicant> persistedApplicant = randomApplicants;
            IQueryable<Applicant> expectedApplicants = persistedApplicant;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllApplicants()).Returns(expectedApplicants);

            //when
            IQueryable<Applicant> actualApplicants =
                this.applicantService.RetrieveAllApplicants();

            //then
            actualApplicants.Should().BeEquivalentTo(expectedApplicants);

            this.storageBrokerMock.Verify(broker =>
            broker.SelectAllApplicants(), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
