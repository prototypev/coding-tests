namespace UserRegistration.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    using UserRegistration.Core.Entities;
    using UserRegistration.Core.Persistence;
    using UserRegistration.Core.Services;

    /// <summary>
    /// Tests for user registration service.
    /// </summary>
    [TestFixture]
    public class UserRegistrationServiceTests
    {
        #region Fields

        /// <summary>
        /// The user registration service.
        /// </summary>
        private IUserRegistrationService _userRegistrationService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Sets up the test.
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            IList<User> users = new List<User>();
            IQueryable<User> queryable = users.AsQueryable();

            Mock<IDbSet<User>> mockUsers = new Mock<IDbSet<User>>();
            mockUsers.Setup(x => x.Provider)
                     .Returns(queryable.Provider);
            mockUsers.Setup(x => x.Expression)
                     .Returns(queryable.Expression);
            mockUsers.Setup(x => x.ElementType)
                     .Returns(queryable.ElementType);
            mockUsers.Setup(x => x.GetEnumerator())
                     .Returns(queryable.GetEnumerator());
            mockUsers.Setup(x => x.Add(It.IsAny<User>()))
                     .Callback<User>(users.Add);

            Mock<DatabaseContext> mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(x => x.Users)
                       .Returns(mockUsers.Object);

            this._userRegistrationService = new UserRegistrationService(mockContext.Object);
        }

        /// <summary>
        /// Tears down the test.
        /// </summary>
        [TearDown]
        public void TearDownTest()
        {
            this._userRegistrationService.Dispose();
        }

        /// <summary>
        /// Tests registering a duplicate user.
        /// </summary>
        [Test]
        public void TestRegisterDuplicateUser()
        {
            User user = new User
                            {
                                UserName = "abcde", 
                                Password = "Ab123456"
                            };

            Assert.DoesNotThrow(() => this._userRegistrationService.RegisterUser(user), "Should register successfully");
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register with same user name");
        }

        /// <summary>
        /// Tests registering with invalid passwords.
        /// </summary>
        [Test]
        public void TestRegisterUserWithInvalidPassword()
        {
            User user = new User
                            {
                                UserName = "abcde"
                            };
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register without password");

            user.Password = "Ab12345";
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register with password that is too short");

            user.Password = "ab123456";
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register with password that has no uppercase character");

            user.Password = "AB123456";
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register with password that has no lowercase character");

            user.Password = "Abcdefgh";
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register with password that has no numbers");
        }

        /// <summary>
        /// Tests registering with invalid user names.
        /// </summary>
        [Test]
        public void TestRegisterUserWithInvalidUserName()
        {
            User user = new User
                            {
                                Password = "Abcd1234"
                            };
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register without user name");

            user.UserName = "abcd";
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register with user name that is too short");

            user.UserName = "!@#$%";
            Assert.Throws<ValidationException>(() => this._userRegistrationService.RegisterUser(user), "Should fail to register with user name with non-alpha numeric characters");
        }

        /// <summary>
        /// Tests registering with a null user.
        /// </summary>
        [Test]
        public void TestRegisterUserWithNull()
        {
            Assert.Throws<ArgumentNullException>(() => this._userRegistrationService.RegisterUser(null), "Should fail to register without a user");
        }

        #endregion
    }
}