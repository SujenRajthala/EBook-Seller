using EBook_Seller.Data.IRepo;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepo _bookRepo;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepo bookRrepo,IUnitOfWork uniteOfWork)
        {
            _bookRepo = bookRrepo;
            _unitOfWork = uniteOfWork;
        }

        public async Task<List<BookResponseDTO>> GetBooks()
        {
            var books = await _bookRepo.GetBooks();
            var booksResponse = books.Select(b => new BookResponseDTO { Name = b.Name, Detail = b.Details, ISBN = b.ISBN }).ToList();
            return booksResponse;
        }

        public async Task<BookResponseDTO> GetBookByName(string bookName)
        {
            var book=await _bookRepo.GetBookByName(bookName);
            if (book == null) return null;
            return new BookResponseDTO
            {
                ISBN = book.ISBN,
                Name = book.Name,
                Detail = book.Details
            };
        }

        public async Task<BookResponseDTO> GetBookById(int id)
        {
            var book = await _bookRepo.GetBookById(id);
            return new BookResponseDTO
            {
                ISBN = book.ISBN,
                Name = book.Name,
                Detail = book.Details
            };
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

        public async Task EditBook(int id,AddBookDTO editedData)
        {
            var existDuplicate = await _bookRepo.IsEditable(id, editedData);
            if (existDuplicate != null)
            {
                if (existDuplicate.ISBN == editedData.ISBN &&existDuplicate.Name==editedData.Name) 
                    throw new InvalidOperationException($"There is already a record with Name: {editedData.Name} and ISBM: {editedData.ISBN}");

                if (existDuplicate.ISBN == editedData.ISBN) 
                    throw new InvalidOperationException($"There is already a record with ISBM: {editedData.ISBN}");

                throw new InvalidOperationException($"There is already a record with Name: {editedData.Name}");

            }
            var oldBookData =await _bookRepo.GetBookById(id);
            oldBookData.Name = editedData.Name;
            oldBookData.Details = editedData.Details;
            oldBookData.ISBN = editedData.ISBN;

            await _unitOfWork.SaveChangesAsync();
        }

    }
}
