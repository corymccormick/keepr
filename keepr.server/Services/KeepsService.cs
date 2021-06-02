using System;
using System.Collections.Generic;
using keepr.server.Models;
using keepr.server.Repositories;

namespace keepr.server.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
      _keepsRepository = keepsRepository;
    }
    internal IEnumerable<Keep> GetAll()
    {
      return _keepsRepository.GetAll();
    }

    internal Keep Create(Keep newKeep)
    {
      return _keepsRepository.Create(newKeep);
    }

    internal Keep GetById(int id)
    {
      Keep keep = _keepsRepository.GetById(id);
      if (keep == null)
      {
        throw new Exception("Invalid Keep Id");
      }
      return keep;
    }

    internal Keep Update(Keep k, string id)
    {
      // Business Logic
      Keep keep = _keepsRepository.GetById(k.Id);

      // let x = findOneAndUpdate({userId: userId, id: id}, update)

      if (keep == null)
      {
        throw new Exception("Invalid keep Id");
      }
      if (keep.CreatorId != id)
      {
        throw new Exception("You are not allowed to edit");
      }

      return _keepsRepository.Update(k);
    }

    internal void Delete(int id, string creatorId)
    {
      Keep keep = GetById(id);
      if (keep.CreatorId != creatorId)
      {
        throw new Exception("You cannot delete another users keep");
      }
      _keepsRepository.Delete(id);
    }
  }
}