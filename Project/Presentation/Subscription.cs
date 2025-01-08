public static class Subscription
{
    public static void ManageMenu()
    {
        string[] MenuNames = [];
        Action[] Actions = [];

        if (SubscriptionLogic.IsSubscribed(AccountsLogic.CurrentAccount.Id))
        {
            MenuNames = ["Subscription status", "Subscription terms and services", "Cancel subscription", "Go back to main menu"];
            Actions = [Info, TermsAndServices, CancelSubscription, Menu.MainMenu];
        }
        else if (SubscriptionLogic.IsSubscriptionCancelledButValid(AccountsLogic.CurrentAccount.Id))
        {
            MenuNames = ["Subscription status", "Subscription terms and services", "Go back to main menu"];
            Actions = [Info, TermsAndServices, Menu.MainMenu];
        }

        SelectingMenu.MenusSelect(MenuNames, Actions, "");

    }

    public static void Info()
    {
        PresentationHelper.ClearConsole();

        int userId = AccountsLogic.CurrentAccount.Id;
        SubscriptionModel subscription = SubscriptionLogic.GetUserSubscription(userId);
        string RenewalOrEndDate = SubscriptionLogic.IsSubscribed(userId) ? $"renewal date: {subscription.RenewalDate}" : $"end date: {subscription.ExpirationDate}";

        Console.WriteLine("You have a valid subscription:");
        Console.WriteLine("");

        Console.WriteLine($"Free movies left: {subscription.Views}");
        Console.WriteLine($"Member code: {subscription.MembershipNumber}");
        Console.WriteLine($"Subscription start date: {subscription.StartDate}");
        Console.WriteLine($"Subscription {RenewalOrEndDate}");

        Console.WriteLine("");
        Console.WriteLine("Press any key to go back");

        PresentationHelper.PressAnyToContinue(ManageMenu);
    }

    public static void TermsAndServices()
    {
        PresentationHelper.ClearConsole();

        Console.WriteLine("Terms and services");
        Console.WriteLine("");

        Console.WriteLine("1. The purchaser acknowledges that the subscription is valid for one year from the purchase date and will renew automatically.");
        Console.WriteLine("2. The purchaser acknowledges that upon cancellation, the cancellation date will align with the renewal date.");
        Console.WriteLine("3. The purchaser acknowledges and agrees this subscription is non-refundable under any circumstances.");
        Console.WriteLine("4. The purchaser acknowledges and agrees to the cheapest seats being assigned when using the membership codes, for example when using in bulk.");
        Console.WriteLine("5. The purchaser acknowledges and agrees that any misuse of the subscription may result in the loss of access without compensation.");
        Console.WriteLine("6. The purchaser acknowledges and agrees that changes to the Terms of Service may occur and will be communicated in advance.");

        Console.WriteLine("");


        if (SubscriptionLogic.IsSubscribed(AccountsLogic.CurrentAccount.Id) || SubscriptionLogic.IsSubscriptionCancelledButValid(AccountsLogic.CurrentAccount.Id))
        {
            Console.WriteLine("Press any key to go back");
            PresentationHelper.PressAnyToContinue(ManageMenu);
        }
    }

    public static int TermsAndServicesAgreement()
    {
        TermsAndServices();

        if (SelectingMenu.YesNoSelect(
            
            """
            Terms and services

            1. The purchaser acknowledges that the subscription is valid for one year from the purchase date and will renew automatically.
            2. The purchaser acknowledges that upon cancellation, the cancellation date will align with the renewal date.
            3. The purchaser acknowledges and agrees this subscription is non-refundable under any circumstances.
            4. The purchaser acknowledges and agrees to comply with all applicable laws and regulations when using the subscription.
            5. The purchaser acknowledges and agrees that any misuse of the subscription may result in the loss of access without compensation.
            6. The purchaser acknowledges and agrees that changes to the Terms of Service may occur and will be communicated in advance.

            Do you agree to the terms and services?
            """
        ) == false)
        {
            return 0;
        }

        return 1;
    }

    public static void CancelSubscription()
    {
        SubscriptionModel subscription = SubscriptionLogic.GetUserSubscription(AccountsLogic.CurrentAccount.Id);
        DateTime? conditionalText = SubscriptionLogic.IsSubscribed(AccountsLogic.CurrentAccount.Id) ? subscription.RenewalDate : subscription.ExpirationDate;
        bool confirm = SelectingMenu.YesNoSelect($"Are you sure you want to cancel your subscription? \nIt will remain active until {conditionalText}. \nNOTE: As stated in line 3 of the Terms of Service, the purchaser acknowledges and agrees that this subscription is non-refundable under any circumstances.");

        if (confirm)
        {
            SubscriptionLogic.CancelSubscription(AccountsLogic.CurrentAccount.Id);
            ManageMenu();
        }

        ManageMenu();
    }
}