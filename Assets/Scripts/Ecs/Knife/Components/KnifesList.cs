using UnityEngine;
using System.Collections.Generic;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifesList: IInizializable
    {
        LinkedList<KnifeMono> knifes;

        public void Inizialize()
        {
            knifes = new LinkedList<KnifeMono>();
        }

        public void AddKnife(KnifeMono knife)
        {
            knifes.AddLast(knife);
        }

        public void DestroyAll()
        {
            foreach (var knife in knifes)
            {
                if(knife != null)
                Object.Destroy(knife.gameObject);
            }

            knifes.Clear();
        }
    }
}