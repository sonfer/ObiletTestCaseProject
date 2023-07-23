using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Obilet.Data.Requestes;
using Obilet.Data.Responses;
using Obilet.Service.LocationServices;
using Obilet.Service.SessionServices;
using Obilet.Shared.Responses;
using Obilet.Web.Models;

namespace Obilet.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISessionService _sessionService;
    private readonly ILocationService _locationService;
    public GetSessionResponse? _sessionResponse;
    public SearchJourneyViewModel? _searchJourneySession;
    public GetJourneyRequest? _sessionJourneyRequest;


    public HomeController(ILogger<HomeController> logger, ISessionService sessionService,
        ILocationService locationService)
    {
        _logger = logger;
        _sessionService = sessionService;
        _locationService = locationService;
        _sessionResponse = new GetSessionResponse();
        _searchJourneySession = new SearchJourneyViewModel();
        _sessionJourneyRequest = new GetJourneyRequest();
    }

    public async Task<IActionResult> Index()
    {
        var searchJourneySessionObject = HttpContext.Session.GetString("search-journey");
        if (!string.IsNullOrWhiteSpace(searchJourneySessionObject))
            _searchJourneySession = JsonSerializer.Deserialize<SearchJourneyViewModel>(searchJourneySessionObject);

        var request = new GetSessionRequest()
        {
            Type = 1,
            Connection = new Connection
            {
                Ipaddress = "165.114.41.21", Port = "5117",
            },
            Browser = new Browser() { Name = "Chrome", Version = "47.0.0.12" }
        };

        var data = await _sessionService.GetSession(request);
        HttpContext.Session.SetString("user-session", JsonSerializer.Serialize(data.Data));

        if (_searchJourneySession?.JourneyDate == null)
            _searchJourneySession.JourneyDate = DateTime.Now.ToString("dd MMMM yyyy dddd");
        return View(_searchJourneySession);
    }

    [HttpPost]
    public async Task<IActionResult> Index(SearchJourneyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var userSessionObject = HttpContext.Session.GetString("user-session");
        if (!string.IsNullOrWhiteSpace(userSessionObject))
            _sessionResponse = JsonSerializer.Deserialize<GetSessionResponse>(userSessionObject);

        if (model.Departure == null || model.Arrival == null)
            return Error();
        HttpContext.Session.SetString("search-journey", JsonSerializer.Serialize(model));

        var request = new GetLocationRequest();
        request.Data = $"{model.Departure}, {model.Arrival}";
        request.Date = Convert.ToDateTime(model.JourneyDate);
        request.Language = "tr-TR";
        request.DeviceSession = new DeviceSession()
        {
            DeviceId = _sessionResponse!.DeviceId,
            SessionId = _sessionResponse.SessionId,
        };

        var response = await _locationService.GetLocations(request);
        var locations = response.Data;

        var departure = locations.FirstOrDefault(d => d.CityName == model.Departure);
        var arrival = locations.FirstOrDefault(d => d.CityName == model.Arrival);

        var journeyRequest = new GetJourneyRequest
        {
            Date = request.Date,
            Language = request.Language,
            DeviceSession = request.DeviceSession,
            Data = new JourneyRequestData()
            {
                DepartureDate = request.Date,
                OriginId = departure!.Id,
                DestinationId = arrival!.Id
            }
        };

        HttpContext.Session.SetString("journey-session", JsonSerializer.Serialize(journeyRequest));

        return RedirectToAction("Index", "Journey");
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}