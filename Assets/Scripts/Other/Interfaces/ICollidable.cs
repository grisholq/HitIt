namespace HitIt.Other
{
    public interface ICollidable
    {
        bool IsTrigger { get; set; }
        void SetColliderActivity(bool state);
    }
}