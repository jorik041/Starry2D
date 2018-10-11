using UnityEngine;

namespace Starry2D
{
    [CreateAssetMenu(fileName = "CapsuleCollider2DData", menuName = "Starry2D/CapsuleCollider", order = 2)]
    public sealed class CapsuleCollider2DData : Collider2DData
    {
        /// <summary>
        ///     The positional offset of the capsule's origin from the transform's position.
        /// </summary>
        public Vector2 PositionOffset;
    
        /// <summary>
        ///     The width and height (respectively) of the capsule collider.
        /// </summary>
        public Vector2 Size;
    
        /// <summary>
        ///     The direction that the capsule is built in.
        /// </summary>
        public CapsuleDirection2D Direction;
        
        /// <summary>
        ///     The angular offset of the capsule's origin from the transform's rotation.
        /// </summary>
        public float AngleOffset;
        
        public override RaycastHit2D Cast(Vector2 position, Vector2 scale, float rotation, Vector2 motion)
        {
            return Physics2D.CapsuleCast(position + PositionOffset,
                Size * scale,
                Direction,
                rotation + AngleOffset,
                motion.normalized,
                motion.magnitude,
                CollisionMask.value,
                CollisionMinDepth,
                CollisionMaxDepth);
        }
    }
}