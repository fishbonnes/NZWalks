﻿namespace NZWalks2.API.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public List<User_Role> UserRoles { get; set; }
    }
}
