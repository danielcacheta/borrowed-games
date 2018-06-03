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
        private readonly ApplicationDbContext _dbContext;
        private bool disposed;

        public FriendsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Friend friend)
        {
            _dbContext.Friend.Add(friend);
        }

        public void Update(Friend friend)
        {
            _dbContext.Entry(friend).State = EntityState.Modified;
        }

        public void Delete(Friend friend)
        {
            _dbContext.Friend.Remove(friend);
        }

        public Friend Find(long id)
        {
            return _dbContext.Friend.Find(id);
        }

        public IEnumerable<Friend> FindAll()
        {
            return _dbContext.Friend;
        }

        public bool IsDuplicate(Friend friend)
        {
            return _dbContext.Friend
                .Where(x => x.Id != friend.Id &&
                x.Name.Trim() == friend.Name.Trim())
                .Any();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
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
