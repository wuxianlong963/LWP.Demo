using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LWP.Lib.Unity;

namespace LWP.Lib
{
    public class ProductClassManager
    {
        public bool AddProductClass(ProductClass pClass)
        {
            if (string.IsNullOrWhiteSpace(pClass.No))
            {
                var p = GetProductClass(pClass.ParentId);
                var pNo = p != null ? p.No : "";

                var list = GetProductClassByParentId(pClass.ParentId);
                if (list.Any())
                {
                    var entity = list.OrderByDescending(d => d.No).First();
                    pClass.No = pNo + DataFmt.GetIncreaseClassNo(entity.No.Substring(entity.No.Length - 4));
                }
                else
                {
                    pClass.No = pNo + DataFmt.GetIncreaseClassNo("");
                }
            }

            using (var entity = new YusongCompanyEntities())
            {
                entity.ProductClasses.AddObject(pClass);
                entity.SaveChanges();
            }

            return true;
        }

        public bool UpdateProductClass(ProductClass pClass)
        {
            using (var entity = new YusongCompanyEntities())
            {
                //var p = entity.
            }

            return false;
        }

        public ProductClass GetProductClass(int id)
        {
            using (var entity = new YusongCompanyEntities())
            {
                return entity.ProductClasses.Where(d => d.Id == id).FirstOrDefault();
            }
        }

        public IList<ProductClass> GetAllProductClass()
        {
            using (var entity = new YusongCompanyEntities())
            {
                return entity.ProductClasses.ToList();
            }
        }

        public IList<ProductClass> GetProductClassByParentId(int parentId)
        {
            using (var entity = new YusongCompanyEntities())
            {
                return entity.ProductClasses.Where(d=>d.ParentId == parentId).ToList();
            }
        }

        public IList<ProductClass> GetProductClassList(string classNo, bool includeSelf)
        {
            if (string.IsNullOrWhiteSpace(classNo)) return null;

            using (var entity = new YusongCompanyEntities())
            {
                var query = entity.ProductClasses.Where(d => d.No.Contains(classNo)).ToList();
                if(includeSelf)
                    return  query;

                return query.Where(d => d.No != classNo).ToList();
            }
        }
    }
}
