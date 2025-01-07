public static class Subscription
{

    // TODO: Fix IsSubscribed() and conditional rendering of ManageMenu(), removing the "opt out of subscription" option when needed
    // TODO: Make sure methods dont immediately jump to FollowUp() method

    public static void ManageMenu()
    {
        string[] MenuNames;
        Action[] Actions;

        if (SubscriptionLogic.IsSubscribed(AccountsLogic.CurrentAccount.Id))
        {
            MenuNames = ["Subscription status", "Opt out of subscription", "Go back to main menu"];
            Actions = [Info, CancelSubscription, Menu.MainMenu];
        }
        else
        {
            MenuNames = ["Subscription status", "Go back to main menu"];
            Actions = [Info, Menu.MainMenu];
        }

        SelectingMenu.MenusSelect(MenuNames, Actions);

    }

    public static void Info()
    {
        int userId = AccountsLogic.CurrentAccount.Id;
        SubscriptionModel subscription = SubscriptionLogic.GetUserSubscription(userId);
        string RenewalOrEndDate = SubscriptionLogic.IsSubscribed(userId) ? $"renewal date: {subscription.RenewalDate}" : $"end date: {subscription.ExpirationDate}";

        Console.WriteLine("You have an active subscription:");
        Console.WriteLine($"Movie usages left: {subscription.Views}");
        Console.WriteLine($"Member code: {subscription.MembershipNumber}");
        Console.WriteLine($"Subscription start date: {subscription.StartDate}");
        Console.WriteLine($"Subscription {RenewalOrEndDate}");

        FollowUp();
    }

    public static void CancelSubscription()
    {
        bool confirm = SelectingMenu.YesNoSelect($"Are you sure you want to cancel your subscription? \nIt will remain active until xxx. \nNOTE: The purchaser acknowledges and agrees this subscription is non-refundable under any circumstances.");

        if (confirm)
        {
            SubscriptionLogic.CancelSubscription(AccountsLogic.CurrentAccount.Id);
            Menu.MainMenu();
        }

        ManageMenu();
    }

    public static void FollowUp()
    {
        string[] MenuNames = { "Go back to manage menu", "Go back to main menu" };
        Action[] Actions = { ManageMenu, Menu.MainMenu };
        SelectingMenu.MenusSelect(MenuNames, Actions);
    }
}