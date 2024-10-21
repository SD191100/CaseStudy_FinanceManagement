using DAO_Library;
using Entity_Library;
using Exception_Library;
using Utility_Library;

internal class Program
{
    private static void Main(string[] args)
    {

        string connectionString = DBPropertyUtil.GetConnectionString();

        IFinanceRepository financeRepository = new FinanceRepositoryImpl(connectionString);
        bool exit = false;

        while (!exit)
        {
            // Display menu options to the user
            Console.WriteLine("\n--- Finance Management System ---");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. Delete User");
            Console.WriteLine("4. Delete Expense");
            Console.WriteLine("5. Update Expense");
            Console.WriteLine("6. View All Expenses for a User");
            Console.WriteLine("7. View Expense Categories");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddUser(financeRepository);
                    break;
                case "2":
                    AddExpense(financeRepository);
                    break;
                case "3":
                    DeleteUser(financeRepository);
                    break;
                case "4":
                    DeleteExpense(financeRepository);
                    break;
                case "5":
                    UpdateExpense(financeRepository);
                    break;
                case "6":
                    ViewAllExpenses(financeRepository);
                    break;
                case "7":  // Handle Option 7 - View Categories
                    ViewCategories(financeRepository);
                    break;
                case "8":
                    exit = true;
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    // Method to add a new user
    static void AddUser(IFinanceRepository financeRepository)
    {
        try
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            User newUser = new User { Username = username, Password = password, Email = email };
            if (financeRepository.CreateUser(newUser))
            {
                Console.WriteLine("User added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add user.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding user: {ex.Message}");
        }
    }

    // Method to add a new expense
    static void AddExpense(IFinanceRepository financeRepository)
    {
        try
        {
            Console.Write("Enter user ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter expense amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter category ID: ");
            int categoryId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter date (yyyy-mm-dd): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Expense newExpense = new Expense
            {
                UserId = userId,
                Amount = amount,
                CategoryId = categoryId,
                Date = date,
                Description = description
            };

            if (financeRepository.CreateExpense(newExpense))
            {
                Console.WriteLine("Expense added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add expense.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding expense: {ex.Message}");
        }
    }

    // Method to delete a user
    static void DeleteUser(IFinanceRepository financeRepository)
    {
        try
        {
            Console.Write("Enter user ID to delete: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            if (financeRepository.DeleteUser(userId))
            {
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete user. Make sure the user ID is correct.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user: {ex.Message}");
        }
    }

    // Method to delete an expense
    static void DeleteExpense(IFinanceRepository financeRepository)
    {
        try
        {
            Console.Write("Enter expense ID to delete: ");
            int expenseId = Convert.ToInt32(Console.ReadLine());

            if (financeRepository.DeleteExpense(expenseId))
            {
                Console.WriteLine("Expense deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete expense. Make sure the expense ID is correct.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting expense: {ex.Message}");
        }
    }

    // Method to update an existing expense
    static void UpdateExpense(IFinanceRepository financeRepository)
    {
        try
        {
            Console.Write("Enter user ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter expense ID to update: ");
            int expenseId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter updated amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter updated category ID: ");
            int categoryId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter updated date (yyyy-mm-dd): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter updated description: ");
            string description = Console.ReadLine();

            Expense updatedExpense = new Expense
            {
                ExpenseId = expenseId,
                UserId = userId,
                Amount = amount,
                CategoryId = categoryId,
                Date = date,
                Description = description
            };

            if (financeRepository.UpdateExpense(userId, updatedExpense))
            {
                Console.WriteLine("Expense updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update expense.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating expense: {ex.Message}");
        }
    }

    // Method to view all expenses for a specific user
    static void ViewAllExpenses(IFinanceRepository financeRepository)
    {
        try
        {
            Console.Write("Enter user ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            List<Expense> expenses = financeRepository.GetAllExpenses(userId);

            Console.WriteLine($"\nExpenses for User ID: {userId}");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"Expense ID: {expense.ExpenseId}, Amount: {expense.Amount}, Category ID: {expense.CategoryId}, Date: {expense.Date.ToShortDateString()}, Description: {expense.Description}");
            }
        }
        catch (ExpenseNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving expenses: {ex.Message}");
        }
    }

    // Method to view categories in the Main program
    static void ViewCategories(IFinanceRepository financeRepository)
    {
        try
        {
            List<ExpenseCategory> categories = financeRepository.GetAllCategories();

            if (categories.Count == 0)
            {
                Console.WriteLine("No categories found.");
            }
            else
            {
                Console.WriteLine("\n--- Expense Categories ---");
                foreach (var category in categories)
                {
                    Console.WriteLine($"Category ID: {category.CategoryId}, Name: {category.CategoryName}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving categories: {ex.Message}");
        }
    }
}