using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM Keeps WHERE isPrivate = 0;";
      return _db.Query<Keep>(sql);
    }

    internal IEnumerable<Keep> GetByUser(string userId)
    {
      string sql = "SELECT * FROM Keeps WHERE userId = @userId";
      return _db.Query<Keep>(sql, new { userId });
    }
    internal Keep GetById(int id)
    {
      string sql = "SELECT * FROM Keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }
    internal Keep Create(Keep newKeep)
    {
      string sql = @"INSERT INTO Keeps
            (userid, name, description, img, isprivate, views, shares, keeps)
            VALUES
            (@userid, @name, @description, @img, @isPrivate, @views, @shares, @keeps);
            SELECT LAST_INSERT_ID();";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }


    //NOTE NEED TO ADD USER ID BRUH
    //string userId, userId,  AND userId = @UserId 
    internal bool Delete(int id)
    {
      string sql = "DELETE FROM Keeps WHERE id = @Id LIMIT 1";
      int rowsAffected = _db.Execute(sql, new { id });
      return rowsAffected == 1;
    }

    public bool Update(Keep updatedKeep)
    {
      string sql = @"
      UPDATE Keeps SET
      name = @name,
      description = @description,
      img = @img,
      isPrivate = @isPrivate,
      shares = @shares,
      views = @views, 
      keeps = @keeps
      WHERE id = @id AND userid = @userId LIMIT 1;";
      int rowsAffected = _db.Execute(sql, updatedKeep);
      return rowsAffected == 1;
    }
  }
}