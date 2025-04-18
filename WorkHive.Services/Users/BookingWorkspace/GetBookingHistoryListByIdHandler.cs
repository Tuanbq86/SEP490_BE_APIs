﻿using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Repositories.IUnitOfWork;
using WorkHive.Services.Users.DTOs;

namespace WorkHive.Services.Users.BookingWorkspace;

public record GetBookingHistoryListByIdQuery(int UserId) 
    : IQuery<GetBookingHistoryListByIdResult>;
public record GetBookingHistoryListByIdResult(List<BookingHistory> BookingHistories);


public class GetBookingHistoryListByIdHandler(IBookingWorkspaceUnitOfWork bookingUnit)
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
            var workspaceImages = bookingHistory.BookingHistoryWorkspaceImages ?? new List<BookingHistoryWorkspaceImage>();

            bookingHistory.Booking_StartDate = item.StartDate!;
            bookingHistory.Booking_EndDate = item.EndDate!;
            bookingHistory.Booking_Price = item.Price!;
            bookingHistory.Booking_Status = item.Status;
            bookingHistory.Booking_CreatedAt = item.CreatedAt!;
            bookingHistory.Payment_Method = item.Payment.Method;
            bookingHistory.Workspace_Name = item.Workspace.Name;
            bookingHistory.Workspace_Capacity = item.Workspace.Capacity!;
            bookingHistory.Workspace_Category = item.Workspace.Category;
            bookingHistory.Workspace_Description = item.Workspace.Description;
            bookingHistory.Workspace_Area = item.Workspace.Area!;
            bookingHistory.Workspace_Id = item.WorkspaceId;
            bookingHistory.Booking_Id = item.Id;
            bookingHistory.Workspace_CleanTime = item.Workspace.CleanTime!;

            if (item.PromotionId.HasValue)
            {
                var promotionForBooking = bookingUnit.promotion.GetById(item.PromotionId.Value);
                bookingHistory.Promotion_Code = promotionForBooking?.Code;
                bookingHistory.Discount = promotionForBooking?.Discount ?? 0;
            }
            else
            {
                bookingHistory.Promotion_Code = null;
                bookingHistory.Discount = 0;
            }

            bookingHistory.IsReview = item.IsReview.GetValueOrDefault();
            bookingHistory.IsFeedback = item.IsFeedback.GetValueOrDefault();
            // Add new attribute
            bookingHistory.License_Name = item.Workspace.Owner.LicenseName;
            bookingHistory.License_Address = item.Workspace.Owner.LicenseAddress;

            foreach(var amenity in item.BookingAmenities)
                amenities.Add(new BookingHistoryAmenity 
                ((int)amenity.Quantity!, amenity.Amenity.Name, (decimal)amenity.Amenity.Price!, amenity.Amenity.ImgUrl));

            foreach(var beverage in item.BookingBeverages)
                beverages.Add(new BookingHistoryBeverage
                    ((int)beverage.Quantity!, beverage.Beverage.Name, (decimal)beverage.Beverage.Price!, beverage.Beverage.ImgUrl));

            foreach (var image in item.Workspace.WorkspaceImages)
                workspaceImages.Add(new BookingHistoryWorkspaceImage(image.Image.ImgUrl));

            bookingHistory.BookingHistoryAmenities = amenities;
            bookingHistory.BookingHistoryBeverages = beverages;
            bookingHistory.BookingHistoryWorkspaceImages = workspaceImages;

            results.Add(bookingHistory);
        }

        return new GetBookingHistoryListByIdResult(results);
    }
}