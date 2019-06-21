using System;
using Common.Api.Builders.Resource;
using Common.Api.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logsheet.Api.Logsheets
{
    [DefaultControllerRoute("{userId}/logsheets")]
    public class LogsheetController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        public LogsheetController(IResourceBuilder resourceBuilder)
        {
            _resourceBuilder = resourceBuilder;
        }

        //TODO: Discuss AllowAnonymous
        [AllowAnonymous]
        [HttpGet("resourceTemplate", Name = RouteDictionary.LogsheetTemplate)]
        public IActionResult Get(Guid userId)
        {
            return new ObjectResult(
                _resourceBuilder.Build(new CreateLogsheetResource())
            );
        }
    }
}
