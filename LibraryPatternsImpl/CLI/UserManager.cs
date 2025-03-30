using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.CLI;

public class UserManager
{
    private readonly Dictionary<string, IUser> _users = new();

    public IUser? LoginOrRegister()
    {
        while (true)
        {
            Console.WriteLine("\n--- Welcome to the Library ---");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        var user = Login();
                        if (user != null) return user;
                        break;
                    }
                case "2": return Register();
                case "0": return null;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private IUser? Login()
    {
        Console.Write("Enter first name: ");
        string? firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string? lastName = Console.ReadLine();

        string key = GetKey(firstName, lastName);
        if (_users.TryGetValue(key, out var user))
        {
            Console.WriteLine($"Welcome back, {user.FirstName}!");
            return user;
        }

        Console.WriteLine("User not found.");
        return null;
    }

    private IUser Register()
    {
        Console.Write("Enter first name: ");
        string? firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string? lastName = Console.ReadLine();

        string key = GetKey(firstName, lastName);
        if (_users.ContainsKey(key))
        {
            Console.WriteLine("User already exists.");
            return _users[key];
        }

        var newUser = new LibraryUser(firstName ?? "Unnamed", lastName ?? "User");
        _users[key] = newUser;
        Console.WriteLine($"User registered: {newUser.FirstName} {newUser.LastName}");
        return newUser;
    }

    private string GetKey(string? firstName, string? lastName) =>
        $"{firstName?.ToLower()}_{lastName?.ToLower()}";
}