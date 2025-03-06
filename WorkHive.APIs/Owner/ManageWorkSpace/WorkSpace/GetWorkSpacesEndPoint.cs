﻿using Carter;
using MediatR;
using WorkHive.Services.Owners.ManageWorkSpace.CRUD_Base_Workspace;

namespace WorkHive.APIs.Owner.ManageWorkSpace.WorkSpace
{ 
public record GetWorkSpacesResponse(List<GetWorkSpacesResult> Workspaces);

public class GetWorkSpacesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/workspaces/", async (ISender sender) =>
        {
            var query = new GetWorkSpacesQuery();
            var result = await sender.Send(query);
            var response = new GetWorkSpacesResponse(result);

            return Results.Ok(response);
        })
        .WithName("GetWorkSpaces")
        .Produces<GetWorkSpacesByOwnerIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get all workspace ")
        .WithDescription("Retrieve all workspaces.");
    }
}
}
