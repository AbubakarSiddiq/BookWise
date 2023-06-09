namespace BookWise.Web.Controllers
{
    using BookWise.Web.Services;
    using Microsoft.AspNetCore.Mvc;


    /// <summary>
    /// Controller for config.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IServiceOptionsService serviceOptionsService;

        /// <summary>
        /// Initialises a new instance of the <see cref="ConfigController"/> class.
        /// </summary>
        /// <param name="serviceOptionsService">The service options service.</param>
        public ConfigController(IServiceOptionsService serviceOptionsService)
        {
            this.serviceOptionsService = serviceOptionsService;
        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <returns>
        /// The result.
        /// </returns>
        public ActionResult Get()
        {
            return this.Ok(this.serviceOptionsService.GetServiceOptions());
        }
    }
}
