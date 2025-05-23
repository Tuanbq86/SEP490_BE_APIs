﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Admins.BanStaff;

namespace WorkHive.Services.Admins.BanOwner;

public record BanOwnerCommand(int OwnerId) : ICommand<BanOwnerResult>;
public record BanOwnerResult(string Notification, int? IsBan);

public class BanOwnerHandler(IWorkspaceOwnerUnitOfWork ownerUnit, IBookingWorkspaceUnitOfWork bookUnit)
    : ICommandHandler<BanOwnerCommand, BanOwnerResult>
{
    public async Task<BanOwnerResult> Handle(BanOwnerCommand command, 
        CancellationToken cancellationToken)
    {
        var owner = ownerUnit.WorkspaceOwner.GetById(command.OwnerId);

        if (owner is null)
        {
            return new BanOwnerResult("Không tìm thấy tài khoản owner phù hợp", 0);
        }

        //Ban và gửi thông báo
        owner.IsBan = 1;
        owner.Status = "Fail";
        await ownerUnit.WorkspaceOwner.UpdateAsync(owner);

        var workspaces = bookUnit.workspace.GetAll().Where(x => x.OwnerId == owner.Id).ToList();
        if(workspaces.Count > 0)
        {
            foreach(var item in workspaces)
            {
                item.Status = "Inactive";
                await bookUnit.workspace.UpdateAsync(item);
            }
        }

        var ownerNotification = new OwnerNotification
        {
            OwnerId = owner.Id,
            Description = "Tài khoản đã bị cấm",
            Status = "Active",
            IsRead = 0,
            CreatedAt = DateTime.Now
        };
        await ownerUnit.OwnerNotification.CreateAsync(ownerNotification);

        return new BanOwnerResult("Cập nhật trạng thái tài khoản thành công", owner.IsBan);
    }
}
