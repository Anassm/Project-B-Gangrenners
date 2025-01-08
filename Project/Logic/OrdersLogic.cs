using System.Data.Common;
using System.Text;
using System.Transactions;

public class OrdersLogic
{
    private static List<OrderModel> _orders { get; set; } = OrdersAccess.LoadAll();

    public static void UpdateOrders(OrderModel order)
    {
        int index = _orders.FindIndex(o => o.Id == order.Id);

        if (index != -1)
        {
            // Update existing order
            _orders[index] = order;
        }
        else
        {
            // Add a new order
            _orders.Add(order);
        }

        // Save changes to the data layer
        OrdersAccess.WriteAll(_orders);
    }

    public static int CreateOrder(int reservationId)
    {
        var order = new OrderModel(GetId(), reservationId, GenerateUniqueTakeawayCode());
        UpdateOrders(order);
        return order.Id;
    }

    public static string GenerateUniqueTakeawayCode()
    {
        return Guid.NewGuid().ToString().Substring(0, 8);
    }

    public static int GetId()
    {
        return _orders.Count > 0 ? _orders.Max(x => x.Id) + 1 : 1;
    }

    public static OrderModel? GetOrderById(int id)
    {
        return _orders.FirstOrDefault(x => x.Id == id);
    }   

    public static bool AddItemToOrder(int orderId, IItem item, int quantity)
    {  

        var CurrentOrder = GetOrderById(orderId);

        if (CurrentOrder == null)
        {
            return false;
        }

        if (CurrentOrder.Items == null)
        {

            CurrentOrder.Items = new List<(IItem item, int quantity)>();
        }
        else
        {

            var existingItem = CurrentOrder.Items.FirstOrDefault(x => x.item.Id == item.Id && x.item.FileName == item.FileName);
            if (existingItem.item != null)
            {
                CurrentOrder.Items.Remove(existingItem);
                quantity += existingItem.quantity;
            }
        }


        CurrentOrder.Items.Add((item, quantity));

        if(!UpdateItemReference(orderId, item, quantity))
        {
            return false;
        }

        CurrentOrder.TotalPrice = GetTotalPrice(orderId);

        UpdateOrders(CurrentOrder);
        return true;

    }

    public static bool UpdateItemReference(int orderId, IItem item, int quantity)
    {   
        OrderModel? currentOrder = GetOrderById(orderId);

        if (currentOrder == null)
        {
            return false;
        }

        if (currentOrder.ItemReferences == null)
        {
            currentOrder.ItemReferences = new List<(int itemId, string fileName, int quantity)>();
        }
        var existingReference = currentOrder.ItemReferences.FirstOrDefault(i => i.itemId == item.Id && i.fileName == item.FileName);

        if (existingReference != default)
        {
            // Update the quantity of an existing item
            currentOrder.ItemReferences.Remove(existingReference);
            currentOrder.ItemReferences.Add((item.Id, item.FileName, existingReference.quantity + quantity));
        }
        else
        {
            // Add a new item to the order
            currentOrder.ItemReferences.Add((item.Id, item.FileName, quantity));
        }

        return true;
    }
    

    public static double GetTotalPrice(int orderId)
    {   
        var CurrentOrder = GetOrderById(orderId);

        if (CurrentOrder == null)
        {
            return 0;
        }

        return GetTotalPrice(CurrentOrder);
    }

    public static double GetTotalPrice(OrderModel order)
    {
        return order.Items.Sum(x => x.item.Price * x.quantity);
    }

    private static IItem? LoadItemById(string fileName, int id)
    {
        return fileName switch
        {
            "food" => FoodAccess.LoadAll().FirstOrDefault(item => item.Id == id),
            "drinks" => DrinksAccess.LoadAll().FirstOrDefault(item => item.Id == id),
            "products" => ProductsAccess.LoadAll().FirstOrDefault(item => item.Id == id),
            "deals" => DealsAccess.LoadAll().FirstOrDefault(item => item.Id == id),
            _ => null
        };
    }

    public static void LoadFullItems(OrderModel order)
    {
        order.Items.Clear();

        foreach (var reference in order.ItemReferences)
        {
            IItem? item = LoadItemById(reference.fileName, reference.itemId);
            
            if (item != null)
            {
                order.Items.Add((item, reference.quantity));
            }
        }

        GetTotalPrice(order);

        UpdateOrders(order);
    }

    public static int GetOrderByReservationId(int reservationId)
    {
        var order = _orders.FirstOrDefault(x => x.ReservationId == reservationId);
        return order != null ? order.Id : -1;
    }

    public static string GetProductString(int orderId)
    {
        var order = GetOrderById(orderId);

        if (order == null)
        {
            return "Order not found";
        }

        LoadFullItems(order);
    
        var sb = new StringBuilder();

        foreach (var item in order.Items)
        {
            sb.AppendLine($"{item.item.Name} x{item.quantity}");
        }

        return sb.ToString();
    }



    
}