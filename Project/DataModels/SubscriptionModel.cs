using System.Text.Json.Serialization;

public class SubscriptionModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userid")]
    public int UserId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("membershipnumber")]
    public int MembershipNumber { get; set; }

    [JsonPropertyName("views")]
    public int Views { get; set; }

    [JsonPropertyName("startdate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("enddate")]
    public DateTime ExpirationDate { get; set; }

    // End date isn't given but rather calculated on creation
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