using Microsoft.AspNetCore.Mvc;
using Onatrix.Interfaces;
using Onatrix.Models;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;

namespace Onatrix.Controllers;

public class FormController(
    IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoDatabaseFactory databaseFactory,
    ServiceContext services,
    AppCaches appCaches,
    IProfilingLogger profilingLogger,
    IPublishedUrlProvider publishedUrlProvider, IPublishedContentQuery contentQuery, IFormService formService) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
  private readonly IFormService _formService = formService;
  private readonly IPublishedContentQuery _contentQuery = contentQuery;
  public IActionResult SupportSubmit(SupportModel model)
  {
    if (!ModelState.IsValid)
    {
      return CurrentUmbracoPage();
    }

    var success = _formService.CreateSupportRequest(model);
    if (success)
      TempData["SupportSuccess"] = "Thank you! We'll get back to you soon.";

    return Redirect($"{CurrentPage?.Url()}#support");
  }
  public IActionResult CallBackSubmit(CallBackModel form)
  {
    var formSettings = _contentQuery.ContentAtRoot().OfType<FormSettings>().FirstOrDefault();
    var allowedOptions = formSettings?.CallBackOptions ?? [];

    if (!allowedOptions.Contains(form.Option))
    {
      ModelState.AddModelError(nameof(form.Option), "The selected option is invalid.");
    }

    if (!ModelState.IsValid)
    {
      return CurrentUmbracoPage();
    }
    var success = _formService.CreateCallBackRequest(form);
    if (success)
      TempData["CallBackSuccess"] = "Thank you! We'll call you soon.";
    return Redirect($"{CurrentPage?.Url()}#request");

  }
  public IActionResult QuestionSubmit(QuestionModel form)
  {
    if (!ModelState.IsValid)
    {
      return CurrentUmbracoPage();
    }

    var success = _formService.CreateQuestionRequest(form);
    if (success)
      TempData["QuestionSuccess"] = "Thank you for your question. We'll get back to you soon.";
    return Redirect($"{CurrentPage?.Url()}#questionSection");
  }
}