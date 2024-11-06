using BorrowABook.Backend.Dtos;

namespace BorrowABook.Backend.Endpoints;

public static class BooksEndpoints
{
    const string getBook = "Get Book";
    private static readonly List<BookDto> books = [

        new(
            1,
            "Harry Potter och dödrelikerna.",
            "Joanne K. Rowling",
            "Mellanåldersböcker, Fantasy",
            2007,
            "Svenska",
            @"Det sista sommarlovet från Hogwarts är snart slut och Harry Potter väntar otåligt på Privet Drive. 
            Medlemmar ur Fenixorden ska komma och hämta honom och föra honom till en säker plats, 
            utan att Voldemort och hans anhängare får reda på vart han tar vägen. 
            Men när han väl kommit i säkerhet - vad ska han göra då? Hur ska Harry kunna utföra den 
            livsfarliga och omöjliga uppgiften som professor Dumbledore har gett honom?",
            1
        ),
        new(
            2,
            "Död zon",
            "Stephen King",
            "Skräck, Skönlitteratur, Romaner",
            1983,
            "Svenska",
            @"Johnny Smith är nyutexaminerad lärare med hela livet framför sig. En kväll, 
            på väg hem från sin flickvän, frontalkrockar hans taxi med en bil och Johnny blir allvarligt skadad. 
            Han ligger i koma i nästan fem år och vaknar upp med en hjärnskada. 
            Ett litet område av hjärnan har slagits ut - Johnny tänker på det som den döda zonen. 
            Samtidigt har Johnny fått förmågan att se glimtar ur människors liv; 
            ögonblick både i det förflutna och i framtiden",
            2
        )
    ];
    public static RouteGroupBuilder MapBooksEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("books");


        group.MapGet("", () => books);

        group.MapGet("{id}", (int id) => books.Find(book => book.Id == id))
            .WithName(getBook);

        group.MapPost("", (NewBookDto book) =>
        {

            int booksAvailible = 1;
            foreach (var b in books)
            {
                if (b.Title.Equals(book.Title) && b.Author.Equals(book.Author))
                {
                    booksAvailible++;
                }
            }

            BookDto newBook = new(
                books.Count + 1,
                book.Title,
                book.Author,
                book.Genre,
                book.ReleaseYear,
                book.Language,
                book.Description,
                booksAvailible
            );
            books.Add(newBook);
            return Results.CreatedAtRoute(getBook, new { id = newBook.Id }, newBook);

        });

        group.MapPut("{id}", (int id, UpdateBookDto book) =>
        {
            var index = books.FindIndex(book => book.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            books[index] = new BookDto(
                id,
                book.Title,
                book.Author,
                book.Genre,
                book.ReleaseYear,
                book.Language,
                book.Description,
                books[index].BooksAvailible
            );
            return Results.NoContent();
        });

        group.MapDelete("{id}", (int id) =>
        {
            books.RemoveAll(book => book.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
