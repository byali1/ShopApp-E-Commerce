using System;
using System.Collections.Generic;
using System.Text;
using ShopApp.Entities;

namespace ShopApp.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int categoryId);
        Category GetByIdWithProducts(int categoryId);

        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);

        void DeleteProductFromCategory(int categoryId, int productId);
    }
}
