using BorrowedGames.Controllers;
using BorrowedGames.Data;
using BorrowedGames.Data.Repositories.Interfaces;
using BorrowedGames.Models;
using BorrowedGames.Tests.Stubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorrowedGames.Tests
{
    [TestClass]
    public class GamesTest
    {
        private static IGamesRepository _repositoryStub;
        private static GamesController _sut;
        private static Game _game;

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            _repositoryStub = new GameRepositoryStub();
            _sut = new GamesController(_repositoryStub);
        }

        [TestCleanup]
        public void Cleanuo()
        {
            ClearList();
        }

        [TestMethod]
        public void IsAbleToAddGame()
        {
            Assert.AreEqual(0, GetGamesList().Count());
            Task<IActionResult> insertResult = InsertGame();

            Assert.AreEqual(1, GetGamesList().Count());
            Assert.AreEqual(TaskStatus.RanToCompletion, insertResult.Status);
            Assert.AreEqual(true, insertResult.IsCompletedSuccessfully);

            var insertedGame = GetGamesList()[0];

            Assert.AreEqual(_game.Name, insertedGame.Name);
        }

        [TestMethod]
        public async Task IsAbleToFindGameAsync()
        {
            await InsertGame();

            var existingGame = GetGamesList()[0];

            var foundGame = _repositoryStub.Find(existingGame.Id).Result;

            Assert.AreEqual(_game.Name, foundGame.Name);
        }

        [TestMethod]
        public async Task IsAbleToUpdateGameAsync()
        {
            await InsertGame();

            var existingGame = GetGamesList()[0];

            var gameUpdated = _repositoryStub.Find(existingGame.Id).Result;
            gameUpdated.Name = "My Game's Name Updated Test";
            var updateResult = _sut.Edit(gameUpdated.Id, gameUpdated);

            var updatedGame = _repositoryStub.Find(existingGame.Id).Result;
            Assert.AreEqual(gameUpdated.Name, GetGamesList()[0].Name);
        }

        [TestMethod]
        public async Task IsAbleToListGamesAsync()
        {
            await InsertGame();

            var newGame = new Game
            {
                Name = "My New Game's Name Test"
            };
            var insertResult = _sut.Create(newGame);

            var foundGames = _repositoryStub.FindAll().Result;
            Assert.AreEqual(2, foundGames.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task IsNotAbleToInsertDuplicateGameAsync()
        {
            await InsertGame();

            var newGame = new Game
            {
                Name = _game.Name
            };
            _repositoryStub.Add(newGame);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task IsNotAbleToUpdateToDuplicateGameAsync()
        {
            await InsertGame();

            var newGame = new Game
            {
                Name = "My New Game's Name Test"
            };
            _repositoryStub.Add(newGame);

            var existingGame = GetGamesList()[0];

            var gameUpdated = _repositoryStub.Find(existingGame.Id).Result;
            gameUpdated.Name = newGame.Name;
            _repositoryStub.Update(gameUpdated);
        }

        [TestMethod]
        public async Task IsAbleToRemoveGameAsync()
        {
            await InsertGame();
            var count = GetGamesList().Count;
            var existingGame = GetGamesList()[0];
            var deleteResult = _sut.Delete(existingGame.Id);

            Assert.AreEqual(count, GetGamesList().Count);

            _repositoryStub.Delete(existingGame);

            Assert.AreEqual(count - 1, GetGamesList().Count);
        }

        private static Task<IActionResult> InsertGame()
        {
            _game = new Game
            {
                Name = "My Game's Name Test"
            };

            var insertResult = _sut.Create(_game);
            return insertResult;
        }

        private IList<Game> GetGamesList()
        {
            return _repositoryStub
                .FindAll()
                .Result
                .ToList();
        }

        private void ClearList()
        {
            foreach (var game in GetGamesList())
                _repositoryStub.Delete(game);
        }
    }
}
