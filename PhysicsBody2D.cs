using UnityEngine;

// Read me before using!
// This physics body is recommended for a controlled motion simulation where translations/velocity changes
// are done through this script or an override of it. It requires Physics2D.AutoSyncTransforms to be set
// to false, or else it will sync many more times than necessary, eating up performance!
// This script replaces the need for SOLID COLLISION RESPONSE. You may still wish to use a KINEMATIC rigidbody
// for the purpose of detecting trigger collisions. In that case, you will want to use a trigger collider, and
// a kinematic rigidbody with this script.

namespace Starry2D
{
    public class PhysicsBody2D : MonoBehaviour
    {
        private const float ShellRadius = 0.005f;
        
        /// <summary>
        ///     The data necessary to build the virtual collider for this object.
        /// </summary>
        [SerializeField]
        protected Collider2DData ColliderData;
    
        /// <summary>
        ///     Use this if you need more fine control over the motion of an object but still need safe physics.
        ///     This is applied to the next fixedUpdate and is reset to Vector2.zero afterwards.
        /// </summary>
        protected Vector2 Translation;
        
        /// <summary>
        ///     Physics-safe velocity. Modify as you please.
        /// </summary>
        protected Vector2 Velocity;
    
        /// <summary>
        ///     Override this in a subclass. Modify Translation and Velocity as you please.
        /// </summary>
        protected virtual void UpdateMotion() { }
        
        /// <summary>
        ///     Override this in a subclass. You are free to change Translation and Velocity in this method if you
        ///     require it. Be aware that this method does not "remember" what colliders it last hit, so write your logic
        ///     accordingly.
        /// </summary>
        protected virtual void OnCollision2D(RaycastHit2D hit) { }
        
        private void FixedUpdate()
        {
            UpdateMotion();
            RunPhysics();
        }
        
        private void RunPhysics()
        {
            var deltaTime = Time.fixedDeltaTime;
            var iterations = 0;
            while (iterations < ColliderData.CollisionMaxSteps)
            {
                iterations++;
                
                // Nothing to do here...
                if (Velocity == Vector2.zero && Translation == Vector2.zero)
                {
                    deltaTime = 0;
                    break;
                }
            
                var initialMove = (Velocity * deltaTime) + Translation;
                Translation = Vector2.zero;
                var hit = ColliderData.Cast(transform.position, transform.lossyScale,
                    transform.rotation.eulerAngles.z, initialMove);
    
                // No hit detected, so move the full way and exit the loop.
                if (hit.collider == null)
                {
                    transform.position = new Vector3(transform.position.x + initialMove.x, 
                                                     transform.position.y + initialMove.y,
                                                     transform.position.z);
                    deltaTime = 0;
                    break;
                }
            
                // Hit detected here, correct the velocity if necessary.
                var projection = Vector2.Dot(Velocity, hit.normal);
                if (projection < 0)
                    Velocity -= projection * hit.normal * (1 + ColliderData.BounceFactor);
                
                // Move the transform to the position of the hit, with some padding to prevent re-collision.
                transform.position = hit.centroid + (hit.normal * ShellRadius);
    
                // Make sure we track how much time has passed to make this step.
                deltaTime -= deltaTime * hit.fraction;
                
                // Lastly, call OnCollision2D to allow for other scripts to override on-hit behavior.
                OnCollision2D(hit);
            }
    
            if (!ColliderData.CollisionSafeResolution)
            {
                var move = Velocity * deltaTime;
                transform.position = new Vector3(transform.position.x + move.x, 
                                                 transform.position.y + move.y,
                                                 transform.position.z);
            }
        }
    }
}