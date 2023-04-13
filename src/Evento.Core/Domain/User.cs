using System.ComponentModel.DataAnnotations.Schema;

namespace Evento.Core.Domain
{
    public enum Role
    {
        user,
        admin
    }

    [Table("User")]
    public class User 
    {
        // Properties
        public int ID { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTimeExpired { get; set; }
        public bool ResetPasswordUsed { get; set; }


        // Navigation Properties
        public virtual ICollection<Ticket> Ticket { get; set; }


        public User() { }

        public User(string role, string name, string email, string? passwordSalt = null, string? password = null)
        {
            SetRole(role);
            SetName(name);
            SetEmail(email);
            SetPassword(password, passwordSalt);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception($"User can not have an empty name!");

            Name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception($"User can not have an empty email!");

            Email = email.ToLowerInvariant();
        }

        public void SetPassword(string? password, string? passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception($"User can not have an empty password!");

            Password = password;
            PasswordSalt = passwordSalt;
            if(!string.IsNullOrWhiteSpace(ResetPasswordToken)) ResetPasswordUsed = true;
        }

        public void SetRole(string role)
        {

            if (string.IsNullOrWhiteSpace(role))
                throw new Exception($"User can not have an empty roll!");

            role = role.ToLowerInvariant();

            Role enumRole;

            if (!Enum.TryParse(role, out enumRole))
            {
                throw new Exception($"User can not have a role: {role}");
            }

            Role = enumRole;
        }

        public void SetResetPasswordToken(string token)
        {
            ResetPasswordTimeExpired = DateTime.UtcNow;
            ResetPasswordToken = token;
            ResetPasswordUsed = false;
        }

        public void ClearResetPassword()
        {
            ResetPasswordTimeExpired = null;
            ResetPasswordToken = null;
            ResetPasswordUsed = false;
        }
    }
}
