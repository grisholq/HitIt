using HitIt.Storage;
using HitIt.Other;
using UnityEngine;

namespace HitIt.Ecs
{
    public class KnifePositioner : IInizializable
    {
        private KnifesSettings settings;
        private KnifesMono knifes;

        public void Inizialize()
        {
            knifes = StorageFacility.Instance.GetTransform(TransformObject.KnifesMono).GetComponent<KnifesMono>();
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();            
        }   
        
        public void SetKnifePosition(Transform knife, KnifePositions position)
        {
            switch(position)
            {
                case KnifePositions.Active:
                    knife.position = knifes.ActiveKnifePosition.position;
                    break;

                case KnifePositions.Secondary:
                    knife.position = knifes.SecondaryKnifePosition.position;
                    break;
            }
        }
    }
}