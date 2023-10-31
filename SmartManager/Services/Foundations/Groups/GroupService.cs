//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Loggings;
using SmartManager.Brokers.Storages;
using SmartManager.Models.Groups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Groups
{
    public partial class GroupService : IGroupService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        public GroupService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Group> AddGroupAsync(Group group) =>
        TryCatch(async () =>
        {
            ValidateGroupOnAdd(group);

            return await this.storageBroker.InsertGroupAsync(group);
        });

        public ValueTask<Group> RetrieveGroupByIdAsync(Guid groupId) =>
        TryCatch(async () =>
        {
            ValidateGroupId(groupId);

            var maybeGroup =
              await this.storageBroker.SelectGroupByIdAsync(groupId);

            ValidateStorageGroup(maybeGroup, groupId);

            return await this.storageBroker.SelectGroupByIdAsync(groupId);
        });
        public IQueryable<Group> RetrieveAllGroups() =>
            TryCatch(() => this.storageBroker.SelectAllGroups());

        public ValueTask<Group> ModifyGroupAsync(Group group) =>
        TryCatch(async () =>
        {
            ValidateGroupOnModify(group);

            var maybeGroup =
                await this.storageBroker.SelectGroupByIdAsync(group.GroupId);

            ValidateAgainstStorageGroupOnModify(inputAccount: group, storageGroup: maybeGroup);

            return await this.storageBroker.UpdateAppolicantAsync(group);
        });

        public async ValueTask<Group> RemoveGroupAsync(Guid groupid)
        {
            ValidateGroupId(groupid);

            var maybeGroup = await this.storageBroker.SelectGroupByIdAsync(groupid);

            ValidateStorageGroup(maybeGroup, groupid);

            return await this.storageBroker.DeleteGroupAsync(maybeGroup);
        }
    }
}
