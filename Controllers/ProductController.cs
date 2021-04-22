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

        // so im pretty sure the demo he provided used the entity framework stuff.
        // im not sure if we still need to use that in order to interact with the db.
        // GET: api/<controller>  
        [HttpGet]
        /*public Product Get()
        {
            ProductDTO productDTO = new ProductDTO()
            {
                Name = "Student 1",
                Age = 25,
                City = "New York"
            };

            return _mapper.Map<Product>(productDTO);
        }*/
        // GET: api/product
        [HttpGet]
        public IActionResult Get()
        {
            var items = _rep.Get<Product>();
            var DTOs = _mapper.Map<IEnumerable<ProductDTO>>(items);
            return Ok(DTOs);
        }

        // GET api/product/:productId:
        [HttpGet("{productId}", Name = "GetGenericProduct")]
        public IActionResult Get(int productId)
        {
            var products = _rep.Get<Product>().Where(p =>
            p.ProductID.Equals(productId));

            var DTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(DTOs);
        }

        // POST api/product
        [HttpPost]
        public IActionResult Post([FromBody] ProductDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var itemToCreate = _mapper.Map<Product>(DTO);

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
    }
}
