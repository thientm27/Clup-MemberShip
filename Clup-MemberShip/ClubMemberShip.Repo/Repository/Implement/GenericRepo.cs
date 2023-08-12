﻿using System.Linq.Expressions;
using ClubMemberShip.Repo.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubMemberShip.Repo.Repository.Implement;

public class GenericRepo<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ClubMembershipContext Context;

    public GenericRepo(ClubMembershipContext context)
    {
        this.Context = context;
    }

    public List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
    {
        return includes
            .Aggregate(Context.Set<TEntity>().AsQueryable(),
                (entity, property) => entity.Include(property))
            .Where(x => x.Status != Status.Deleted)
            .ToList();
    }

    public List<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        // return new List<TEntity>();
        // using var context = new ClubMembershipContext();
        IQueryable<TEntity> query = Context.Set<TEntity>();
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split
                         (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }

        return query.Where(x => x.Status != Status.Deleted).ToList();
    }

    public void CreateAutoId(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity? GetById(object? id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public IQueryable<TEntity> GetAll()
    {
        // using var context = new ClubMembershipContext();
        return Context.Set<TEntity>();
    }

    public void Create(TEntity entity)
    {
        // using var context = new ClubMembershipContext();
        Context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entityToUpdate)
    {
        // using var context = new ClubMembershipContext();
        Context.Set<TEntity>().Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public void Delete(object? id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            entity.Status = Status.Deleted;
            Update(entity);
        }
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }
}