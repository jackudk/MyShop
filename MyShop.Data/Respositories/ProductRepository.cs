using MyShop.Data.InfraStructure;
using MyShop.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MyShop.Data.Respositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tag && p.Status
                        orderby p.CreatedDate descending
                        select p;

            totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return query;
        }
    }
}
