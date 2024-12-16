using System.Text.Json.Serialization;

public class SubscriptionModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userid")]
    public int UserId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("membershipNumber")]
    public int MembershipNumber { get; set; }

    [JsonPropertyName("views")]
    public int Views { get; set; }

    [JsonPropertyName("StartDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("expirationDate")]
    public DateTime ExpirationDate { get; set; }

    // Default Constructor, end date isn't given but calculated on creation
    public SubscriptionModel(int id, int userid, string name, int membershipNumber, int views, DateTime startDate)
    {
        Id = id;
        UserId = userid;
        Name = name;
        MembershipNumber = membershipNumber;
        Views = views;
        StartDate = startDate;
        ExpirationDate = startDate.AddYears(1); // Sub duration is 1 year
    }

}