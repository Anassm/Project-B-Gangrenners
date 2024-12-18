using System.Data.Common;

public class SubscriptionLogic
{
    static private List<SubscriptionModel> _subscriptions { get; set; } = SubscriptionAccess.LoadAll();

    public static void AddSubscription(int userId)
    {
        int id = _subscriptions.Count + 1;
        string name = "Loyal";
        int membershipNumber = int.Parse(DateTime.Now.Year.ToString() + userId.ToString());
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
        if (subscription == null || subscription.ExpirationDate < DateTime.Now)
        {
            return false;
        }

        return true;
    }

}