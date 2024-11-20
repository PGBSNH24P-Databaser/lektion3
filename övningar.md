# Lösningar på övningar (lektion2-4)

## Del 1

### 1.

```sql
CREATE TABLE IF NOT EXISTS persons (name TEXT, age INT);
```

### 2.

```sql
CREATE TABLE products (
  name TEXT,
  description TEXT,
  price DECIMAL(10,2)
);
```

### 3.

```sql
INSERT INTO persons (name, age) VALUES
  ('Ironman', 47),
  ('Superman', 33),
  ('Batman', 26);
```

### 4.

```sql
INSERT INTO products (name, description, price) VALUES
  ('Bread', 'Fresh whole grain bread', 25.50),
  ('Milk', 'Organic whole milk', 12.99),
  ('Coffee', 'Dark roast coffee beans', 89.00);
```

### 5.

```sql
ALTER TABLE products ADD COLUMN category TEXT;
UPDATE products SET category = 'Bakery' WHERE name = 'Bread';
UPDATE products SET category = 'Dairy' WHERE name = 'Milk';
UPDATE products SET category = 'Beverages' WHERE name = 'Coffee';
```

## Del 2

### 6.

```sql
CREATE TABLE movies (
  title TEXT,
  year INTEGER,
  rating INTEGER CHECK (rating BETWEEN 1 AND 5)
);


INSERT INTO movies (title, year, rating) VALUES
  ('The Shawshank Redemption', 1994, 5),
  ('Inception', 2010, 5),
  ('The Matrix', 1999, 5),
  ('Interstellar', 2014, 4),
  ('Pulp Fiction', 1994, 5),
  ('The Dark Knight', 2008, 5),
  ('Fight Club', 1999, 4),
  ('Forrest Gump', 1994, 5),
  ('The Godfather', 1972, 5),
  ('Goodfellas', 1990, 4),
  ('The Silence of the Lambs', 1991, 4),
  ('Se7en', 1995, 4),
  ('The Usual Suspects', 1995, 4),
  ('Schindler''s List', 1993, 5),
  ('The Green Mile', 1999, 4),
  ('Saving Private Ryan', 1998, 5),
  ('Jurassic Park', 1993, 4),
  ('The Lion King', 1994, 5),
  ('Titanic', 1997, 4),
  ('The Matrix Reloaded', 2003, 3),
  ('Avatar', 2009, 4),
  ('The Avengers', 2012, 4),
  ('Black Panther', 2018, 4),
  ('Joker', 2019, 4),
  ('Parasite', 2019, 5),
  ('Dune', 2021, 4),
  ('No Time to Die', 2021, 3),
  ('The Batman', 2022, 4),
  ('Top Gun: Maverick', 2022, 4),
  ('Everything Everywhere All at Once', 2022, 5);
```

### 7.

```sql
-- 1
SELECT * FROM movies;

-- 2
SELECT title FROM movies;

-- 3
SELECT title, rating FROM movies;

-- 4
SELECT * FROM movies WHERE rating > 3;

-- 5
SELECT title FROM movies
WHERE year = 2020 AND rating > 3;

-- 6
SELECT year, COUNT(*)
FROM movies
GROUP BY year
ORDER BY year;

-- 7
SELECT * FROM movies
ORDER BY year;
```

### 8.

```sql
CREATE TABLE books (
  title TEXT,
  author TEXT,
  pages INTEGER,
  published DATE
);

INSERT INTO books (title, author, pages, published) VALUES
  ('The Hobbit', 'J.R.R. Tolkien', 310, '1937-09-21'),
  ('Dune', 'Frank Herbert', 412, '1965-08-01'),
  ('1984', 'George Orwell', 328, '1949-06-08');
```

### 9.

```sql
SELECT * FROM books
WHERE published > '2000-01-01'
AND pages > 300;
```

### 10.

```sql
CREATE TABLE employees (
  name TEXT,
  department TEXT,
  salary DECIMAL
);

INSERT INTO employees (name, department, salary) VALUES
  ('John Doe', 'IT', 75000),
  ('Jane Smith', 'IT', 82000),
  ('Bob Johnson', 'HR', 65000),
  ('Alice Brown', 'HR', 67000),
  ('Charlie Wilson', 'Sales', 85000),
  ('Diana Miller', 'Sales', 88000);

SELECT department, AVG(salary)
FROM employees
GROUP BY department;
```

## Del 3

### 11.

```sql
CREATE TABLE orders (
  id SERIAL PRIMARY KEY,
  customer_name TEXT,
  order_date DATE,
  total_amount NUMERIC(10,2)
);

INSERT INTO orders (customer_name, order_date, total_amount) VALUES
  ('John Smith', CURRENT_DATE - INTERVAL '5 days', 150.00),
  ('Mary Johnson', CURRENT_DATE - INTERVAL '15 days', 275.50),
  ('Bob Wilson', CURRENT_DATE - INTERVAL '2 days', 420.75),
  ('Alice Brown', CURRENT_DATE - INTERVAL '45 days', 190.25),
  ('Charlie Davis', CURRENT_DATE - INTERVAL '7 days', 340.00);

SELECT * FROM orders
WHERE order_date >= CURRENT_DATE - INTERVAL '1 month';
```

### 12.

```sql
ALTER TABLE employees ADD COLUMN hire_date DATE;

UPDATE employees SET hire_date = CURRENT_DATE - INTERVAL '3 years' WHERE name = 'John Doe';
UPDATE employees SET hire_date = CURRENT_DATE - INTERVAL '1 year' WHERE name = 'Jane Smith';
UPDATE employees SET hire_date = CURRENT_DATE - INTERVAL '4 years' WHERE name = 'Bob Johnson';
UPDATE employees SET hire_date = CURRENT_DATE - INTERVAL '2 years' WHERE name = 'Alice Brown';
UPDATE employees SET hire_date = CURRENT_DATE - INTERVAL '5 years' WHERE name = 'Charlie Wilson';
UPDATE employees SET hire_date = CURRENT_DATE - INTERVAL '1 year' WHERE name = 'Diana Miller';

SELECT * FROM employees
WHERE hire_date <= CURRENT_DATE - INTERVAL '2 years';
```

### 13.

```sql
CREATE TABLE tasks (
  title TEXT,
  status TEXT CHECK (status IN ('pending', 'in_progress', 'completed')),
  created_at DATE,
  completed_at DATE
);

INSERT INTO tasks (title, status, created_at, completed_at) VALUES
  ('Task 1', 'completed', '2024-01-01', '2024-01-15'),
  ('Task 2', 'pending', '2024-01-02', NULL),
  ('Task 3', 'in_progress', '2024-01-03', NULL),
  ('Task 4', 'completed', '2024-01-04', '2024-01-20'),
  ('Task 5', 'pending', '2024-01-05', NULL),
  ('Task 6', 'in_progress', '2024-01-06', NULL),
  ('Task 7', 'completed', '2024-01-07', '2024-01-25');

SELECT status, COUNT(*)
FROM tasks
GROUP BY status;
```

### 14.

```sql
CREATE TABLE students (
  name TEXT,
  grade INTEGER CHECK (grade BETWEEN 1 AND 100),
  course TEXT
);

INSERT INTO students (name, grade, course) VALUES
  ('Student 1', 85, 'Math'),
  ('Student 2', 92, 'Math'),
  ('Student 3', 78, 'Math'),
  ('Student 4', 88, 'Physics'),
  ('Student 5', 95, 'Physics'),
  ('Student 6', 82, 'Chemistry'),
  ('Student 7', 90, 'Biology'),
  ('Student 8', 87, 'Biology'),
  ('Student 9', 91, 'Physics'),
  ('Student 10', 84, 'Chemistry');

SELECT course, AVG(grade)
FROM students
GROUP BY course
HAVING COUNT(*) >= 2;
```
