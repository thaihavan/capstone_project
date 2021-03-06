﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NotificationService.Helpers;
using NotificationService.Hubs;
using NotificationService.Models;
using NotificationService.Repositories;
using NotificationService.Repositories.Interfaces;
using NotificationService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository = null;
        public NotificationService(IOptions<AppSettings> settings)
        {
            _notificationRepository = new NotificationRepository(settings);
        }

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public Notification Add(Notification notification)
        {
            return _notificationRepository.Add(notification);
        }

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            return _notificationRepository.GetNotifications(userId);
        }
    }
}
