using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BorrowedGames.TestHelpers
{
    public static class AssertExtension
    {
        public static T Throws<T>(Action expressionUnderTest,
                                  string exceptionMessage = "Expected exception has not been thrown by target of invocation."
                                 ) where T : Exception
        {
            try
            {
                expressionUnderTest();
            }
            catch (T exception)
            {
                return exception;
            }

            Assert.Fail(exceptionMessage);
            return null;
        }

        public static void DoesNotThrow<T>(Action expressionUnderTest,
                                  string exceptionMessage = "Unexpected exception has been thrown by target of invocation."
                                 ) where T : Exception
        {
            try
            {
                expressionUnderTest();
            }
            catch (T)
            {
                Assert.Fail(exceptionMessage);
            }
        }
    }
}
