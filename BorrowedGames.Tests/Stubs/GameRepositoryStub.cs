using BorrowedGames.Data.Repositories.Interfaces;
using BorrowedGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorrowedGames.Tests.Stubs
{
    public class GameRepositoryStub : IGamesRepository
    {
        private const string GAME_ALREADY_REGISTERED = "This game is already registered";

        private IList<Game> _list;
        private long _lastId;

        public GameRepositoryStub()
        {
            _list = new List<Game>();
            _lastId = 0;
        }

        public void Add(Game game)
        {
            if (IsDuplicate(game))
                throw new InvalidOperationException(GAME_ALREADY_REGISTERED);

            _lastId++;
            game.Id = _lastId;
            _list.Add(game);
        }

        public void Delete(Game game)
        {
            _list.Remove(game);
        }

        public void Dispose()
        {
            _list = null;
        }

        public async Task<Game> Find(long id)
        {
            return await Task.Run(() => _list.Where(x => x.Id == id).FirstOrDefault());
        }

        public async Task<IEnumerable<Game>> FindAll()
        {
            return await Task.Run(() => _list.AsEnumerable());
        }

        public bool IsDuplicate(Game game)
        {
            return _list.Any(
                x => x.Id != game.Id &&
                x.Name.Trim() == game.Name.Trim());
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        public void Update(Game game)
        {
            if (IsDuplicate(game))
                throw new InvalidOperationException(GAME_ALREADY_REGISTERED);
            Delete(Find(game.Id).Result);
            Add(game);
        }
    }
}
