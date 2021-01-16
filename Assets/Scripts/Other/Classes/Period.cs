using UnityEngine;

namespace HitIt.Other
{
    public class Period
    {
        private float lastTime;
        private float period;

        public Period(float time , float period)
        {
            this.period = period;
            lastTime = time;
        }

        public bool Passed(float time)
        {
            return time >= (lastTime + period);
        }

        public void SetLastTime(float time)
        {
            lastTime = time;
        }     
    }
}