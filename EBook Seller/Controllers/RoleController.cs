using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using EBook_Seller.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBook_Seller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string Name)
        {
            var role=await _service.AddRoleAsync(Name);
            return Ok($"{role.RoleName} is Successfully Added");
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(AssignRoleDTO userRole)
        {
            await _service.AssignRoleAsync(userRole);
            return Ok("Your Role Has been Assigned!");
        }
    }
}
