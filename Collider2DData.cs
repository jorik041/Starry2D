using UnityEngine;

namespace Starry2D
{
    public class Collider2DData : ScriptableObject
    {
        /// <summary>
        ///     How much the velocity is reversed upon collision to create a "bounce".
        /// </summary>
        public float BounceFactor;
    
        /// <summary>
        ///     Selects which objects will be returned from the collision checks.
        /// </summary>
        public LayerMask CollisionMask;
        
        /// <summary>
        ///     Only return objects with a Z position above this value.
        /// </summary>
        public float CollisionMinDepth = float.MinValue;
    
        /// <summary>
        ///     Only return objects with a Z position below this value.
        /// </summary>
        public float CollisionMaxDepth = float.MaxValue;
    
        /// <summary>
        ///     The max number of steps the continuous collision algorithm is allowed to make.
        ///     If this number is reached, the algorithm will exit early for performance.
        /// </summary>
        public int CollisionMaxSteps = 3;
    
        /// <summary>
        ///     When CollisionMaxSteps is reached, SafeResolution stops moving the rigidbody to avoid undetected collisions,
        ///     at the cost of ignoring some motion. If false, the body will make one final motion with disregard to collision.
        /// </summary>
        public bool CollisionSafeResolution = true;
    
        /// <summary>
        ///     Casts the 2D physics shape and returns the first impact.
        ///     This will always return a RaycastHit2D but if collider == null, this means that there was no hit.
        /// </summary>
        public virtual RaycastHit2D Cast(Vector2 position, Vector2 scale, float rotation, Vector2 motion)
        {
            throw new System.NotImplementedException(
                "Collider2DData.Cast() was called... please use a subclass that overrides this method!");
        }
    }
}