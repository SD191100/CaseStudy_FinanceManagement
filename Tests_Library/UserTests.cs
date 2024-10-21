using DAO_Library;
using Entity_Library;
using Utility_Library;
namespace Tests_Library
{
    [TestFixture]
    public class UserTests
    {
        private IFinanceRepository _repo;

        [SetUp]
        public void Setup()
        {
            // You can initialize objects here
            string connectionString = DBPropertyUtil.GetConnectionString();

            
            _repo = new FinanceRepositoryImpl(connectionString);
        }

        [Test]
        public void CreateUser_ShouldReturnTrue_WhenUserIsValid()
        {
            // Arrange
            var user = new User
            {
                Username = "testUser",
                Password = "password123",
                Email = "testuser@example.com"
            };

            // Act
            bool result = _repo.CreateUser(user);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CreateUser_ShouldThrowException_WhenUserIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _repo.CreateUser(null));
        }
    }
}