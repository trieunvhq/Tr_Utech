 
using System;
namespace QRMS.interfaces
{
    public interface IBiometricPieAuthenticate
    {
        void RegisterOrAuthenticate();

        bool CheckSDKGreater29();
    }
}
