using Microsoft.AspNetCore.Mvc;
using Onatrix.Interfaces;
using Onatrix.Models;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Onatrix.Controllers;

public class FormController(
    IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoDatabaseFactory databaseFactory,
    ServiceContext services,
    AppCaches appCaches,
    IProfilingLogger profilingLogger,
    IPublishedUrlProvider publishedUrlProvider, IFormService formService) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
  private readonly IFormService _formService = formService;
  public IActionResult SupportSubmit(SupportModel model)
  {
    if (!ModelState.IsValid)
    {
      return CurrentUmbracoPage();
    }
    var success = _formService.CreateSupportRequest(model);

    TempData["success"] = "Your request has been submitted!";
    return RedirectToCurrentUmbracoPage();
  }
  public IActionResult CallBackSubmit(CallBackModel form) {
    if (!ModelState.IsValid)
    {
      return CurrentUmbracoPage();
    }
    var success = _formService.CreateCallBackRequest(form);
    return RedirectToCurrentUmbracoPage();
  }
  public IActionResult QuestionSubmit(QuestionModel form) {
    if (!ModelState.IsValid)
    {
      return CurrentUmbracoPage();
    }

    var success = _formService.CreateQuestionRequest(form);
    return RedirectToCurrentUmbracoPage();
  }
}