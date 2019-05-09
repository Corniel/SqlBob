# SQL Bob
## Building SQL queries dynamically

### Summary
SQL Bob is a lightweight library that allows developers to build SQL queries
dynamically. It offers a fluent API which allows to define a SQL query as a
nested structure. Under the hood, the ToString() method uses a StringBuilder
to convert the structure in to an actual SQL query string that can be consumed
by a SQL Data reader or ORM of choice.

### Fluent
Setting up a SELECT query could look a bit like this:
```C#
var query = Query
    .Select("*")
    .From("Persons", "p")
    .Where("p.Name LIKE @name + '%'");
```
with results in:
```SQL
SELECT *
FROM
    Persons p
WHERE
   p.Name LIKE @name + '%'
```

It also possible to create expressions using classes instead of string
expressions. Also you can add or update parts of your query later:
``` C#
var join = Join.Inner("Users").As("u").On("u.Id = p.User");
query.JoinClause.Add(join);
```

### No model mapping
As this is NOT should not be a replacement of an ORM, no logic is provided to
generate SQL based on object structures. It tries to do one thing, and it tries
to do that good. ORM's that allow (or encourage) to provide SQL such as Dapper
work really well with SQL Bob.

### variables
Please don't try to be smarter than SQL Server. use `@variables`, and let the
SQL Client resolve the variables. By doing so, your are safe to SQL injection
out of the box.

## Syntax errors
The disadvantage of a fluent API is that is allows statements to be partly created.
Take the following example:
```CSharp
var join = Join.Inner("Persons").As("p");
```
The ```ToString()``` will lead to the following:
```SQL
INNER JOIN
    Persons AS p ON /* Can't apply a JOIN without an ON condition. */
```
Where ```Minified()``` will throw an SQL syntax exception.
The reason that the ```ToString()``` doesn't throw, is a practical one: it is
used all over the place, and logging and debugging code might fail on such
behavior.

## SQL statements
### Alias
Aliases can be powerful. The ```Alias``` type support reusing aliases.
```CSharp
Alias tblAlias = "c";
var select = tblAlias + "[FullName]"; // c.[FullName]
```

### JOIN statements
Creating JOIN statements can be don in several ways:
#### INNER JOIN
```CSharp
var join = Join.Inner("Persons").As("p").On("p.ID = u.Person");
```
```SQL
INNER JOIN
    Persons AS p ON p.ID = u.Person
```
#### LEFT JOIN
```CSharp
var join = Join.Left("Persons").As("p").On("p.ID = u.Person");
```
```SQL
LEFT JOIN
    Persons AS p ON p.ID = u.Person
```
#### RIGHT JOIN
```CSharp
var join = Join.Right("Persons").As("p").On("p.ID = u.Person");
```
```SQL
RIGHT JOIN
    Persons AS p ON p.ID = u.Person
```
#### FULL OUTER JOIN
```CSharp
var join = Join.FullOuter("Persons").As("p").On("p.ID = u.Person");
```
```SQL
FULL OUTER JOIN
    Persons AS p ON p.ID = u.Person
```
#### Raw
In some cases you just want to label a raw SQL string as JOIN statement:
```CSharp
Join join = "INNER JOIN Persons p ON p.ID = u.Person";
```
Or, doing the same:
```CSharp
var join = Join.Raw("INNER JOIN Persons p ON p.ID = u.Person");
```

### Logical operators
To build up logical statements programmatically, SQL Bob offers the ```Logical```
factory method.

#### AND operator
In code:
```CSharp
var condition = Logical.And("[dbo].[Ammount] = 17", "[dbo].[Id] <> 666");
```
In SQL:
```SQL
([dbo].[Ammount] = 17 AND [dbo].[Id] <> 666)
```
#### OR operator
In code:
```CSharp
var condition = Logical.Or("[dbo].[Ammount] = 17", "[dbo].[Id] <> 666");
```
In SQL:
```SQL
([dbo].[Ammount] = 17 OR [dbo].[Id] <> 666)
```

#### NOT operator
In code:
```CSharp
var condition = Logical.Not("[dbo].[Ammount] = 17");
```
In SQL:
```SQL
NOT([dbo].[Ammount] = 17)
```

#### XOR 'operator'
SQL (Server) doesn't provide an XOR (Exclusive Or) operator. So to provide an
XOR it coverted:
```CSharp
var condition = Logical.Xor("@A", "@B");
```
In SQL:
```SQL
((@A OR @B) AND NOT((@A AND @B)))
```

#### = operator
In code:
```CSharp
var condition = Logical.Equal("[dbo].[Col]", "@par");
```
In SQL:
```SQL
[dbo].[Col] = @par
```

#### <> operator
In code:
```CSharp
var condition = Logical.NotEqual("[dbo].[Col]", "@par");
```
In SQL:
```SQL
[dbo].[Col] <> @par
```

#### > operator
In code:
```CSharp
var condition = Logical.GreaterThan("[dbo].[Col]", "@par");
```
In SQL:
```SQL
[dbo].[Col] > @par
```

#### >= operator
In code:
```CSharp
var condition = Logical.GreaterThanOrEqual("[dbo].[Col]", "@par");
```
In SQL:
```SQL
[dbo].[Col] >= @par
```

#### < operator
In code:
```CSharp
var condition = Logical.LessThan("[dbo].[Col]", "@par");
```
In SQL:
```SQL
[dbo].[Col] < @par
```

#### <= operator
In code:
```CSharp
var condition = Logical.LessThanOrEqual("[dbo].[Col]", "@par");
```
In SQL:
```SQL
[dbo].[Col] <= @par
```
