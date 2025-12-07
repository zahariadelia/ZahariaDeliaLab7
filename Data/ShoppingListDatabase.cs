using SQLite;
using ZahariaDeliaLab7.Models;

namespace ZahariaDeliaLab7.Data
{
    public class ShopListDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ShopListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
        }

        // ShopList CRUD
        public Task<int> SaveShopListAsync(ShopList shoplist)
            => shoplist.ID != 0 ? _database.UpdateAsync(shoplist) : _database.InsertAsync(shoplist);

        public Task<int> DeleteShopListAsync(ShopList shoplist)
            => _database.DeleteAsync(shoplist);

        // Product CRUD
        public Task<int> SaveProductAsync(Product product)
            => product.ID != 0 ? _database.UpdateAsync(product) : _database.InsertAsync(product);

        public Task<int> DeleteProductAsync(Product product)
            => _database.DeleteAsync(product);

        public Task<List<Product>> GetProductsAsync()
            => _database.Table<Product>().ToListAsync();

        // ListProduct (associations)
        public Task<int> SaveListProductAsync(ListProduct listp)
            => listp.ID != 0 ? _database.UpdateAsync(listp) : _database.InsertAsync(listp);

        public Task<int> DeleteListProductAsync(ListProduct listp)
            => _database.DeleteAsync(listp);

        public Task<List<Product>> GetListProductsAsync(int shoplistid)
            => _database.QueryAsync<Product>(
                "select P.ID, P.Description from Product P " +
                "inner join ListProduct LP on P.ID = LP.ProductID " +
                "where LP.ShopListID = ?",
                shoplistid);

        public async Task<ListProduct?> GetListProductAssociationAsync(int shoplistId, int productId)
        {
            var res = await _database.QueryAsync<ListProduct>(
                "select * from ListProduct where ShopListID = ? and ProductID = ?",
                shoplistId, productId);
            return res.Count > 0 ? res[0] : null;
        }
        public async Task<ListProduct?> GetListProductAssociationAsync(int shoplistId, int productId)
        {
            var res = await _database.QueryAsync<ListProduct>(
                "select * from ListProduct where ShopListID = ? and ProductID = ?",
                shoplistId, productId);
            return res.Count > 0 ? res[0] : null;
        }

        public Task<int> DeleteListProductAsync(ListProduct listp)
        {
            return _database.DeleteAsync(listp);
        }

    }
}
