﻿using FluentValidation;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Exceptions;

namespace WorkHive.Services.Users.RegisterUser;

public record RegisterUserCommand(string Name, string Email, 
    string Phone, string Password) : ICommand<RegisterUserResult>;

public record RegisterUserResult(string Notification);

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
    }
}

public class RegisterUserHandler(IUserUnitOfWork userUnit)
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand command, 
        CancellationToken cancellationToken)
    {
        //Checking exist used email and phone number for registering

        var existEmailOrPhoneUser = userUnit.User.GetAll().
            Where(x => x.Email.ToLower().Equals(command.Email.ToLower()) || 
            x.Phone.ToLower().Equals(command.Phone.ToLower())).FirstOrDefault();

        if (existEmailOrPhoneUser is not null)
            throw new BadRequestEmailOrPhoneException("Email or Phone has been used");
        
        //Create new user for registering

        var tempUser = userUnit.User.RegisterUserByPhoneAndEmail(command.Name, command.Email, 
            command.Phone, command.Password);

        var newUser = new User
        {
            Name = tempUser.Name,
            Email = tempUser.Email,
            Phone = tempUser.Phone,
            //Using Bcrypt to hash password using SHA-512 algorithm
            //Work factor time so long when increment for safety(13)
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword(tempUser.Password, 13),
            RoleId = 4
        };

        userUnit.User.Create(newUser);
        
        await userUnit.SaveAsync();

        return new RegisterUserResult("Register successfully");
    }
}
