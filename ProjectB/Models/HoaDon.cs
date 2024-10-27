﻿using Microsoft.AspNetCore;
using ProjectB.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectB.Models
{
    public class HoaDon
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
       

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderStatus { get; set; }

        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

    }

}
