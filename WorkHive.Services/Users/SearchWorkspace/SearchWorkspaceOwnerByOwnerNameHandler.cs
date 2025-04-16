﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Data.Models;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Constant;

namespace WorkHive.Services.Users.SearchWorkspace;

public record SearchWorkspaceOwnerByOwnerNameQuery(string? OwnerName) : IQuery<SearchWorkspaceOwnerByOwnerNameResult>;
public record SearchWorkspaceOwnerByOwnerNameResult(List<WorkspaceOwnerByOwnerNameDTO> WorkspaceOwnerByOwnerNameDTOs);
public record WorkspaceOwnerByOwnerNameDTO(
    string Phone,
    string Email,
    string GoogleMapUrl,
    string LicenseName,
    string LicenseAddress,
    string Avatar,
    double RateAverage,
    int NumberOfBooking);

public class SearchWorkspaceOwnerByOwnerNameHandler(IBookingWorkspaceUnitOfWork bookingUnit)
    : IQueryHandler<SearchWorkspaceOwnerByOwnerNameQuery, SearchWorkspaceOwnerByOwnerNameResult>
{
    public async Task<SearchWorkspaceOwnerByOwnerNameResult> Handle(SearchWorkspaceOwnerByOwnerNameQuery query, 
        CancellationToken cancellationToken)
    {
        List<WorkspaceOwnerByOwnerNameDTO> result = new List<WorkspaceOwnerByOwnerNameDTO>();

        var owners = bookingUnit.Owner.GetOwnerForSearch().ToList();

        if (!string.IsNullOrEmpty(query.OwnerName))
        {
            owners = (List<WorkspaceOwner>)owners
                .Where(w => EF.Functions.Like(w.LicenseName, $"%{query.OwnerName}%"));
        }

        foreach(var item in owners)
        {
            var workspaces = bookingUnit.workspace.GetAll().Where(w => w.OwnerId == item.Id).ToList();

            // Tính số sao trung bình của tất cả các workspace của owner
            double rate = 0;

            foreach(var workspace in workspaces)
            {
                int rateSum = 0;
                int count = 0;
                var workspaceRatings = bookingUnit.workspaceRating.GetAll()
                    .Where(wr => wr.WorkspaceId == workspace.Id).ToList();

                foreach (var rateItem in workspaceRatings)
                {
                    var rating = bookingUnit.rating.GetById(rateItem.RatingId);
                    rateSum += (int)rating.Rate!;
                    count += 1;
                }
                if (count > 0)
                {
                    rate += (double)rateSum / count;
                }
            }

            rate = Math.Round(rate, 1);

            // Tính số lượng booking của tất cả các workspace của owner
            int numberOfBooking = 0;
            var bookings = bookingUnit.booking.GetAll().Where(b => b.Status.ToLower().Trim()
            .Equals(BookingStatus.Success.ToString().ToLower().Trim()));
            foreach(var workspaceForBooking in workspaces)
            {
                foreach(var booking in bookings)
                {
                    if (booking.WorkspaceId == workspaceForBooking.Id)
                    {
                        numberOfBooking += 1;
                    }
                }
            }

            result.Add(new WorkspaceOwnerByOwnerNameDTO(
                item.Phone,
                item.Email,
                item.GoogleMapUrl,
                item.LicenseName,
                item.LicenseAddress,
                item.Avatar,
                rate,
                numberOfBooking));
        }

        return new SearchWorkspaceOwnerByOwnerNameResult(result);
    }
}
