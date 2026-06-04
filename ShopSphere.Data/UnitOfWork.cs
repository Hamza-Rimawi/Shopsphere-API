using ShopSphere.Business.Database;
using ShopSphere.Business.Repositories;
using ShopSphere.Data.Repositories;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; } 
        IProductCategoryRepository ProductCategories { get; }  // ← Add this
        IProductRepository Products { get; }
        IProductImageRepository ProductImages { get; }
        IReviewRepository Reviews { get; }
        IOrderRepository Orders { get; }
        IOrderItemsRepository Orderitems { get; }
        IPaymentRepository Payments { get; }
        IShippingRepository Shipping { get; }
        IWalletTransactionsRepository WalletTransactions { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public UnitOfWork(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
            Customers = new CustomerRepository(sqlDataAccess);
            ProductCategories = new ProductCategoryRepository(sqlDataAccess);
            Products = new ProductRepository(sqlDataAccess);
            ProductImages= new ProductImageRepository(sqlDataAccess);
            Reviews = new ReviewRepository(sqlDataAccess);
            Orders = new OrderRepository(sqlDataAccess);
            Orderitems = new OrderItemsRepository(sqlDataAccess);
            Payments = new PaymentRepository(sqlDataAccess);
            Shipping = new ShippingRepository(sqlDataAccess);
            WalletTransactions = new WalletTransactionsRepository(sqlDataAccess);
                RefreshTokenRepository = new RefreshTokenRepository(sqlDataAccess);


        }

        public ICustomerRepository Customers { get; }
        public IProductCategoryRepository ProductCategories { get; }
        public IProductRepository Products { get; }
        public IProductImageRepository ProductImages { get; }
        public IReviewRepository Reviews { get; }
        public IOrderRepository Orders { get; }
        public IOrderItemsRepository Orderitems { get; }
        public IPaymentRepository Payments { get; }
        public IShippingRepository Shipping { get; }
        public IWalletTransactionsRepository WalletTransactions { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
    }
}
