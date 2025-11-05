using Onatrix.Interfaces;
using Onatrix.Models;
using System.Diagnostics;
using Umbraco.Cms.Core.Services;

namespace Onatrix.Services;
public class FormService(ServiceContext services) : IFormService
{
  private readonly ServiceContext _services = services;

  public bool CreateSupportRequest(SupportModel form)
  {
    if (form == null || string.IsNullOrWhiteSpace(form.Email))
      return false;

    var contentService = _services.ContentService;
    var parent = contentService!.GetRootContent().FirstOrDefault(x => x.Name == "Support");
    if (parent == null)
      return false;

    try
    {
      var submission = contentService.Create(
          name: $"{DateTime.Now:yyyy-MM-dd HH:mm} :{Guid.NewGuid().ToString("N")[..4]}",
          parentId: parent.Id,
          documentTypeAlias: "supportSubmission"
      );

      submission.SetValue("supportSubmissionEmail", form.Email);
      var result = contentService.Save(submission);
      if (!result.Success)
        return false;

      return true;
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return false;
    }
  }
  public bool CreateCallBackRequest(CallBackModel form)
  {
    if (form == null)
      return false;

    var contentService = _services.ContentService;
    var parent = contentService!.GetRootContent().FirstOrDefault(x => x.Name == "Call Backs");
    if (parent == null)
      return false;

    try
    {
      var submission = contentService.Create(
        name: $"{DateTime.Now:yyyy-MM-dd HH:mm} :{Guid.NewGuid().ToString("N")[..4]}",
        parentId: parent.Id,
        documentTypeAlias: "callBackSubmission"
        );

      submission.SetValue("callBackSubmission", form);
      var result = contentService.Save(submission);
      if (!result.Success)
        return false;

      return true;
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return false;
    }
  }
  public bool CreateQuestionRequest(QuestionModel form)
  {
    if (form == null)
      return false;

    var contentService = _services.ContentService;
    var parent = contentService!.GetRootContent().FirstOrDefault(x => x.Name == "Questions");
    if (parent == null)
      return false;
    try
    {
      var submission = contentService.Create(
        name: $"{DateTime.Now:yyyy-MM-dd HH:mm} :{Guid.NewGuid().ToString("N")[..4]}",
        parentId: parent.Id,
        documentTypeAlias: "questionSubmission"
        );

      submission.SetValue("questionSubmissionName", form.Name);
      submission.SetValue("questionSubmissionEmail", form.Email);
      submission.SetValue("questionSubmissionText", form.Question);
      var result = contentService.Save(submission);
      if (!result.Success)
        return false;

      return true;
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return false;
    }
  }
}
