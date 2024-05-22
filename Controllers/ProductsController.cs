using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Core.Specifications;
using AutoMapper;
using SKINET_Ecommerce.DTOs;

namespace API.Controllers
{
    //ControllerBase class is used when you don't need View Support(e.g. WebAPI returning JSON Data)
    //Controller class is used when we need View Support(e.g. MVC App)
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        /*private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }*/

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo, 
            IGenericRepository<ProductBrand> productBrandRepo, 
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper) {
                _productRepo = productRepo;
                _productBrandRepo = productBrandRepo;
                _productTypeRepo = productTypeRepo;
                _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDTO>>> GetProducts()
        {
            //var products = await _repo.GetProductListAsync();
            //var products = await _productRepo.ListAllAsync();
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            //var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            //var product = await _repo.GetProductByIDAsync(id);
            //var product = await _productRepo.GetByIDAsync(id);
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            return Ok(_mapper.Map<Product, ProductToReturnDTO>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            //var productBrands = await _repo.GetProductBrandsListAsync();
            var productBrands = await _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            //var productTypes = await _repo.GetProductTypesListAsync();
            var productTypes = await _productTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }
    }
}
