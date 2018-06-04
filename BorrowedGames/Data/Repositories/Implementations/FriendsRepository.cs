using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BorrowedGames.Data.Repositories.Interfaces;
using BorrowedGames.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowedGames.Data.Repositories.Implementations
{
    public class FriendsRepository : IFriendsRepository
    {
        private const string FRIEND_ALREADY_REGISTERED = "This friend is already registered";

        private readonly ApplicationDbContext _dbContext;
        private bool disposed;

        public FriendsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Friend friend)
        {
            if (IsDuplicate(friend))
                throw new InvalidOperationException(FRIEND_ALREADY_REGISTERED);
            _dbContext.Friend.Add(friend);
        }

        public void Update(Friend friend)
        {
            if (IsDuplicate(friend))
                throw new InvalidOperationException(FRIEND_ALREADY_REGISTERED);
            _dbContext.Entry(friend).State = EntityState.Modified;
        }

        public void Delete(Friend friend)
        {
            _dbContext.Friend.Remove(friend);
        }

        public async Task<Friend> Find(long id)
        {
            return await _dbContext.Friend.FindAsync(id);
        }

        public async Task<IEnumerable<Friend>> FindAll()
        {
            return await _dbContext.Friend.ToListAsync();
        }

        public bool IsDuplicate(Friend friend)
        {
            return _dbContext.Friend
                .Where(x => x.Id != friend.Id &&
                x.Name.Trim() == friend.Name.Trim())
                .Any();
        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
