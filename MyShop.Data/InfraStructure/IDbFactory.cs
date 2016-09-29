using System;

namespace MyShop.Data.InfraStructure
{
    public interface IDbFactory : IDisposable
    {
        MyShopDbContext Init();
    }
}