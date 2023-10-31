//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using EFxceptions.Models.Exceptions;
using SmartManager.Models.Groups;
using SmartManager.Models.Groups.Exceptions;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace SmartManager.Services.Foundations.Groups
{
    public partial class GroupService
    {
        private delegate ValueTask<Group> ReturningGroupFunction();
        private delegate IQueryable<Group> ReturningGroupsFunction();

        private async ValueTask<Group> TryCatch(ReturningGroupFunction returningGroupFunction)
        {
            try
            {
                return await returningGroupFunction();
            }
            catch (NullGroupException nullGroupExeption)
            {
                throw CreateAndLogValidationException(nullGroupExeption);
            }
            catch (InvalidGroupException invalidGroupException)
            {
                throw CreateAndLogValidationException(invalidGroupException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsGroupException =
                    new AlreadyExistsGroupException(duplicateKeyException);

                throw CreateAndALogDependencyValidationException(alreadyExistsGroupException);
            }
            catch (SqlException sqlException)
            {
                var failedGroupStorageException = new FailedGroupStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedGroupStorageException);
            }
            catch (Exception exception)
            {
                var failedGroupServiceException = new FailedGroupServiceException(exception);

                throw CreateAndLogServiceException(failedGroupServiceException);
            }
        }

        private IQueryable<Group> TryCatch(ReturningGroupsFunction returningGroupsFunction)
        {
            try
            {
                return returningGroupsFunction();
            }
            catch (SqlException SqlException)
            {
                var failedGroupStorageException =
                    new FailedGroupStorageException(SqlException);

                throw CreateAndLogCriticalDependencyException(failedGroupStorageException);
            }
            catch (Exception exception)
            {
                var failedGroupServiceException =
                    new FailedGroupServiceException(exception);

                throw CreateAndLogServiceException(failedGroupServiceException);
            }
        }

        private GroupDependencyValidationException CreateAndALogDependencyValidationException(Xeption exception)
        {
            var groupDependencyValidationException =
                new GroupDependencyValidationException(exception);
            this.loggingBroker.LogError(groupDependencyValidationException);

            return groupDependencyValidationException;
        }

        private GroupValidationException CreateAndLogValidationException(Xeption exception)
        {
            var groupValidationException =
                new GroupValidationException(exception);
            this.loggingBroker.LogError(groupValidationException);

            return groupValidationException;
        }

        private GroupDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var groupDependencyException = new GroupDependencyException(exception);
            this.loggingBroker.LogCritical(groupDependencyException);

            return groupDependencyException;
        }

        private GroupServiceException CreateAndLogServiceException(Xeption exception)
        {
            var groupServiceException = new GroupServiceException(exception);
            this.loggingBroker.LogError(groupServiceException);

            return groupServiceException;
        }
    }
}
