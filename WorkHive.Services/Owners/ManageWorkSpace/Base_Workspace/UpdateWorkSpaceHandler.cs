﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;

namespace WorkHive.Services.Owners.ManageWorkSpace.CRUD_Base_Workspace
{
    public record UpdateWorkSpaceCommand(
        int Id, 
        string Name, 
        string Description, 
        int Capacity, 
        string Category, 
        string Status, 
        int CleanTime, 
        int Area, 
        TimeOnly? OpenTime,
        TimeOnly? CloseTime, 
        int? Is24h,
        string Code,
        List<PriceDTO> Prices, 
        List<ImageDTO> Images, 
        List<FacilityDTO> Facilities, 
        List<PolicyDTO> Policies,
        List<DetailDTO> Details
        ): ICommand<UpdateWorkspaceResult>;

    public record UpdateWorkspaceResult(string Notification);

    public class UpdateWorkSpaceValidator : AbstractValidator<UpdateWorkSpaceCommand>
    {
        public UpdateWorkSpaceValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id không tồn tại");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Yêu cầu nhập tên")
                .MaximumLength(100).WithMessage("Tên không quá 100 ký tự");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0");

            RuleFor(x => x.Area)
                .GreaterThan(0).WithMessage("Diện tích phải lớn hơn 0");
        }
    }

    public class UpdateWorkspaceHandler(IWorkSpaceManageUnitOfWork workSpaceManageUnit)
    : ICommandHandler<UpdateWorkSpaceCommand, UpdateWorkspaceResult>
    {
        public async Task<UpdateWorkspaceResult> Handle(UpdateWorkSpaceCommand command, CancellationToken cancellationToken)
        {
            var workspace = await workSpaceManageUnit.Workspace.GetByIdAsync(command.Id);
            if (workspace == null)
                return new UpdateWorkspaceResult($"Workspace Id {command.Id} không tìm thấy.");



            // Cập nhật thông tin cơ bản
            workspace.Name = command.Name;
            workspace.Description = command.Description;
            workspace.Capacity = command.Capacity;
            workspace.Category = command.Category;
            workspace.Status = command.Status;
            workspace.CleanTime = command.CleanTime;
            workspace.Area = command.Area;
            workspace.OpenTime = command.OpenTime;
            workspace.CloseTime = command.CloseTime;
            workspace.Is24h = command.Is24h;
            workspace.Code = command.Code;
            workspace.UpdatedAt = DateTime.Now;


            // **Lấy danh sách WorkspacePrice hiện tại**
            var existingWorkspacePrices = await workSpaceManageUnit.WorkspacePrice
                .GetWorkspacePricesByWorkspaceId(workspace.Id);

            // **Duyệt qua từng Price trong danh sách command**
            foreach (var priceDTO in command.Prices)
            {
                var existingPrice = existingWorkspacePrices
                    .FirstOrDefault(wp => wp.Price.Category == priceDTO.Category);

                if (existingPrice != null) 
                {
                    existingPrice.Price.AveragePrice = priceDTO.Price;
                }
            }

            // === Xóa toàn bộ danh sách ảnh cũ ===
            var existingImages = await workSpaceManageUnit.WorkspaceImage.GetByWorkspaceIdAsync(workspace.Id);
            if (existingImages.Any())
            {
                await workSpaceManageUnit.WorkspaceImage.DeleteWorkspaceImagesAsync(existingImages);
            }

            // === Xóa toàn bộ danh sách chính sách (Policies) cũ ===
            var existingPolicies = await workSpaceManageUnit.WorkspacePolicy.GetByWorkspaceIdAsync(workspace.Id);
            if (existingPolicies.Any())
            {
                await workSpaceManageUnit.WorkspacePolicy.DeleteWorkspacePoliciesAsync(existingPolicies);
            }

            // === Xóa toàn bộ danh sách tiện ích (Facilities) cũ ===
            var existingFacilities = await workSpaceManageUnit.WorkspaceFacility.GetByWorkspaceIdAsync(workspace.Id);
            if (existingFacilities.Any())
            {
                await workSpaceManageUnit.WorkspaceFacility.DeleteWorkspaceFacilitiesAsync(existingFacilities);
            }

            // === Xóa toàn bộ danh sách chi tiết (Details) cũ ===
            var existingDetails = await workSpaceManageUnit.WorkspaceDetail.GetByWorkspaceIdAsync(workspace.Id);
            if (existingDetails.Any())
            {
                await workSpaceManageUnit.WorkspaceDetail.DeleteWorkspaceDetailsAsync(existingDetails);
            }


            //await workSpaceManageUnit.SaveAsync();

            // === Thêm mới danh sách ảnh ===
            var newImages = command.Images.Select(i => new Image { ImgUrl = i.ImgUrl, Title = "Workspace Image" }).ToList();
            await workSpaceManageUnit.Image.CreateImagesAsync(newImages);

            var workspaceImages = newImages.Select(img => new WorkspaceImage
            {
                WorkspaceId = workspace.Id,
                ImageId = img.Id,
                Status = "Active"
            }).ToList();

            await workSpaceManageUnit.WorkspaceImage.CreateWorkspaceImagesAsync(workspaceImages);

            // === Thêm mới danh sách tiện ích (Facilities) ===
            var newFacilities = command.Facilities.Select(f => new Facility { Name = f.FacilityName }).ToList();
            await workSpaceManageUnit.Facility.CreateFacilitiesAsync(newFacilities);

            var workspaceFacilities = newFacilities.Select(fac => new WorkspaceFacility
            {
                WorkspaceId = workspace.Id,
                FacilityId = fac.Id
            }).ToList();

            await workSpaceManageUnit.WorkspaceFacility.CreateWorkspaceFacilitiesAsync(workspaceFacilities);

            // === Thêm mới danh sách chính sách (Policies) ===
            var newPolicies = command.Policies.Select(p => new Policy { Name = p.PolicyName }).ToList();
            await workSpaceManageUnit.Policy.CreatePoliciesAsync(newPolicies);

            var workspacePolicies = newPolicies.Select(pol => new WorkspacePolicy
            {
                WorkspaceId = workspace.Id,
                PolicyId = pol.Id
            }).ToList();

            await workSpaceManageUnit.WorkspacePolicy.CreateWorkspacePoliciesAsync(workspacePolicies);

            // === Thêm mới danh sách chi tiết (Details) ===
            var newDetails = command.Details.Select(d => new Detail { Name = d.DetailName }).ToList();
            await workSpaceManageUnit.Detail.CreateDetailsAsync(newDetails);

            var workspaceDetails = newDetails.Select(det => new WorkspaceDetail
            {
                WorkspaceId = workspace.Id,
                DetailId = det.Id
            }).ToList();
            await workSpaceManageUnit.WorkspaceDetail.CreateWorkspaceDetailsAsync(workspaceDetails);


            var ownerNotification = new OwnerNotification
            {
                Description = $"Chúc mừng! Bạn đã cập nhật thành công không gian làm việc có tên {workspace.Name}. Hãy bắt đầu tổ chức các công việc của bạn ngay thôi.",
                OwnerId = workspace.OwnerId,
                CreatedAt = DateTime.Now
            };
            await workSpaceManageUnit.OwnerNotification.CreateAsync(ownerNotification);

            await workSpaceManageUnit.SaveAsync();

            return new UpdateWorkspaceResult($"Workspace '{workspace.Name}' updated successfully!");
        }
    }
}
