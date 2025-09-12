using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketSelling.Application.Dtos.User;
using TicketSelling.Core.Entities;
using TicketSelling.Infrastructure.Email;

namespace TicketSelling.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController(UserManager<AppUser> _userManager, 
                SignInManager<AppUser> _signInManager, IMapper _mapper,
                IConfiguration _configuration, IEmailSender _emailSender) : ControllerBase
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

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid username or password.");

            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            // Add role claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                roles 
            });
        }

        [Authorize]
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

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Encode token for URL safety
            var encodedToken = System.Web.HttpUtility.UrlEncode(token);

            // Build reset link (frontend will consume this)
            var resetLink = $"https://your-frontend.com/reset-password?email={user.Email}&token={encodedToken}";

            await _emailSender.SendAsync(user.Email, "Password Reset", $"Click here to reset: {resetLink}");


            return Ok(new { message = "Password reset link sent to your email." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound("User not found.");

            // Decode token (reverse of UrlEncode)
            var decodedToken = System.Web.HttpUtility.UrlDecode(dto.Token);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, dto.NewPassword);

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
            var res = _mapper.Map<UserDto>(user);
            return Ok(res);
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

        [HttpGet("{id}/cards")]
        public async Task<IActionResult> GetUserCards(int id)
        {
            var user = await _userManager.Users
                .Include(u => u.UserSavedCards)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound("User not found.");

            return Ok(user.UserSavedCards);
        }

        //[HttpPost("{id}/cards")]
        //public async Task<IActionResult> SaveCard(int id, [FromBody] SaveCardDto dto)
        //{
        //    var user = await _userManager.Users
        //        .Include(u => u.UserSavedCards)
        //        .FirstOrDefaultAsync(u => u.Id == id);

        //    if (user == null) return NotFound("User not found.");

        //    var card = _mapper.Map<UserSavedCard>(dto);
        //    card.UserId = user.Id;

        //    user.UserSavedCards.Add(card);
        //    await _userManager.UpdateAsync(user);

        //    return Ok(card);
        //}

        [HttpDelete("{id}/cards/{cardId}")]
        public async Task<IActionResult> DeleteCard(int id, int cardId)
        {
            var user = await _userManager.Users
                .Include(u => u.UserSavedCards)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound("User not found.");

            var card = user.UserSavedCards.FirstOrDefault(c => c.Id == cardId);
            if (card == null) return NotFound("Card not found.");

            user.UserSavedCards.Remove(card);
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        // ---------------------------
        // 5. USER ORDERS / TICKETS / REWARDS
        // ---------------------------

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetUserOrders(int id)
        {
            var user = await _userManager.Users
                .Include(u => u.MerchandiseOrders)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user.MerchandiseOrders);
        }

        [HttpGet("{id}/tickets")]
        public async Task<IActionResult> GetUserTickets(int id)
        {
            var user = await _userManager.Users
                .Include(u => u.Tickets) 
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user.Tickets);
        }

        [HttpGet("{id}/rewards")]
        public async Task<IActionResult> GetUserRewards(int id)
        {
            var user = await _userManager.Users
                .Include(u => u.GiftRewards) 
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user.GiftRewards);
        }

    }

}
