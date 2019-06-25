using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonalCabinet.DAL.Repositories;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;
using System;

namespace PersonalCabinet.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly IGenericRepository<Contact> userRepository;
        private readonly IGenericRepository<Purchase> purchaseRepository;

        public UnitOfWork(IOptions<Settings> settings)
        {
            userRepository = new UserRepository(settings);

            purchaseRepository = new PurchaseRepository(settings);
        }

        public IGenericRepository<Contact> Users
            => userRepository ?? throw new MongoException("Can not to read user repository");

        public IGenericRepository<Purchase> Purchases
            => purchaseRepository ?? throw new MongoException("Can not to read purchase repository");

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
