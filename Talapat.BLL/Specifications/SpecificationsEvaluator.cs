using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities;

namespace Talapat.BLL.Specifications
{
    public class SpecificationsEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity>GetQuery(IQueryable<TEntity>inputQuery,ISpecicfication<TEntity>Spec)
        {
            var Query = inputQuery;
            if (Spec.Criteria != null)
                Query = Query.Where(Spec.Criteria);
            if (Spec.OrderBy != null)
                Query = Query.OrderBy(Spec.OrderBy);
            if (Spec.OrderByDescinding != null)
                Query = Query.OrderByDescending(Spec.OrderByDescinding);
            if (Spec.IsBaginationEnabled)
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            Query = Spec.Includes.Aggregate(Query, (currentQuery, Include) => currentQuery.Include(Include));
            return Query;
        }
    }
}
