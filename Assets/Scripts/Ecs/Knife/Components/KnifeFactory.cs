﻿using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeFactory : IInizializable
    {
        private KnifesSettings settings;
        private KnifesMono knifes;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();
            knifes = StorageFacility.Instance.GetTransform(TransformObject.KnifesMono).GetComponent<KnifesMono>();
        }

        public KnifeMono GetKnife()
        {
            KnifeMono knife = Object.Instantiate(settings.Knife).GetComponent<KnifeMono>();
            knife.transform.eulerAngles = Vector3.zero;
            knife.transform.SetParent(knifes.KnifesParent);
            knife.Rigidbody.maxAngularVelocity = settings.MaxAngularVelocity;
            knife.SpawnTime = Time.time;
            
            knife.Stop(false);
            knife.ColliderType = KnifeColliderType.Active;
            
            return knife;
        }
    }
}