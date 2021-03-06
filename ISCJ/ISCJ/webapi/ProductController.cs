﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Models.api;
using MA.Core;
using MA.Core.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class ProductController : BaseController
    {
        private ProductManager _productManager;

        public ProductController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpPost()]
        public JsonResult AddProduct(AddProductInput input)
        {
            return new JsonResult(_productManager.AddProduct(GetCallContext(), input).ProductId);
        }

        [HttpGet()]
        public JsonResult AllProducts()
        {
            return new JsonResult(_productManager.GetAllProducts(GetCallContext()));
        }

        [HttpGet("{productId}")]
        public ActionResult GetProduct(Guid productId)
        {
            var output = _productManager.GetProductById(GetCallContext(), productId);
            if(output==null)
            {
                return new JsonResult(null);

            }
            else
            {
                return new JsonResult(output);
            }
            
        }


        [HttpGet("productcode/{productCode}")]
        public ActionResult GetProductByCode(string productCode)
        {
            var output = _productManager.GetProductByCode(GetCallContext(), productCode);
            if (output == null)
            {
                return new NotFoundResult();

            }
            else
            {
                return new JsonResult(output);
            }

        }


    }
}