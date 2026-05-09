namespace EBook_Seller.Data.IRepo
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
        
    }
}
