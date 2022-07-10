using Assert = NUnit.Framework.Assert;
using NUnit.Framework;
using System;

namespace C868_Performance_Assessment.Tests
{
    public class LoginTests
    {
        private static User u = new User();
        private static Exception exception;
        private static readonly string testUser = "test";
        private static readonly string testPass = "test";
        private static readonly string dbConn = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";

        [SetUp]
        public void SetGlobalUserTest()
        {
            try
            {
                u = Login.SetGlobalUser(testUser, testPass, dbConn);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        [Test]
        public void ReturnUser()
        {
            Assert.IsTrue(u.UserId > 0);
        }

        [Test]
        public void ReturnMatchingUser()
        {
            Assert.IsTrue(u.UserName == testUser);
        }

        [Test]
        public void NoExceptionsThrown()
        {
            Assert.IsNull(exception);
        }
    }
}