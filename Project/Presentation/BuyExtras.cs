public static class BuyExtras
{
    public static bool test = false;
    public static void ProductMenu(bool isTicket, ReservationModel reservation)
    {
        
        if (isTicket)
        {
            string StartMessage = "Choose a product category to buy from";
            string[] MenuNames = { "Food", "Drinks", "Products", "Deals","Continue to overview" };
            Action[] MenuActions =
            {
                () => DisplayFood(reservation),
                () => DisplayDrinks(reservation),
                () => DisplayProducts(reservation),
                () => DisplayDeals(reservation),
                () => BuyTicket.ReservationOverview(reservation)
            };
            SelectingMenu.MenusSelect(MenuNames, MenuActions, StartMessage);
        }
        else
        {
            string StartMessage = "Choose a product category to buy from";
            string[] MenuNames = {"Food", "Drinks", "Products", "Deals", "Go back"};
            Action[] Actions = {() => DisplayFood(reservation),() => DisplayDrinks(reservation),() => DisplayProducts(reservation), () => DisplayDeals(reservation),SeeReservations.AllReservations};
            SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
        }

    }

    public static void DisplayFood(ReservationModel reservation)
    {
        string StartMessage = "Different categories of food";
        string[] MenuNames = FoodLogic.GetCategories().ToArray().Concat(["Go back"]).ToArray();
        Action[] Actions = new Action[MenuNames.Length];
        for (int i = 0; i < MenuNames.Length-1; i++)
        {
            string category = MenuNames[i];
            Actions[i] = () => DisplayFoodByCategory(category, reservation);
        }
        Actions[MenuNames.Length-1] = () => ProductMenu(true, reservation);

        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void DisplayFoodByCategory(string category, ReservationModel reservation)
    {
        string StartMessage = "Choose a food to buy";
        string[] MenuNames = FoodLogic.GetByCategory(category).Select(x => x.ToString()).ToArray().Concat(["Go back"]).ToArray();
        Action[] Actions = new Action[MenuNames.Length];
        for (int i = 0; i < MenuNames.Length-1; i++)
        {
            string food = MenuNames[i].Split(" -")[0];
            Actions[i] = () => AddItemToReservation(FoodLogic.GetByName(food), reservation);
        }
        Actions[MenuNames.Length-1] = () => DisplayFood(reservation);

        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void DisplayDrinks(ReservationModel reservation)
    {
        string StartMessage = "Different categories of drinks";
        string[] MenuNames = DrinksLogic.GetCategories().ToArray().Concat(["Go back"]).ToArray();
        Action[] Actions = new Action[MenuNames.Length];
        for (int i = 0; i < MenuNames.Length-1; i++)
        {
            string category = MenuNames[i];
            Actions[i] = () => DisplayDrinksByCategory(category, reservation);
        }
        Actions[MenuNames.Length-1] = () => ProductMenu(true, reservation);

        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void DisplayDrinksByCategory(string category, ReservationModel reservation)
    {
        string StartMessage = "Choose a drink to buy";
        string[] MenuNames = DrinksLogic.GetByCategory(category).Select(x => x.ToString()).ToArray().Concat(["Go back"]).ToArray();
        Action[] Actions = new Action[MenuNames.Length];
        for (int i = 0; i < MenuNames.Length-1; i++)
        {
            string drink = MenuNames[i].Split(" -")[0];
            Actions[i] = () => AddItemToReservation(DrinksLogic.GetByName(drink), reservation);
        }
        Actions[MenuNames.Length-1] = () => DisplayDrinks(reservation);

        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void DisplayProducts(ReservationModel reservation)
    {
        string StartMessage = "Choose a product to buy";
        string[] MenuNames = ProductsLogic.GetAll().Select(x => x.ToString()).ToArray().Concat(["Go back"]).ToArray();
        Action[] Actions = new Action[MenuNames.Length];
        for (int i = 0; i < MenuNames.Length-1; i++)
        {
            string product = MenuNames[i].Split(" -")[0];
            Actions[i] = () => AddItemToReservation(ProductsLogic.GetByName(product), reservation);
        }
        Actions[MenuNames.Length-1] = () => ProductMenu(true, reservation);

        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void DisplayProductsByCategory(string category, ReservationModel reservation)
    {
        
    }

    public static void DisplayDeals(ReservationModel reservation)
    {
        string StartMessage = "Choose a deal to buy";
        string[] MenuNames = DealsLogic.GetAll().Select(x => x.ToString()).ToArray().Concat(["Go back"]).ToArray();
        Action[] Actions = new Action[MenuNames.Length];
        for (int i = 0; i < MenuNames.Length-1; i++)
        {
            string deal = MenuNames[i].Split(" -")[0];
            Actions[i] = () => AddItemToReservation(DealsLogic.GetByName(deal), reservation);
        }
        Actions[MenuNames.Length-1] = () => ProductMenu(true, reservation);

        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void AddItemToReservation(IItem item, ReservationModel reservation)
    {
        System.Console.WriteLine("How many of this item do you want to buy?");
        int amount = int.Parse(Console.ReadLine());
        if(OrdersLogic.GetOrderByReservationId(reservation.Id) == 0 || OrdersLogic.GetOrderByReservationId(reservation.Id) == -1)
        {
            int id = OrdersLogic.CreateOrder(reservation.Id);
            OrdersLogic.AddItemToOrder(id, item, amount);
            
        }
        else
        {
            OrdersLogic.AddItemToOrder(OrdersLogic.GetOrderByReservationId(reservation.Id), item, amount);
            
        }
        System.Console.WriteLine($"Added {amount} of {item.Name} to your order.");
        System.Console.WriteLine("Press any key to continue.");
        PresentationHelper.PressAnyToContinue(() => ProductMenu(true, reservation));
    }
}