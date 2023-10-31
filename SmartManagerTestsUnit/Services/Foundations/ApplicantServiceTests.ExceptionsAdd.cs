//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using EFxceptions.Models.Exceptions;
using FluentAssertions;
using Moq;
using SmartManager.Models.Applicants;
using SmartManager.Models.Applicants.Exceptions;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;
namespace SmartManagerTestsUnit.Services.Foundations
{
    public partial class ApplicantServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursLogItAsync()
        {
            //given
            Applicant someApplicant = CreateRandomApplicant();

            SqlException sqlException = GetSqlError();
            var failedApplicantStorageException =
                new FailedApplicantStorageException(sqlException);

            var expectedApplicantDependencyException =
                new ApplicantDependencyException(failedApplicantStorageException);

            this.storageBrokerMock.Setup(broker => broker.InsertApplicantAsync(someApplicant))
                .ThrowsAsync(sqlException);

            //when
            ValueTask<Applicant> addApplicantTask =
                this.applicantService.AddApplicantAsync(someApplicant);

            //then
            await Assert.ThrowsAsync<ApplicantDependencyException>(() =>
                addApplicantTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertApplicantAsync(someApplicant), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedApplicantDependencyException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            // given
            string someMessage = GetRandomString();
            Applicant someApplicant = CreateRandomApplicant();
            var duplicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsApplicantException =
                new AlreadyExistsApplicantException(duplicateKeyException);

            var expectedApplicantDependencyValidationException =
                new ApplicantDependencyValidationException(alreadyExistsApplicantException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertApplicantAsync(someApplicant)).ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<Applicant> addApplicantTask =
                this.applicantService.AddApplicantAsync(someApplicant);

            var actualApplicantDependencyValidationException =
                await Assert.ThrowsAsync<ApplicantDependencyValidationException>(addApplicantTask.AsTask);

            // then
            actualApplicantDependencyValidationException.Should()
                .BeEquivalentTo(expectedApplicantDependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertApplicantAsync(someApplicant), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedApplicantDependencyValidationException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            Applicant someApplicant = CreateRandomApplicant();

            var serviceException = new Exception();

            var failedApplicantServiceException =
                new FailedApplicantServiceException(serviceException);

            var excpectedApplicantServiceException =
                new ApplicantServiceException(failedApplicantServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertApplicantAsync(someApplicant)).ThrowsAsync(serviceException);

            //when
            ValueTask<Applicant> addApplicantTask =
                this.applicantService.AddApplicantAsync(someApplicant);

            //then
            await Assert.ThrowsAsync<ApplicantServiceException>(() =>
                addApplicantTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertApplicantAsync(someApplicant), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    excpectedApplicantServiceException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
