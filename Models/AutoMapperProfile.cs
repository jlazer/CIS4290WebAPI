using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            
            CreateMap<Cart, CartDTO>();
            CreateMap<CartDTO, Cart>();
            
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            
            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewDTO, Review>();

            CreateMap<Review, ReviewUpdateDTO>();
            CreateMap<ReviewUpdateDTO, Review>();

            CreateMap<Product, ProductUpdateDTO>();
            CreateMap<ProductUpdateDTO, Product>();

            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();


        }

    }
}
