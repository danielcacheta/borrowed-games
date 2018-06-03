using BorrowedGames.Controllers;
using BorrowedGames.Data;
using BorrowedGames.Data.Repositories.Interfaces;
using BorrowedGames.Models;
using BorrowedGames.Tests.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

        [TestMethod]
        public void IsAbleToAddFriend()
        {
            _friend = new Friend
            {
                Name = "My Friend's Name Test",
                Phone = "(99) 99999-9999"
            };

            Assert.AreEqual(0, GetFriendsList().Count());

            var insertResult = _sut.Create(_friend);

            Assert.AreEqual(1, GetFriendsList().Count());
            Assert.AreEqual(TaskStatus.RanToCompletion, insertResult.Status);
            Assert.AreEqual(true, insertResult.IsCompletedSuccessfully);

            var insertedFriend = GetFriendsList()[0];

            Assert.AreEqual(_friend.Name, insertedFriend.Name);
            Assert.AreEqual(_friend.Phone, insertedFriend.Phone);
        }

        [TestMethod]
        public void IsAbleToFindFriend()
        {
            var existingFriend = GetFriendsList()[0];

            var loadedForEditionResult = _sut.Details(existingFriend.Id);
            Assert.AreEqual(TaskStatus.RanToCompletion, loadedForEditionResult.Status);
            Assert.AreEqual(true, loadedForEditionResult.IsCompletedSuccessfully);

            var foundFriend = _repositoryStub.Find(existingFriend.Id);

            Assert.AreEqual(_friend.Name, foundFriend.Name);
            Assert.AreEqual(_friend.Phone, foundFriend.Phone);
        }



        [TestMethod]
        public void IsAbleToUpdateFriend()
        {
            var existingFriend = GetFriendsList()[0];

            var loadedForEditionResult = _sut.Edit(existingFriend.Id);
            Assert.AreEqual(TaskStatus.RanToCompletion, loadedForEditionResult.Status);
            Assert.AreEqual(true, loadedForEditionResult.IsCompletedSuccessfully);

            var friendUpdated = _repositoryStub.Find(existingFriend.Id);
            friendUpdated.Name = "My Friend's Name Updated Test";
            friendUpdated.Phone = "(99) 99999-9998";
            var updateResult = _sut.Edit(friendUpdated.Id, friendUpdated);
            Assert.AreEqual(TaskStatus.RanToCompletion, loadedForEditionResult.Status);
            Assert.AreEqual(true, loadedForEditionResult.IsCompletedSuccessfully);

            var updatedFriend = _repositoryStub.Find(existingFriend.Id);
            Assert.AreEqual(friendUpdated.Name, GetFriendsList()[0].Name);
            Assert.AreEqual(friendUpdated.Phone, GetFriendsList()[0].Phone);
        }

        [TestMethod]
        public void IsAbleToListFriends()
        {
            var existingFriend = GetFriendsList()[0];

            var newFriend = new Friend
            {
                Name = "My New Friend's Name Test",
                Phone = "(99) 99999-9997"
            };
            var insertResult = _sut.Create(newFriend);

            var loadedForEditionResult = _sut.Index();
            Assert.AreEqual(TaskStatus.RanToCompletion, loadedForEditionResult.Status);
            Assert.AreEqual(true, loadedForEditionResult.IsCompletedSuccessfully);

            var foundFriends = _repositoryStub.FindAll();
            Assert.AreEqual(2, foundFriends.Count());
        }

        [TestMethod]
        public void IsAbleToRemoveFriend()
        {
            var count = GetFriendsList().Count;
            var existingFriend = GetFriendsList()[0];
            var deleteResult = _sut.Delete(existingFriend.Id);
            Assert.AreEqual(TaskStatus.RanToCompletion, deleteResult.Status);
            Assert.AreEqual(true, deleteResult.IsCompletedSuccessfully);

            Assert.AreEqual(count, GetFriendsList().Count);

            var deleteConfirmedResult = _sut.DeleteConfirmed(existingFriend.Id);
            Assert.AreEqual(TaskStatus.RanToCompletion, deleteConfirmedResult.Status);
            Assert.AreEqual(true, deleteConfirmedResult.IsCompletedSuccessfully);

            Assert.AreEqual(count - 1, GetFriendsList().Count);
        }



        private IList<Friend> GetFriendsList()
        {
            return _repositoryStub
                .FindAll()
                .ToList();
        }
    }
}
