using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.server.Models;
using keepr.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;


    public ProfilesController(AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
    {
      _accountService = accountService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }


    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfile(string id)
    {
      try
      {
        Profile p = _accountService.GetProfileById(id);
        return Ok(p);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<IEnumerable<Keep>> GetKeepsByProfileId(string id)
    {
      try
      {
        IEnumerable<Keep> keeps = _keepsService.GetKeepsByProfileId(id);
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByProfileId(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        IEnumerable<Vault> vaults = _vaultsService.GetVaultsByProfileId(id, userInfo.Id);
        return Ok(vaults);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}