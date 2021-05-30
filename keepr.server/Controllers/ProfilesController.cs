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

    public ProfilesController(AccountService accountService)
    {
      _accountService = accountService;
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


  }
}