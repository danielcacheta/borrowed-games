using BorrowedGames.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BorrowedGames.Data.Repositories.Interfaces
{
    public interface IGamesRepository : IDisposable
    {
        Task<IEnumerable<Game>> FindAll();
        Task<Game> Find(long id);
        void Add(Game game);
        void Update(Game game);
        void Delete(Game game);
        bool IsDuplicate(Game game);
        Task Save();
    }
}
