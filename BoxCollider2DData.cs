using UnityEngine;

namespace Starry2D
{
    [CreateAssetMenu(fileName = "BoxCollider2DData", menuName = "Starry2D/BoxCollider", order = 1)]
    public sealed class BoxCollider2DData : Collider2DData
    {
        /// <summary>
        ///     The positional offset of the box's origin from the transform's position.
        /// </summary>
        public Vector2 PositionOffset;

        /// <summary>
        ///     The width and height (respectively) of the box collider.
        /// </summary>
        public Vector2 Size;
    
        /// <summary>
        ///     The angular offset of the box's origin from the transform's rotation.
        /// </summary>
        public float AngleOffset;
    
        public override RaycastHit2D Cast(Vector2 position, Vector2 scale, float rotation, Vector2 motion)
        {
            return Physics2D.BoxCast(position + PositionOffset,
                Size * scale,
                rotation + AngleOffset,
                motion.normalized,
                motion.magnitude,
                CollisionMask.value,
                CollisionMinDepth,
                CollisionMaxDepth);
        }
    }
}