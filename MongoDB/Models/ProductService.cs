using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.Models
{
    public class ProductService
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<Product> ProductCollection;//جدول محصولات=>ProductCollection
        public ProductService()
        {
            var client= new MongoClient();
            db = client.GetDatabase("MyDatabase");
            ProductCollection = db.GetCollection<Product>("Products");
        }

        public bool AddProduct(Product product)
        {
            try
            {
                ProductCollection.InsertOne(product);//افزودن به جدول محصولات
                return true;
            }
            catch (Exception ex) { return false; }


        }
        public bool UpdateProduct(Product product) 
        {
            var filter=Builders<Product>.Filter.Eq(p=>p.Id , product.Id);
            var productForUpdate = Builders<Product>.Update
                .Set(p => p.Name, product.Name)
                .Set(p => p.Price, product.Price)
                .Set(p => p.Category, product.Category)
                .Set(p => p.Description, product.Description);
             var updateResult= ProductCollection.UpdateOne(filter, productForUpdate);
            if (updateResult.ModifiedCount == 1 && updateResult.MatchedCount ==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public  Product GetProductById(Guid id)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Id , id);
            Product product= ProductCollection.Find(filter).FirstOrDefault();
            return product;
        }
        public List<Product> GetAllProducts()
        {
            return ProductCollection.Find(new BsonDocument()).ToList();
        }
        public bool DeleteProductById(Guid id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = ProductCollection.DeleteOne(filter);
            if (deleteResult.DeletedCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
