using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }

    public Keep GetById(int id)
    {
      Keep foundKeep = _repo.GetById(id);
      if (foundKeep == null)
      {
        throw new Exception("Invalid Keep Id");
      }
      return foundKeep;
    }

    public IEnumerable<Keep> GetByUser(string userId)
    {
      return _repo.GetByUser(userId);


    }
    public IEnumerable<Keep> GetMyKeeps(string userId)
    {
      IEnumerable<Keep> foundKeeps = GetMyKeeps(userId);
      if (foundKeeps == null)
      {
        throw new Exception("Invalid user Id");
      }
      return foundKeeps;
    }
    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal object Delete(string userId, int id)
    {
      GetById(id);
      bool deleted = _repo.Delete(userId, id);
      if (!deleted)
      {
        throw new Exception("You can't delete a Keep you don't own!");
      }
      return "Deleted!";
    }

    //NOTE Need a way to toggle the IsPrivate 
    //Also, is a new function required to update the views and shares? Most likely
    public Keep Update(Keep updatedKeep)
    {
      Keep foundKeep = GetById(updatedKeep.Id);
      updatedKeep.Title = updatedKeep.Title == null ? updatedKeep.Title : foundKeep.Title;
      updatedKeep.Article = updatedKeep.Article == null ? updatedKeep.Article : foundKeep.Article;
      updatedKeep.Img = updatedKeep.Img == null ? updatedKeep.Img : foundKeep.Img;
      bool updated = _repo.Update(updatedKeep);
      if (!updated)
      {
        throw new Exception("You can't update a keep if you aren't the owner");
      }
      return updatedKeep;
    }

  }
}