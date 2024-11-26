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
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("dateofbirth")]
    public DateTime Dateofbirth { get; set; }



    // Constructor
    public AccountModel(int id, string emailAddress, string password, string firstName, string lastName, DateTime dateofbirth)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Dateofbirth = dateofbirth;
    }
    public override string ToString()
    {
        return $"ID: {Id}\n" + $"E-mail address: {EmailAddress}\n" + $"Password: {Password}\n" + $"Full name: {FirstName} {LastName}\n" + $"Date of birth: {Dateofbirth}\n";
    }

}




