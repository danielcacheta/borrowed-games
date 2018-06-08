using BorrowedGames.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BorrowedGames.Helpers.Tests
{
    [TestClass]
    public class GuardTests
    {
        [TestMethod]
        public void ForInvalidId_When_Id_Property_Is_Not_Valid_Guid()
        {
            var expectedMessage = "Customer Id is not a valid ID";
            var invalidGuid = new Guid();

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForValidId("Customer Id", invalidGuid));

            Assert.AreEqual(expectedMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForInvalidId_Does_Not_Throw_Exception_When_Id_Property_Is_Valid_Guid()
        {
            var validGuid = Guid.NewGuid();

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.ForValidId("Customer Id", validGuid));
        }

        [TestMethod]
        public void ForInvalidId_When_Id_Property_Is_Not_Valid_Guid_With_Custom_Message()
        {
            var customMessage = "Guid is not valid";
            var invalidGuid = new Guid();

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForValidId(invalidGuid, customMessage));

            Assert.AreEqual(customMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForInvalidId_Does_Not_Throw_Exception_When_Id_Property_Is_Valid_Guid_With_Custom_Message()
        {
            var customMessage = "Guid is not valid";
            var validGuid = Guid.NewGuid();

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.ForValidId(validGuid, customMessage));
        }

        [TestMethod]
        public void ForInvalidId_When_Id_Property_Is_Negative()
        {
            var expectedMessage = "Customer Id is not a valid ID";

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForValidId("Customer Id", -1));

            Assert.AreEqual(expectedMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForInvalidId_Does_Not_Throw_Exception_When_Id_Property_Is_Positive()
        {
            var validGuid = Guid.NewGuid();

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.ForValidId("Customer Id", 1));
        }

        [TestMethod]
        public void ForInvalidId_When_Id_Property_Is_Negative_With_Custom_Message()
        {
            var customMessage = "Id can not be negative";

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForValidId(-1, customMessage));

            Assert.AreEqual(customMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForInvalidId_Does_Not_Throw_Exception_When_Id_Property_Is_Positive_With_Custom_Message()
        {
            var customMessage = "Id can not be negative";

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.ForValidId(1, customMessage));
        }

        [TestMethod]
        public void ForNegative_When_Quantity_Property_Is_Negative()
        {
            var expectedMessage = "Quantity can not be negative";

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForNegative("Quantity", -1));

            Assert.AreEqual(expectedMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForNegative_Does_Not_Throw_Exception_When_Quantity_Property_Is_Positive()
        {
            AssertExtension.DoesNotThrow<Exception>(
                () => Guard.ForNegative("Quantity", 1));
        }

        [TestMethod]
        public void ForNullOrEmptyDefaultMessage_When_Is_Empty()
        {
            var expectedMessage = "Customer Name is required";

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForNullOrEmptyDefaultMessage("Customer Name", ""));

            Assert.AreEqual(expectedMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForNullOrEmptyDefaultMessage_When_Is_Null()
        {
            var expectedMessage = "Quantity can not be negative";

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForNegative("Quantity", -1));

            Assert.AreEqual(expectedMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForNullOrEmptyDefaultMessage_Does_Not_Throw_Exception_When_Value_Is_Not_Empty()
        {
            AssertExtension.DoesNotThrow<Exception>(
                () => Guard.ForNullOrEmptyDefaultMessage("Customer Name", "Text"));
        }

        [TestMethod]
        public void ForNullOrEmpty_When_Is_Empty_With_Custom_Message()
        {
            var customMessage = "Value can not be empty";

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForNullOrEmpty("", customMessage));

            Assert.AreEqual(customMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForNullOrEmpty_When_Is_Null_With_Custom_Message()
        {
            var customMessage = "Value can not be empty";

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.ForNullOrEmpty(null, customMessage));

            Assert.AreEqual(customMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void ForNullOrEmpty_Does_Not_Throw_Exception_When_Value_Is_Not_Empty_With_Custom_Message()
        {
            var customMessage = "Value can not be empty";

            AssertExtension.DoesNotThrow<Exception>(
                () => Guard.ForNullOrEmptyDefaultMessage("Text", customMessage));
        }

        [TestMethod]
        public void StringLength_When_Text_Is_Too_Long()
        {
            var expectedMessage = "Comment can't be longer than 500 characters";
            var comment = new string('*', 501);

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.StringLength("Comment", comment, 500));

            Assert.AreEqual(expectedMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void StringLength_Does_Not_Throw_Exception_When_Text_Is_Not_Too_Long()
        {
            var comment = new string('*', 500);

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.StringLength("Comment", comment, 500));
        }

        [TestMethod]
        public void StringLength_When_Text_Is_Too_Long_With_Custom_Message()
        {
            var customMessage = "This field supports only 500 characters";
            var comment = new string('*', 501);

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.StringLength(comment, 500, customMessage));

            Assert.AreEqual(customMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void StringLength_Does_Not_Throw_Exception_When_Text_Is_Not_Too_Long_With_Custom_Message()
        {
            var customMessage = "This field supports only 500 characters";
            var comment = new string('*', 500);

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.StringLength(comment, 500, customMessage));
        }

        [TestMethod]
        public void StringLength_When_Text_Is_Too_Long_For_Allowed_Range()
        {
            var expectedMessage = "Comment must contain between 100 and 500 characters";
            var comment = new string('*', 501);

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.StringLength("Comment", comment, 100, 500));

            Assert.AreEqual(expectedMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void StringLength_When_Text_Is_Too_Short_For_Allowed_Range()
        {
            var expectedMessage = "Comment must contain between 100 and 500 characters";
            var comment = new string('*', 99);

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.StringLength("Comment", comment, 100, 500));

            Assert.AreEqual(expectedMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void StringLength_Does_Not_Throw_Exception_When_Text_Is_Not_Too_Long_For_Allowed_Range()
        {
            var comment = new string('*', 500);

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.StringLength("Comment", comment, 100, 500));
        }

        [TestMethod]
        public void StringLength_When_Text_Is_Too_Long_For_Allowed_Range_With_Custom_Message()
        {
            var customMessage = "This field supports between 100 and 500 characters";
            var comment = new string('*', 501);

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.StringLength(comment, 100, 500, customMessage));

            Assert.AreEqual(customMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void StringLength_When_Text_Is_Too_Short_For_Allowed_Range_With_Custom_Message()
        {
            var customMessage = "This field supports between 100 and 500 characters";
            var comment = new string('*', 99);

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.StringLength(comment, 100, 500, customMessage));

            Assert.AreEqual(customMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void StringLength_Does_Not_Throw_Exception_When_Text_Is_Not_Too_Long_For_Allowed_Range_With_Custom_Message()
        {
            var customMessage = "This field supports between 100 and 500 characters";
            var comment = new string('*', 500);

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.StringLength(comment, 100, 500, customMessage));
        }

        [TestMethod]
        public void AreEqual_When_Values_Do_Not_Match_With_Custom_Message()
        {
            var customMessage = "Values don't not match";

            var exceptionThrown = AssertExtension.Throws<Exception>(
                () => Guard.AreEqual("Text 1", "Text 1 ", customMessage));

            Assert.AreEqual(customMessage, exceptionThrown.Message);
        }

        [TestMethod]
        public void AreEqual_Does_Not_Throw_Exception_When_Values_Match_With_Custom_Message()
        {
            var customMessage = "Values don't not match";

            AssertExtension.DoesNotThrow<Exception>(
             () => Guard.AreEqual("Text 1", "Text 1", customMessage));
        }
    }
}
