using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.server.Models;

namespace keepr.server.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> GetAll()
    {
      string sql = @"
         SELECT  
         k.*,
         a.* 
         FROM keeps k 
         JOIN accounts a ON k.creatorId = a.id;";

      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, splitOn: "id");
    }

    internal Keep GetById(int id)
    {
      string sql = @"
      SELECT 
        k.*,
        a.* 
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id";
      return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }
      , new { id }, splitOn: "id").FirstOrDefault();
    }

    internal IEnumerable<Keep> GetByProfileId(string accountId)
    {
      string sql = @"
                SELECT 
                    k.*,
                    a.* 
                FROM keeps k
                JOIN accounts a ON a.id = k.creatorId
                WHERE k.creatorId = @accountId ;
                ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
  {
    k.Creator = p;
    return k;
  }, new { accountId }).ToList();
    }



    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (CreatorId, name, description, img, views, keeps)
      VALUES
      (@CreatorId, @Name, @Description, @Img, @Views, @Keeps);
      SELECT LAST_INSERT_ID()";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }

    internal Keep Update(Keep k)
    {
      string sql = @"
            UPDATE keeps 
            SET 
                name = @Name,
                description = @Description,
                img = @Img
            WHERE id = @Id;
            ";
      _db.Execute(sql, k);
      return k;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}
