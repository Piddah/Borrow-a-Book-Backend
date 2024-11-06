using System.ComponentModel.DataAnnotations;

namespace BorrowABook.Backend.Dtos;

public record class NewBookDto(
    [Required][StringLength(50)] string Title,
    [Required][StringLength(40)] string Author,
    [Required][StringLength(30)] string Genre,
    [StringLength(4)] int ReleaseYear,
    [Required][StringLength(20)] string Language,
    [StringLength(200)] string Description,
    int BooksAvailible
);
