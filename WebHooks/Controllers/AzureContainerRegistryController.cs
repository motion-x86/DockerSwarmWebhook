using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebHooks;
using WebHooks.Jobs;
using WebHooks.Models;

namespace DockerWebHook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureContainerRegistryController : ControllerBase
    {
        private readonly ACRConfig config;
        private readonly ILogger logger;

        public AzureContainerRegistryController(IOptions<ACRConfig> config, ILogger<AzureContainerRegistryController> logger)
        {
            this.config = config.Value;
            this.logger = logger;
        }

        // POST api/docker/:token
        [HttpPost("{token}")]
        public IActionResult Post(Guid token, [FromBody] ACRPayload payload)
        {
            var image = payload?.GetImageName();
            if (token != this.config.Token || string.IsNullOrEmpty(image))
            {
                logger.LogWarning($"WebHook Request Failed: Bad Token [{token}] or Image [{image}]");
                return BadRequest();
            } else if (!config.Services.ContainsKey(image))
            {
                logger.LogWarning($"WebHook Request Failed: Invalid image [{image}]");
                return BadRequest();
            }

            logger.LogInformation($"WebHook Request Succeeded: For image [{image}]");
            Program.DeploymentQueue.AddJob(new SwarmServiceUpdateJob(config, payload));

            return Ok();
        }
    }
}
