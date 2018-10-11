using UnityEngine;

namespace Starry2D
{
    [CreateAssetMenu(fileName = "CircleCollider2DData", menuName = "Starry2D/CircleCollider", order = 3)]
    public sealed class CircleCollider2DData : Collider2DData
    {
        /// <summary>
        ///     The positional offset of the circle's origin from the transform's position.
        /// </summary>
        public Vector2 PositionOffset;

        /// <summary>
        ///     The radius of the circle collider. This scales alongside the MAGNITUDE of the parent transform's X/Y scale.
        /// </summary>
        public float Radius;
    
        public override RaycastHit2D Cast(Vector2 position, Vector2 scale, float rotation, Vector2 motion)
        {
            return Physics2D.CircleCast(position + PositionOffset,
                Radius * scale.magnitude,
                motion.normalized,
                motion.magnitude,
                CollisionMask.value,
                CollisionMinDepth,
                CollisionMaxDepth);
        }
    }
}