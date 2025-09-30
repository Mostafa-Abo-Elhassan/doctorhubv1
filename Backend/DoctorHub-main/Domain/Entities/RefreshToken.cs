﻿namespace Domain.Entities
{
    public class RefreshToken
    {

        public int Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; } = false;

        // Navigation property
        public User User { get; set; }
    }
}
