using BorrowedGames.Data.Repositories.Interfaces;
using BorrowedGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorrowedGames.Tests.Stubs
{
    public class FriendRepositoryStub : IFriendsRepository
    {
        private const string FRIEND_ALREADY_REGISTERED = "This friend is already registered";

        private IList<Friend> _list;
        private long _lastId;

        public FriendRepositoryStub()
        {
            _list = new List<Friend>();
            _lastId = 0;
        }

        public void Add(Friend friend)
        {
            if (IsDuplicate(friend))
                throw new InvalidOperationException(FRIEND_ALREADY_REGISTERED);

            _lastId++;
            friend.Id = _lastId;
            _list.Add(friend);
        }

        public void Delete(Friend friend)
        {
            _list.Remove(friend);
        }

        public void Dispose()
        {
            _list = null;
        }

        public async Task<Friend> Find(long id)
        {
            return await Task.Run(() => _list.Where(x => x.Id == id).FirstOrDefault());
        }

        public async Task<IEnumerable<Friend>> FindAll()
        {
            return await Task.Run(() => _list.AsEnumerable());
        }

        public bool IsDuplicate(Friend friend)
        {
            return _list.Any(
                x => x.Id != friend.Id &&
                x.Name.Trim() == friend.Name.Trim());
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        public void Update(Friend friend)
        {
            if (IsDuplicate(friend))
                throw new InvalidOperationException(FRIEND_ALREADY_REGISTERED);
            Delete(Find(friend.Id).Result);
            Add(friend);
        }
    }
}
