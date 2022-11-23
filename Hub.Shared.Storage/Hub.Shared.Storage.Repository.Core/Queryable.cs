using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hub.Shared.Storage.Repository.Core;

public class Queryable<TEntity> where TEntity : EntityBase
{
    public Expression<Func<TEntity, object>> OrderBy { get; init; }
    public Expression<Func<TEntity, object>> OrderByDescending { get; init; }
    public Expression<Func<TEntity, object>> ThenOrderByDescending { get; init; }
    public Expression<Func<TEntity, bool>> Where { get; set; }
    public Expression<Func<TEntity, object>> Include
    {
        get => Includes.FirstOrDefault();
        set => Includes.Add(value);
    }

    public IList<Expression<Func<TEntity, object>>> Includes { get; init; } = new List<Expression<Func<TEntity, object>>>();
    public int? Take { get; set; }
    public int? Skip { get; set; }
}