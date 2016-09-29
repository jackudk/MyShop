using MyShop.Data.InfraStructure;
using MyShop.Data.Respositories;
using MyShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Service
{
    public interface IPostService
    {
        Post Add(Post post);
        void Update(Post post);
        Post Delete(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalPage);
        IEnumerable<Post> GetAllByCategoryPaging(int categoryID, int page, int pageSize, out int totalPage);
        Post GetByID(int id);
        IEnumerable<Post> getAllByTagPaging(string tag, int page, int pageSize, out int totalPage);
        void SaveChanges();
    }
    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }
        public Post Add(Post post)
        {
            return _postRepository.Add(post);
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryID, int page, int pageSize, out int totalPage)
        {
            return _postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryID, out totalPage, page, pageSize, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> getAllByTagPaging(string tag, int page, int pageSize, out int totalPage)
        {
            return _postRepository.GetAllByTagPaging(tag, page, pageSize, out totalPage);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalPage)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalPage, page, pageSize);
        }

        public Post GetByID(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }
    }
}
