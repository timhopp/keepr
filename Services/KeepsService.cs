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

    public Keep GetById(string userId, int id)
    {
      Keep foundKeep = _repo.GetById(id);
      if (foundKeep.IsPrivate)
      {
        Keep foundPrivateKeep = _repo.GetPrivateById(userId, id);
        if (foundPrivateKeep == null)
        {
          throw new Exception("Invalid User");
        }
        return foundPrivateKeep;
      }
      if (foundKeep == null)
      {
        throw new Exception("Invalid Keep Id");
      }
      return foundKeep;
    }
    public Keep GetByIdPublic(int id)
    {
      Keep foundKeep = _repo.GetById(id);
      if (foundKeep == null)
      {
        throw new Exception("Invalid User");
      }
      else if (foundKeep.IsPrivate == true)
      {
        throw new Exception("You can't access this");
      };

      return foundKeep;
    }

    public IEnumerable<Keep> GetByUser(string userId)
    {
      return _repo.GetByUser(userId);


    }
    public IEnumerable<Keep> GetMyKeeps(string userId)
    {
      IEnumerable<Keep> foundKeeps = _repo.GetByUser(userId);
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

    //string userId, userId,
    internal object Delete(string userId, int id)
    {
      GetById(userId, id);
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
      Keep foundKeep = GetById(updatedKeep.UserId, updatedKeep.Id);
      updatedKeep.Name = updatedKeep.Name == null ? foundKeep.Name : updatedKeep.Name;
      updatedKeep.Description = updatedKeep.Description == null ? foundKeep.Description : updatedKeep.Description;
      updatedKeep.Img = updatedKeep.Img == null ? foundKeep.Img : updatedKeep.Img;
      updatedKeep.IsPrivate = updatedKeep.IsPrivate == false ? foundKeep.IsPrivate : updatedKeep.IsPrivate;
      bool updated = _repo.Update(updatedKeep);
      if (!updated)
      {
        throw new Exception("You can't update a keep if you aren't the owner");
      }
      return updatedKeep;
    }

  }
}
