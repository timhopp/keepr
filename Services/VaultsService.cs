using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Repositories;
using Keppr.Models;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    public Vault GetById(string userId, int id)
    {
      Vault foundVault = _repo.GetById(userId, id);
      if (foundVault == null)
      {
        throw new Exception("Invalid Vault Id");
      }
      return foundVault;
    }

    public IEnumerable<Vault> GetByUser(string userId)
    {
      return _repo.GetByUser(userId);
    }
    public IEnumerable<Vault> GetMyVaults(string userId)
    {
      IEnumerable<Vault> foundVaults = _repo.GetByUser(userId);
      if (foundVaults == null)
      {
        throw new Exception("Invalid user Id");
      }
      return foundVaults;
    }
    public Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }


    internal object Delete(string userId, int id)
    {
      GetById(userId, id);
      bool deleted = _repo.Delete(userId, id);
      if (!deleted)
      {
        throw new Exception("You can't delete a Vault you don't own!");
      }
      return "Deleted!";
    }

    //NOTE Need a way to toggle the IsPrivate 
    //Also, is a new function required to update the views and shares? Most likely
    public Vault Update(Vault updatedVault)
    {
      Vault foundVault = GetById(updatedVault.UserId, updatedVault.Id);
      updatedVault.Name = updatedVault.Name == null ? foundVault.Name : updatedVault.Name;
      updatedVault.Description = updatedVault.Description == null ? foundVault.Description : updatedVault.Description;

      bool updated = _repo.Update(updatedVault);
      if (!updated)
      {
        throw new Exception("You can't update a Vault if you aren't the owner");
      }
      return updatedVault;
    }

  }
}