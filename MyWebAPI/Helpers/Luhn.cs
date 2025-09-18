namespace MyWebAPI.Helpers;
public static class Luhn
{
    public static bool Validate(string cardNumber)
    {
        // Remove spaces and dashes
        string cleanedNumber = new string(cardNumber.Where(char.IsDigit).ToArray());

        int sum = 0;
        bool isSecond = false;
        for (int i = cleanedNumber.Length - 1; i >= 0; i--)
        {
            int digit = cleanedNumber[i] - '0';
            if (isSecond)
            {
                digit *= 2;
            }
            sum += digit / 10;
            sum += digit % 10;
            isSecond = !isSecond;
        }
        return (sum % 10 == 0);
    }
}
