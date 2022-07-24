using Bank.Models;

#nullable disable

namespace Store
{
    public class Program
    {
        public static void Main()
        {
            StoreClient storeClient = new StoreClient();

            storeClient.CreateOrderPost(new Order { OrderId = 1, CardId = 1, DebitAmount = 1000.0m });
            storeClient.GetStatusPost(1);

            storeClient.CreateOrderPost(new Order { OrderId = 2, CardId = 1, DebitAmount = 1500.0m });
            storeClient.GetStatusPost(2);

            storeClient.CreateOrderPost(new Order { OrderId = 3, CardId = 2, DebitAmount = 2000.0m });
            storeClient.GetStatusPost(3);

            // карты id: 5 не существует
            storeClient.CreateOrderPost(new Order { OrderId = 4, CardId = 5, DebitAmount = 2000.0m });
            storeClient.GetStatusPost(4);

            // пробуем снять меньше 1000$
            storeClient.CreateOrderPost(new Order { OrderId = 5, CardId = 3, DebitAmount = 500.0m });
            storeClient.GetStatusPost(5);

            // пробуем снять -1000$
            storeClient.CreateOrderPost(new Order { OrderId = 6, CardId = 3, DebitAmount = -1000.0m });
            storeClient.GetStatusPost(6);
        }
    }
}
