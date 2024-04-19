using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities;

namespace Talapat.BLL.Specifications.ProductSpecification
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams) : base(p =>
         (string.IsNullOrEmpty(productParams.SearchValue) || p.Name.ToLower().Contains(productParams.SearchValue)) && 
        (!productParams.typeId.HasValue || p.ProductTypeId == productParams.typeId.Value) && (!productParams.brandId.HasValue || p.ProductBrandId == productParams.brandId.Value))
        {

        }
    }
}
