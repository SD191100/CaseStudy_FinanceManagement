using DAO_Library;
using Entity_Library;
using Exception_Library;
using Utility_Library;

namespace FinanceManagement.Tests
{
    [TestFixture]
    public class ExpenseTests
    {
        private IFinanceRepository _repo;

        [SetUp]
        public void Setup()
        {
            string connectionString = DBPropertyUtil.GetConnectionString("db.properties");
            _repo = new FinanceRepositoryImpl(connectionString);
        }

        [Test]
        public void CreateExpense_ShouldReturnTrue_WhenExpenseIsValid()
        {
            // Arrange
            var expense = new Expense
            {
                UserId = 1,
                Amount = 100.50m,
                CategoryId = 1,
                Date = DateTime.Now,
                Description = "Test expense"
            };

            // Act
            bool result = _repo.CreateExpense(expense);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CreateExpense_ShouldThrowException_WhenExpenseIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _repo.CreateExpense(null));
        }

        [Test]
        public void GetAllExpenses_ShouldReturnList_WhenUserIdIsValid()
        {
            // Act
            var expenses = _repo.GetAllExpenses(1);

            // Assert
            Assert.IsNotNull(expenses);
            Assert.IsNotEmpty(expenses);
        }

        [Test]
        public void GetAllExpenses_ShouldThrowUserNotFoundException_WhenUserIdIsInvalid()
        {
            // Act & Assert
            Assert.Throws<UserNotFoundException>(() => _repo.GetAllExpenses(-1));
        }

        [Test]
        public void DeleteExpense_ShouldReturnTrue_WhenExpenseIdIsValid()
        {
            // Act
            bool result = _repo.DeleteExpense(1); // Assuming expenseId 1 exists

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteExpense_ShouldThrowExpenseNotFoundException_WhenExpenseIdIsInvalid()
        {
            // Act & Assert
            Assert.Throws<ExpenseNotFoundException>(() => _repo.DeleteExpense(-1));
        }
    }
}
