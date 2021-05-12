using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{

    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;

        IGenericEFRepository _rep;

        public CustomerController(IGenericEFRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }
        // GET: api/customer
        [HttpGet]
        public IActionResult Get()
        {
            var items = _rep.Get<Customer>();
            var DTOs = _mapper.Map<IEnumerable<CustomerDTO>>(items);
            return Ok(DTOs);
        }
        [HttpGet("{Id}", Name = "GetGenericCustomer")]
        //public IActionResult Get(int Id)
        //{
        //    var customers = _rep.Get<Customer>().Where(p =>
        //    p.Id.Equals(Id));

        //    var DTOs = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        //    return Ok(DTOs);
        //}
        // GET api/customer/:Email:
        [HttpGet("Email", Name = "GetCustomerEmail")]
        public IActionResult GetByEmail(string Email)
        {
            var customers = _rep.Get<Customer>().Where(p =>
            p.Email.Equals(Email));

            var DTOs = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return Ok(DTOs);
        }
    }
}
