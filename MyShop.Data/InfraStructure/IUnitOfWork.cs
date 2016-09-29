namespace MyShop.Data.InfraStructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}