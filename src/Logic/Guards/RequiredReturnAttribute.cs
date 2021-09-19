using System;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;

namespace Logic.Guards
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiredReturnAttribute : Attribute, IMethodAsyncAdvice
    {
        public async Task Advise(MethodAsyncAdviceContext context)
        {
            // TODO: Check for null in method return value
            object returnValue = context.ReturnValue;
            await context.ProceedAsync();
        }
    }
}
