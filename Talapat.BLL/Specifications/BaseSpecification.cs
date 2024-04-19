using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talapat.BLL.Specifications
{
    public class BaseSpecification<T> : ISpecicfication<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria;
        }
        public BaseSpecification()
        {
                
        }
        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescinding { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsBaginationEnabled { get; set; }

        public void AddInclude(Expression<Func<T, object>> Include)
        {
            Includes.Add(Include);
        }
        public void AddOrderBy(Expression<Func<T, object>> oderBy)
        {
            OrderBy = oderBy;
        }
        public void AddOrderByDescinding(Expression<Func<T, object>> oderByDesc)
        {
            OrderByDescinding = oderByDesc;
        }
        public void ApplyPagination(int skip , int take)
        {
            IsBaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
