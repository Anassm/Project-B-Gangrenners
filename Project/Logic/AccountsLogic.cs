using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    private static List<AccountModel> _accounts { get; set; } = AccountsAccess.LoadAll();

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    // Load all the accounts to the list inside logic class
    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    // Get an account by id
    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    // Check if the login is valid
    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

    public static bool CheckPassword(string password)
    {
        if (password.Length < 8)
        {
            return false;
        }

        string SpecialCharacters = "!@#$%^&*()[]?<>:;{}~/";

        bool HasUpperCase = false;
        bool HasSpecialCharacter = false;
        bool HasDigit = false;

        foreach (char Char in password)
        {
            if (char.IsUpper(Char))
            {
                HasUpperCase = true;
            }
            if (char.IsDigit(Char))
            {
                HasDigit = true;
            }
            if (SpecialCharacters.Contains(Char))
            {
                HasSpecialCharacter = true;
            }
        }

        if (HasUpperCase && HasDigit && HasSpecialCharacter)
        {
            return true;
        }
        return false;
    }

    public static bool CheckEmail(string email)
    {
        if (email == null)
        {
            return false;
        }
        if (email.Length < 5)
        {
            return false;
        }
        if (CheckEmailInUse(email))
        {
            return false;
        }
        bool HasAtSymbol = false;
        bool HasDotSymbol = false;
        if (email.Contains("@"))
        {
            HasAtSymbol = true;
        }
        int DotIndex = email.IndexOf(".");
        if (DotIndex != -1 && (DotIndex > 0 || DotIndex < email.Length-1))
        {
            HasDotSymbol = true;
        }
        if (HasAtSymbol && HasDotSymbol)
        {
            return true;
        }
        return false;
        
    }

    public static bool CheckEmailInUse(string email)
    {
        if (_accounts.Exists(i => i.EmailAddress == email))
        {
            return true;
        }
        return false;
    }

    public static bool CheckDateOfBirth(string dateofbirth)
    {
        try
            {
                DateTime dateOfBirth = DateTime.Parse(dateofbirth);
                bool isWithinValidRange = dateOfBirth <= DateTime.Now && dateOfBirth >= DateTime.Now.AddYears(-120);

                if (isWithinValidRange)
                {
                    return true;
                }
            }
            catch (FormatException)
            {
                return false;
            }
            return false;
    }

    public static bool CheckName(string name)
    {
        if (name == null)
        {
            return false;
        }
        if (name.Length < 2)
        {
            return false;
        }
        return true;
    }
    public static void AddAccount(string email, string password, string firstname, string lastname, DateTime dateofbirth)
    {
        _accounts.Add(new AccountModel(_accounts.Count+1, email, password, firstname, lastname, dateofbirth));
        AccountsAccess.WriteAll(_accounts);
    }
}