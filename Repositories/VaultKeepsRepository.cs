using System;
using System.Collections.Generic;
using Keepr.Models;
using Dapper;
using System.Data;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal int Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO VaultKeeps
      (vaultId, keepId)
      VALUES
      (@VaultId, @KeepId);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }

    internal IEnumerable<VaultKeep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
        SELECT
        keeps.*,
        vaultkeeps.id as vaultKeepId,
        vault.name as vaultName
        FROM VaultKeeps
        INNER JOIN keeps on keeps.id = vaultkeeps.keepId
        WHERE(vaultkeeps.vaultId = @vaultId)";
      return _db.Query<VaultKeep>(sql, new { vaultId });
    }
  }
}