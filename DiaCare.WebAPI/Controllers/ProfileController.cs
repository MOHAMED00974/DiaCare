using DiaCare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DiaCare.Domain.DTOS;
using Microsoft.AspNetCore.Http;
using DiaCare.Infrastructure;
using DiaCare.Application.Helpers;


[Authorize] 
[ApiController]
[Route("api/[controller]")]
public class ProfileController : Controller
{
    private readonly IProfileServices _profileService; 

    public ProfileController(IProfileServices profileService) 
    {
        _profileService = profileService;
    }

    [HttpGet("Me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var profile = await _profileService.GetProfileAsync(userId);
        //  Result Wrapper
        return Ok(Result<object>.Success(profile));
    }



}