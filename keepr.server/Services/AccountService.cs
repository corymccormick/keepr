using System;
using keepr.server.Models;
using keepr.server.Repositories;

namespace keepr.server.Services
{
  public class AccountService
  {
    private readonly AccountRepository _repo;
    public AccountService(AccountRepository repo)
    {
      _repo = repo;
    }



    internal Account GetOrCreateProfile(Account userInfo)
    {
      Account profile = _repo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Profile GetProfileById(string id)
    {
      return _repo.GetById(id);
    }
  }
}