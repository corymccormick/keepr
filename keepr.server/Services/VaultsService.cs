using System;
using keepr.server.Models;
using System.Collections.Generic;
using keepr.server.Repositories;
using static keepr.server.Models.VaultKeep;

namespace keepr.server.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepository;
    private readonly VaultKeepsRepository _vkrepo;

    public VaultsService(VaultsRepository vaultsRepository, VaultKeepsRepository vkrepo)
    {
      _vaultsRepository = vaultsRepository;
      _vkrepo = vkrepo;
    }

    internal Vault Create(Vault newVault)
    {
      return _vaultsRepository.Create(newVault);
    }

    internal IEnumerable<Vault> GetAll()
    {
      return _vaultsRepository.GetAll();
    }

    internal Vault GetById(int id)
    {
      Vault vault = _vaultsRepository.GetById(id);
      if (vault == null)
      {
        throw new Exception("Invalid Vault Id");
      }
      return vault;
    }

    internal Vault Update(Vault v, string id)
    {
      Vault vault = _vaultsRepository.GetById(v.Id);
      if (vault == null)
      {
        throw new Exception("Invalid keep Id");
      }
      if (vault.CreatorId != id)
      {
        throw new Exception("You are not allowed to edit");
      }

      return _vaultsRepository.Update(v);
    }

    internal void Delete(int id, string creatorId)
    {
      Vault vault = GetById(id);
      if (vault.CreatorId != creatorId)
      {
        throw new Exception("You cannot delete another users vault");
      }
      _vaultsRepository.Delete(id);
    }


    internal IEnumerable<Vault> GetVaultsByProfileId(string id, string UserId)
    {
      return _vaultsRepository.GetVaultsByProfileId(id, UserId != id);

    }

    internal IEnumerable<VaultKeepViewModel> GetKeeps(int vaultId)
    {
      return _vkrepo.GetKeepsByVaultId(vaultId);
      // NOTE start over in vault controller moving to keeps service and kicking to vkrepo getkeepsbyvaultId
    }

    internal IEnumerable<Vault> GetKeepsByVaultId(int id)
    {
      return _vaultsRepository.GetByVaultId(id);
    }

    internal IEnumerable<VaultKeepViewModel> GetVaultKeepsById(int vaultId)
    {
      return _vkrepo.GetVaultByKeepsId(vaultId);
    }
  }
}