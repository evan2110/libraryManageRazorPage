using System;
using System.Collections.Generic;

namespace ManageBookLibrary.BusinessObject;

public partial class Account
{
    public int AccountId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public bool Gender { get; set; }

    public bool? Status { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<BooksBorrow> BooksBorrows { get; set; } = new List<BooksBorrow>();

    public virtual Role Role { get; set; } = null!;

    public Account(int accountId, string? firstName, string? lastName, string email, int roleId, DateTime? createdTime, DateTime? updateTime, string? phone, string? address, bool gender, bool? status, string password, ICollection<BooksBorrow> booksBorrows, Role role)
    {
        AccountId = accountId;
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
        BooksBorrows = booksBorrows;
        Role = role;
    }

    public Account(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public Account() { }
}
