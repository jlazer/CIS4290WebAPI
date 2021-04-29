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
    [Route("api/cart")]
    public class CartController : Controller
    {

        private readonly IMapper _mapper;

        IGenericEFRepository _rep;

        public CartController(IGenericEFRepository rep, IMapper mapper)
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
        // GET: api/cart
        [HttpGet]
        public IActionResult Get()
        {
            var items = _rep.Get<Cart>();
            var DTOs = _mapper.Map<IEnumerable<CartDTO>>(items);
            return Ok(DTOs);
        }

        // GET api/cart/:cartid:
        [HttpGet("{id}", Name = "GetGenericCart")]
        public IActionResult Get(int id)
        {
            var cart = _rep.Get<Cart>().Where(p =>
            p.Id.Equals(id));

            var DTOs = _mapper.Map<IEnumerable<CartDTO>>(cart);
            return Ok(DTOs);
        }

        // POST api/cart
        [HttpPost]
        public IActionResult Post([FromBody] CartDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var itemToCreate = _mapper.Map<Cart>(DTO);

            _rep.Add(itemToCreate);

            if (!_rep.Save()) return StatusCode(500,
                 "A problem occured while handling your request.");
            var createdDTO = _mapper.Map<CartDTO>(itemToCreate);

            return CreatedAtRoute("GetGenericCart",
                new { id = createdDTO.id }, createdDTO);
        }

        // PUT api/cart/:cartNo:
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CartDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _rep.Get<Cart>(id);
            if (entity == null) return NotFound();

            _mapper.Map(DTO, entity);

            if (!_rep.Save()) return StatusCode(500,
                "A Problem Happend while handling your request.");
            return NoContent();
        }

        // DELETE api/cart/:Id:
        [HttpDelete("{productId}")]
        public IActionResult Delete(int Id)
        {
            if (!_rep.Exists<Cart>(Id)) return NotFound();

            var entityToDelete = _rep.Get<Cart>(Id);

            _rep.Delete(entityToDelete);

            if (!_rep.Save()) return StatusCode(500,
                 "A problem occured while handling your request.");

            return NoContent();

        }
    }
}
