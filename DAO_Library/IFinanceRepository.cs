using Entity_Library;

namespace DAO_Library
{
    public interface IFinanceRepository
    {
        bool CreateUser(User user);
        bool CreateExpense(Expense expense);
        bool DeleteUser(int userId);
        bool DeleteExpense(int expenseId);
        List<Expense> GetAllExpenses(int userId);
        bool UpdateExpense(int userId, Expense expense);
        List<ExpenseCategory> GetAllCategories();
    }
}



