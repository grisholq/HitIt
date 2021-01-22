using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeForces : IInizializable
    {
        private KnifesSettings settings;

        private Vector3 throwForce;
        private Vector3 ricochetTorque;

        public Vector3 ThrowForce
        {
            get
            {
                return throwForce;
            }
        }

        public Vector3 RicochetForce
        {
            get
            {
                Vector3 f = new Vector3();

                f.x = Random.Range(settings.RicochetForceMin.x, settings.RicochetForceMax.x);
                f.y = Random.Range(settings.RicochetForceMin.y, settings.RicochetForceMax.y); 
                f.z = Random.Range(settings.RicochetForceMin.z, settings.RicochetForceMax.z);

                return f;
            }
        }

        public Vector3 RicochetTorque
        {
            get
            {
                return ricochetTorque;
            }
        }

        public Vector3 RandomForce
        {
            get
            {
                Vector3 f = new Vector3();

                f.x = Random.Range(settings.RandomForceMin.x, settings.RandomForceMax.x);
                f.y = Random.Range(settings.RandomForceMin.y, settings.RandomForceMax.y);
                f.z = Random.Range(settings.RandomForceMin.z, settings.RandomForceMax.z);

                return f;
            }
        }

        public Vector3 RandomTorque
        {
            get
            {
                Vector3 t = new Vector3();

                t.x = Random.Range(settings.RandomTorqueMin.x, settings.RandomTorqueMax.x);
                t.y = Random.Range(settings.RandomTorqueMin.y, settings.RandomTorqueMax.y);
                t.z = Random.Range(settings.RandomTorqueMin.z, settings.RandomTorqueMax.z);

                return t;
            }
        }

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();

            throwForce = settings.ThrowForceDirection * settings.ThrowForceMultiplier;
            ricochetTorque = settings.RicochetSpin * settings.RicochetSpinMultiplier;
        }       
    }  
}