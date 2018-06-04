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
    public class FriendsTest
    {
        private static IFriendsRepository _repositoryStub;
        private static FriendsController _sut;
        private static Friend _friend;

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            _repositoryStub = new FriendRepositoryStub();
            _sut = new FriendsController(_repositoryStub);
        }

        [TestCleanup]
        public void Cleanuo()
        {
            ClearList();
        }

        [TestMethod]
        public void IsAbleToAddFriend()
        {
            Assert.AreEqual(0, GetFriendsList().Count());
            Task<IActionResult> insertResult = InsertFriend();

            Assert.AreEqual(1, GetFriendsList().Count());
            Assert.AreEqual(TaskStatus.RanToCompletion, insertResult.Status);
            Assert.AreEqual(true, insertResult.IsCompletedSuccessfully);

            var insertedFriend = GetFriendsList()[0];

            Assert.AreEqual(_friend.Name, insertedFriend.Name);
            Assert.AreEqual(_friend.Phone, insertedFriend.Phone);
        }

        [TestMethod]
        public async Task IsAbleToFindFriendAsync()
        {
            await InsertFriend();

            var existingFriend = GetFriendsList()[0];

            var foundFriend = _repositoryStub.Find(existingFriend.Id).Result;

            Assert.AreEqual(_friend.Name, foundFriend.Name);
            Assert.AreEqual(_friend.Phone, foundFriend.Phone);
        }

        [TestMethod]
        public async Task IsAbleToUpdateFriendAsync()
        {
            await InsertFriend();

            var existingFriend = GetFriendsList()[0];

            var friendUpdated = _repositoryStub.Find(existingFriend.Id).Result;
            friendUpdated.Name = "My Friend's Name Updated Test";
            friendUpdated.Phone = "(99) 99999-9998";
            var updateResult = _sut.Edit(friendUpdated.Id, friendUpdated);

            var updatedFriend = _repositoryStub.Find(existingFriend.Id).Result;
            Assert.AreEqual(friendUpdated.Name, GetFriendsList()[0].Name);
            Assert.AreEqual(friendUpdated.Phone, GetFriendsList()[0].Phone);
        }

        [TestMethod]
        public async Task IsAbleToListFriendsAsync()
        {
            await InsertFriend();

            var newFriend = new Friend
            {
                Name = "My New Friend's Name Test",
                Phone = "(99) 99999-9997"
            };
            var insertResult = _sut.Create(newFriend);

            var foundFriends = _repositoryStub.FindAll().Result;
            Assert.AreEqual(2, foundFriends.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task IsNotAbleToInsertDuplicateFriendAsync()
        {
            await InsertFriend();

            var newFriend = new Friend
            {
                Name = _friend.Name,
                Phone = "(99) 99999-9997"
            };
            _repositoryStub.Add(newFriend);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task IsNotAbleToUpdateToDuplicateFriendAsync()
        {
            await InsertFriend();

            var newFriend = new Friend
            {
                Name = "My New Friend's Name Test",
                Phone = "(99) 99999-9997"
            };
            _repositoryStub.Add(newFriend);

            var existingFriend = GetFriendsList()[0];

            var friendUpdated = _repositoryStub.Find(existingFriend.Id).Result;
            friendUpdated.Name = newFriend.Name;
            _repositoryStub.Update(friendUpdated);
        }

        [TestMethod]
        public async Task IsAbleToRemoveFriendAsync()
        {
            await InsertFriend();
            var count = GetFriendsList().Count;
            var existingFriend = GetFriendsList()[0];
            var deleteResult = _sut.Delete(existingFriend.Id);

            Assert.AreEqual(count, GetFriendsList().Count);

            _repositoryStub.Delete(existingFriend);

            Assert.AreEqual(count - 1, GetFriendsList().Count);
        }

        private static Task<IActionResult> InsertFriend()
        {
            _friend = new Friend
            {
                Name = "My Friend's Name Test",
                Phone = "(99) 99999-9999"
            };

            var insertResult = _sut.Create(_friend);
            return insertResult;
        }

        private IList<Friend> GetFriendsList()
        {
            return _repositoryStub
                .FindAll()
                .Result
                .ToList();
        }

        private void ClearList()
        {
            foreach (var friend in GetFriendsList())
                _repositoryStub.Delete(friend);
        }
    }
}
