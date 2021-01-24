using HitIt.Other;

namespace HitIt.Ecs
{ 
    public class LogObjectsSetter
    {
        public void Activate(ILogObject logObject)
        {
            logObject.Rigidbody.isKinematic = false;
            logObject.SetColliderActivity(true);
        }

        public void Stop(ILogObject logObject, bool collidable)
        {
            logObject.Rigidbody.isKinematic = true;
            logObject.SetColliderActivity(collidable);
        }

        public void Deactivate(ILogObject logObject)
        {
            logObject.Rigidbody.isKinematic = false;
            logObject.SetColliderActivity(false);
        }
    }
}