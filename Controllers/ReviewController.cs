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
        [Route("api/review")]
        public class ReviewController : Controller
        {
            private readonly IMapper _mapper;

            IGenericEFRepository _rep;

            public ReviewController(IGenericEFRepository rep, IMapper mapper)
            {
                _rep = rep;
                _mapper = mapper;
            }


            // GET: api/review
            [HttpGet]
            public IActionResult Get()
            {
                var items = _rep.Get<Review>();
                var DTOs = _mapper.Map<IEnumerable<ReviewDTO>>(items);
                return Ok(DTOs);
            }


            // GET api/review/:reviewId:
            [HttpGet("{productId}", Name = "GetGenericReview")]
            public IActionResult Get(int productId)
            {
                var products = _rep.Get<Review>().Where(p =>
                p.ProductID.Equals(productId));

                var DTOs = _mapper.Map<IEnumerable<ReviewDTO>>(products);
                return Ok(DTOs);
            }

            // POST api/review
            [HttpPost]
            public IActionResult Post([FromBody] ReviewDTO DTO)
            {
                if (DTO == null) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var itemToCreate = _mapper.Map<Review>(DTO);

                _rep.Add(itemToCreate);

                if (!_rep.Save()) return StatusCode(500,
                     "A problem occured while handling your request.");
                var createdDTO = _mapper.Map<ReviewDTO>(itemToCreate);

                return CreatedAtRoute("GetGenericReview",
                    new { productId = createdDTO.ProductID }, createdDTO);
            }


            // PUT api/review/:reviewId:
            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] ReviewUpdateDTO DTO)
            {
                if (DTO == null) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var entity = _rep.Get<Review>(id);
                if (entity == null) return NotFound();

                _mapper.Map(DTO, entity);

                if (!_rep.Save()) return StatusCode(500,
                    "A Problem Happend while handling your request.");
                return NoContent();
            }

            // DELETE api/review/:reviewId:
            [HttpDelete("{productId}")]
            public IActionResult Delete(int productId)
            {
                if (!_rep.Exists<Review>(productId)) return NotFound();

                var entityToDelete = _rep.Get<Review>(productId);

                _rep.Delete(entityToDelete);

                if (!_rep.Save()) return StatusCode(500,
                     "A problem occured while handling your request.");

                return NoContent();

            }
        }
    }
