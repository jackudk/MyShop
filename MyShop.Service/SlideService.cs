using MyShop.Data.InfraStructure;
using MyShop.Data.Respositories;
using MyShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Service
{
    public interface ISlideService
    {
        Slide Add(Slide Slide);

        void Update(Slide Slide);

        Slide Delete(int id);

        IEnumerable<Slide> GetAll();

        IEnumerable<Slide> GetMulti(Expression<Func<Slide, bool>> where);

        IEnumerable<Slide> GetAllPaging(int page, int pageSize, out int totalPage);

        IEnumerable<Slide> GetAllPaging(string keyWord, int page, int pageSize, out int totalRows);

        Slide GetByID(int id);

        void SaveChanges();
    }

    public class SlideService : ISlideService
    {
        private ISlideRepository _slideRepository;
        private IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository SlideRepository, IUnitOfWork unitOfWork)
        {
            this._slideRepository = SlideRepository;
            this._unitOfWork = unitOfWork;
        }

        public Slide Add(Slide Slide)
        {
            return _slideRepository.Add(Slide);
        }

        public Slide Delete(int id)
        {
            return _slideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public IEnumerable<Slide> GetMulti(Expression<Func<Slide, bool>> predicate)
        {
            return _slideRepository.GetMulti(predicate);
        }

        public IEnumerable<Slide> GetAllPaging(int page, int pageSize, out int totalRows)
        {
            return _slideRepository.GetMultiPaging(null, x => x.CreatedDate, out totalRows, page, pageSize);
        }

        public IEnumerable<Slide> GetAllPaging(string keyWord, int page, int pageSize, out int totalRows)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return _slideRepository.GetMultiPaging(null, x => x.CreatedDate, out totalRows, page, pageSize);
            }
            return _slideRepository.GetMultiPaging(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord), x => x.CreatedDate, out totalRows, page, pageSize);
        }

        public Slide GetByID(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide Slide)
        {
            _slideRepository.Update(Slide);
        }
    }
}
