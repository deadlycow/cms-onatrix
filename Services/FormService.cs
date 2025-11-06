using Onatrix.Interfaces;
using Onatrix.Models;
using System.Diagnostics;
using Umbraco.Cms.Core.Services;

namespace Onatrix.Services;
public class FormService(IContentService service) : IFormService
{
  private readonly IContentService _service = service;

  public bool CreateSupportRequest(SupportModel form)
  {
    if (form == null || string.IsNullOrWhiteSpace(form.Email))
      return false;

    var parent = _service!.GetRootContent().FirstOrDefault(x => x.Name == "Support");
    if (parent == null)
      return false;

    try
    {
      var submission = _service.Create(
          name: $"{DateTime.Now:yyyy-MM-dd HH:mm} :{Guid.NewGuid().ToString("N")[..4]}",
          parentId: parent.Id,
          documentTypeAlias: "supportSubmission"
      );

      submission.SetValue("supportSubmissionEmail", form.Email);
      var result = _service.Save(submission);
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

    var parent = _service!.GetRootContent().FirstOrDefault(x => x.Name == "Call Backs");
    if (parent == null)
      return false;

    try
    {
      var submission = _service.Create(
        name: $"{DateTime.Now:yyyy-MM-dd HH:mm} :{Guid.NewGuid().ToString("N")[..4]}",
        parentId: parent.Id,
        documentTypeAlias: "callBackSubmission"
        );

      submission.SetValue("callBackName", form.Name);
      submission.SetValue("callBackPhone", form.PhoneNumber);
      submission.SetValue("callBackEmail", form.Email);
      submission.SetValue("callBackOption", form.Option);
      var result = _service.Save(submission);
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

    var parent = _service!.GetRootContent().FirstOrDefault(x => x.Name == "Questions");
    if (parent == null)
      return false;
    try
    {
      var submission = _service.Create(
        name: $"{DateTime.Now:yyyy-MM-dd HH:mm} :{Guid.NewGuid().ToString("N")[..4]}",
        parentId: parent.Id,
        documentTypeAlias: "questionSubmission"
        );

      submission.SetValue("questionSubmissionName", form.Name);
      submission.SetValue("questionSubmissionEmail", form.QEmail);
      submission.SetValue("questionSubmissionText", form.Question);
      var result = _service.Save(submission);
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
