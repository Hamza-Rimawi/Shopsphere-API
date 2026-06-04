using AutoMapper;
using ShopSphere.Business.DTOs.Customer;
using ShopSphere.Business.DTOs.ProductCategory;
using ShopSphere.Business.DTOs.Product;
using ShopSphere.Business.DTOs.ProductImage;
using ShopSphere.Business.DTOs.Review;
using ShopSphere.Business.DTOs.Order;
using ShopSphere.Business.DTOs.OrderItems;
using ShopSphere.Business.DTOs.Payment;
using ShopSphere.Business.DTOs.Shipping;
using ShopSphere.Business.DTOs.WalletTransactions;
using ShopSphere.Business.DTOs.RefreshTokens;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ShopSphere.Business.Helpers
{
    public class MappingProfile: Profile
    {
       
            public MappingProfile()
           {
            CreateMap<Customer, CustomerDto>();
            CreateMap<UpdateDto, Customer>();
            CreateMap<RegisterDto, Customer>();
            CreateMap<CustomerWithoutPassword, CustomerDto>();
            CreateMap<GetAllCustomerRequestDto, GetAllCustomerRequest>();
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto,Product >();
            CreateMap<AddProductDto, Product>();
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<AddProductImageDto, ProductImage>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewView, ReviewViewDto>();
            CreateMap<NewReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
            CreateMap<Order, OrderDto>();
            CreateMap<CheckoutDto, Checkout>();
            CreateMap<Checkout, CheckoutDto>();
            CreateMap<CartItemParameterDto, CartItemParameter>();
            CreateMap<CartItemParameter, CartItemParameterDto>();
            CreateMap<OrderItems, OrderItemsDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Shipping, ShippingDto>();
            CreateMap<UpdateShippingDto, Shipping>();
            CreateMap<ProductPageRequestDto, ProductPageRequest>();
            CreateMap<ProductView, ProductViewDto>();
            CreateMap<WalletTransactionsView,WalletTransactionsViewDto>();
            CreateMap<AddWalletMoneyRequest, AddWalletMoneyRequestDto>();
            CreateMap<AddWalletMoneyRequestDto, AddWalletMoneyRequest>();
            CreateMap<OrderViewDto, OrderView>();
            CreateMap<OrderView, OrderViewDto>();
            CreateMap<UpdateOrderResult,UpdateOrderResultDto>();
            CreateMap<GetAllOrderRequestDto, GetAllOrderRequest>();
            CreateMap<OrderCountRequestDto, OrderCountRequest>();
            CreateMap<ShippingView, ShippingViewDto>();
            CreateMap<GetAllShippingRequestDto, GetAllShippingRequest>();
            CreateMap<GetShippingCountRequestDto, GetShippingCountRequest>();
            CreateMap<UpdateShippingResult, UpdateShippingResultDto>();
            CreateMap<ProductImageDto, ProductImage>();
            CreateMap<Customer, UserDto>();
            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<AddRefreshTokenDto, AddRefreshToken>();
        }
       
    }
}
