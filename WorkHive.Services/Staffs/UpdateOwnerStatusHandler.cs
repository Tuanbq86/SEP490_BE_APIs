﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;

namespace WorkHive.Services.Staff
{
    public record UpdateOwnerStatusCommand(int Id, int UserId, string Message, string Status) : ICommand<UpdateOwnerStatusResult>;

    public record UpdateOwnerStatusResult(string Notification);

    public class UpdateOwnerStatusHandler(IWalletUnitOfWork unit) : ICommandHandler<UpdateOwnerStatusCommand, UpdateOwnerStatusResult>
    {
        public async Task<UpdateOwnerStatusResult> Handle(UpdateOwnerStatusCommand command, CancellationToken cancellationToken)
        {
            var owner = await unit.WorkspaceOwner.GetByIdAsync(command.Id);
            if (owner == null) return new UpdateOwnerStatusResult("Owner not found");

            if (command.Status != "Fail" && command.Status != "Success")
                return new UpdateOwnerStatusResult("Invalid status value. Use 'Fail' or 'Success'.");

            owner.Status = command.Status;
            owner.Message = command.Message;
            owner.UpdatedAt = DateTime.UtcNow;

            var existingWallet = await unit.OwnerWallet.GetByOwnerIdAsync(command.Id);
            var walletStatus = command.Status == "Success" ? "Active" : "Inactive"; 

            if (existingWallet == null)
            {
                var newWallet = new Wallet
                {
                    Balance = 0,
                    Status = walletStatus 
                };

                await unit.Wallet.CreateAsync(newWallet);

                var ownerWallet = new OwnerWallet
                {
                    OwnerId = owner.Id,
                    WalletId = newWallet.Id,
                    UserId = command.UserId,
                    Status = walletStatus 
                };

                var ownerNotification = new OwnerNotification
                {
                    Description = $"Tài khoản {owner.LicenseName} đã được phê duyệt",
                    Status = "Active",
                    OwnerId = command.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = 0,
                    Title = "Yêu cầu xác thực tài khoản"
                };

                await unit.OwnerNotification.CreateAsync(ownerNotification);
                await unit.OwnerWallet.CreateAsync(ownerWallet);
            }
            else
            {               
                existingWallet.UserId = command.UserId;
                existingWallet.Status = walletStatus; 
                await unit.OwnerWallet.UpdateAsync(existingWallet);
            }

            await unit.WorkspaceOwner.UpdateAsync(owner);
            await unit.SaveAsync(); 

            return new UpdateOwnerStatusResult($"Owner status updated to {command.Status} and wallet set to '{walletStatus}'");
        }
    }
}
