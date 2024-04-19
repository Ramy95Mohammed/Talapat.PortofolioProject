using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities;

namespace Talapat.BLL.Specifications.ProductSpecification
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        //This Constructor Is Used For Get All Products
        public ProductWithTypeAndBrandSpecification(ProductSpecParams productParams) : base(p =>
       (string.IsNullOrEmpty(productParams.SearchValue) || p.Name.ToLower().Contains(productParams.SearchValue))&& 
        (!productParams.typeId.HasValue || p.ProductTypeId == productParams.typeId.Value) && (!productParams.brandId.HasValue || p.ProductBrandId == productParams.brandId.Value))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);
            
            
            ApplyPagination(productParams.PageSize * (productParams.PageIndex -1 ) , productParams.PageSize);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescinding(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }

        }
        //This Constructor Is Used For Get Specific Product With Id
        public ProductWithTypeAndBrandSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
