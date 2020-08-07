using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using System.Diagnostics.Tracing;

namespace Craftwork.Feedstock.Api.Domain.Core.Enum
{
    public class StatusEnum
    {
        public static Status Enable => new Status("Active", true);

        public static Status Disable => new Status("Disable", false);

        public static Status Convert(bool status)
        {
            return status ? Enable : Disable;
        }
    }
}
