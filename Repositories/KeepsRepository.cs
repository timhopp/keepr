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
      string sql = "SELECT * FROM Keeps WHERE userID = @userId";
      return _db.Query<Keep>(sql);
    }
    internal Keep GetById(int id)
    {
      string sql = "SELECT * FROM Keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }
    internal Keep Create(Keep newKeep)
    {
      string sql = @"INSERT INTO Keeps
            (userid, title, article, img, isprivate, views, shares, keeps)
            VALUES
            (@userid, @title, @article, @img, @isPrivate, @views, @shares, @keeps);
            SELECT LAST_INSERT_ID();";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }


    //NOTE NEED TO ADD USER ID BRUH
    internal bool Delete(string userId, int id)
    {
      string sql = "DELETE FROM Keeps WHERE id = @Id AND userId = @UserId LIMIT 1";
      int rowsAffected = _db.Execute(sql, new { userId, id });
      return rowsAffected == 1;
    }

    public bool Update(Keep updatedKeep)
    {
      string sql = @"
      UPDATE Keeps SET
      title = @title,
      article = @article,
      img = @img
      WHERE id = @id AND userid = @UserId LIMIT 1;";
      int rowsAffected = _db.Execute(sql, updatedKeep);
      return rowsAffected == 1;
    }
  }
}