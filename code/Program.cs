namespace code;

using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=lektion3";

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var createTableSql = @"CREATE TABLE IF NOT EXISTS todos (
            title TEXT,
            description TEXT,
            completed BOOLEAN DEFAULT FALSE
        )";

        using var createTableCmd = new NpgsqlCommand(createTableSql, connection);
        createTableCmd.ExecuteNonQuery();

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
                    for (int i = 2; i < commandParts.Length; i++) {
                        description += commandParts[i] + " ";
                    }

                    var insertTodoSql = "INSERT INTO todos (title, description) VALUES (@title, @description)";
                    using (var insertTodoCmd = new NpgsqlCommand(insertTodoSql, connection)) {
                        insertTodoCmd.Parameters.AddWithValue("@title", title);
                        insertTodoCmd.Parameters.AddWithValue("@description", description);

                        if (insertTodoCmd.ExecuteNonQuery() == -1) {
                            Console.WriteLine("Failed");
                        }
                    }
                                        
                    Console.WriteLine("Created todo and saved to db.");
                    break;
                case "remove-todo":
                    var removeTitle = commandParts[1];

                    var deleteTodoSql = "DELETE FROM todos WHERE title = @title";
                    using (var deleteTodoCmd = new NpgsqlCommand(deleteTodoSql, connection)) {
                        deleteTodoCmd.Parameters.AddWithValue("@title", removeTitle);
                        deleteTodoCmd.ExecuteNonQuery();
                    }
                                        
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
