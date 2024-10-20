using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity_Library;
using Microsoft.Data.SqlClient;
using Utility_Library;
using Exception_Library;

namespace DAO_Library
{
    public class FinanceRepositoryImpl : IFinanceRepository
    {

        private string connectionString;

        public FinanceRepositoryImpl(string connString)
        {
            connectionString = connString;
        }


        // Method to create a user in the database
        public bool CreateUser(User user)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connectionString))
            {
                string query = "INSERT INTO Users (UserName, Password, Email) VALUES (@username, @password, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@Email", user.Email);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Method to create an expense for a user in the database
        public bool CreateExpense(Expense expense)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connectionString))
            {
                string query = "INSERT INTO Expenses (UserId, Amount, CategoryId, Date, Description) VALUES (@userId, @amount, @categoryId, @date, @description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", expense.UserId);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@categoryId", expense.CategoryId);
                cmd.Parameters.AddWithValue("@date", expense.Date);
                cmd.Parameters.AddWithValue("@description", expense.Description);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Method to delete a user from the database
        public bool DeleteUser(int userId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE UserId = @userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Method to delete an expense from the database
        public bool DeleteExpense(int expenseId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connectionString))
            {
                string query = "DELETE FROM Expenses WHERE ExpenseID = @expenseId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@expenseId", expenseId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Method to get all expenses for a specific user
        public List<Expense> GetAllExpenses(int userId)
        {
            List<Expense> expenses = new List<Expense>();

            using (SqlConnection conn = DBConnUtil.GetConnection(connectionString))
            {
                string query = "SELECT * FROM Expenses WHERE UserId = @userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Expense expense = new Expense
                        {
                            ExpenseId = reader.GetInt32(reader.GetOrdinal("ExpenseID")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                            CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Description = reader.GetString(reader.GetOrdinal("Description"))
                        };
                        expenses.Add(expense);
                    }
                }
            }

            if (expenses.Count == 0)
            {
                throw new ExpenseNotFoundException("No expenses found for the user.");
            }

            return expenses;
        }

        // Method to update an expense for a specific user
        public bool UpdateExpense(int userId, Expense expense)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connectionString))
            {
                string query = "UPDATE Expenses SET amount = @Amount, CategoryId = @categoryId, Date = @date, Description = @description " +
                               "WHERE ExpenseID = @expenseId AND UserId = @userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@categoryId", expense.CategoryId);
                cmd.Parameters.AddWithValue("@date", expense.Date);
                cmd.Parameters.AddWithValue("@description", expense.Description);
                cmd.Parameters.AddWithValue("@expenseId", expense.ExpenseId);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Method to retrieve all categories from the ExpenseCategories table
        public List<ExpenseCategory> GetAllCategories()
        {
            List<ExpenseCategory> categories = new List<ExpenseCategory>();

            using (SqlConnection conn = DBConnUtil.GetConnection(connectionString))
            {
                string query = "SELECT * FROM ExpenseCategories";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ExpenseCategory category = new ExpenseCategory
                        {
                            CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName"))
                        };
                        categories.Add(category);
                    }
                }
            }

            return categories;
        }
    }
}
