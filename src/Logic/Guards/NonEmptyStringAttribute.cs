using System;
using ArxOne.MrAdvice.Advice;
using Dawn;

namespace Logic.Guards
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class NonEmptyStringAttribute : Attribute, IParameterAdvice
    {
        public void Advise(ParameterAdviceContext context)
        {
            var value = (string)context.Value;
            Guard.Argument(value, context.TargetName).NotNull().NotEmpty();
            context.Proceed();
        }
    }
}
