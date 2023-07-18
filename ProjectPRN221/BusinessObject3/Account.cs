using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPRN221.BusinessObject3;

public partial class Account
{
    [Required(ErrorMessage = "First name is required")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Role ID is required")]
    public int RoleId { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    public string? Address { get; set; }

    public bool Gender { get; set; }

    public bool? Status { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    public int AccountId { get; set; }

    public virtual ICollection<BooksBorrow> BooksBorrows { get; set; } = new List<BooksBorrow>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Role Role { get; set; } = null!;

    public Account(string? firstName, string? lastName, string email, int roleId, DateTime? createdTime, DateTime? updateTime, string? phone, string? address, bool gender, bool? status, string password, int accountId, ICollection<BooksBorrow> booksBorrows, Role role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        RoleId = roleId;
        CreatedTime = createdTime;
        UpdateTime = updateTime;
        Phone = phone;
        Address = address;
        Gender = gender;
        Status = status;
        Password = password;
        AccountId = accountId;
        BooksBorrows = booksBorrows;
        Role = role;
    }

    public Account(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public Account()
    {
    }

    public Account(string? firstName, string? lastName, string email, int roleId, DateTime? createdTime, DateTime? updateTime, string? phone, string? address, bool gender, bool? status, string password, int accountId, ICollection<BooksBorrow> booksBorrows, ICollection<Comment> comments, Role role) : this(firstName, lastName)
    {
        Email = email;
        RoleId = roleId;
        CreatedTime = createdTime;
        UpdateTime = updateTime;
        Phone = phone;
        Address = address;
        Gender = gender;
        Status = status;
        Password = password;
        AccountId = accountId;
        BooksBorrows = booksBorrows;
        Comments = comments;
        Role = role;
    }
}
