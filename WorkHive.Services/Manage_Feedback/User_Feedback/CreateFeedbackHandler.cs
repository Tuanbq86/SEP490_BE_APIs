﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkHive.Data.Models;
using WorkHive.BuildingBlocks.CQRS;
using WorkHive.Repositories.IUnitOfWork;

namespace WorkHive.Services.Manage_Feedback.User_Feedback
{
    public record CreateFeedbackCommand(string Title, string Description, int UserId, int BookingId , List<ImageFeedbackDTO>? Images = null) : ICommand<CreateFeedbackResult>;

    public record ImageFeedbackDTO(string ImgUrl);

    public record CreateFeedbackResult(string Notification);

    class CreateFeedbackHandler(IFeedbackManageUnitOfWork unit) : ICommandHandler<CreateFeedbackCommand, CreateFeedbackResult>
    {
        private const string DefaultImageTitle = "Feedback Image";
        private const string DefaultStatus = "Active";


        public async Task<CreateFeedbackResult> Handle(CreateFeedbackCommand command, CancellationToken cancellationToken)
        {
            var existingFeedback = await unit.Feedback.GetFirstFeedbackByBookingId(command.BookingId);
            var existingBooking = await unit.Booking.GetByIdAsync(command.BookingId);
            var workspace = await unit.Workspace.GetByIdAsync(existingBooking.WorkspaceId);
            var existingUser = await unit.User.GetByIdAsync(command.UserId);
            //var owner = await unit.WorkspaceOwner.GetByIdAsync(workspace.OwnerId);


            if (existingUser == null)
            {
                return new CreateFeedbackResult("User not found.");
            }
            if (existingFeedback != null)
            {
                return new CreateFeedbackResult("Booking already has feedback. Cannot create another feedback.");
            }

            List<Image> images = command.Images?.Select(i => new Image
            {
                ImgUrl = i.ImgUrl,
                Title = DefaultImageTitle,
                CreatedAt = DateTime.Now
            }).ToList() ?? new List<Image>();

            var newFeedback = new Feedback
            {
                Title = command.Title,
                Description = command.Description,
                UserId = command.UserId,
                BookingId = command.BookingId,
                Status = DefaultStatus,
                CreatedAt = DateTime.Now
            };

            var booking = await unit.Booking.GetByIdAsync(command.BookingId);
            if (booking != null)
            {
                booking.IsFeedback = 1;
                await unit.Booking.UpdateAsync(booking);
                await unit.SaveAsync();
            }

            if (images.Any())
            {
                await unit.Image.CreateImagesAsync(images);
            }

            var ownerNotification = new OwnerNotification
            {
                Description = $"{newFeedback.Description}.",
                Status = "Active",
                OwnerId = workspace.OwnerId,
                CreatedAt = DateTime.Now,
                IsRead = 0,
                Title = $"Phản hổi từ {existingUser.Name}. {newFeedback.Title}"
            };


            await unit.Feedback.CreateAsync(newFeedback);
            await unit.OwnerNotification.CreateAsync(ownerNotification);
            await unit.SaveAsync();

            if (images.Any())
            {
                var imageFeedbacks = images.Select(img => new ImageFeedback
                {
                    ImageId = img.Id,
                    FeedbackId = newFeedback.Id,
                    Status = DefaultStatus
                }).ToList();

                await unit.ImageFeedback.CreateImageFeedbackAsync(imageFeedbacks);
                await unit.SaveAsync();
            }

            return new CreateFeedbackResult("Feedback created successfully");
        }
    }
}
