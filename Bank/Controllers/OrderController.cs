using Microsoft.AspNetCore.Mvc;
using Bank.Models;

#nullable disable

namespace Bank.Controllers
{
    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private BankContext _db;
        public OrderController(BankContext context, ILogger<OrderController> logger)
        {
            _logger = logger;
            _db = context;
        }

        [HttpPost]
        [Route("create-order")]
        public string CreateOrder(Order order)
        {
            Card card = _db.Cards.FirstOrDefault(card => card.CardId == order.CardId);

            if (card == null || order.DebitAmount < 0)
            {
                order.ResultStatus = Status.Failure;

                return $"Process is failure.";
            }

            order.ResultStatus = Status.Process;

            _db.Orders.Add(order);

            // имитируем нагрузку/задержку
            Thread.Sleep(GetRandomMillisecond());

            if (order.DebitAmount < 1000)
            {
                order.ResultStatus = Status.Failure;
            }
            else
            {
                order.ResultStatus = Status.Success;
                card.Score -= order.DebitAmount;
            }

            _db.SaveChanges();

            _logger.LogInformation($"DebitAmout: {order.DebitAmount} Score: {card.Score} Status order: {order.ResultStatus}.");

            return "Process is succes.";
        }

        [HttpGet]
        [Route("get-status/{id}")]
        public string GetStatus(int id)
        {
            Order order = _db.Orders.FirstOrDefault(order => order.OrderId == id);

            if (order == null)
            {
                return $"Order does not exit. {Status.Failure}.";
            }

            _logger.LogInformation($"Status order for id={id}: {order.ResultStatus}.");

            return $"{order.ResultStatus}";
        }

        private int GetRandomMillisecond()
        {
            Random random = new Random();
            return random.Next(2, 7) * 1000;
        }
    }
}