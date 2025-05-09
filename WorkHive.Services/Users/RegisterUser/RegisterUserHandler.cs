﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IRepositories;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Constant.Wallet;
using WorkHive.Services.Exceptions;

namespace WorkHive.Services.Users.RegisterUser;

public record RegisterUserCommand(string Name, string Email, 
    string Phone, string Password, string Sex) : ICommand<RegisterUserResult>;

public record RegisterUserResult(string Token, string Notification);

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
            .MinimumLength(5).WithMessage("Length of name have to have minimum being 5")
            .MaximumLength(50).WithMessage("Length of name have to have maximum being 50");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email format is required");

        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required")
            .Length(10).WithMessage("The number of characterics is exact 10 characterics");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");

        RuleFor(x => x.Sex).NotEmpty().WithMessage("Sex is required");
    }
}

public class RegisterUserHandler(IUserUnitOfWork userUnit, ITokenRepository tokenRepo,
    IHttpContextAccessor httpContext)
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand command, 
        CancellationToken cancellationToken)
    {
        //Checking exist used email and phone number for registering

        var existEmailAndPhoneUser = userUnit.User.GetAll()
            .Where(u => !string.IsNullOrEmpty(u.Phone))
            .Where(x => x.Email.Trim().ToLower().Equals(command.Email.Trim().ToLower()) ||
            x.Phone.Trim().ToLower().Equals(command.Phone.Trim().ToLower())).FirstOrDefault();

        if (existEmailAndPhoneUser is not null)
            return new RegisterUserResult("", "Email và số điện thoại đã được sử dụng");
        
        //Create new user for registering

        var tempUser = userUnit.User.RegisterUser(command.Name, command.Email, 
            command.Phone, command.Password, command.Sex);

        var newUser = new User
        {
            Name = tempUser.Name,
            Email = tempUser.Email.Trim(),
            Phone = tempUser.Phone.Trim(),
            Sex = tempUser.Sex,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Status = "Active",
            Avatar = "https://res.cloudinary.com/dcq99dv8p/image/upload/v1745717996/registerAvatar_d1j7br.jpg",
            //Using Bcrypt to hash password using SHA-512 algorithm
            //Work factor time so long when increment for safety(13)
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword(tempUser.Password, 13),
            RoleId = 4,
            IsBan = 0
        };

        await userUnit.User.CreateAsync(newUser);

        var wallet = new Wallet
        {
            Balance = 0,
            Status = WalletStatus.Active.ToString()
        };
        await userUnit.Wallet.CreateAsync(wallet);

        var customerWallet = new CustomerWallet
        {
            Status = WalletStatus.Active.ToString(),
            WalletId = wallet.Id,
            UserId = newUser.Id,
            IsLock = 0
        };
        userUnit.CustomerWallet.Create(customerWallet);

        await userUnit.SaveAsync();

        //var userNotifi = new UserNotification
        //{
        //    UserId = newUser.Id,
        //    IsRead = 0,
        //    CreatedAt = DateTime.Now,
        //    Description = "Đăng ký thành công",
        //    Status = "Active"
        //};
        //await userUnit.UserNotification.CreateAsync(userNotifi);

        var token = tokenRepo.GenerateJwtToken(newUser);

        httpContext.HttpContext!.Session.SetString("token", token);

        return new RegisterUserResult(token, "Đăng ký thành công");
    }
}
