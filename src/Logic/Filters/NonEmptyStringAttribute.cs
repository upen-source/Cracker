using System;
using ArxOne.MrAdvice.Advice;
using Dawn;

namespace Logic.Filters
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class NonEmptyStringAttribute : Attribute, IParameterAdvice
    {
        public string ErrorMessage { get; }

        public NonEmptyStringAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public void Advise(ParameterAdviceContext context)
        {
            var value = (string)context.Value;
            Guard.Argument(value, context.TargetName)
                .NotNull(ErrorMessage)
                .NotEmpty(_ => ErrorMessage);
            context.Proceed();
        }
    }
}
