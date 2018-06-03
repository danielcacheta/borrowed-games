using BorrowedGames.Data.Repositories.Interfaces;
using BorrowedGames.Models;
using System.Collections.Generic;
using System.Linq;

namespace BorrowedGames.Tests.Stubs
{
    public class FriendRepositoryStub : IFriendsRepository
    {
        private List<Friend> _list;
        private long _lastId;

        public FriendRepositoryStub()
        {
            _list = new List<Friend>();
            _lastId = 0;
        }

        public void Add(Friend friend)
        {
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

        public Friend Find(long id)
        {
            return _list.Find(x => x.Id == id);
        }

        public IEnumerable<Friend> FindAll()
        {
            return _list;
        }

        public bool IsDuplicate(Friend friend)
        {
            return _list.Any(
                x => x.Id != friend.Id &&
                x.Name.Trim() == friend.Name.Trim());
        }

        public void Save()
        {

        }

        public void Update(Friend friend)
        {
            Delete(Find(friend.Id));
            Add(friend);
        }
    }
}
