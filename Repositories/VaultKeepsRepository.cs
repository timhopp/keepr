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
      (vaultId, keepId, userId)
      VALUES
      (@VaultId, @KeepId, @userId);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
        SELECT
        keeps.*,
        vaultkeeps.id as vaultKeepId
        FROM VaultKeeps
        INNER JOIN keeps on keeps.id = vaultkeeps.keepId
        WHERE(vaultkeeps.vaultId = @vaultId)";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId });
    }
  }
}