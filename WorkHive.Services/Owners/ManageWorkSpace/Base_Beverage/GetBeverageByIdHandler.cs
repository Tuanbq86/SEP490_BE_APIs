﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.BuildingBlocks.Exceptions;
using WorkHive.Repositories.IUnitOfWork;

namespace WorkHive.Services.Owners.ManageWorkSpace.Base_Beverage
{
    public record GetBeverageByIdQuery(int id) : IQuery<GetBeverageByIdResult>;
    public record GetBeverageByIdResult(int Id, string Name, decimal? Price, string ImgUrl, string Description, string Category, string Status, int WorkspaceId);


    public class GetBeverageByIdValidator : AbstractValidator<GetBeverageByIdQuery>
    {
        public GetBeverageByIdValidator()
        {
            RuleFor(x => x.id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }

    public class GetBeverageByIdHandler(IWorkSpaceManageUnitOfWork BeverageManageUnit)
    : IQueryHandler<GetBeverageByIdQuery, GetBeverageByIdResult>
    {
        public async Task<GetBeverageByIdResult> Handle(GetBeverageByIdQuery query, CancellationToken cancellationToken)
        {
            var beverage = await BeverageManageUnit.Beverage.GetByIdAsync(query.id);
            if (beverage == null)
            {
                return null;
            }

            return new GetBeverageByIdResult(
                beverage.Id,
                beverage.Name,
                beverage.Price,
                beverage.ImgUrl,
                beverage.Description,
                beverage.Category,
                beverage.Status,
                beverage.OwnerId
            );
        }

    }
}
