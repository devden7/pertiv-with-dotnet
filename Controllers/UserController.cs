using System;
using Microsoft.AspNetCore.Mvc;
using Pertiv_be_with_dotnet.Dtos;
using Pertiv_be_with_dotnet.Enums;
using Pertiv_be_with_dotnet.Helper;
using Pertiv_be_with_dotnet.Models;
using Pertiv_be_with_dotnet.Services;

namespace Pertiv_be_with_dotnet.Controllers
{
    [ApiController]
    [Route("admin")]
    public class UserController : ControllerBase
    {
        private static UserService _userService { get; set; }

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-staff")]
        public IActionResult CreateStaff([FromBody] UserDto dto)
        {
            try
            {
                var UserModel = new UserModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = AccountPassword.HashPassword(dto.Password),
                    Role = UserRole.staff,

                };
                _userService.CreateStaffAccount(UserModel);
                return CreatedAtAction(nameof(CreateStaff), new
                {
                    success = true,
                    statusCode = 201,
                    message = "Staff account created successfully",
                });
            }
            catch (Exception ex) {
                return Conflict(new
                {
                    success = false,
                    statusCode = 409,
                    message = ex.Message,
                });
            }
        }

        [HttpGet("staffs")]
        public IActionResult GetStaffList()
        {
            var data = _userService.GetStaffAccount();

            return Ok(new
            {
                success = true,
                statusCode = 200,
                data
            });
        }

        [HttpDelete("delete-staff/{id}")]
        public IActionResult DeleteStaff([FromRoute] Guid id) {
            try {
                _userService.DeleteStaffAccount(id);
                return CreatedAtAction(nameof(DeleteStaff), new
                {
                    success = true,
                    statusCode = 201,
                    message = "Staff account deleted successfully",
                });
            } catch (Exception ex) {
                return NotFound(new
                {
                    success = false,
                    statusCode = 404,
                    message = ex.Message,
                });
            }
        }

        [HttpPut("update-staff/{id}")]
        public IActionResult UpdateStaffAccount([FromRoute] Guid id, [FromBody] UserDto dto)
        {
            try
            {
                var userModel = new UserModel
                {
                    Name = dto.Name,
                    Password = dto.Password,
                };
                _userService.UpdateStaffAccount(id,userModel);
                return CreatedAtAction(nameof(UpdateStaffAccount), new
                {
                    success = true,
                    statusCode = 201,
                    message = "Staff account updated successfully",
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    success = false,
                    statusCode = 404,
                    message = ex.Message,
                });
            }
         }
    }
}
