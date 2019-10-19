
using System;
using System.Collections.Generic;
using System.Linq;
using AppLockerService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AppLockerService.Controllers
{
    [Route("")]
    [ApiController]
    public class AppLockerController : ControllerBase
    {
        private AppLockerServiceContext _dbContext;
        private IHostingEnvironment _hostingEnvironment;
        public AppLockerController(
          AppLockerServiceContext dbContext,
          IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }



        /// <summary>
        /// For creating a new app, we require a name and a description.
        /// The system creates a code and marks the app as locked by default.
        /// The endpoint returns the app object.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public ActionResult<App> CreateNewApp(dynamic json)
        {
            try
            {
                (string name, string description) = (json.name, json.description);
                var code = new Random().Next();
                var app = new App()
                {
                    Code = code,
                    Name = name,
                    Description = description,
                    IsLocked = true,
                    Reason = "Newly created applications are locked by default."
                };

                _dbContext.Apps.Add(app);
                _dbContext.SaveChanges();

                return Ok(app);
            }
            catch (Exception e)
            {
                if (_hostingEnvironment.IsDevelopment())
                    return BadRequest(e);
                return BadRequest(
                   $"Something went wrong while creating application entry. Input: {json}"
                );
            }
        }


        /// <summary>
        /// Locks the application identified by the provided code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("unlock")]
        public ActionResult<App> Unlock(int code) => ChangeLockStatus(code, false);

        /// <summary>
        /// Unlocks the application identified by the provided code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("lock")]
        public ActionResult<App> Lock(int code) => ChangeLockStatus(code, true);

        public ActionResult<App> ChangeLockStatus(int code, bool isLocked)
        {
            try
            {
                var app = _dbContext.Apps.FirstOrDefault(o => o.Code == code);
                if (app == null) return BadRequest($"No applications found with code {code}");

                app.IsLocked = isLocked;
                _dbContext.SaveChanges();

                return Ok(app);
            }
            catch (Exception e)
            {
                if (_hostingEnvironment.IsDevelopment())
                    return BadRequest(e);
                return BadRequest(
                  $"Something went wrong while changing lock status for {code}. Please try again later."
                );
            }
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<App>> GetAllApps()
        {
            return _dbContext.Apps.Select(app => app).ToList();
        }


    }
}



