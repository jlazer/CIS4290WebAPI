using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Services;



namespace WebAPI.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;

        IGenericEFRepository _rep;

        public ProductController(IGenericEFRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;

        }

        //This gets all products in the Product table
        // GET: api/product
        [HttpGet]
        public IActionResult Get()
        {
            var items = _rep.Get<Product>();
            var DTOs = _mapper.Map<IEnumerable<ProductDTO>>(items);
            return Ok(DTOs);
        }
        // This method gets product by product id provided in request url
        // GET api/product/:productId:
        [HttpGet("{productId}", Name = "GetGenericProduct")]
        public IActionResult Get(int productId)
        {
            // create a product variable based on the product entity using the entity framework repository
            // find products in the datbase with matching product id
            var products = _rep.Get<Product>().Where(p =>
            p.ProductID.Equals(productId));

            // place the product into a data transfer object that is then serialized and sent back as a request
            var DTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(DTOs);
        }

        // POST api/product
        [HttpPost]
        public IActionResult Post([FromBody] ProductDTO DTO)
        {
            // ensure the data transfer object is valid and not empty
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // use automapper to mapp the dto sent to a entity of the same type
            var itemToCreate = _mapper.Map<Product>(DTO);
            System.Diagnostics.Debug.WriteLine(itemToCreate);

            // add the entity to the entity framework repository
            _rep.Add(itemToCreate);

            if (!_rep.Save()) return StatusCode(500,
                 "A problem occured while handling your request.");
            var createdDTO = _mapper.Map<ProductDTO>(itemToCreate);

            return CreatedAtRoute("GetGenericProduct",
                new { productId = createdDTO.ProductID }, createdDTO);
        }

        // PUT api/product/:productId:
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductUpdateDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _rep.Get<Product>(id);
            if (entity == null) return NotFound();

            _mapper.Map(DTO, entity);

            if (!_rep.Save()) return StatusCode(500,
                "A Problem Happend while handling your request.");
            return NoContent();
        }

        // DELETE api/product/:productId:
        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            if (!_rep.Exists<Product>(productId)) return NotFound();

            var entityToDelete = _rep.Get<Product>(productId);

            _rep.Delete(entityToDelete);

            if (!_rep.Save()) return StatusCode(500,
                 "A problem occured while handling your request.");

            return NoContent();

        }

        [HttpGet("SubCategoryID", Name = "GetSubCategoryProduct")]
        public IActionResult GetBySubCateogory(int SubCategory)
        {
            var products = _rep.Get<Product>().Where(p =>
            p.SubCategoryID.Equals(SubCategory));

            var DTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(DTOs);
        }

        [HttpGet("Featured", Name = "GetFeaturedProduct")]
        public IActionResult GetByFeatured(int MainCategory)
        {
            var products = _rep.Get<Product>().Where(p =>
            p.MainCategoryID.Equals(MainCategory) & p.FeaturedProduct.Equals(1));
            

            var DTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(DTOs);
        }
    }
}
