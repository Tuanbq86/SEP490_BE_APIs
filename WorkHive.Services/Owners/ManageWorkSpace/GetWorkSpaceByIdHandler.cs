﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.BuildingBlocks.Exceptions;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Exceptions;
using WorkHive.Services.Users.LoginUser;

namespace WorkHive.Services.Owners.ManageWorkSpace
{
    public record GetWorkSpaceByIdCommand(int id) : ICommand<GetWorkSpaceByIdResult>;
    public record GetWorkSpaceByIdResult(int Id, string Name, string Description, int? Capacity, string Category, string Status, int? CleanTime, int? Area, int OwnerId);


    public class GetWorkSpaceByIdValidator : AbstractValidator<GetWorkSpaceByIdCommand>
    {
        public GetWorkSpaceByIdValidator()
        {

        }
    }

    public class GetWorkSpaceByIdHandler(IWorkSpaceManageUnitOfWork workSpaceManageUnit)
    : ICommandHandler<GetWorkSpaceByIdCommand, GetWorkSpaceByIdResult>
    {
        public async Task<GetWorkSpaceByIdResult> Handle(GetWorkSpaceByIdCommand command, CancellationToken cancellationToken)
        {
            var workspace = await workSpaceManageUnit.Workspace.GetByIdAsync(command.id);
            if (workspace == null)
            {
                throw new NotFoundException("WorkSpace not found!");
            }

            return new GetWorkSpaceByIdResult(
                workspace.Id,
                workspace.Name,
                workspace.Description,
                workspace.Capacity,
                workspace.Category,
                workspace.Status,
                workspace.CleanTime,
                workspace.Area,
                workspace.OwnerId
            );
        }

    }
}
