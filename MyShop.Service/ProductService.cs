using MyShop.Data.InfraStructure;
using MyShop.Data.Respositories;
using MyShop.Model.Models;
using System.Collections.Generic;

namespace MyShop.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalPage);

        IEnumerable<Product> GetAllByCategoryPaging(int categoryID, int page, int pageSize, out int totalPage);

        Product GetByID(int id);

        IEnumerable<Product> getAllByTagPaging(string tag, int page, int pageSize, out int totalPage);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _ProductRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository ProductRepository, IUnitOfWork unitOfWork)
        {
            this._ProductRepository = ProductRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product Product)
        {
            return _ProductRepository.Add(Product);
        }

        public Product Delete(int id)
        {
            return _ProductRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _ProductRepository.GetAll(new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAllByCategoryPaging(int categoryID, int page, int pageSize, out int totalPage)
        {
            return _ProductRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryID, out totalPage, page, pageSize, new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> getAllByTagPaging(string tag, int page, int pageSize, out int totalPage)
        {
            return _ProductRepository.GetAllByTagPaging(tag, page, pageSize, out totalPage);
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalPage)
        {
            return _ProductRepository.GetMultiPaging(x => x.Status, out totalPage, page, pageSize);
        }

        public Product GetByID(int id)
        {
            return _ProductRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
            _ProductRepository.Update(Product);
        }
    }
}