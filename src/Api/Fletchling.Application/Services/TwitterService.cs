﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels;
using Tweetinvi;

namespace Fletchling.Application.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly ITwitterClient _client;

        public TwitterService(ITwitterClient client)
        {
            _client = client;
        }

        public async Task<List<TwitterUser>> SearchUsersAsync(string username)
        {
            var res = await _client.Search.SearchUsersAsync(username);

            var users = new List<TwitterUser>();
            foreach (var user in res)
            {
                users.Add(new TwitterUser
                {
                    Id = user.Id,
                    Username = user.ScreenName,
                    DisplayName = user.Name,
                    Verified = user.Verified,
                    ProfileImageUrl = user.ProfileImageUrl
                });
            }

            return users;
        }

        public async Task<TwitterUser> GetUserAsync(long userId)
        {
            var res = await _client.UsersV2.GetUserByIdAsync(userId);
            var user = new TwitterUser
            {
                Id = long.Parse(res.User.Id),
                Username = res.User.Username,
                DisplayName = res.User.Name,
                Verified = res.User.Verified,
                ProfileImageUrl = res.User.ProfileImageUrl
            };
            return user;
        }
    }
}