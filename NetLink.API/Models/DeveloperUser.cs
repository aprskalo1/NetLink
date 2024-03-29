﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models
{
    public class DeveloperUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DeveloperId { get; set; }

        [Required]
        public string? EndUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Developer? Developer { get; set; }

        public EndUser? EndUser { get; set; }
    }
}
