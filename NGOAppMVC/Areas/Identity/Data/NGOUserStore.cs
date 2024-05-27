using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NGOAppMVC.Data;

namespace NGOAppMVC.Areas.Identity.Data
{
    public class NGOUserStore : IUserStore<NGOUser>, IUserPasswordStore<NGOUser>, IUserEmailStore<NGOUser>
    {
        private readonly NGOAppMVCContext _context;

        public NGOUserStore(NGOAppMVCContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(NGOUser user, CancellationToken cancellationToken)
        {
            _context.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(NGOUser user, CancellationToken cancellationToken)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<NGOUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
        }

        public async Task<NGOUser> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == userName.ToLower(), cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(NGOUser user, CancellationToken cancellationToken)
        {
            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public Task SetUserNameAsync(NGOUser user, string userName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<string> GetUserIdAsync(NGOUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(NGOUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(NGOUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult($"{user.FirstName} {user.LastName}" );
        }

        public Task SetNormalizedUserNameAsync(NGOUser user, string normalizedName, CancellationToken cancellationToken)
        {
            // Do nothing as we do not use NormalizedUserName
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(NGOUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(NGOUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(NGOUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(NGOUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(NGOUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(NGOUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(NGOUser user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<NGOUser> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken)!;
        }

        public Task<string> GetNormalizedEmailAsync(NGOUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email?.ToUpper())!;
        }

        public Task SetNormalizedEmailAsync(NGOUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            // Do nothing as we do not use NormalizedEmail
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Dispose context if necessary
        }
    }
}
