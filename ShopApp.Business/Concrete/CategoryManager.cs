using System;
using System.Collections.Generic;
using System.Text;
using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {

        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category GetById(int categoryId)
        {
            return _categoryDal.GetById(categoryId);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            return _categoryDal.GetByIdWithProducts(categoryId);
        }

        public void Create(Category entity)
        {
            _categoryDal.Create(entity);
        }

        public void Update(Category entity)
        {
            _categoryDal.Update(entity);
        }

        public void Delete(Category entity)
        {
            _categoryDal.Delete(entity);
        }

        public void DeleteProductFromCategory(int categoryId, int productId)
        {
            _categoryDal.DeleteProductFromCategory(categoryId,productId);
        }
    }
}
