using MyWebAPI.Models;
using System.Text.RegularExpressions; 

namespace MyWebAPI.Helpers;

public class PasswordValidator
{
    private readonly string _password;
    private readonly List<string> _rules = new List<string>();

    public PasswordValidator(string password)
    {
        _password = password;
    }

    public PasswordValidationResult Validate()
    {
        bool isValid = true;

        // Rule 1: Minimum length
        if (_password.Length < 8)
        {
            _rules.Add("Must be at least 8 characters long.");
            isValid = false;
        }

        // Rule 2: Contains at least one uppercase letter
        if (!_password.Any(char.IsUpper))
        {
            _rules.Add("Must contain at least one uppercase letter.");
            isValid = false;
        }

        // Rule 3: Contains at least one lowercase letter
        if (!_password.Any(char.IsLower))
        {
            _rules.Add("Must contain at least one lowercase letter.");
            isValid = false;
        }

        // Rule 4: Contains at least one digit
        if (!_password.Any(char.IsDigit))
        {
            _rules.Add("Must contain at least one digit.");
            isValid = false;
        }

        // Rule 5: Contains at least one special character
        var specialChars = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
        if (!specialChars.IsMatch(_password))
        {
            _rules.Add("Must contain at least one special character.");
            isValid = false;
        }

        return new PasswordValidationResult(isValid, _rules);
    }
}