using System;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Pugster
{
    public class RangeAttribute : ParameterPreconditionAttribute
    {
        public int? MinValue { get; }
        public int? MaxValue { get; }
        public bool AllowNull { get; set; } = false;

        public RangeAttribute(int min, int max)
        {
            MinValue = min;
            MaxValue = max;
        }

        public override Task<PreconditionResult> CheckPermissions(ICommandContext context, ParameterInfo parameter, object value, IServiceProvider services)
        {
            if (value == null && AllowNull) return Task.FromResult(PreconditionResult.FromSuccess());

            int number = int.Parse(value.ToString());
            if (number < MinValue || number > MaxValue)
            {
                var builder = new StringBuilder($"{parameter.Name} must be ");
                if (MinValue != null)
                    builder.Append($"greater than {MinValue}");
                if (MaxValue != null)
                {
                    if (MinValue != null)
                        builder.Append(" and ");
                    builder.Append($"less than {MaxValue}");
                }

                return Task.FromResult(PreconditionResult.FromError(builder.ToString()));
            }
            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
