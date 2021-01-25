using UnityEngine;
using System.Collections;
using HitIt.Other;
using System;

namespace HitIt.Ecs
{
    public class Delayer
    {
        public void Delay(Action action, float seconds)
        {
            GlobalMono.Instance.StartCoroutine(DelaySecs(action, seconds));
        }

        private IEnumerator DelaySecs(Action action, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            action();
        }
    }
}