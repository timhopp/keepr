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
      SELECT LAST_INSERT_ID();
      UPDATE Keeps SET keeps = keeps + 1 WHERE id = @keepId";
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
    internal bool Delete(string userid, int id, int keepId)
    {
      string sql = @"DELETE FROM VaultKeeps WHERE id = @Id AND userid = @UserId LIMIT 1; UPDATE Keeps SET keeps = keeps - 1 WHERE id = @keepId;
      ";
      int rowsAffected = _db.Execute(sql, new { userid, id, keepId });
      return rowsAffected == 2;
    }


    internal VaultKeep GetByVaultKeepId(int id)
    {
      string sql = "SELECT * FROM VaultKeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}