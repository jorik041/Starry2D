# Starry2D
Simple(r) 2D physics for Unity.

This is a "virtual rigidbody/collider" for Unity2D projects that require finer control over their motion without resorting to "pass-through" bugs or complex math methods for conversion to velocity/forces with a dynamic continuous rigidbody. It runs all its calculations in FixedUpdate, and has no need for Collider2D or Rigidbody. It will work with or without a rigidbody, depending on your needs.
## Features
- Translate and change velocity under continuous physics- no passthrough bugs!
- OnCollision2D allows subclasses of PhysicsBody2D to interact with other colliders.
- Choose collision mask, depth range, continuous step limit, and safe resolution independent of layering/rigidbody settings.
- Supports boxes, capsules, and circles, configurable via scriptable objects.
## Why You Might Need It
- If you want to freely transform and change velocity while being free of pass-through bugs...
- If you want to define specific solid collisions without changing the global layermask...
- If you like a tidy Inspector menu...
## Quick Setup Guide
First, you'll need to take all the .cs files in the master branch and paste them somewhere into your Unity2D project. Next, inside the Unity Editor window, you'll need to navigate to Edit > Project Settings > Physics2D. In the inspector, disable "Auto Sync Transforms". Starry2D is now set up, and you just need to create collider data and attach PhysicsBody2D to a gameObject.
## Collider Data
Collider Data is stored in scriptable objects, and a reference to one of these is required for the PhysicsBody to work. Create the Collider Data through the right click menu in the Unity Project window. Navigate to Create > Starry2D and then choose the collider of the shape you need. 
#### Bounce Factor
Normally, when a collision happens, the velocity is equalled out on the axis of the hit's normal. Increasing this value increases the amount of "bounce" an object has. 0 means no bounce, and 1 means that the object's full velocity will be reflected back. **Please do not use negative values for this.**
#### CollisionMin/MaxDepth
Depth refers to the Z axis, which is usually unused in Unity2D. For Starry2D, you can use it as an extra way to isolate collisions by selecting the range of Z values this collider will accept.
#### CollisionMaxSteps
In order to provide truly continuous collision, physics are run in simple linear steps, and collisions are processed between these steps. In most cases, this will never exceed 1-2 steps. There are some corner cases where a high-bounce object is capable of causing many physics steps, which is unwanted for performance. The default value of 3 is recommended for most projects, and higher values may be desired for slow tick rates or exceptionally fast/bouncy colliders.
#### CollisionSafeResolution
The results from this body are safe and continuous by default- however if the simulation exits early due to CollisionMaxSteps being reached, there is a possibility of unsafe motion. Ticking this as true, the default value, will not process any motion outside of the collision steps. If false, this will ensure that all motion is seen through regardless of whether the max steps was regarded or not. Since it does not check for collision in this final step, it is "unsafe", and generally only wanted if preserving the magnitude of motion is more important than the accuracy of collisions.
## Subclassing PhysicsBody2D
The ideal use of PhysicsBody2D is some kind of character controller. Player controllers often have to be very responsive and capable of handling special exceptions in collision resolution, so this is the perfect situation for using Starry2D's PhysicsBody.
#### protected override void UpdateMotion()
Handle all motion here, using FixedTime. This is your new FixedUpdate. If you want to translate, simply modify/add to the Translation field. Don't use Transform.Translate or Rigidbody2D.MovePosition. If you want to change velocity, simply modify/add to the Velocity field. Don't use Rigidbody2D.velocity.
#### protected override void OnCollision2D(RaycastHit2D hit)
This is the "cheap replacement" of OnCollisionStay2D, but it works identically to it. Handle any hit logic here, and changing Translation/Velocity is safe here, too. Don't use OnCollisionEnter/Stay/Exit2D, it won't work as this isn't a Rigidbody. You may want to have a Kinematic Rigidbody if you want OnTriggerEnter/Stay/Exit2D.
##### Anything else?
That's about it. You're free to use Awake, Start, Update, OnTrigger, and most of the things you'd be able to access from a MonoBehavior.
## Known Issues
- *If Physics2D.AutoSyncTransforms is true, PhysicsBody2D will guzzle CPU time.* There's no way around this, just make sure you follow the Quick Setup Guide properly.
- *The circle colliders use the magnitude of transform.lossyScale's X and Y values.* This is kinda cheap but I can't figure out another way to do it.
- *When using Translation to move instead of Velocity, PhysicsBody2D cannot "slide" across surfaces.* I am not sure if I can fix this, but I will give it a good shot.
- *Setting BounceFactor above 1 in a tightly closed area will cause the object to bounce and quickly rack up velocity until it is NaN.* I can't imagine this possibly being a production issue, but if it is, I guess I could add a "MaxVelocity". It just kinda seems silly at this stage.
