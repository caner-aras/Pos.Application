using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Merchant.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly ITransactionService transactionService;

        public TestController(ITransactionService _transactionService)
        {
            transactionService = _transactionService;
        }

        public IActionResult Index()
        {
            transactionService.GetTransactions(1);

            return null;
        }
    }
}
