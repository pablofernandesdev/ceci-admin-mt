﻿using Bogus;
using CeciAdminMT.Domain.DTO.Notification;

namespace CeciAdminMT.Test.Fakers.Notification
{
    public class NotificationFaker
    {
        public static Faker<NotificationSendDTO> NotificationSendDTO()
        {
            return new Faker<NotificationSendDTO>()
                .CustomInstantiator(p => new NotificationSendDTO
                {
                    Body = p.Random.Words(3),
                    IdUser = p.Random.Int(),
                    Title = p.Random.Word()
                });
        }
    }
}
