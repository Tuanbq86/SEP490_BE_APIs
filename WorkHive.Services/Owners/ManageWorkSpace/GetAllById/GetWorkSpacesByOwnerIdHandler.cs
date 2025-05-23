﻿using FluentValidation;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.BuildingBlocks.Exceptions;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Owners.ManageWorkSpace.CRUD_Base_Workspace;

namespace WorkHive.Services.Owners.ManageWorkSpace.GetAllById;

public record GetWorkSpacesByOwnerIdQuery(int Id) : IQuery<List<GetWorkSpaceByOwnerIdResult>>;

public record GetWorkSpaceByOwnerIdResult(int Id, string Name, string Address, string GoogleMapUrl, string Description, int? Capacity, string Category, 
    string Status, DateTime? CreatedAt, DateTime? UpdatedAt , int? CleanTime, int? Area, int OwnerId, TimeOnly? OpenTime, TimeOnly? CloseTime, int? Is24h, string Code, string LicenseName, string phone, List<WorkspacesPriceDTO> Prices,
List<WorkspacesImageDTO> Images, List<WorkspaceFacilityDTO> Facilities, List<WorkspacePolicyDTO> Policies, List<WorkspaceDetailDTO> Details);

public record WorkspacesPriceDTO(int Id, decimal? Price, string Category);
public record WorkspacesImageDTO(int Id, string ImgUrl);
public record WorkspaceFacilityDTO(int Id, string FacilityName);
public record WorkspacePolicyDTO(int Id, string PolicyName);
public record WorkspaceDetailDTO(int Id, string DetailName);



public class GetWorkSpacesByOwnerIdValidator : AbstractValidator<GetWorkSpacesByOwnerIdQuery>
{
    public GetWorkSpacesByOwnerIdValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Owner ID must be greater than 0");
    }
}
public class GetWorkSpacesByOwnerIdHandler(IWorkSpaceManageUnitOfWork workSpaceManageUnit)
: IQueryHandler<GetWorkSpacesByOwnerIdQuery, List<GetWorkSpaceByOwnerIdResult>>
{
    public async Task<List<GetWorkSpaceByOwnerIdResult>> Handle(GetWorkSpacesByOwnerIdQuery Query, 
        CancellationToken cancellationToken)
    {
        var workspaces = await workSpaceManageUnit.Workspace.GetAllWorkSpaceByOwnerIdAsync(Query.Id);

        if (workspaces == null || !workspaces.Any())
        {
            return null;
        }
        WorkspaceOwner owner = await workSpaceManageUnit.WorkspaceOwner.GetByIdAsync(Query.Id);
        return workspaces.Select(ws => new GetWorkSpaceByOwnerIdResult(
            ws.Id,
            ws.Name,
            owner.LicenseAddress,
            owner.GoogleMapUrl,
            ws.Description,
            ws.Capacity,
            ws.Category,
            ws.Status,
            ws.CreatedAt,
            ws.UpdatedAt,
            ws.CleanTime,
            ws.Area,
            ws.OwnerId,
            ws.OpenTime,
            ws.CloseTime,
            ws.Is24h,
            ws.Code,
            ws.Owner.LicenseName,
            ws.Owner.Phone,
            ws.WorkspacePrices.Select(wp => new WorkspacesPriceDTO(
                wp.Price.Id,
                wp.Price.AveragePrice,
                wp.Price.Category
            )).ToList(),
            ws.WorkspaceImages.Select(wi => new WorkspacesImageDTO(
                wi.Image.Id,
                wi.Image.ImgUrl
            )).ToList(),
            ws.WorkspaceFacilities.Select(wf => new WorkspaceFacilityDTO(
                wf.Facility.Id,
                wf.Facility.Name
            )).ToList(),
            ws.WorkspacePolicies.Select(wp => new WorkspacePolicyDTO(
                wp.Policy.Id,
                wp.Policy.Name
            )).ToList(),
            ws.WorkspaceDetails.Select(wd => new WorkspaceDetailDTO(
                wd.Detail.Id,
                wd.Detail.Name
            )).ToList()
            )).ToList();
    }
}
