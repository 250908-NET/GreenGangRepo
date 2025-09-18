namespace MyWebAPI.Models;
public record PasswordValidationResult(bool IsValid, List<string> Rules);