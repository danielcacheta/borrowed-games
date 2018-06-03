using BorrowedGames.Models;
using System;
using System.Collections.Generic;

namespace BorrowedGames.Data.Repositories.Interfaces
{
    public interface IFriendsRepository : IDisposable
    {
        IEnumerable<Friend> FindAll();
        Friend Find(long id);
        void Add(Friend friend);
        void Update(Friend friend);
        void Delete(Friend friend);
        bool IsDuplicate(Friend friend);
        void Save();
    }
}
