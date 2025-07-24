using ExpenseTrackerApi.Models;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId);
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync()
        {
            var userId = GetCurrentUserId();
            var accounts = await _accountRepository.GetAccountsByUserIdAsync(userId);

            // Mapping from Entity to DTO happens in the service
            return accounts.Select(a => new AccountDto
            {
                AccountId = a.AccountId,
                Name = a.Name,
                Type = a.Type,
                Currency = a.Currency
            });
        }

        public async Task<Account> CreateAccountAsync(CreateAccountDto accountDto)
        {
            var userId = GetCurrentUserId();
            var account = new Account
            {
                UserId = userId,
                Name = accountDto.Name,
                Type = accountDto.Type,
                Currency = accountDto.Currency
            };

            return await _accountRepository.CreateAccountAsync(account);
        }
    }
}
