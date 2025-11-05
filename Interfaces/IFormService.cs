using Onatrix.Models;

namespace Onatrix.Interfaces;
public interface IFormService
{
  bool CreateCallBackRequest(CallBackModel form);
  bool CreateQuestionRequest(QuestionModel form);
  bool CreateSupportRequest(SupportModel form);
}