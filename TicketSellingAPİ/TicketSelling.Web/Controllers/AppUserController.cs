using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.Application.Dtos.User;
using TicketSelling.Core.Entities;

namespace TicketSelling.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IMapper _mapper) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized("Invalid username or password.");

            return Ok(new { message = "Login successful." });
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logged out successfully." });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return NotFound("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Password changed successfully." });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound("User not found.");

            // Generate reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // ⚡ Normally you would EMAIL this token to the user.
            // For testing, we return it directly in response.
            return Ok(new
            {
                message = "Password reset token generated. (Send this via email in production!)",
                resetToken = token
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound("User not found.");

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Password reset successfully." });
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            user.Email = dto.Email ?? user.Email;
            user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User deleted successfully." });
        }

        [HttpGet("{id}/loyalty")]
        public async Task<IActionResult> GetLoyalty(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.LoyaltyPoints,
                user.LoyaltyTier
            });
        }

        [HttpPost("{id}/add-points")]
        public async Task<IActionResult> AddLoyaltyPoints(int id, [FromBody] int points)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            user.LoyaltyPoints += points;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new
            {
                message = "Points added successfully",
                user.LoyaltyPoints
            });
        }

        [HttpPut("{id}/tier")]
        public async Task<IActionResult> UpdateLoyaltyTier(int id, [FromBody] LoyaltyTier tier)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            user.LoyaltyTier = tier;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new
            {
                message = "Loyalty tier updated successfully",
                user.LoyaltyTier
            });
        }


        // ---------------------------
        // 4. SAVED CARDS
        // ---------------------------

        //[HttpGet("{id}/cards")]
        //public async Task<IActionResult> GetUserCards(int id) { ... }

        //[HttpPost("{id}/cards")]
        //public async Task<IActionResult> SaveCard(int id, SaveCardDto dto) { ... }

        //[HttpDelete("{id}/cards/{cardId}")]
        //public async Task<IActionResult> DeleteCard(int id, int cardId) { ... }

        // ---------------------------
        // 5. USER ORDERS / TICKETS
        // ---------------------------

        //[HttpGet("{id}/orders")]
        //public async Task<IActionResult> GetUserOrders(int id) { ... }

        //[HttpGet("{id}/tickets")]
        //public async Task<IActionResult> GetUserTickets(int id) { ... }

        //[HttpGet("{id}/rewards")]
        //public async Task<IActionResult> GetUserRewards(int id) { ... }
    }

}
