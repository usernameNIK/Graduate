﻿using System.ComponentModel.DataAnnotations;
using WebApplicationGraduateWorkFinal.Model;

namespace WebApplication123.Models
{
    public class RegisterNewAccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }

        //public byte[] PinHash { get; set; }
        //public byte[] PinSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Pin Must not be more than 4 digits")]
        public string Pin { get; set; }
        [Required]

        [Compare("Pin", ErrorMessage = "Pins do not match")]
        public string ConfirmPin { get; set; }
    }
}
