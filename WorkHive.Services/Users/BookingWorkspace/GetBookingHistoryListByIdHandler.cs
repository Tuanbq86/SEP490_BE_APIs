﻿using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Repositories.IRepositories;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Users.DTOs;

namespace WorkHive.Services.Users.BookingWorkspace;

public record GetBookingHistoryListByIdQuery(int UserId) 
    : IQuery<GetBookingHistoryListByIdResult>;
public record GetBookingHistoryListByIdResult(List<BookingHistory> BookingHistories);


public class GetBookingHistoryListByIdHandler(IBookingWorkspaceUnitOfWork bookingUnit, ITokenRepository tokenRepo)
    : IQueryHandler<GetBookingHistoryListByIdQuery, GetBookingHistoryListByIdResult>
{
    public async Task<GetBookingHistoryListByIdResult> Handle(GetBookingHistoryListByIdQuery query, 
        CancellationToken cancellationToken)
    {
        var bookings = await bookingUnit.booking.GetAllBookingByUserId(query.UserId);

        List<BookingHistory> results = new List<BookingHistory>();

        foreach(var item in bookings)
        {
            var bookingHistory = new BookingHistory();

            //If null amenities and beverages will assign default list[]
            var amenities = bookingHistory.BookingHistoryAmenities ?? new List<BookingHistoryAmenity>();
            var beverages = bookingHistory.BookingHistoryBeverages ?? new List<BookingHistoryBeverage>();


            bookingHistory.Booking_StartDate = (DateTime)item.StartDate!;
            bookingHistory.Booking_EndDate = (DateTime)item.EndDate!;
            bookingHistory.Booking_Price = (decimal)item.Price!;
            bookingHistory.Booking_Status = item.Status;
            bookingHistory.Booking_CreatedAt = (DateTime)item.CreatedAt!;
            bookingHistory.Payment_Method = item.Payment.PaymentMethod;
            bookingHistory.Workspace_Name = item.Workspace.Name;
            bookingHistory.Workspace_Capacity = (int)item.Workspace.Capacity!;
            bookingHistory.Workspace_Category = item.Workspace.Category;
            bookingHistory.Workspace_Description = item.Workspace.Description;
            bookingHistory.Workspace_Area = (int)item.Workspace.Area!;
            bookingHistory.Workspace_CleanTime = (int)item.Workspace.CleanTime!;

            //If null amenities and beverages will assign default "No Promotion"
            bookingHistory.Promotion_Code = item.Promotion?.Code ?? "No Promotion";
            //If null amenities and beverages will assign default value: 0
            bookingHistory.Discount = (int)(item.Promotion?.Discount ?? 0);

            foreach(var amenity in item.BookingAmenities)
                amenities.Add(new BookingHistoryAmenity 
                ((int)amenity.Quantity!, amenity.Amenity.Name, (decimal)amenity.Amenity.Price!));

            foreach (var beverage in item.BookingBeverages)
                beverages.Add(new BookingHistoryBeverage
                    ((int)beverage.Quantity!, beverage.Beverage.Name, (decimal)beverage.Beverage.Price!));

            bookingHistory.BookingHistoryAmenities = amenities;
            bookingHistory.BookingHistoryBeverages = beverages;

            results.Add(bookingHistory);
        }

        return new GetBookingHistoryListByIdResult(results);
    }
}