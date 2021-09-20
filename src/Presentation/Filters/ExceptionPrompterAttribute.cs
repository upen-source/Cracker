using System;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;

namespace Presentation.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExceptionPrompterAttribute : Attribute, IMethodAsyncAdvice
    {
        public async Task Advise(MethodAsyncAdviceContext context)
        {
            try
            {
                await context.ProceedAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
