using System;
using System.Collections.Generic;
using keepr.server.Models;
using keepr.server.Repositories;

namespace keepr.server.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkrepo;
    public VaultKeepsService(VaultKeepsRepository vkrepo)
    {
      _vkrepo = vkrepo;
    }

    internal object createVaultKeep(VaultKeep vk)
    {
      return _vkrepo.Create(vk);
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep vaultKeep = _vkrepo.GetById(id);
      if (vaultKeep.CreatorId != userId)
      {
        throw new Exception("You cannot delete another users vault keep");
      }
    }

  }
}
