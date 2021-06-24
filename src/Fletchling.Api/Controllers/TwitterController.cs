﻿using Fletchling.Twitter.Models;
using Fletchling.Twitter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Fletchling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {        
        private readonly ITwitterService _twitterService;

        public TwitterController(ITwitterService twitterService)
        {            
            _twitterService = twitterService;
        }

        [Route("user/search")]
        [HttpGet]
        public async Task<ActionResult<List<TwitterUser>>> SearchUser([FromQuery, Required] string username)
        {
            var res = await _twitterService.SearchUsersAsync(username);            
            return res;
        }
    }
}
