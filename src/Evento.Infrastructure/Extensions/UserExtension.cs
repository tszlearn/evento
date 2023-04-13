using Evento.Core.Domain;
using Evento.Infrastructure.Services;

namespace Evento.Infrastructure.Extensions
{
    internal static class UserExtension
    {
        public static void SetPasswordHash(this User user, string password, string pepper, int iteration)
        {
            var salt = PasswordHasher.GenerateSalt();
            var hash = PasswordHasher.ComputeHash(password, salt, pepper, iteration);
            user.SetPassword(hash, salt);
        }

        public static bool ValidatePasswordToken(this User user)
        {
            var timeExpired = DateTime.UtcNow - user.ResetPasswordTimeExpired;

            if(timeExpired.HasValue && timeExpired.Value.TotalMinutes > 60)
            {
                return true;
            }

            return false;
        }
    }
}
