using System.Collections.Generic;
using System.Linq;

namespace EcoStore.Web.Models
{
    public class Cart
    {
        private List<CartLine> _cartLines;

        public Cart()
        {
            _cartLines = new List<CartLine>();
        }

        public void AddItem(Product product, int amount)
        {
            var existedProductLine = _cartLines.FirstOrDefault(p => p.Product.Title.Equals(product.Title));
            if (existedProductLine == null)
            {
                _cartLines.Add(new CartLine
                {
                    Product = product,
                    Amount = amount,
                });
            }
            else
            {
                existedProductLine.Amount += amount;
            }
        }

        public void RemoveProduct(string productTitle)
        {
            _cartLines.RemoveAll(c => c.Product.Title.Equals(productTitle));
        }

        public decimal ComputeTotalValue()
        {
            return _cartLines.Sum(c => c.Product.Price * c.Amount);

        }
        public void Clear()
        {
            _cartLines.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get => _cartLines;
        }
    }
}
