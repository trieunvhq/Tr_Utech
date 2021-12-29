using System;
using System.Threading.Tasks;

namespace QRMS.interfaces
{
    public interface IBiometricAuthenticateService
    {
        String GetAuthenticationType();
        Task<bool> AuthenticateUserIDWithTouchID();
        bool fingerprintEnabled();
    }
}
