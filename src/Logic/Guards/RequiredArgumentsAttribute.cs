using System;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;
using Dawn;

namespace Logic.Guards
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiredArgumentsAttribute : Attribute, IMethodAsyncAdvice
    {
        public async Task Advise(MethodAsyncAdviceContext context)
        {
            Guard.Argument(context.Arguments).DoesNotContainNull();
            await context.ProceedAsync();
        }
    }
}
