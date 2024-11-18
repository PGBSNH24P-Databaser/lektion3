---
author: Lektion 3
date: MMMM dd, YYYY
paging: "%d / %d"
---

# Lektion 3

Hej och välkommen!

**PÅMINNELSE**: Teams lektion på fredag.

## Agenda

1. Frågor, quiz och repetition
2. Genomgång av övningar (del 1-3)
3. Genomgång av
   1. Constraints
   2. Funktioner
   3. C# och PostgreSQL
4. Enkelt projektbygge
5. Gruppövning
6. Övning med handledning
7. Quiz frågor

---

# Fråga

Kommer vi att lära oss mer om unit testing? Om inte, varför?

# Svar

Nej, inte direkt. Vi kommer att titta på testning i frontend kursen, men specifikt unit testing är något vi i så fall gör som extra genomgång.

Jag/vi fokuserar hellre på andra saker som är viktigare till en början.

Vi kanske kan titta på unit testing i backend kursen.

---

# Fråga

Kommer vi att lära oss hur man debuggar? T.ex. med breakpoints och själva debugging verktygen som finns. Har hört att det är användbart i arbetslivet.

# Svar

Man får jätte gärna lära sig Visual Studio Code debugging på egen hand om man finner det intressant och användbart, men det är inte något vi kommer att gå igenom tillsammans.

---

# Fråga

Hur är SQL syntax uppbyggd? (förkortat från en väldigt lång fråga)

# Svar

Vi kan prata om syntax lite extra idag när vi går igenom övningar tillsammans.

<https://www.postgresql.org/docs/current/sql-syntax.html>

---

# Fråga

I documents/4-sql-relations.md finns denna kod:

```sql
CREATE TABLE order_items (
    order_id INTEGER REFERENCES orders(id),
    product_id INTEGER REFERENCES products(id),
    quantity INTEGER,
    PRIMARY KEY (order_id, product_id)
);
```

Varför? Finns det inget före detta uttryck:
`PRIMARY KEY (order_id, product_id)`

Hur ska detta tolkas?

# Svar

Om man menar varför det inte finns ett kolumn namn och data typ innan så är det för att det skall finnas två primary keys. Detta kallas en "composite primary key" och syntaxen ser ut så.

Mer om detta när vi kommer in på relationer.

---

# Angående frågan SQL-joins exempel

Vi tar detta när vi kommer in på relationer. Jag tipsar om att testa query-exempel själv för att se vad som händer.

---

# Fråga

Jag har läst alla dokument under på GitHub och uppfattat (från lektion 1) att man ska ha koll på teorin, men mycket av det som står i de olika filerna känns överkurs att man ska ha koll på (bara av att läsa filerna).

Vad är din avsikt:

- bara allmän kunskap, utan förväntan att vi ska förstå allt (bara av att läsa teorin),
- eller annan förväntan och i så fall vilken?

Uppdatera vad du har för förväntningar (utöver att ta det på lektionen), genom att uppdatera den inledande texten på varje sida där det är relevant, så att man kan få rätt uppfattning av syftet med ditt upplägg är inför varje del vi ska lära oss.

# Svar

För denna specifika situation så är min avsikt allmän kunskap. Generellt så vill jag - och detta är väldigt viktigt - att ni lär er att tänka för er själva.

---

# Fråga

Jag har problem med att avsluta en terminal och ta mig in igen vid ett annat tillfälle.

# Svar

Det finns korta förklaringar för:

- Lektion 1: <https://github.com/PGBSNH24P-Databaser/lektion1>
- Lektion 2: <https://github.com/PGBSNH24P-Databaser/lektion2>

`docker exec` gör så att man öppnar upp containern där databasen körs. Man vet att man är här om terminal-outputten börjar med `root@`.

`psql` startar klienten för att börja skriva queries. Man vet att man är här om terminal-outputten börjar med `postgres=#`, eller ett annat namn om man har anslutit till en annan "intern" databas.

Använd `\q` för att gå ut ur `psql`. Använd `exit` för att gå ut ur `docker exec`.

---

# Quiz frågor

- Vad gör `select name, power from heroes where power > 9000`?
- Vad saknas: `INSERT ? ?`
- Vad saknas: `DELETE ? ?`
- Vad är kommandot för att skapa en ny tabell?
- Vilket nyckelord används för att slå ihop två villkor?
- Vad är skillnaden mellan `LIKE` och `=` i WHERE-villkor?
- Vad är en "primary key" och varför är den viktig?
- Vad gör `DISTINCT` i en SELECT?
- Vilken constraint används för att förhindra

---

# Tabeller (repetition)

Innehåller rader och kolumner.

Skapa tabell:

```sql
CREATE TABLE IF NOT EXISTS users (
  name TEXT,
  email VARCHAR(100)
);
```

Lägg in rad:

```sql
INSERT INTO users (name, email) VALUES ('Ironman', 'tony@stark.com');
```

---

# Constraints

Extra krav, begränsningar och funktionalitet som kan appliceras på kolumner.

- `NOT NULL`
  - Värde kan inte vara null och måste få ett värde vid insert
- `UNIQUE`
  - Värde måste vara unikt och får inte finnas (för kolumnen) i tabell redan
- `CHECK`
  - Ser till att värde stämmer med vissa villkor
- `DEFAULT`
  - Kolumn får "default" värde om inget specificeras
- `PRIMARY KEY`
  - Identifierande kolumn, ofta `int` eller `guid`
- `REFERENCES` (FOREIGN KEY)
  - Refererande kolumn (till en primary key)
- `SERIAL`
  - Tekniskt sätt inte en constraint, men har extra funktionalitet
  - `BIGINT` datatyp i bakgrunden
  - Ökar automatiskt (increment)

---

# Constraints exempel

```sql
CREATE TABLE user_accounts (
  id SERIAL PRIMARY KEY,
  username VARCHAR(50) UNIQUE NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  password VARCHAR(100) NOT NULL,
  age INT CHECK (age >= 18 AND age <= 120),
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  manager_id INT REFERENCES user_accounts(id) DEFAULT NULL,
);
```

---

# SQL Funktioner

- Matematiska funktioner
- Sträng funktioner
- Tid och datum funktioner
- Aggregate funktioner
- GROUP BY, ORDER BY, HAVING

---

# Matematiska funktioner

- Enkla (+, -, *, /)
- Bitwise (AND, NOT, XOR, SHIFT)
- Funktioner (abs, exp, floor, log, sqrt)

<https://www.postgresql.org/docs/current/functions-math.html>

---

# Exempel på matematiska funktioner

```sql
CREATE TABLE products (price DECIMAL(10, 2));

SELECT price * 1.08 - 5 FROM products;
SELECT sqrt(price) FROM products;
SELECT abs(-price * 50) FROM products;
```

---

# Sträng funktioner

- Concatenation (||)
- Längd på sträng (char_length)
- Omvandla små eller stora bokstäver (lower & upper)
- Substring
- Trim (ltrim, btrim)

Många av dessa funktioner kan och bör göras med kod istället.

<https://www.postgresql.org/docs/current/functions-string.html>

---

# Exempel på sträng funktioner

```sql
CREATE TABLE employees (
    name VARCHAR(255),
    place VARCHAR(255)
);

SELECT 'Welcome to ' || place || ', ' || name || '.' FROM employees;
SELECT place FROM employees WHERE char_length(name) > 3;
SELECT upper(name) FROM employees;
```

---

# Datum och tid funktioner

- Addition och subtraktion (dagar, intervaller m.m)
- Ålder (age)
- Nuvarande tid (current_time, current_date)
- Truncate (date_trunc)
- Extrahera (extract)

<https://www.postgresql.org/docs/current/functions-datetime.html>

---

# Exempel på datum funktioner

```sql
CREATE TABLE appointments (
    start_date DATE,
    end_date DATE
);

INSERT INTO appointments (start_date, end_date) VALUES (current_date, '2020-05-14');
SELECT * FROM appointments WHERE start_date - 30 > '2024-10-01';
SELECT EXTRACT(DAY FROM start_date) FROM appointments WHERE end_date BETWEEN '2019-12-31' AND '2020-01-31';
```

---

# Aggregate funktioner

- Sum
- Count
- Min & max
- Avg

<https://www.postgresql.org/docs/current/functions-aggregate.html>

---

# Exempel på aggregate funktioner

```sql
CREATE TABLE salaries (
    employee_name TEXT,
    salary DECIMAL(10, 2),
    department VARCHAR(255)
);

SELECT avg(salary) FROM salaries;
SELECT count(*) FROM salaries;
SELECT sum(salary) FROM salaries;
```

---

# Andra funktioner

```sql
-- GROUP BY: Lägg alla med samma kolumner i en grupp
SELECT department, avg(salary) FROM salaries GROUP BY department;

-- ORDER BY: Sortera baserat på kolumn
SELECT employee_name, salary FROM salaries ORDER BY salary DESC;

-- HAVING: Inkludera endast grupper som matchar villkor
SELECT department, avg(salary) FROM salaries GROUP BY department HAVING avg(salary) > 30000;
```

<https://www.postgresql.org/docs/current/queries-table-expressions.html#QUERIES-GROUP>

---

# C# och PostgreSQL

## Steg 1: Skapa C# projekt

Man kan skippa detta steg och göra nästa steg om man har ett existerande projekt.

```sh
mkdir my-project
cd my-project
dotnet new console --use-program-main
```

## Steg 2: Hämta PostgreSQL driver

Det är ett bibliotek för att ansluta till PostgreSQL och kommunicera med databasen.

`dotnet add package Npgsql`

---

# Anslut till databas

```csharp
using Npgsql;

// Host=localhost innebär att databasen ligger lokalt på din dator.
// Eftersom databasen ligger i Docker på din dator så är den lokal.
string connectionString = "Host=localhost;Username=postgres;Password=your_password;Database=your_database";

// Skapa anslutningen
// 'using' för automatisk resurshantering
using var connection = new NpgsqlConnection(connectionString);

// Öppna anslutningen till databasen (detta är ekvivalent med 'psql')
connection.Open();
```

---

# NpgsqlCommand klassen

```csharp
// Skapa en query
var sql = "CREATE TABLE IF NOT EXISTS products (id INT, title TEXT, date DATE)";

// Skapa objekt för att skicka query
// 'using' för automatisk resurshantering
using var cmd = new NpgsqlCommand(sql, connection);

// Skicka/exekvera query
cmd.ExecuteNonQuery(); // Det finns flera varianter av denna funktion
```

---

# Query argument

```csharp
// Skapa en query och lägg in parametrar
var sql = "INSERT INTO products (id, title, date) VALUES(@id, @title, @date);";

using var cmd = new NpgsqlCommand(sql, connection);

// Lägg in argument till matchande parametrar
cmd.Parameters.AddWithValue("@id", 1);
cmd.Parameters.AddWithValue("@title", "Godis");
cmd.Parameters.AddWithValue("@date", DateTime.Now);

cmd.ExecuteNonQuery();

// Gör aldrig såhär (det kan utsättas för SQL injections)
// string name = "Ironman";
// var sql = $"INSERT INTO products (id, title, date) VALUES(1, {name}, '');";
```

---

# Query resultat

```csharp
var sql = "SELECT id, name FROM pets";
using var cmd = new NpgsqlCommand(sql, connection);

// 'ExecuteReader' returnerar alla resultat 
using var reader = cmd.ExecuteReader();
// Alternativt 'ExecuteScalar' ifall ett enda resultat (en cell) returneras, exempelvis:
// long rowCount = (long) cmd.ExecuteScalar();

// 'Read' returnerar true om det finns en rad att hämta
while (reader.Read())
{
    // Hämta varje kolumn för sig genom GetDatatype(column index)
    var id = reader.GetInt32(0); 
    var name = reader.GetString(1);

    Console.WriteLine($"{id}: {name}");
}
```

---

# Todo/task applikation

Funktionalitet:

- Skapa tasks
- Radera tasks
- Avklara tasks (senare)

Kommandon:

- `create-task <title> <deadline>`
- `delete-task <title>`
- `complete-task <title>`

---

# Gruppövningar

## Övning 1: implementera funktionalitet

Implementera ett nytt kommando: `complete-todo <title>`

Det skall gå att specificera en todo/task genom titel och markera den som avklarad.

## Övning 2: förbättra genom OOP

Skriv om koden så att den utnyttjar OOP för databas relaterade saker. Skapa ett interface, exempelvis `TaskStorage`, och sedan en implementation för PostgreSQL. Motivera varför detta är bättre eller sämre.

## Bonus utmaningar

1. Implementera kommando för att förskjuta en deadline
2. Implementera in-memory caching med en Dictionary

---

# Quiz frågor

- Vilken klass används för att ansluta till PostgreSQL genom C#?
- Varför skall man inte lägga in query-argument genom string concatenation?
- Hur lägger man in argument med en `NpsqlCommand` query?
- Vilka argument tar `NpgsqlCommand` klass-constructorn in?
- Vad heter funktionen för att summera saker?
- Vad är `MAX` för typ av funktion?
- Förklara `SERIAL`
- Vad gör `UNIQUE`?
- Vad är det för skillnad på `NOT NULL` och `CHECK`?
- Vad är en "primary key" och varför är den viktig?
- Hur många constraints kan en kolumn ha?
