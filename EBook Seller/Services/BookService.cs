using EBook_Seller.Data;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepo _bookRepo;

        public BookService(IBookRepo bookRrepo)
        {
            _bookRepo = bookRrepo;
        }
        public async Task AddAsyncBook(AddBookDTO bookData)
        {
            var newBook = new Book
            {
                Name = bookData.Name,
                Details = bookData.Details,
                ISBN = bookData.ISBN,
            };

            if (await _bookRepo.DoesExist(newBook))
            {
                throw new InvalidOperationException($"Book with ISBN {newBook.ISBN} or {newBook.Name} is already in the system.");
            }
            await _bookRepo.AddAsyncBook(newBook);

        }

        public async Task AddRangeAsyncBook(List<AddBookDTO> bookListData)
        {
            var inputNames = bookListData.Select(bl => bl.Name).ToList();
            var inputIsbns = bookListData.Select(bl => bl.ISBN).ToList();

            var duplicates = await _bookRepo.MatchingBooks(inputNames, inputIsbns);
            //var newBooks = bookListData.Where(bl => duplicates.Contains(bl.Name));
        }
    }
}
