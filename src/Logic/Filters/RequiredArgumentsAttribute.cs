using System;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;
using Dawn;

namespace Logic.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiredArgumentsAttribute : Attribute, IMethodAsyncAdvice
    {
        public string ErrorMessage { get; set; } = "Valor nulo";

        public async Task Advise(MethodAsyncAdviceContext context)
        {
            Guard.Argument(context.Arguments).DoesNotContainNull(_ => ErrorMessage);
            await context.ProceedAsync();
        }
    }
}
