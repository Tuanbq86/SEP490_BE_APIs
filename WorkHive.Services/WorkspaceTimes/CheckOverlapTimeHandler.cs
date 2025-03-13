﻿using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Constant;
using WorkHive.Services.Exceptions;

namespace WorkHive.Services.WorkspaceTimes;

public record CheckTimesCommand(int WorkspaceId, string StartDate, string EndDate) : ICommand<CheckTimesResult>;
public record CheckTimesResult(string Notification);

public class CheckOverlapTimeHandler(IBookingWorkspaceUnitOfWork bookUnit)
    : ICommandHandler<CheckTimesCommand, CheckTimesResult>
{
    public async Task<CheckTimesResult> Handle(CheckTimesCommand command, 
        CancellationToken cancellationToken)
    {
        var workspace = bookUnit.workspace.GetById(command.WorkspaceId);

        //Cast startdate and enddate base on request
        var startDateTime = DateTime.ParseExact(command.StartDate, "HH:mm dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture);
        var endDateTime = DateTime.ParseExact(command.EndDate, "HH:mm dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture);

        //cast only time
        var startTime = TimeOnly.FromDateTime(startDateTime);
        var endTime = TimeOnly.FromDateTime(endDateTime);

        if ((startTime < workspace.OpenTime || endTime > workspace.CloseTime) 
            && !(workspace.Is24h.Equals(1)))
        {
            return new CheckTimesResult("Thời gian không nằm trong giờ mở cửa của workspace");
        }

        if(!(workspace.Is24h.Equals(1)) && startTime >= workspace.OpenTime 
            && endTime <= workspace.CloseTime)
        {
            var timesOfWorkspaceId = bookUnit.workspaceTime.GetAll()
                .Where(x => x.WorkspaceId.Equals(command.WorkspaceId)).ToList();

            var TimesHandlingOrInuse = timesOfWorkspaceId
                .Where(x => x.Status.Equals(WorkspaceTimeStatus.InUse.ToString())
                         || x.Status.Equals(WorkspaceTimeStatus.Handling.ToString())).ToList();

            foreach (var item in TimesHandlingOrInuse)
            {
                item.EndDate = item.EndDate.GetValueOrDefault()
                    .AddMinutes(workspace.CleanTime.GetValueOrDefault());
            }

            if (bookUnit.workspaceTime.IsOverlap(TimesHandlingOrInuse, startDateTime, endDateTime))
            {
                return new CheckTimesResult("Khoảng thời gian đã được sử dụng");
            }
            else
            {
                return new CheckTimesResult("Khoảng thời gian phù hợp");
            }
        }

        if (workspace.Is24h.Equals(1))
        {
            var timesOfWorkspaceId = bookUnit.workspaceTime.GetAll()
                .Where(x => x.WorkspaceId.Equals(command.WorkspaceId)).ToList();

            var TimesHandlingOrInuse = timesOfWorkspaceId
                .Where(x => x.Status.Equals(WorkspaceTimeStatus.InUse.ToString())
                         || x.Status.Equals(WorkspaceTimeStatus.Handling.ToString())).ToList();

            foreach (var item in TimesHandlingOrInuse)
            {
                item.EndDate = item.EndDate.GetValueOrDefault()
                    .AddMinutes(workspace.CleanTime.GetValueOrDefault());
            }

            if (bookUnit.workspaceTime.IsOverlap(TimesHandlingOrInuse, startDateTime, endDateTime))
            {
                return new CheckTimesResult("Khoảng thời gian đã được sử dụng");
            }
            else
            {
                return new CheckTimesResult("Khoảng thời gian phù hợp");
            }
        }
        else
        {
            return new CheckTimesResult("Workspace không hoạt động 24h.");
        }
    }
}
