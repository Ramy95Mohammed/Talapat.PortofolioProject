using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talapat.Api.DTOS;
using Talapat.Api.Errors;
using Talapat.Api.Helpers;
using Talapat.BLL.Interfaces;
using Talapat.BLL.Specifications;
using Talapat.BLL.Specifications.ProductSpecification;
using Talapat.DAL.Entities;

namespace Talapat.Api.Controllers
{
    
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> typesRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> ProductRepo,IGenericRepository<ProductBrand> ProductBrandRepo,IGenericRepository<ProductType> TypesRepo,IMapper mapper)
        {
            productRepo = ProductRepo;
            productBrandRepo = ProductBrandRepo;
            typesRepo = TypesRepo;
            this.mapper = mapper;
        }
        //[Authorize]      
        [CachedResponse(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<IReadOnlyList<ProductToReturnDto>>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            // var products =await productRepo.GetAllAsync();
            var spec = new ProductWithTypeAndBrandSpecification(productParams);            
            var products = await productRepo.GetAllWithSpecAsync(spec);

            var CountSpec = new ProductWithFilterForCountSpecification(productParams);
            var Count = await productRepo.GetCountAsync(CountSpec);
            var pagination = new Pagination<ProductToReturnDto>() {
                Data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products),
                PageIndex = productParams
            .PageIndex,PageSize = productParams
            .PageSize,Count = Count};
            return Ok(pagination);
        }
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {           
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await productRepo.GetByIdWithSpec(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));
            return Ok(mapper.Map<Product, ProductToReturnDto>( product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await productBrandRepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await typesRepo.GetAllAsync();
            return Ok(Types);
        }
    }
}
