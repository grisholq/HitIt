using UnityEngine;
using System.Collections;
using HitIt.Other;
using System;

namespace HitIt.Ecs
{
    public class Delayer
    {
        public void Delay(Action first , Action second, float seconds)
        {
            GlobalMono.Instance.StartCoroutine(DelaySecs(first, second, seconds));
        }

        private IEnumerator DelaySecs(Action first, Action second, float seconds)
        {
            first();
            yield return new WaitForSeconds(seconds);
            second();
        }
    }
}