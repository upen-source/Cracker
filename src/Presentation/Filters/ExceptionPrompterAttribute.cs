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
                WriteError(e.Message);
            }
        }

        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }
    }
}
