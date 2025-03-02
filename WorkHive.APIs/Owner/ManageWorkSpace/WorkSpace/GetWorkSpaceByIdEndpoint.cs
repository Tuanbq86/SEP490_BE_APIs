﻿using Carter;
using Mapster;
using MediatR;
using WorkHive.Services.Owners.ManageWorkSpace;
using WorkHive.Services.Owners.ManageWorkSpace.GetById;

namespace WorkHive.APIs.Owner.ManageWorkSpace.WorkSpace
{
    public record GetWorkSpaceByIdResponse(int Id, string Name, string Description, int? Capacity, string Category, string Status, int? CleanTime, int? Area, int OwnerId);

    public class GetWorkSpaceByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/workspaces/{id}", async (int id, ISender sender) =>
            {
                var command = new GetWorkSpaceByIdCommand(id);
                var result = await sender.Send(command);
                var response = result.Adapt<GetWorkSpaceByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetWorkSpaceById")
            .Produces<GetWorkSpaceByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Workspace by ID")
            .WithDescription("Retrieve a workspace using its ID.");
        }
    }
}

