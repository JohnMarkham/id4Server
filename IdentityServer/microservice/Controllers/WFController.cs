using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace microservice.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class WFController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WFController> _logger;

        public WFController(ILogger<WFController> logger)
        {
            _logger = logger;
        }


        // GET: /api/WF/1
        [Authorize(Policy = "AuthClient")]
        [HttpGet("{id}")]
        public IEnumerable<WeatherFing> GetId()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherFing
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

        }

        // GET: /api/WF
        [Authorize(Policy = "PublicSecure")]
        [HttpGet]
        public IEnumerable<WeatherFing> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherFing
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            
        }

        // POST: /api/file/WF
        
        [HttpPost("file")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

    }
}
