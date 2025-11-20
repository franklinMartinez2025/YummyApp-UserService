using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public string PasswordHash { get; private set; }

        public string? ProfilePictureUrl { get; private set; }

        public UserRole Role { get; private set; }

        public bool IsActive { get; private set; }

        private readonly List<Address> _addresses = new();

        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

        private readonly List<RefreshToken> _refreshTokens = new();

        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        private User() { }

        public User(string fullName, string email, string passwordHash, string phoneNumber, UserRole role)
        {
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            Role = role;
            IsActive = true;
        }

        public void UpdateProfile(string fullName, string phoneNumber, string? profilePictureUrl)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            ProfilePictureUrl = profilePictureUrl;
            UpdateAudit();
        }

        public void AddAddress(string street, string city, string zipCode, string country, bool isMain, double lat, double lng)
        {
            if (isMain)
            {
                // Si la nueva es principal, desmarcamos las otras
                foreach (var addr in _addresses) addr.SetMain(false);
            }

            var newAddress = new Address(Id, street, city, zipCode, country, isMain, lat, lng);
            _addresses.Add(newAddress);
            UpdateAudit();
        }

        public string? PasswordResetToken { get; private set; }
        public DateTime? PasswordResetTokenExpiry { get; private set; }

        public void SetPasswordResetToken(string token, DateTime expiry)
        {
            PasswordResetToken = token;
            PasswordResetTokenExpiry = expiry;
            UpdateAudit();
        }

        public void ClearPasswordResetToken()
        {
            PasswordResetToken = null;
            PasswordResetTokenExpiry = null;
            UpdateAudit();
        }

        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            UpdateAudit();
        }

        public void AddRefreshToken(string token, DateTime expiry)
        {
            _refreshTokens.Add(new RefreshToken(Id, token, expiry));
        }
    }
}
