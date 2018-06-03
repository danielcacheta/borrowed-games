using BorrowedGames.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BorrowedGames.Data.Repositories.Interfaces
{
    public interface IFriendsRepository : IDisposable
    {
        Task<IEnumerable<Friend>> FindAll();
        Task<Friend> Find(long id);
        void Add(Friend friend);
        void Update(Friend friend);
        void Delete(Friend friend);
        bool IsDuplicate(Friend friend);
        Task Save();
    }
}
