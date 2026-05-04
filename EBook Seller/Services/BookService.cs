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
            var duplicateNamesInList = bookListData.GroupBy(bl => bl.Name).Where(g => g.Count()>1).Select(g=>g.Key).ToList();
            var duplicateIsbnsInList = bookListData.GroupBy(bl => bl.ISBN).Where(g => g.Count() > 1).Select(g => g.Key).ToList();

            if (duplicateIsbnsInList.Any() || duplicateNamesInList.Any())
            {
                var exceptionText="Your List contains internal duplicates: ";
                if (duplicateNamesInList.Any()) exceptionText += string.Join(" ,", duplicateNamesInList);
                if (duplicateIsbnsInList.Any()) exceptionText += string.Join(" ,", duplicateIsbnsInList);
                throw new InvalidOperationException(exceptionText);
            }

            var inputNames = bookListData.Select(bl => bl.Name).ToList(); //[a,b,c,d,e]
            var inputIsbns = bookListData.Select(bl => bl.ISBN).ToList(); //[1,2,3,4,1]

            var duplicates = await _bookRepo.MatchingBooks(inputNames, inputIsbns);

            if (duplicates.Any())
            {
                var dupString = string.Join(" ,", duplicates.Select(d => d.Name).ToList());
                throw new InvalidOperationException($"[{dupString}] already does exists in the system.");
            }

            var newBooks = bookListData
                .Select(bl=>new Book
                {
                    Name=bl.Name,
                    Details=bl.Details,
                    ISBN=bl.ISBN
                }).ToList();

            await _bookRepo.AddRangeAsyncBook(newBooks);
        }
    }
}
