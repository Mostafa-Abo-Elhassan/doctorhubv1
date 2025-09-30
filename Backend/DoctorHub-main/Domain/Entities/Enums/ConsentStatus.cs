using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Enums
{
    public enum ConsentStatus
    {
        Active,     // الإذن شغال
        Revoked,    // الإذن اتسحب
        Expired     // الإذن انتهى (لو ليه مدة زمنية محددة)
    }
}
