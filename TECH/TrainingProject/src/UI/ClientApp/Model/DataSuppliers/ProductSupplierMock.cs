using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Media.Imaging;

namespace ClientApp.Model.DataSuppliers
{
    class ProductSupplierMock : IProductSupplier
    {
        private readonly List<Product> _products;

        public ProductSupplierMock()
        {
            _products = new List<Product>();

            string imageDirFolder = ConfigurationManager.AppSettings["TestDataFolder"];

            List<BitmapImage> p1Images = new List<BitmapImage>()
            {
                new BitmapImage(new Uri($"{imageDirFolder}1_1.jpg")),
                new BitmapImage(new Uri($"{imageDirFolder}1_2.jpg"))
            };

            _products.Add(
                Product.CreateProduct(Guid.NewGuid(),
                    "Корнишоны маринованные",
                    "Компания «СелижароВО» производит овощные консервы по традиционным рецептам. Свежие помидоры, отборные огурцы, пряный перец и другие овощи становятся основой для натуральных консервов «СелижароВО». В консервации сохраняются витамины и микроэлементы, которыми богаты свежие овощи. А удивительное разнообразие вкусов делает овощные консервы одним из неотъемлемых продуктов русского стола.",
                    p1Images[0],
                    p1Images
                )
            );

            List<BitmapImage> p2Images = new List<BitmapImage>()
            {
                new BitmapImage(new Uri($"{imageDirFolder}2_1.jpg")),
                new BitmapImage(new Uri($"{imageDirFolder}2_2.jpg")),
                new BitmapImage(new Uri($"{imageDirFolder}2_3.jpg"))
            };

            _products.Add(
                Product.CreateProduct(Guid.NewGuid(),
                    "Овощное ассорти Дворек",
                    "Как сделать повседневный обед или ужин настоящим праздником? Как устроить пикник зимой, да еще и прямо у себя дома? Конечно, с помощью ярких, аппетитных, сочных овощей. Ассорти из спелых томатов, хрустящих огурчиков, пикантных патиссонов сделают каждый ужин настоящим праздником вкуса и украсят стол, сделав его по-летнему ярким.",
                    p2Images[0],
                    p2Images
                )
            );

            List<BitmapImage> p3Images = new List<BitmapImage>()
            {
                new BitmapImage(new Uri($"{imageDirFolder}3_1.jpg")),
                new BitmapImage(new Uri($"{imageDirFolder}3_2.jpg")),
                new BitmapImage(new Uri($"{imageDirFolder}3_3.jpg"))
            };

            _products.Add(
                Product.CreateProduct(Guid.NewGuid(),
                    "Чесночные огурцы",
                    "Традиционные маринованные огурцы с добавлением чеснока приготовлены по старинным польским рецептам с использованием современных технологий. Прекрасно дополнят первые и вторые блюда, также станут отличной закуской.",
                    p3Images[0],
                    p3Images
                )
            );
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(Guid productId)
        {
            return _products.Find(p => p.Id.Equals(productId));
        }
    }
}
