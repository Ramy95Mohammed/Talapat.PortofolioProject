using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talapat.BLL.Specifications.ProductSpecification
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        public int _PageSize = 5;
        public int PageSize { get { return _PageSize; } set { _PageSize = value > MaxPageSize ? 50:value; } }
        public string? Sort { get; set; }
        public int? typeId { get; set; }
        public int? brandId { get; set; }
        private string _SearchValue;
        public string SearchValue { get { return _SearchValue; } set { _SearchValue = value.ToLower(); } }
    }
}
