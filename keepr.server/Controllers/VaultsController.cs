using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.server.Models;
using keepr.server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static keepr.server.Models.VaultKeep;

namespace keepr.server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly AccountService _accountService;


    public VaultsController(VaultsService vaultsService, AccountService accountService)
    {
      _vaultsService = vaultsService;
      _accountService = accountService;

    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Account fullAccount = _accountService.GetOrCreateProfile(userInfo);
        newVault.CreatorId = userInfo.Id;
        Vault vault = _vaultsService.Create(newVault);
        vault.Creator = fullAccount;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vault>> GetAll()
    {
      try
      {
        IEnumerable<Vault> vaults = _vaultsService.GetAll();
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Vault> GetById(int id)
    {
      try
      {
        Vault found = _vaultsService.GetById(id);
        return Ok(found);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [Authorize]
    [HttpPut("{id}")]

    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault v)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        v.Id = id;
        Vault newVault = _vaultsService.Update(v, userInfo.Id);
        newVault.Creator = userInfo;
        return Ok(newVault);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        // REVIEW DO NOT TRUST THE CLIENT..... EVER
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vaultsService.Delete(id, userInfo.Id);
        return Ok("Successfully Removed");

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }



  }
}