﻿using Microsoft.Extensions.Configuration;
using Net.payOS.Types;
using Net.payOS;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Constant;
using WorkHive.Services.Exceptions;

namespace WorkHive.Services.WorkspaceTimes;

public record UpdateTimeCommand(long OrderCode, int BookingId) : ICommand<UpdateTimeResult>;
public record UpdateTimeResult(string Notification);

public class UpdateWorkspaceTimeStatusHandler(IBookingWorkspaceUnitOfWork bookUnit, IConfiguration configuration)
    : ICommandHandler<UpdateTimeCommand, UpdateTimeResult>
{
    private readonly string ClientID = configuration["PayOS:ClientId"]!;
    private readonly string ApiKey = configuration["PayOS:ApiKey"]!;
    private readonly string CheckSumKey = configuration["PayOS:CheckSumKey"]!;
    public async Task<UpdateTimeResult> Handle(UpdateTimeCommand command, 
        CancellationToken cancellationToken)
    {
        var bookWorkspace = bookUnit.booking.GetById(command.BookingId);

        PayOS payOS = new PayOS(ClientID, ApiKey, CheckSumKey);

        PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation(command.OrderCode);

        var Status = paymentLinkInformation.status.ToString();

        //Pay Failed

        if (!(Status.Equals(PayOSStatus.PAID.ToString())))
        {
            var workspaceTime = bookUnit.workspaceTime.GetAll()
                .FirstOrDefault(x => x.BookingId.Equals(command.BookingId));
            if (workspaceTime is null)
            {
                return new UpdateTimeResult("Yêu cầu không hợp lệ");
            }

            bookUnit.workspaceTime.Remove(workspaceTime!);

            var booking = bookUnit.booking.GetById(command.BookingId);
            if (booking is null)
            {
                return new UpdateTimeResult("Yêu cầu không hợp lệ");
            }

            booking.Status = BookingStatus.Fail.ToString();

            bookUnit.booking.Update(booking);

            await bookUnit.SaveAsync();
        }


        //Pay successfully
        if (Status.Equals(PayOSStatus.PAID.ToString()))
        {
            var workspaceTime = bookUnit.workspaceTime.GetAll()
                .FirstOrDefault(x => x.BookingId.Equals(command.BookingId));
            if (workspaceTime is null)
            {
                return new UpdateTimeResult("Yêu cầu không hợp lệ");
            }

            var booking = bookUnit.booking.GetById(command.BookingId);
            if (booking is null)
            {
                return new UpdateTimeResult("Yêu cầu không hợp lệ");
            }


            booking.Status = BookingStatus.Success.ToString();
            workspaceTime!.Status = WorkspaceTimeStatus.InUse.ToString();

            bookUnit.booking.Update(booking);
            bookUnit.workspaceTime.Update(workspaceTime);

            await bookUnit.SaveAsync();
        }

        return new UpdateTimeResult("Cập nhật trạng thái thành công");
    }
}
