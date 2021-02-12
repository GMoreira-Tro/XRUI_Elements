using InputManager;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Class that permits a locomotion system with one of the joysticks thumbstick through xr rig rigidbody's speed.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class XRRigRigidbodyWalk : NoPhysicsPseudoWalk
{
    /// <summary>
    /// The xrRig rigidbody.
    /// </summary>
    private new Rigidbody rigidbody;
    
    /// <summary>
    /// Get the rigidbody's velocity.
    /// </summary>
    /// <returns>The rigidbody's velocity.</returns>
    public Vector3 RigidbodyVelocity()
    {
        return rigidbody.velocity;
    }
    /// <summary>
    /// Default MonoBehavior Start().
    /// </summary>
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        AxixHandle2D.OnValueChange += AxisValuesChange;
    }
    /// <summary>
    /// Set rigidbody's velocity to (0,0,0) when the Object is disable.
    /// </summary>
    private void OnDisable()
    {
        rigidbody.velocity = new Vector3(0, 0, 0);
    }
}
