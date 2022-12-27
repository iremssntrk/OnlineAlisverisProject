using Business.Abstract;
using Business.Concreate;
using DataAccess.Concreate.EntityFramework;
using Entities.Concreate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //loosely coupled
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            //dependency chain
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
               return Ok(result);
            return BadRequest(result.Message);
        }


        [HttpPost("Add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("transaction")]

        public IActionResult Transaction(Product product)
        {
            var result= _productService.AddTransactionalTest(product);
            return Ok(result);
        }
    }
}
