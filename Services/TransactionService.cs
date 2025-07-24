using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Interfaces;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionService(ITransactionRepository transactionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _transactionRepository = transactionRepository;
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

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync()
        {
            var userId = GetCurrentUserId();
            var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId);

            return transactions.Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                Type = t.Type,
                Amount = t.Amount,
                Date = t.Date,
                Note = t.Note,
                CategoryName = t.Category?.Name,
                AccountName = t.Account?.Name,
                FromAccountName = t.FromAccount?.Name,
                ToAccountName = t.ToAccount?.Name
            });
        }

        public async Task<Transaction> CreateTransactionAsync(CreateTransactionDto transactionDto)
        {
            var userId = GetCurrentUserId();
            var transaction = new Transaction
            {
                UserId = userId,
                Type = transactionDto.Type,
                Amount = transactionDto.Amount,
                Date = transactionDto.Date,
                Note = transactionDto.Note,
                AccountId = transactionDto.AccountId,
                CategoryId = transactionDto.CategoryId,
                FromAccountId = transactionDto.FromAccountId,
                ToAccountId = transactionDto.ToAccountId
            };

            return await _transactionRepository.CreateTransactionAsync(transaction);
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            var userId = GetCurrentUserId();
            var transaction = await _transactionRepository.GetTransactionByIdAndUserIdAsync(transactionId, userId);

            if (transaction == null)
            {
                return false;
            }

            return await _transactionRepository.DeleteTransactionAsync(transaction);
        }
    }
}
