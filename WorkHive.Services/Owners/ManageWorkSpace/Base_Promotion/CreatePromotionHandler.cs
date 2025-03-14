﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;

namespace WorkHive.Services.Owners.ManageWorkSpace.Base_Promotion
{
    public record CreatePromotionCommand(string Code, int Discount, DateTime StartDate, DateTime EndDate, string Status, int WorkspaceId) : ICommand<CreatePromotionResult>;

    public record CreatePromotionResult(string Notification);

    public class CreatePromotionHandler(IWorkSpaceManageUnitOfWork unit) : ICommandHandler<CreatePromotionCommand, CreatePromotionResult>
    {
        public async Task<CreatePromotionResult> Handle(CreatePromotionCommand command, CancellationToken cancellationToken)
        {
            var newPromotion = new Promotion
            {
                Code = command.Code,
                Discount = command.Discount,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Status = command.Status,
                WorkspaceId = command.WorkspaceId,
                CreatedAt = DateTime.UtcNow
            };

            await unit.Promotion.CreateAsync(newPromotion);
            await unit.SaveAsync();

            return new CreatePromotionResult("Promotion created successfully");
        }
    }
}
