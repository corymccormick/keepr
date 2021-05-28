using System;

namespace keepr.server.Models
{
  public class Account
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}