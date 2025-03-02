﻿using Carter;
using MediatR;
using WorkHive.Services.Owmers.ManageBeverage.GetAllById;

namespace WorkHive.APIs.WorkSpace.ManageWorkSpace.Beverage
{
    public record GetBeveragesByWorkSpaceIdResponse(List<BeverageDTO> Beverages);

    public class GetBeveragesByWorkSpaceIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/beverages/WorkSpace/{workspaceId}", async (int WorkSpaceId, ISender sender) =>
            {
                var command = new GetBeveragesByWorkSpaceIdCommand(WorkSpaceId);
                var result = await sender.Send(command);
                var response = new GetBeveragesByWorkSpaceIdResponse(result);

                return Results.Ok(response);
            })
            .WithName("GetBeveragesByWorkSpaceId")
            .Produces<GetBeveragesByWorkSpaceIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Beverages by WorkSpace ID")
            .WithDescription("Retrieve all beverages belonging to a specific WorkSpace.");
        }
    }
}
