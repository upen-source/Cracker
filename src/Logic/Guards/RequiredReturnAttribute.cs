using System;
using System.Globalization;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;
using Dawn;

namespace Logic.Guards
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiredReturnAttribute : Attribute, IMethodAsyncAdvice
    {
        public string ErrorMessage { get; set; }
        public Type   Type         { get; }

        public RequiredReturnAttribute(Type type)
        {
            Type = type;
        }

        public async Task Advise(MethodAsyncAdviceContext context)
        {
            // TODO: Check for null in method return value
            await context.ProceedAsync();
            object returnValue = GetReturnValue(context.ReturnValue);
            Guard.Argument(returnValue).NotNull(ErrorMessage);
        }

        public object GetReturnValue(object asyncReturn)
        {
            return asyncReturn is Task
                ? ((dynamic)asyncReturn).Result
                : Convert.ChangeType(asyncReturn, Type, CultureInfo.InvariantCulture);
        }
    }
}
