//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Moq;
using SmartManager.Brokers.DateTimes;
using SmartManager.Brokers.Loggings;
using SmartManager.Brokers.Storages;
using SmartManager.Models.Applicants;
using SmartManager.Services.Foundations.Applicants;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace SmartManagerTestsUnit.Services.Foundations
{
    public partial class ApplicantServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IApplicantService applicantService;

        public ApplicantServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.applicantService =
                new ApplicantService(
                    storageBroker: this.storageBrokerMock.Object,
                    dateTimeBroker: this.dateTimeBrokerMock.Object,
                    loggingBroker: this.loggingBrokerMock.Object);
        }

        private string GetRandomString() =>
            new MnemonicString().GetValue();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
           actualException => actualException.SameExceptionAs(expectedException);

        private static Applicant CreateRandomApplicant() =>
            CreateApplicantFiller(GetRandomDateTime()).Create();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Filler<Applicant> CreateApplicantFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Applicant>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }

        private static IQueryable<Applicant> GetRandomApplicants()
        {
            return CreateApplicantFiller(dates: GetRandomDateTimeOffset())
                .Create(count: GetRandomNUmber()).AsQueryable();
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNUmber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static SqlException GetSqlError() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));
    }
}
