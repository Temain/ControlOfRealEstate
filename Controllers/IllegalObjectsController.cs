using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ControlOfRealEstate.DataAccess;
using ControlOfRealEstate.Models;
using ControlOfRealEstate.Models.IllegalObjectViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Linq;

namespace ControlOfRealEstate.Controllers
{
    [Produces("application/json")]
    [Route("api/illegal")]
    public class IllegalObjectsController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IllegalObjectsController(IHostingEnvironment appEnvironment, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _appEnvironment = appEnvironment;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("")]
        public List<IllegalObjectViewModel> Get(int? illegalObjectStatusId)
        {
            var illegalObjects = _context.IllegalObjects.AsNoTracking();

            if (illegalObjectStatusId != null)
            {
                illegalObjects = illegalObjects.Where(x => x.StatusId == illegalObjectStatusId);
            }

            var illegalObjectsList = illegalObjects
                .Select(x => new IllegalObjectViewModel
                {
                    IllegalObjectId = x.IllegalObjectId,
                    Name = x.Name,
                    Address = x.Address,
                    StatusId = x.StatusId,
                    StatusName = x.Status.IllegalObjectStatusName,
                    StatusPlacemark = x.Status.IllegalObjectPlacemark,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude
                })
                .ToList();

            return illegalObjectsList;
        }

        // POST: api/illegal/
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Create(CreateIllegalObjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);

                var illegalObject = new IllegalObject
                {
                    Address = viewModel.Address,
                    ApplicationUserId = user.Id,
                    Description = viewModel.Description,
                    Latitude = viewModel.Latitude,
                    Longitude = viewModel.Longitude,
                    StatusId = IllegalObjectStatuses.OnReview,
                    CreatedAt = DateTime.Now
                };

                _context.IllegalObjects.Add(illegalObject);
                _context.SaveChanges();

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize("Administrator")]
        [HttpGet("parse")]
        public IActionResult Parse()
        {
            var illegalObjects = new List<IllegalObjectViewModel>();

            GetGeoposition(illegalObjects);
            GetDescription(illegalObjects);
            SaveToDatabase(illegalObjects);

            return Ok();
        }

        /// <summary>
        /// Получение координат объектов незаконного строительства и краткого описания
        /// </summary>
        private void GetGeoposition(List<IllegalObjectViewModel> illegalObjects)
        {
            var filePath = _appEnvironment.ContentRootPath + "\\wwwroot\\js\\neagent\\illegal-geoposition.js";
            var fileContent = System.IO.File.ReadAllLines(filePath);

            var index = 1;
            var currentObject = new IllegalObjectViewModel();
            foreach (var line in fileContent)
            {
                if (line.Contains($"placemark{index + 1}") || fileContent.Last() == line)
                {
                    illegalObjects.Add(currentObject);
                    currentObject = new IllegalObjectViewModel();
                    index++;
                }

                try
                {
                    Regex pattern = new Regex(@"new YMaps.GeoPoint\((?<longitude>.*), (?<latitude>.*)\), {");
                    Match match = pattern.Match(line);
                    if (match.Success)
                    {
                        currentObject.Longitude = double.Parse(match.Groups["longitude"].Value, CultureInfo.InvariantCulture);
                        currentObject.Latitude = double.Parse(match.Groups["latitude"].Value, CultureInfo.InvariantCulture);
                    }

                    pattern = new Regex("name = \"(?<name>.*) №(?<neagentId>\\d+)\"");
                    match = pattern.Match(line);
                    if (match.Success)
                    {
                        currentObject.NeagentId = int.Parse(match.Groups["neagentId"].Value);
                        currentObject.Name = match.Groups["name"].Value + " №" + currentObject.NeagentId;
                    }

                    pattern = new Regex("description = \"<br/>Адрес: (?<address>.*)<br/>Статус: (?<status>.*)<br/><br/>");
                    match = pattern.Match(line);
                    if (match.Success)
                    {
                        currentObject.Address = match.Groups["address"].Value;
                        currentObject.StatusName = match.Groups["status"].Value;
                    }
                }
                catch (FormatException) { continue; }
            }
        }

        /// <summary>
        /// Получение детального описания объектов незаконного строительства
        /// </summary>
        private void GetDescription(List<IllegalObjectViewModel> illegalObjects)
        {
            var filePath = _appEnvironment.ContentRootPath + "\\wwwroot\\js\\neagent\\illegal-details.json";
            var fileContent = System.IO.File.ReadAllText(filePath);
            JArray illegalObjectsDetails = JArray.Parse(fileContent);
            
            foreach (var illegalObject in illegalObjects)
            {
                try
                {
                    var illegalObjectDetails = illegalObjectsDetails.FirstOrDefault(x => (int?)x["id"] == illegalObject.NeagentId);
                    if (illegalObjectDetails != null)
                    {
                        var data = (string)illegalObjectDetails["data"];

                        Regex pattern = new Regex("<td class=\"tdnm\">Описание объекта:</td>\n<td>(?<description>.*)</td>");
                        Match match = pattern.Match(data);
                        if (match.Success)
                        {
                            illegalObject.Description = match.Groups["description"].Value;
                        }

                        pattern = new Regex("<td class=\"tdnm\">Выявленные нарушения:</td>\n<td>(?<infringement>.*)</td>");
                        match = pattern.Match(data);
                        if (match.Success)
                        {
                            illegalObject.Infringement = match.Groups["infringement"].Value;
                        }

                        pattern = new Regex("<td class=\"tdnm\">Результаты рассмотрения:</td>\n<td>(?<review>.*)</td>");
                        match = pattern.Match(data);
                        if (match.Success)
                        {
                            illegalObject.ResultsOfReview = match.Groups["review"].Value;
                        }

                        pattern = new Regex("<td class=\"tdnm\">Дата добавления:</td>\n<td>(?<created>.*)</td>");
                        match = pattern.Match(data);
                        if (match.Success)
                        {
                            illegalObject.CreatedAt = DateTime.Parse(match.Groups["created"].Value);
                        }
                    }
                }
                catch (FormatException) { continue; }
            }
        }

        /// <summary>
        /// Сохранение полученных объектов в базу данных
        /// </summary>
        private void SaveToDatabase(List<IllegalObjectViewModel> illegalObjects)
        {
            var statuses = _context.IllegalObjectStatuses.AsNoTracking().ToList();

            foreach (var illegalObject in illegalObjects)
            {
                var newIllegalObjectStatus = statuses.FirstOrDefault(s => s.IllegalObjectStatusName == illegalObject.StatusName);
                if (newIllegalObjectStatus == null) continue;

                var illegalObjectInDb = _context.IllegalObjects.FirstOrDefault(x => x.NeagentId == illegalObject.NeagentId);
                if (illegalObjectInDb != null)
                {
                    illegalObjectInDb.CreatedAt = illegalObject.CreatedAt ?? DateTime.Now;

                    _context.IllegalObjects.Update(illegalObjectInDb);
                }
                else
                {
                    var newIllegalObject = new IllegalObject
                    {
                        Name = illegalObject.Name,
                        Description = illegalObject.Description,
                        Address = illegalObject.Address,
                        Infringement = illegalObject.Infringement,
                        Latitude = illegalObject.Latitude,
                        Longitude = illegalObject.Longitude,
                        NeagentId = illegalObject.NeagentId,
                        ResultsOfReview = illegalObject.ResultsOfReview,
                        StatusId = newIllegalObjectStatus.IllegalObjectStatusId
                    };

                    _context.IllegalObjects.Add(newIllegalObject);
                }

                _context.SaveChanges();
            }
        }
    }
}