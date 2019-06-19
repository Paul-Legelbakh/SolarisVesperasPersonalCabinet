using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;
using System;

namespace PersonalCabinet.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly IRepository<Contact> userRepository;
        private readonly IRepository<Purchase> purchaseRepository;

        public UnitOfWork(IOptions<Settings> settings)
        {
            userRepository = Activator.CreateInstance(
                ReflectionHandler.GetClassFromInterface(typeof(IRepository<Contact>)), settings)
                as IRepository<Contact>;

            purchaseRepository = Activator.CreateInstance(
                ReflectionHandler.GetClassFromInterface(typeof(IRepository<Purchase>)), settings)
                as IRepository<Purchase>;
        }

        public IRepository<Contact> Users
            => userRepository ?? throw new MongoException("Can not to read user repository");

        public IRepository<Purchase> Purchases
            => purchaseRepository ?? throw new MongoException("Can not to read purchase repository");

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
