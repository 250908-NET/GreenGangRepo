var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
/*
Challenge 1: Basic Calculator
- Create endpoint `/calculator/add/{a}/{b}` that returns sum of two numbers
- Add endpoints for subtract, multiply, and divide
- Handle division by zero with proper error messages
- Return results as JSON: `{"operation": "add", "result": 15}`
*/

app.MapGet("/calculator/add/{a}/{b}", (double a, double b) =>
{
    return Results.Ok(new {operation = "add", a = a, b = b, result = a + b});
});

app.MapGet("/calculator/subtract/{a}/{b}", (double a, double b) =>
{
    return Results.Ok(new { operation = "subtract", a = a, b = b, result = a - b });
});

app.MapGet("/calculator/multiply/{a}/{b}", (double a, double b) =>
{
    return Results.Ok(new {operation = "multiply", a = a, b = b, result = a * b});
});

app.MapGet("/calculator/divide/{a}/{b}", (double a, double b) =>
{
    if (b == 0)
    {
        return Results.BadRequest(new { error = "Divisor is zero" });
    }

    return Results.Ok(new { operation = "divide", a = a, b = b, result = a / b });
});


/*
Challenge 2: String Manipulator
- Create `/text/reverse/{text}` - returns reversed string
- Add `/text/uppercase/{text}` and `/text/lowercase/{text}`
- Create `/text/count/{text}` - returns character count, word count, vowel count
- Add `/text/palindrome/{text}` - checks if text is a palindrome
*/
app.MapGet("/text/reverse/{text}", (string text) =>
{
    string reversedText = new string(text.Reverse().ToArray());
    return Results.Ok(new { result = reversedText });
});

app.MapGet("/text/uppercase/{text}", (string text) =>
{
    string upperText = new string(text.ToUpper());
    return Results.Ok(new { result = upperText });
});

app.MapGet("/text/lowercase/{text}", (string text) =>
{
    string lowerText = new string(text.ToLower());
    return Results.Ok(new { result = lowerText });
});

app.MapGet("/text/count/{text}", (string text) =>
{
    // Need to fix edge cases
    int characterCount = text.Length;
    int wordCount = text.Split(" ").Length;
    int vowelCount = text.Count(c => "aeiouAEIOU".Contains(c));

    return Results.Ok(new { numOfCharacters = characterCount, numOfWords = wordCount, numOfVowels = vowelCount });

});

app.MapGet("/text/palindrome/{text}", (string text) =>
{
    string reversedText = new string(text.Reverse().ToArray());
    bool isPalindrome = reversedText.ToLower() == text.ToLower();
    return Results.Ok(new { isPalindrome = isPalindrome });

});

/*
Challenge 3: Number Games
- Create `/numbers/fizzbuzz/{count}` - returns FizzBuzz sequence up to count
- Add `/numbers/prime/{number}` - checks if number is prime
- Create `/numbers/fibonacci/{count}` - returns first N Fibonacci numbers
- Add `/numbers/factors/{number}` - returns all factors of a number
*/
app.MapGet("/numbers/fizzbuzz/{count}", (int count) =>
{
    var list = Enumerable.Range(1, count).Select(
        n => n % 15 == 0 ? "FizzBuzz" :
        n % 3 == 0 ? "Fizz" :
        n % 5 == 0 ? "Buzz" : n.ToString());
    return Results.Ok(list);
});

app.MapGet("/numbers/prime/{number}", (int number) =>
{
    if (number <= 1) return Results.Ok(new { isPrime = false });

    bool isPrime = true;
    for (int i = 2; i <= Math.Sqrt(number); i++)
    {
        if (number % i == 0)
        {
            isPrime = false;
            break;
        }
    }
    return Results.Ok(new { isPrime });
});

app.MapGet("/numbers/fibonacci/{count}", (int count) =>
{
    var fibonacci = new List<int>();
    int a = 0;
    int b = 1;
    for (int i = 0; i < count; i++)
    {
        fibonacci.Add(a);
        int temp = a + b;
        a = b;
        b = temp;
    }
    return Results.Ok(new { fibonacci });
});

app.MapGet("/numbers/factors/{number}", (int number) =>
{
    var factors = new List<int>();
    for (int i = 1; i <= number; i++)
    {
        if (number % i == 0)
            factors.Add(i);
    }

    return Results.Ok(new { factors });
});

/*
## Challenge 4: Date and Time Fun
- Create `/date/today` - returns current date in different formats
- Add `/date/age/{birthYear}` - calculates age from birth year
- Create `/date/daysbetween/{date1}/{date2}` - calculates days between dates
- Add `/date/weekday/{date}` - returns day of week for given date
*/

app.MapGet("/date/today/", () =>
{
    DateTime todayDate = DateTime.Now;
    return Results.Ok(new
    {
        date1 = todayDate.ToString("MM/dd/yyyy"),
        date2 = todayDate.ToString("dddd, dd MMMM yyyy"),
        date3 = todayDate.ToString("MMMM dd")
    });
});

app.MapGet("/date/age/{birthYear}", (int birthYear) =>
{
    //Check if birthYear is valid

    DateTime todayDate = DateTime.Now;

    if (birthYear > todayDate.Year)
    {
        return Results.BadRequest(new { error = "You cannot be born tomorrow" });
    }
    int age = todayDate.Year - birthYear;

    return Results.Ok(new { age = age });

});

app.MapGet("/date/daysbetween/{date1}/{date2}", (string date1, string date2) =>
{
    // https://stackoverflow.com/questions/16075159/check-if-a-string-is-a-valid-date-using-datetime-tryparse
    if ((DateTime.TryParse(date1, out DateTime dateOut1)) && (DateTime.TryParse(date2, out DateTime dateOut2)))
    {
        double daysBetween = Math.Abs((dateOut1 - dateOut2).TotalDays);
        return Results.Ok(new { daysBetween = daysBetween });
    }

    return Results.BadRequest(new { error = "Wrong date format" });

});

app.MapGet("/date/weekday/{date}", (string date) =>
{
    if (DateTime.TryParse(date, out DateTime dateOut))
    {
        string weekday = dateOut.ToString("ddd");
        return Results.Ok(new { weekday = weekday });
    }
    return Results.BadRequest(new { error = "Wrong date format" });
});

/*
## Challenge 5: Simple Collections
**Goal**: Practice working with lists and basic LINQ
- Create `/colors` endpoint that returns a predefined list of favorite colors
- Add `/colors/random` - returns a random color from the list
- Create `/colors/search/{letter}` - returns colors starting with that letter
- Add `/colors/add/{color}` (POST) - adds new color to the list
*/

var colors = new List<string> { "Red", "Green", "Blue", "Yellow", "Purple", "Orange" };

app.MapGet("/colors", () =>
{
    return Results.Ok(new { colors });
});

app.MapGet("/colors/random", () =>
{
    string randomColor = colors[new Random().Next(colors.Count)];
    return Results.Ok(new { color = randomColor });
});

app.MapGet("/colors/search/{letter}", (char letter) =>
{
    var found = new List<string>();
    for (int i = 0; i < colors.Count; i++)
    {
        if (colors[i][0] == char.ToUpper(letter))
        {
            found.Add(colors[i]);
        }
    }
    return Results.Ok(new { colors = found });
   
});

app.MapPost("/colors/add/{color}", (string color) =>
{
    if (!colors.Contains(char.ToUpper(color[0]) + color.Substring(1).ToLower()))
    {
        colors.Add(char.ToUpper(color[0]) + color.Substring(1).ToLower());
    }

    return Results.Ok(new { colors });
});

/*
## Challenge 6: Temperature Converter
**Goal**: Practice calculations and different data formats
- Create `/temp/celsius-to-fahrenheit/{temp}` 
- Add `/temp/fahrenheit-to-celsius/{temp}`
- Create `/temp/kelvin-to-celsius/{temp}` and reverse
- Add `/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}` - compares temperatures
*/

app.MapGet("/temp/celsius-to-fahrenheit/{temp}", (double temp) =>
{
    double fahrenheit = (temp * 9 / 5) + 32;
    return Results.Ok(new { celsius = temp, fahrenheit = fahrenheit });

});

app.MapGet("/temp/fahrenheit-to-celsius/{temp}", (double temp) =>
{
    double celsius = (temp - 32) * 5 / 9;
    return Results.Ok(new { fahrenheit = temp, celsius = celsius });

});

app.MapGet("/temp/kelvin-to-celsius/{temp}", (double temp) =>
{
    double celsius = temp - 273.15;
    return Results.Ok(new { kelvin = temp, celsius = celsius });

});

app.MapGet("/temp/celsius-to-kelvin/{temp}", (double temp) =>
{
    double kelvin = temp + 273.15;
    return Results.Ok(new { celsius = temp, kelvin = kelvin });

});

app.MapGet("/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}", (double temp1, string unit1, double temp2, string unit2) =>
{
    double celsius1 = unit1.ToLower() switch
    {
        "celsius" => temp1,
        "fahrenheit" => (temp1 - 32) * 5 / 9,
        "kelvin" => temp1 - 273.15,
        _ => throw new ArgumentException("Invalid unit1")
    };

    double celsius2 = unit2.ToLower() switch
    {
        "celsius" => temp2,
        "fahrenheit" => (temp2 - 32) * 5 / 9,
        "kelvin" => temp2 - 273.15,
        _ => throw new ArgumentException("Invalid unit2")
    };

    string comparison = celsius1 switch
    {
        _ when celsius1 < celsius2 => "less than",
        _ when celsius1 > celsius2 => "greater than",
        _ => "equal to"
    };

    return Results.Ok(new { temp1, unit1, temp2, unit2, comparison });

});

/*
## Challenge 7: Password Generator
**Goal**: Work with random generation and string building
- Create `/password/simple/{length}` - generates random letters/numbers
- Add `/password/complex/{length}` - includes special characters
- Create `/password/memorable/{words}` - generates passphrase with N words
- Add `/password/strength/{password}` - rates password strength
*/

/*
## Challenge 11: Simple Games
- Create `/game/guess-number` (POST) - number guessing game with session
- Add `/game/rock-paper-scissors/{choice}` - play against computer
- Create `/game/dice/{sides}/{count}` - roll N dice with X sides
- Add `/game/coin-flip/{count}` - flip coins and return results
*/


app.Run();

