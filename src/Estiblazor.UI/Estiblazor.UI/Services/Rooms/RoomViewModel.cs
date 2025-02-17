﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using Estiblazor.UI.Services.Users;

namespace Estiblazor.UI.Services.Rooms
{
    public class RoomViewModel(IEnumerable<EstimationStage> estimationStages) : NotifiesPropertyChanged
    {
        private RoomId id = new RoomId(string.Empty);
        public RoomId Id { get => id; set => PropertyChange(ref id, value); }

        private string name = string.Empty;
        public string Name
        {
            get => name;
            set => PropertyChange(ref name, value);
        }

        private ObservableCollection<User> users = [];
        public ObservableCollection<User> Users { get => users; set => PropertyChange(ref users, value); }

        public IEnumerable<IGrouping<UserId, UserChoice>> GetChoicesByUsers() => EstimationStages.SelectMany(y => y.UserChoices).GroupBy(x => x.UserId);

        public IEnumerable<UserId> GetUserIds() => EstimationStages.SelectMany(y => y.UserChoices).Select(x => x.UserId).Distinct();

        public IReadOnlyList<EstimationStage> EstimationStages { get; } = estimationStages.ToList().AsReadOnly();

        //users may be duplicated
        public void AddUser(User user)
        {
            if (users.Any(x => x.Id == user.Id)) return;
            users.Add(user);

        }
        public void RemoveUser(User user)
        {
            var existing = users.FirstOrDefault(x => x.Id == user.Id);
            if (existing is not null) users.Remove(existing);
        }


    }

}
