using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Obilet.Data.Requestes;
using Obilet.Data.Responses;
using Obilet.Service.JourneyServices;
using Obilet.Web.Models;

namespace Obilet.Web.Controllers;

public class JourneyController : Controller
{
    private readonly ILogger<JourneyController> _logger;
    private readonly IJourneyService _journeyService;
    public GetSessionResponse? _sessionResponse;
    public SearchJourneyViewModel? _searchJourneySession;
    

    public JourneyController(ILogger<JourneyController> logger, IJourneyService journeyService)
    {
        _logger = logger;
        _journeyService = journeyService;
        _sessionResponse = new GetSessionResponse();
        _searchJourneySession = new SearchJourneyViewModel();
    }
    
    public async Task<IActionResult> Index()
    {
        GetJourneyRequest? journeyRequest = new GetJourneyRequest();
        SearchJourneyViewModel journeySearch = new SearchJourneyViewModel();
        
        var journeySessionObject = HttpContext.Session.GetString("journey-session");
        if (!string.IsNullOrWhiteSpace(journeySessionObject))
            journeyRequest = JsonSerializer.Deserialize<GetJourneyRequest>(journeySessionObject);

        var searchSessionObject = HttpContext.Session.GetString("search-journey");
        if (!string.IsNullOrWhiteSpace(searchSessionObject))
            journeySearch = JsonSerializer.Deserialize<SearchJourneyViewModel>(searchSessionObject);

        var journeyResponse = await _journeyService.GetLocations(journeyRequest);
        var journeys = journeyResponse.Data;

        ViewData["JourneyData"] =
            $"{journeySearch.Departure} - {journeySearch.Arrival}";
        ViewData["JourneyDataDate"] = journeySearch.JourneyDate;

        return View(journeys);
    }

    

    

    
}