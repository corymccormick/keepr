using System;
using System.Collections.Generic;
using keepr.server.Models;
using keepr.server.Repositories;

namespace keepr.server.Services
{
  public class AccountService
  {
    private readonly AccountRepository _repo;
    private readonly KeepsRepository _keepsRepository;

    public AccountService(AccountRepository repo, KeepsRepository keepsRepository)
    {
      _repo = repo;
      _keepsRepository = keepsRepository;
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