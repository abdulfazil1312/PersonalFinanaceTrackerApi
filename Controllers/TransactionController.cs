using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Controllers
{
    [Authorize] // Secures all endpoints
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        // The controller ONLY knows about the service. No more DbContext here.
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            // The controller just calls the service to get the data.
            var transactions = await _transactionService.GetTransactions();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTransaction(CreateTransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // The controller calls the service to create the transaction.
            var createdTransaction = await _transactionService.CreateTransaction(transactionDto);

            // We can improve this response later, but Ok is fine for now.
            return Ok(createdTransaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var success = await _transactionService.DeleteTransactionAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
