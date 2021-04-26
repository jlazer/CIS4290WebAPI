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
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;

        IGenericEFRepository _rep;

        public CategoryController(IGenericEFRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _rep.Get<Category>();
            var DTOs = _mapper.Map<IEnumerable<CategoryDTO>>(items);
            return Ok(DTOs);
        }

        [HttpGet("{Id}", Name = "GetGenericCategory")]
        public IActionResult GetById(int Id)
        {
            var categories = _rep.Get<Category>().Where(p =>
            p.Id.Equals(Id));

            var DTOs = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(DTOs);
        }

        [HttpGet("Parent", Name = "GetParentCategory")]
        public IActionResult GetByParent(int Parent)
        {
            var categories = _rep.Get<Category>().Where(p =>
            p.Parent.Equals(Parent));

            var DTOs = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(DTOs);
        }

        // POST api/category
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var itemToCreate = _mapper.Map<Category>(DTO);

            _rep.Add(itemToCreate);

            if (!_rep.Save()) return StatusCode(500,
                 "A problem occured while handling your request.");
            var createdDTO = _mapper.Map<CategoryDTO>(itemToCreate);

            return CreatedAtRoute("GetGenericCategory",
                new { Id = createdDTO.Id }, createdDTO);
        }

        // PUT api/category/:Id:
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryUpdateDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _rep.Get<Category>(id);
            if (entity == null) return NotFound();

            _mapper.Map(DTO, entity);

            if (!_rep.Save()) return StatusCode(500,
                "A Problem Happend while handling your request.");
            return NoContent();
        }

        // DELETE api/category/:Id:
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (!_rep.Exists<Category>(Id)) return NotFound();

            var entityToDelete = _rep.Get<Category>(Id);

            _rep.Delete(entityToDelete);

            if (!_rep.Save()) return StatusCode(500,
                 "A problem occured while handling your request.");

            return NoContent();

        }
    }
}
