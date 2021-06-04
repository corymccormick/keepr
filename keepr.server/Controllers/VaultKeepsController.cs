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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    private readonly AccountService _accountService;
    private readonly KeepsService _keepsService;
    public VaultKeepsController(VaultKeepsService vks, AccountService accountService, KeepsService keepsService)
    {
      _vks = vks;
      _accountService = accountService;
      _keepsService = keepsService;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateAsync([FromBody] VaultKeep vk)
    {
      try
      {
        Account userinfo = await HttpContext.GetUserInfoAsync<Account>();
        vk.CreatorId = userinfo.Id;
        var newVk = _vks.createVaultKeep(vk);
        return Ok(newVk);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // [HttpGet("{id}/keeps")]
    // public ActionResult<IEnumerable<VaultKeepViewModel>> GetVaultskeepsById(int id)
    // {
    //   try
    //   {
    //     IEnumerable<VaultKeepViewModel>  = .GetVaultsKeepsById(id);
    //     return Ok();
    //   }
    //   catch (System.Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<VaultKeep>> Delete(int id)
    {
      try
      {
        // REVIEW DO NOT TRUST THE CLIENT..... EVER
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Delete(id, userInfo.Id);
        return Ok("Successfully Removed");

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    // [HttpGet]
    // public ActionResult<IEnumerable<VaultKeep>> GetAll()
    // {
    //   try
    //   {
    //     IEnumerable<VaultKeep> vaultKeeps = _vks.GetAll();
    //     return Ok(vaultKeeps);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }


  }
}