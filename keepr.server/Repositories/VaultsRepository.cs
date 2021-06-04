using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.server.Models;

namespace keepr.server.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (CreatorId, name, description, isPrivate)
      VALUES
      (@CreatorId, @Name, @Description, @IsPrivate);
      SELECT LAST_INSERT_ID()";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }

    internal IEnumerable<Vault> GetAll()
    {
      string sql = @"
         SELECT  
         v.*,
         a.* 
         FROM vaults v 
         JOIN accounts a ON v.creatorId = a.id;";

      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, splitOn: "id");
    }

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT 
        v.*,
        a.* 
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id";
      return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }
      , new { id }, splitOn: "id").FirstOrDefault();
    }

    internal Vault Update(Vault v)
    {
      string sql = @"
            UPDATE vaults 
            SET 
                name = @Name,
                description = @Description,
                isPrivate = @IsPrivate
            WHERE id = @Id;
            ";
      _db.Execute(sql, v);
      return v;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    internal IEnumerable<Vault> GetByVaultId(int Id)
    {
      string sql = $@"
                SELECT 
                    v.*,
                    a.* 
                FROM vaults v
                JOIN accounts a ON a.id = v.creatorId
                WHERE v.creatorId = @Id 
                ";
      // if excludePrivate is true add more to sql string before call where statement interpolation if needed 
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
  {
    v.Creator = p;
    return v;
  }, new { Id }).ToList();
    }

    internal IEnumerable<Vault> GetVaultsByProfileId(string accountId, bool excludePrivate)
    {
      string sql = $@"
                SELECT 
                    v.*,
                    a.* 
                FROM vaults v
                JOIN accounts a ON a.id = v.creatorId
                WHERE v.creatorId = @accountId 
                {(excludePrivate ? "AND v.isPrivate = FALSE" : "")}
                ";
      // if excludePrivate is true add more to sql string before call where statement interpolation if needed 
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
  {
    v.Creator = p;
    return v;
  }, new { accountId }).ToList();
    }
  }
}


