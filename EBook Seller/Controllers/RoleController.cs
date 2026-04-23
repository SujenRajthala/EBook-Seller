using EBook_Seller.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBook_Seller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
