//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using FluentAssertions;
using Moq;
using SmartManager.Models.Applicants.Exceptions;
using System;
using System.Data.SqlClient;
using Xunit;

namespace SmartManagerTestsUnit.Services.Foundations
{
    public partial class ApplicantServiceTests
    {

        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccureAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlError();

            var failedStorageApplicantException =
                new FailedApplicantStorageException(sqlException);

            var expectedApplicantDependencyException =
                new ApplicantDependencyException(failedStorageApplicantException);

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAllApplicants()).Throws(sqlException);

            //when
            Action retrieveAllApplicantsAction = () =>
                this.applicantService.RetrieveAllApplicants();

            ApplicantDependencyException ApplicantDependencyException =
                Assert.Throws<ApplicantDependencyException>(retrieveAllApplicantsAction);

            //then
            ApplicantDependencyException.Should().BeEquivalentTo(
                expectedApplicantDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllApplicants(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedApplicantDependencyException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursLogIt()
        {
            //given
            string someMessage = GetRandomString();
            var serviceException = new Exception(someMessage);

            var failedApplicantServiceException =
                new FailedApplicantServiceException(serviceException);

            var expectedApplicantServiceException =
                new ApplicantServiceException(failedApplicantServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllApplicants()).Throws(serviceException);

            //when
            Action retrieveAllApplicantsAction = () =>
            this.applicantService.RetrieveAllApplicants();

            ApplicantServiceException ApplicantServiceException =
                Assert.Throws<ApplicantServiceException>(retrieveAllApplicantsAction);

            //then
            ApplicantServiceException.Should().BeEquivalentTo(expectedApplicantServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllApplicants(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedApplicantServiceException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
