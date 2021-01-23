using UnityEngine;

namespace HitIt.Ecs
{
    public interface IAttachable
    {
        Transform Transform { get; set; }
    }
}