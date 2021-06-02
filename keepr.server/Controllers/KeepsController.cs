using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.server.Models;
using keepr.server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {

    private readonly KeepsService _keepsService;
    private readonly AccountService _accountService;

    public KeepsController(KeepsService keepsService, AccountService accountService)
    {
      _keepsService = keepsService;
      _accountService = accountService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Account fullAccount = _accountService.GetOrCreateProfile(userInfo);
        newKeep.CreatorId = userInfo.Id;
        Keep keep = _keepsService.Create(newKeep);
        keep.Creator = fullAccount;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Keep>> GetAll()
    {
      try
      {
        IEnumerable<Keep> keeps = _keepsService.GetAll();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        Keep found = _keepsService.GetById(id);
        return Ok(found);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]

    public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep k)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        k.Id = id;
        Keep newKeep = _keepsService.Update(k, userInfo.Id);
        newKeep.Creator = userInfo;
        return Ok(newKeep);

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
        _keepsService.Delete(id, userInfo.Id);
        return Ok("Successfully Removed");

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}