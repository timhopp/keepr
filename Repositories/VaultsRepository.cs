using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keppr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }


    internal IEnumerable<Vault> GetByUser(string userId)
    {
      string sql = "SELECT * FROM Vaults WHERE userId = @UserId";
      return _db.Query<Vault>(sql, new { userId });
    }
    internal Vault GetById(string userId, int id)
    {
      string sql = "SELECT * FROM Vaults WHERE id = @Id AND userId = @userId";
      return _db.QueryFirstOrDefault<Vault>(sql, new { userId, id });
    }
    internal Vault Create(Vault newVault)
    {
      string sql = @"INSERT INTO Vaults
            (userid, name, description)
            VALUES
            (@userid, @name, @description);
            SELECT LAST_INSERT_ID();";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }


    //NOTE NEED TO ADD USER ID BRUH
    //string userId, userId,  AND userId = @UserId 
    internal bool Delete(string userid, int id)
    {
      string sql = "DELETE FROM Vaults WHERE id = @Id AND userid = @UserId LIMIT 1;";
      int rowsAffected = _db.Execute(sql, new { userid, id });
      return rowsAffected == 1;
    }

    public bool Update(Vault updatedVault)
    {
      string sql = @"
      UPDATE Vaults SET
      name = @name,
      description = @description
      WHERE id = @id AND userid = @userId LIMIT 1;";
      int rowsAffected = _db.Execute(sql, updatedVault);
      return rowsAffected == 1;
    }
  }
}