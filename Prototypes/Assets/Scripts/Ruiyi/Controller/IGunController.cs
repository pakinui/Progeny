using Framework;

namespace Controller
{
    public interface IGunController : IController
    {
        void Fire();
        bool CanFire();
    }
}