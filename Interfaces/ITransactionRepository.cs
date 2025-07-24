using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int userId);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionByIdAndUserIdAsync(int transactionId, int userId);
        Task<bool> DeleteTransactionAsync(Transaction transaction);
    }
}
