using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entities;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {

        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public Product GetProductDetails(int id)
        {
            return _productDal.GetProductDetails(id);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }


        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            return _productDal.GetProductsByCategory(category, page, pageSize) as List<Product>;
        }

        public bool Create(Product entity)
        {

            if (Validate(entity))
            {
                _productDal.Create(entity);
                return true;
            }
            return false;

        }

        public void Update(Product entity, int[] categoryIds)
        {
            _productDal.Update(entity, categoryIds);
        }

        public void Update(Product entity)
        {
            _productDal.Update(entity);
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public int GetCountByCategory(string category)
        {
            return _productDal.GetCountByCategory(category);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productDal.GetByIdWithCategories(id);
        }

        public List<Product> GetPopularProducts()
        {
            return _productDal.GetAll();
        }

        public string ErrorMessage { get; set; }
        public bool Validate(Product entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "Ürün adı giriniz.";
                isValid = false;
            }

            //if (entity.Name.Length > 2)
            //{
            //    ErrorMessage += "Ürün adı max 100 karakter olabilir..";
            //    isValid = false;
            //}


            return isValid;
            }
        }
}
