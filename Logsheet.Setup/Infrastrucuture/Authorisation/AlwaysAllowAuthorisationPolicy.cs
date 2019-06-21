using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;

namespace Logsheet.Setup.Infrastrucuture.Authorisation
{
    public class AlwaysAllowAuthorisationPolicy : IAuthorisationPolicy
    {
        public bool HasAccess(IAuthorisationContext authorisationContext)
        {
            return true;
        }
    }
}
