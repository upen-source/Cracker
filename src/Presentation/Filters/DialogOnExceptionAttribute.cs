using ArxOne.MrAdvice.Advice;
using System;
using System.Threading.Tasks;

namespace Presentation.Filters
{
    public class DialogOnExceptionAttribute : ErrorHandlingAttribute, IMethodAdvice
    {
        public string DialogTitle { get; }

        public DialogOnExceptionAttribute(string dialogTitle)
        {
            DialogTitle = dialogTitle;
        }

        public void Advise(MethodAdviceContext context)
        {
            try
            {
                context.Proceed();
            }
            catch (Exception e)
            {
                DisplayError(DialogTitle, e.Message);
            }
        }
    }
}
