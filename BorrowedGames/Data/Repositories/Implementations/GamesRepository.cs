using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BorrowedGames.Data.Repositories.Interfaces;
using BorrowedGames.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowedGames.Data.Repositories.Implementations
{
    public class GamesRepository : IGamesRepository
    {
        private const string GAME_ALREADY_REGISTERED = "This game is already registered";

        private readonly ApplicationDbContext _dbContext;
        private bool disposed;

        public GamesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Game game)
        {
            if (IsDuplicate(game))
                throw new InvalidOperationException(GAME_ALREADY_REGISTERED);
            _dbContext.Game.Add(game);
        }

        public void Update(Game game)
        {
            if (IsDuplicate(game))
                throw new InvalidOperationException(GAME_ALREADY_REGISTERED);
            _dbContext.Entry(game).State = EntityState.Modified;
        }

        public void Delete(Game game)
        {
            _dbContext.Game.Remove(game);
        }

        public async Task<Game> Find(long id)
        {
            return await _dbContext.Game.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> FindAll()
        {
            return await _dbContext.Game.ToListAsync();
        }

        public bool IsDuplicate(Game game)
        {
            return _dbContext.Game
                .Where(x => x.Id != game.Id &&
                x.Name.Trim() == game.Name.Trim())
                .Any();
        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
