
using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concreate;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concreate
{

    //JWT:JaSon Web Token, client istekte bulunur metoda, clienın herhang bir tokenıyoksa HTTP ile döner.
    //Hashing şifreleme algoritması ile veri manipule edilmesi. Veritabanında şifrelerin tutulurken kullanılır.
    //Geri dönüştürülemez. Şifre girilir dönüştürmüş haliyle check edilir.
    //Encrition:Geri dönüşü olan dönüştürme
    public class ProductManager : IProductService
    {
        IProductDal _productDal;    //constructor injection
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [CacheAspect]   //key, value
        public IDataResult<List<Product>> GetAll()
        {

            return new DataResult<List<Product>>(_productDal.GetAll(), true, "ürün listelenedi");
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice > min && p.UnitPrice < max), Messages.ProductListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductListed);
        }

        [ValidationAspect(typeof(ProductValidator))]  //typeof: ctor ile tip ataması yapılıyor attributelara
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result=BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), 
                SameProductNameCheck(product.ProductName), CheckIfCategoryLimitExceded());
            if(result!=null)
               return result;
            _productDal.Add(product);
            return new Result(true, Messages.ProductAdded);
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>((_productDal.Get(p => p.ProductId == productId)), Messages.ProductListed);
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IDataResult<Product> Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessDataResult<Product>(product, Messages.ProductListed);
        }



        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //iş kuralı parçacığı olduğu için private, tek managerda
            int count = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (count >= 10)
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            return new SuccessResult();
        }

        private IResult SameProductNameCheck(string name)
        {
            int count = _productDal.GetAll(p => p.ProductName == name).Count;
            if (count == 0)
                return new SuccessResult(Messages.ProductNameAlreadyExist);
            return new ErrorResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count<=15)
                return new SuccessResult(Messages.CategoryLimitExceded);
            return new ErrorResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if(product.UnitPrice<10)
            {
                throw new Exception("");
                return new ErrorResult("Unitprice 10 un altında");
            }
            return new SuccessResult("ekleme başarılııı");
            
        }
    }
}
