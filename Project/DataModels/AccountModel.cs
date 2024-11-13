using System.Text.Json.Serialization;


public class AccountModel
{
    // Properties
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    // Constructor
    public AccountModel(int id, string emailAddress, string password, string fullName)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
    }

    public override string ToString()
    {
        return $"ID: {Id}\n" + $"E-mail address: {EmailAddress}\n" + $"Password: {Password}\n" + $"Full name: {FullName}";
    }

}




