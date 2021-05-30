﻿using Fletchling.Twitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace Fletchling.Twitter.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly ITwitterClient _client;

        public TwitterService(ITwitterClient client)
        {
            _client = client;
        }

        public async Task<List<User>> SearchUsersAsync(string username)
        {
            var res = await _client.Search.SearchUsersAsync(username);

            var users = new List<User>();
            foreach (var user in res)
            {
                users.Add(new User
                {
                    Id = user.Id,
                    Username = user.ScreenName,
                    DisplayName = user.Name
                });
            }
            
            return users;
        }
    }
}