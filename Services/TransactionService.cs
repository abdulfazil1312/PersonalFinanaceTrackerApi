using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Services
{
    public class TransactionService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User ID not found in token.");
            }
            return int.Parse(userId);
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions()
        {
            var userId = GetCurrentUserId();
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Select(t => new TransactionDto // Map entity to DTO
                {
                    TransactionId = t.TransactionId,
                    Type = t.Type,
                    Amount = t.Amount,
                    Date = t.Date,
                    Note = t.Note,
                    CategoryName = t.Category.Name,
                    AccountName = t.Account.Name,
                    FromAccountName = t.FromAccount.Name,
                    ToAccountName = t.ToAccount.Name
                })
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }


        public async Task<Transaction> CreateTransaction(CreateTransactionDto transactionDto)
        {
            var userId = GetCurrentUserId();

            var transaction = new Transaction
            {
                UserId = userId,
                Type = transactionDto.Type,
                Amount = transactionDto.Amount,
                Date = transactionDto.Date,
                Note = transactionDto.Note
            };

            if (transaction.Type == TransactionType.Expense || transaction.Type == TransactionType.Income)
            {
                transaction.AccountId = transactionDto.AccountId;
                transaction.CategoryId = transactionDto.CategoryId;
            }
            else // Transfer
            {
                transaction.FromAccountId = transactionDto.FromAccountId;
                transaction.ToAccountId = transactionDto.ToAccountId;
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            var userId = GetCurrentUserId();
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId && t.UserId == userId);

            if (transaction == null)
            {
                return false; // Transaction not found or doesn't belong to the user
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        // We can add GetById and Update methods here following the same pattern.

        
    }
}
