﻿using System.ComponentModel.DataAnnotations;

namespace lab10.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; }
    
    [EmailAddress]
    [MaxLength(100)]
    [Required]
    public string Email { get; set; }
    
    public virtual ICollection<Prescription> Prescriptions { get; set; }

}