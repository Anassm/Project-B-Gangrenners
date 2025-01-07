using System.Data.Common;

public class SubscriptionLogic
{
    static private List<SubscriptionModel> _subscriptions { get; set; } = SubscriptionAccess.LoadAll();

    public static void AddSubscription(int userId)
    {
        int id = _subscriptions.Count + 1;
        string name = "Loyal";
        int membershipNumber = int.Parse(DateTime.Now.Year.ToString() + id.ToString());
        int views = 15;
        DateTime startDate = DateTime.Now;

        SubscriptionModel subscription = new SubscriptionModel(id, userId, name, membershipNumber, views, startDate);

        _subscriptions.Add(subscription);
        SubscriptionAccess.WriteAll(_subscriptions);
    }

    public static bool CancelSubscription(int userId)
    {
        SubscriptionModel? subscription = _subscriptions.Find(sub => sub.UserId == userId);
        if (subscription == null)
        {
            return false;
        }

        subscription.ExpirationDate = subscription.RenewalDate;
        subscription.RenewalDate = null;

        SubscriptionAccess.WriteAll(_subscriptions);

        return true;
    }

    public static bool IsSubscribed(int userId)
    {
        SubscriptionModel? subscription = _subscriptions.Find(sub => sub.UserId == userId);
        if (subscription != null && subscription.RenewalDate > DateTime.Now)
        {
            return true;
        }

        return false;
    }

    public static bool IsSubscriptionCancelledButValid(int userId)
    {
        SubscriptionModel? subscription = _subscriptions.Find(sub => sub.UserId == userId);
        if (subscription != null && subscription.ExpirationDate > DateTime.Now && subscription.RenewalDate == null)
        {
            return true;
        }

        return false;
    }

    public static SubscriptionModel? GetUserSubscription(int userId)
    {
        SubscriptionModel? subscription = _subscriptions.Find(sub => sub.UserId == userId);
        if (subscription == null)
        {
            return null;
        }

        return subscription;
    }

    public static int useView(int userId)
    {
        SubscriptionModel? subscription = _subscriptions.Find(sub => sub.UserId == userId);
        if (subscription != null && subscription.Views > 0)
        {
            subscription.Views--;
            SubscriptionAccess.WriteAll(_subscriptions);

            return 0;
        }
        else if (subscription != null && subscription.Views <= 0)
        {
            return -1;
        }

        return -2;
    }
}