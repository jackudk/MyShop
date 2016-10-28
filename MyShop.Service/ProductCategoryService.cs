using MyShop.Data.InfraStructure;
using MyShop.Data.Respositories;
using MyShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyShop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory ProductCategory);

        void Update(ProductCategory ProductCategory);

        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetMulti(Expression<Func<ProductCategory, bool>> where);

        IEnumerable<ProductCategory> GetAllPaging(int page, int pageSize, out int totalPage);

        IEnumerable<ProductCategory> GetAllPaging(string keyWord, int page, int pageSize, out int totalRows);

        IEnumerable<ProductCategory> GetAllByParentID(int parentID);

        ProductCategory GetByID(int id);

        void SaveChanges();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository ProductCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = ProductCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory ProductCategory)
        {
            return _productCategoryRepository.Add(ProductCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetMulti(Expression<Func<ProductCategory, bool>> predicate)
        {
            return _productCategoryRepository.GetMulti(predicate);
        }

        public IEnumerable<ProductCategory> GetAllByParentID(int parentID)
        {
            return _productCategoryRepository.GetMulti(x => x.ParentID == parentID && x.Status);
        }

        public IEnumerable<ProductCategory> GetAllPaging(int page, int pageSize, out int totalRows)
        {
            return _productCategoryRepository.GetMultiPaging(null, x => x.CreatedDate, out totalRows, page, pageSize);
        }

        public IEnumerable<ProductCategory> GetAllPaging(string keyWord, int page, int pageSize, out int totalRows)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return _productCategoryRepository.GetMultiPaging(null, x => x.CreatedDate, out totalRows, page, pageSize);
            }
            return _productCategoryRepository.GetMultiPaging(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord), x => x.CreatedDate, out totalRows, page, pageSize);
        }

        public ProductCategory GetByID(int id)
        {
            return _productCategoryRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory ProductCategory)
        {
            _productCategoryRepository.Update(ProductCategory);
        }
    }
}