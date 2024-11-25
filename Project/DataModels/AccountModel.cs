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

    [JsonPropertyName("firstName")]
    public string firstName { get; set; }

    [JsonPropertyName("lastName")]
    public string lastName { get; set; }

    [JsonPropertyName("dateofbirth")]
    public DateTime dateofbirth { get; set; }



    // Constructor
    public AccountModel(int id, string emailAddress, string password, string firstName, string lastName, DateTime dateofbirth)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        firstName = firstName;
        lastName = lastName;
        dateofbirth = dateofbirth;
    }
    public override string ToString()
    {
        return $"ID: {Id}\n" + $"E-mail address: {EmailAddress}\n" + $"Password: {Password}\n" + $"Full name: {firstName} {lastName}\n" + $"Date of birth: {dateofbirth}\n";
    }

}




