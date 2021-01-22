using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeForcer : IInizializable
    {
        private KnifesMono knifesMono;

        public void Inizialize()
        {
            knifesMono = StorageFacility.Instance.GetTransform(TransformObject.KnifesMono).GetComponent<KnifesMono>();
        }

        public void ForceKnife(KnifeMono knife, Vector3 force)
        {
            knife.ApplyForce(force);
        }

      /*  public void TorqueKnife(KnifeMono knife, Vector3 torque)
        {
            knife.ApplyTorque(force);
        }*/
    }
}