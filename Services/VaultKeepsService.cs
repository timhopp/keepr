using System;
using Keepr.Models;
using System.Collections;
using System.Collections.Generic;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {

    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultService;

    public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
    {
      _repo = repository;
      _vaultService = vaultsService;
    }
    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      int id = _repo.Create(newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    //Don't think i'll ever need this? 
    internal object Get(string value)
    {
      throw new NotImplementedException();
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      return _repo.GetKeepsByVaultId(vaultId);
    }

    internal object Delete(string userId, int id)
    {
      VaultKeep Delete = GetByVaultKeepId(id);
      bool deleted = _repo.Delete(userId, id, Delete.KeepId);
      if (!deleted)
      {
        throw new Exception("You can't delete a Vault Keep you don't own!");
      }
      return "Deleted!";
    }


    public VaultKeep GetByVaultKeepId(int id)
    {
      VaultKeep foundVaultKeep = _repo.GetByVaultKeepId(id);
      if (foundVaultKeep == null)
      {
        throw new Exception("Invalid Keep Id");
      }
      return foundVaultKeep;
    }
  }
}