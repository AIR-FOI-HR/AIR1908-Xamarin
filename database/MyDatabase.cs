﻿using System;
using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;
using database.Entities;

namespace database
{
    public class MyDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public MyDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Store>().Wait();
            _database.CreateTableAsync<Discount>().Wait();
        }

        public Task<List<Store>> GetStores()
        {
            return _database.Table<Store>().ToListAsync();
        }

        public Task<List<Discount>> GetDiscounts()
        {
            return _database.Table<Discount>().ToListAsync();
        }

        public Task<int> InsertStores(Store store)
        {
            return _database.InsertAsync(store);
        }

        public Task<int> InsertDiscounts(Discount discount)
        {
            return _database.InsertAsync(discount);
        }

        public Task<List<Discount>> GetDiscountByStoreId(int storeID)
        {
            return _database.QueryAsync<Discount>("SELECT * FROM Discount WHERE storeId = " + storeID);
        }

        public Task<List<Discount>> GetDiscountById(int discountID)
        {
            return _database.QueryAsync<Discount>("SELECT * FROM Discount WHERE ID = " + discountID);
        }

        public Task<int> DeleteDiscount(Discount discount)
        {
            return _database.DeleteAsync(discount);
        }

        public Task<int> DeleteStore(Store store)
        {
            return _database.DeleteAsync(store);
        }

        public Task<List<Discount>> GetAllDiscountsByStoreId(int storeID)
        {
            return _database.QueryAsync<Discount>("SELECT * FROM Discount WHERE storeId = " + storeID);
        }

        public Task<List<Store>> GetStoreById(int storeID)
        {
            return _database.QueryAsync<Store>("SELECT * FROM Store WHERE ID = " + storeID);
        }

    }
}
