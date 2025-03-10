﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Owners.ManageWorkSpace.GetById;

namespace WorkHive.Services.Owners.ManageWorkSpace.CRUD_Base_Workspace
{
    public record CreateWorkSpaceCommand(string Name, string Description, int Capacity, string Category, string Status, int CleanTime, int Area, int OwnerId, List<PriceDTO> Prices,
    List<ImageDTO> Images) : ICommand<CreateWorkspaceResult>;

    public record PriceDTO(decimal? Price, string Category);
    public record ImageDTO(string ImgUrl);
    public record CreateWorkspaceResult(string Notification);

    public class CreateWorkSpaceValidator : AbstractValidator<CreateWorkSpaceCommand>
    {
        public CreateWorkSpaceValidator()
        {
            //    RuleFor(x => x.Name)
            //        .NotEmpty().WithMessage("Name is required")
            //        .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            //    RuleFor(x => x.Capacity)
            //        .GreaterThan(0).WithMessage("Capacity must be greater than 0");

            //    RuleFor(x => x.Area)
            //        .GreaterThan(0).WithMessage("Area must be greater than 0");

            //    RuleFor(x => x.OwnerId)
            //        .GreaterThan(0).WithMessage("OwnerId is required");
            //}
        }

        public class CreateWorkspaceHandler(IWorkSpaceManageUnitOfWork workSpaceManageUnit)
        : ICommandHandler<CreateWorkSpaceCommand, CreateWorkspaceResult>
        {
            private const string DefaultImageTitle = "Workspace Image";
            private const string DefaultStatus = "Active";

            public async Task<CreateWorkspaceResult> Handle(CreateWorkSpaceCommand command, CancellationToken cancellationToken)
             {
                List<Image> images = command.Images.Select(i => new Image
                {
                    ImgUrl = i.ImgUrl,
                    Title = DefaultImageTitle
                }).ToList() ?? new List<Image>();

                List<Price> prices = command.Prices.Select(p => new Price
                {
                    Category = p.Category,
                    AveragePrice = p.Price
                }).ToList() ?? new List<Price>();

                await workSpaceManageUnit.Image.CreateImagesAsync(images);
                await workSpaceManageUnit.Price.CreatePricesAsync(prices);
                await workSpaceManageUnit.SaveAsync();

                var newWorkSpace = new Workspace
                {
                    Name = command.Name,
                    Description = command.Description,
                    Capacity = command.Capacity,
                    Category = command.Category,
                    Status = command.Status,
                    CleanTime = command.CleanTime,
                    Area = command.Area,
                    OwnerId = command.OwnerId
                };

                await workSpaceManageUnit.Workspace.CreateAsync(newWorkSpace);
                await workSpaceManageUnit.SaveAsync();

                var workspaceImages = images.Select(img => new WorkspaceImage
                {
                    WorkspaceId = newWorkSpace.Id,
                    ImageId = img.Id,
                    Status = DefaultStatus
                }).ToList();

                var workspacePrices = prices.Select(prc => new WorkspacePrice
                {
                    WorkspaceId = newWorkSpace.Id,
                    PriceId = prc.Id,
                    Status = DefaultStatus
                }).ToList();

                await workSpaceManageUnit.WorkspaceImage.CreateWorkspaceImagesAsync(workspaceImages);
                await workSpaceManageUnit.WorkspacePrice.CreateWorkspacePricesAsync(workspacePrices);

                await workSpaceManageUnit.SaveAsync();

                return new CreateWorkspaceResult($"Workspace '{newWorkSpace.Name}' created successfully!");
            }
        }
    }
}
