namespace code;

using Npgsql;

public interface TodoManager
{
    void Save(string title, string description);
    void Remove(string title);
    void Complete(string title);
}

public class PostgresTodoManager : TodoManager
{
    private NpgsqlConnection connection;

    // Logiken för anslutning har flyttats hit.
    public PostgresTodoManager() {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=lektion3";

        this.connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var createTableSql = @"CREATE TABLE IF NOT EXISTS todos (
            title TEXT,
            description TEXT,
            completed BOOLEAN DEFAULT FALSE
        )";

        using var createTableCmd = new NpgsqlCommand(createTableSql, connection);
        createTableCmd.ExecuteNonQuery();
    }

    public void Close() {
        this.connection.Close();
    }

    public void Complete(string title)
    {
        throw new NotImplementedException();
    }

    // Logiken för remove har flyttats hit.
    public void Remove(string title)
    {
        var deleteTodoSql = "DELETE FROM todos WHERE title = @title";
        using (var deleteTodoCmd = new NpgsqlCommand(deleteTodoSql, connection))
        {
            deleteTodoCmd.Parameters.AddWithValue("@title", title);
            deleteTodoCmd.ExecuteNonQuery();
        }
    }

    // Logiken för save har flyttats hit.
    public void Save(string title, string description)
    {
        var insertTodoSql = "INSERT INTO todos (title, description) VALUES (@title, @description)";
        using (var insertTodoCmd = new NpgsqlCommand(insertTodoSql, connection))
        {
            insertTodoCmd.Parameters.AddWithValue("@title", title);
            insertTodoCmd.Parameters.AddWithValue("@description", description);

            insertTodoCmd.ExecuteNonQuery();
        }
    }
}

// Man kan även skapa detta:
// public class ListTodoManager : TodoManager {}



class Program
{
    static void Main(string[] args)
    {
        // Här fanns tidigare logiken för anslutning direkt.
        var manager = new PostgresTodoManager();
        Console.WriteLine("Welcome to the app.");

        var userInput = Console.ReadLine()!;

        while (true)
        {
            var commandParts = userInput.Split(" ");
            switch (commandParts[0])
            {
                case "create-todo":
                    var title = commandParts[1];
                    var description = "";
                    for (int i = 2; i < commandParts.Length; i++)
                    {
                        description += commandParts[i] + " ";
                    }

                    // Här fanns tidigare logiken för save direkt.
                    manager.Save(title, description);

                    Console.WriteLine("Created todo and saved to db.");
                    break;
                case "remove-todo":
                    var removeTitle = commandParts[1];

                    // Här fanns tidigare logiken för remove direkt.
                    manager.Remove(removeTitle);

                    Console.WriteLine("Removed todo from db.");
                    break;
                case "complete-todo":
                    // TODO: Implement 
                    break;
                default:
                    Console.WriteLine("Not a command.");
                    break;
            }

            userInput = Console.ReadLine()!;
        }

    }
}
