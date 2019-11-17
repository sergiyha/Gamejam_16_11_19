using UnityEngine;

namespace FPSTestProject.Helpers.Runtime.ObjectPool
{
    public interface IPoolable
    {
        bool IsActive { get; }

        void Activate(Vector3 position, Quaternion rotation);

        void Deactivate();
    }
}
