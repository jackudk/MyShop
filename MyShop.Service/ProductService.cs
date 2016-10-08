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

        IEnumerable<Product> GetAllPaging(string keyWord, int page, int pageSize, out int totalRows);

        Product GetByID(int id);

        IEnumerable<Product> getAllByTagPaging(string tag, int page, int pageSize, out int totalPage);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository ProductRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = ProductRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product Product)
        {
            return _productRepository.Add(Product);
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll(new string[] { "Product" });
        }

        public IEnumerable<Product> getAllByTagPaging(string tag, int page, int pageSize, out int totalPage)
        {
            return _productRepository.GetAllByTagPaging(tag, page, pageSize, out totalPage);
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRows)
        {
            return _productRepository.GetMultiPaging(null, x => x.CreatedDate, out totalRows, page, pageSize);
        }

        public IEnumerable<Product> GetAllPaging(string keyWord, int page, int pageSize, out int totalRows)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return _productRepository.GetMultiPaging(null, x => x.CreatedDate, out totalRows, page, pageSize);
            }
            return _productRepository.GetMultiPaging(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord), x => x.CreatedDate, out totalRows, page, pageSize);
        }

        public Product GetByID(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
            _productRepository.Update(Product);
        }
    }
}