using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWP.Lib
{
    public class ProductManage
    {
        public bool AddProduct(Product product)
        {
            using (var entity = new YusongCompanyEntities())
            {
                entity.Products.AddObject(product);
                entity.SaveChanges();
            }

            return true;
        }
    }
}
