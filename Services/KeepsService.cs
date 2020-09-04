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

    public Keep Update(Keep updatedKeep)
    {
      Keep foundKeep = GetById(updatedKeep.Id);
      updatedKeep.Name = updatedKeep.Name == null ? updatedKeep.Name : foundKeep.Name;
      updatedKeep.Description = updatedKeep.Description == null ? updatedKeep.Description : foundKeep.Description;
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