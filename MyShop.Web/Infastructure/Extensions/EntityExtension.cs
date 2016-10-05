using MyShop.Model.Models;
using MyShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Web.Infastructure.Extensions
{
    public static class EntityExtension
    {
        public static void ClonePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVM)
        {
            postCategory.ID = postCategoryVM.ID;
            postCategory.Name = postCategoryVM.Name;
            postCategory.Alias = postCategoryVM.Alias;
            postCategory.Image = postCategoryVM.Image;
            postCategory.Description = postCategoryVM.Description;
            postCategory.ParentID = postCategoryVM.ParentID;
            postCategory.DisplayOrder = postCategoryVM.DisplayOrder;
            postCategory.HomeFlag = postCategoryVM.HomeFlag;

            postCategory.CreatedBy = postCategoryVM.CreatedBy;
            postCategory.CreatedDate = postCategoryVM.CreatedDate;
            postCategory.UpdatedBy = postCategoryVM.UpdatedBy;
            postCategory.UpdatedDate = postCategoryVM.UpdatedDate;
            postCategory.MetaDescription = postCategoryVM.MetaDescription;
            postCategory.MetaKeyword = postCategoryVM.MetaKeyword;
            postCategory.Status = postCategoryVM.Status;

        }

        public static void ClonePost(this Post post, PostViewModel postVM)
        {
            post.ID = postVM.ID;
            post.Name = postVM.Name;
            post.Alias = postVM.Alias;
            post.Image = postVM.Image;
            post.Description = postVM.Description;
            post.CategoryID = postVM.CategoryID;
            post.Content = postVM.Content;
            post.HomeFlag = post.HomeFlag;
            post.HotFlag = post.HotFlag;
            post.ViewCount = post.ViewCount;

            post.CreatedBy = postVM.CreatedBy;
            post.CreatedDate = postVM.CreatedDate;
            post.UpdatedBy = postVM.UpdatedBy;
            post.UpdatedDate = postVM.UpdatedDate;
            post.MetaDescription = postVM.MetaDescription;
            post.MetaKeyword = postVM.MetaKeyword;
            post.Status = postVM.Status;

        }

        public static void CloneProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVM)
        {
            productCategory.ID = productCategoryVM.ID;
            productCategory.Name = productCategoryVM.Name;
            productCategory.Alias = productCategoryVM.Alias;
            productCategory.Image = productCategoryVM.Image;
            productCategory.Description = productCategoryVM.Description;
            productCategory.ParentID = productCategoryVM.ParentID;
            productCategory.DisplayOrder = productCategoryVM.DisplayOrder;
            productCategory.HomeFlag = productCategoryVM.HomeFlag;

            productCategory.CreatedBy = productCategoryVM.CreatedBy;
            productCategory.CreatedDate = productCategoryVM.CreatedDate;
            productCategory.UpdatedBy = productCategoryVM.UpdatedBy;
            productCategory.UpdatedDate = productCategoryVM.UpdatedDate;
            productCategory.MetaDescription = productCategoryVM.MetaDescription;
            productCategory.MetaKeyword = productCategoryVM.MetaKeyword;
            productCategory.Status = productCategoryVM.Status;

        }

        public static void CloneProduct(this Product product, ProductViewModel productVM)
        {
            product.ID = productVM.ID;
            product.Name = productVM.Name;
            product.Alias = productVM.Alias;
            product.Image = productVM.Image;
            product.CategoryID = productVM.CategoryID;
            product.MoreImages = productVM.MoreImages;
            product.Price = productVM.Price;
            product.PromotionPrice = productVM.PromotionPrice;
            product.Warranty = productVM.Warranty;
            product.Description = productVM.Description;
            product.Content = productVM.Content;
            product.HomeFlag = product.HomeFlag;
            product.HotFlag = product.HotFlag;
            product.ViewCount = product.ViewCount;

            product.CreatedBy = productVM.CreatedBy;
            product.CreatedDate = productVM.CreatedDate;
            product.UpdatedBy = productVM.UpdatedBy;
            product.UpdatedDate = productVM.UpdatedDate;
            product.MetaDescription = productVM.MetaDescription;
            product.MetaKeyword = productVM.MetaKeyword;
            product.Status = productVM.Status;

        }
}
}