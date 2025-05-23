﻿using Carter;
using Mapster;
using MediatR;
using WorkHive.Services.Owners.Base_Owner;

namespace WorkHive.APIs.Owner.Manage_Owner
{
    public record UpdateWorkspaceOwnerRequest(string Phone, string Email, string Sex, string GoogleMapUrl, string Status, string LicenseName, string LicenseNumber, string LicenseAddress, decimal? CharterCapital, string LicenseFile, string? Facebook, string? Instagram, string? Tiktok, string PhoneStatus);
    public record UpdateWorkspaceOwnerResponse(string Notification);

    public class UpdateWorkspaceOwnerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/workspace-owners/{id}", async (int id, UpdateWorkspaceOwnerRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateWorkspaceOwnerCommand>() with { Id = id };
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateWorkspaceOwnerResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateWorkspaceOwner")
            .Produces<UpdateWorkspaceOwnerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Owner")
            .WithSummary("Update an existing workspace owner")
            .RequireAuthorization("Owner")
            .WithDescription("Updates a workspace owner by its ID with the provided details.");
        }
    }
}
//Owner