using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.server.Models;
using static keepr.server.Models.VaultKeep;

namespace keepr.server.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal object Create(VaultKeep vk)
    {
      string sql = @"
            INSERT INTO 
                vault_keeps(creatorId, vaultId, keepId)
            VALUES(@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID();
            ";
      vk.Id = _db.ExecuteScalar<int>(sql, vk);
      return vk;
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT
        vk.*
      FROM
        vault_keeps vk
      WHERE
         vk.id = @id";
      return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vault_keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }


    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, bool excludePrivate)
    {
      string sql = $@"
                 SELECT
                k.*,
                vk.id as vaultKeepId,
                vk.keepId as keepId,
                vk.vaultId as vaultId
                FROM
                vault_keeps vk
                JOIN vaults v ON v.id = vk.vaultId
                JOIN keeps k ON k.id = vk.keepId
                WHERE
                vk.vaultId = @vaultId
                {(excludePrivate ? "AND v.isPrivate = FALSE" : "")}
                ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
    }

    internal IEnumerable<VaultKeepViewModel> GetVaultByKeepsId(int vaultId)
    {
      string sql = @"
                SELECT
                k.*,
                vk.id as vaultKeepId,
                vk.keepId as keepId,
                vk.vaultId as vaultId
                FROM
                vault_keeps vk
                JOIN vaults v ON v.id = vk.vaultId
                JOIN keeps k ON k.id = vk.keepId
                WHERE
                vk.vaultId = @vaultId;
            ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
    }
  }
}
