﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Owners.ManageWorkSpace.Base_Workspace;

namespace WorkHive.Services.Owners.ManageWorkSpace.CRUD_Base_Workspace
{
    public record CreateWorkSpaceCommand(string Name, string Description, int Capacity, string Category, string Status, int CleanTime, int Area, int OwnerId, TimeOnly? OpenTime, TimeOnly? CloseTime, int? Is24h, string Code, List<PriceDTO> Prices,
    List<ImageDTO> Images, List<FacilityDTO> Facilities, List<PolicyDTO> Policies, List<DetailDTO> Details) : ICommand<CreateWorkspaceResult>;

    public record PriceDTO(decimal? Price, string Category);
    public record ImageDTO(string ImgUrl);
    public record FacilityDTO(string FacilityName);
    public record PolicyDTO(string PolicyName);
    public record DetailDTO(string DetailName);

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
                    Title = DefaultImageTitle,
                    CreatedAt = DateTime.Now
                }).ToList() ?? new List<Image>();


                 List<Price> prices = command.Prices.Select(p => new Price
                {
                    Category = p.Category,
                    AveragePrice = p.Price
                }).ToList() ?? new List<Price>();

                List<Facility> facilities = command.Facilities.Select(f => new Facility
                {
                    Name = f.FacilityName
                }).ToList() ?? new List<Facility>();


                List<Policy> policies = command.Policies.Select(p => new Policy
                {
                    Name = p.PolicyName
                }).ToList() ?? new List<Policy>();

                List<Detail> details = command.Details.Select(d => new Detail
                {
                    Name = d.DetailName
                }).ToList() ?? new List<Detail>();



                await workSpaceManageUnit.Image.CreateImagesAsync(images);
                await workSpaceManageUnit.Price.CreatePricesAsync(prices);
                await workSpaceManageUnit.Facility.CreateFacilitiesAsync(facilities);
                await workSpaceManageUnit.Policy.CreatePoliciesAsync(policies);
                await workSpaceManageUnit.Detail.CreateDetailsAsync(details);

                var newWorkSpace = new Workspace
                {
                    Name = command.Name,
                    Description = command.Description,
                    Capacity = command.Capacity,
                    Category = command.Category,
                    Status = command.Status,
                    CleanTime = command.CleanTime,
                    Area = command.Area,
                    OwnerId = command.OwnerId,
                    OpenTime = command.OpenTime,
                    CloseTime = command.CloseTime,
                    Is24h = command.Is24h,
                    Code = command.Code,
                    CreatedAt = DateTime.Now
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

                var workspaceFacilities = facilities.Select(fac => new WorkspaceFacility
                {
                    WorkspaceId = newWorkSpace.Id,
                    FacilityId = fac.Id
                }).ToList();

                var workspacePolicies = policies.Select(pol => new WorkspacePolicy
                {
                    WorkspaceId = newWorkSpace.Id,
                    PolicyId = pol.Id
                }).ToList();

                var workspaceDetails = details.Select(det => new WorkspaceDetail
                {
                    WorkspaceId = newWorkSpace.Id,
                    DetailId = det.Id
                }).ToList();


                var ownerNotification = new OwnerNotification
                {
                    Description = $"Chúc mừng! Bạn đã tạo thành công một không gian làm việc mới có tên {newWorkSpace.Name}. Hãy bắt đầu tổ chức các công việc của bạn ngay thôi.",
                    Status = DefaultStatus,
                    OwnerId = command.OwnerId,
                    CreatedAt = DateTime.Now,
                    IsRead = 0, 
                    Title = "Không gian làm việc mới đã được tạo"
                };

                await workSpaceManageUnit.OwnerNotification.CreateAsync(ownerNotification);
                await workSpaceManageUnit.WorkspaceImage.CreateWorkspaceImagesAsync(workspaceImages);
                await workSpaceManageUnit.WorkspacePrice.CreateWorkspacePricesAsync(workspacePrices);
                await workSpaceManageUnit.WorkspacePolicy.CreateWorkspacePoliciesAsync(workspacePolicies);
                await workSpaceManageUnit.WorkspaceFacility.CreateWorkspaceFacilitiesAsync(workspaceFacilities);
                await workSpaceManageUnit.WorkspaceDetail.CreateWorkspaceDetailsAsync(workspaceDetails);

                await workSpaceManageUnit.SaveAsync();

                return new CreateWorkspaceResult($"Workspace '{newWorkSpace.Name}' created successfully!");
            }
        }
    }
}
