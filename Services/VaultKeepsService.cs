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

    internal IEnumerable<VaultKeep> GetKeepsByVaultId(int vaultId)
    {
      return _repo.GetKeepsByVaultId(vaultId);
    }
  }
}