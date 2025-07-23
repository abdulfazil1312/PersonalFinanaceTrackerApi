using ExpenseTrackerApi.Models;

namespace ExpenseTrackerAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountsByUserIdAsync(int userId);
        Task<Account> CreateAccountAsync(Account account);
    }
}
