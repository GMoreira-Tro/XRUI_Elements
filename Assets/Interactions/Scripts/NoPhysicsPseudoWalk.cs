using InputManager;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Class that permits a locomotion system with one of the joysticks thumbstick through changing xr rig transform.position.
/// </summary>
public class NoPhysicsPseudoWalk : WalkSystemBase
{
    /// <summary>
    /// Axis handle 2d equivalent to both hands.
    /// </summary>
    public AxisHandler2D AxixHandle2D = null;

    /// <summary>
    /// Default MonoBehavior Start().
    /// </summary>
    private void Start()
    {
        AxixHandle2D.OnValueChange += AxisValuesChange;
    }
    /// <summary>
    /// The event called when values of the 2d axis stick has changed.
    /// </summary>
    /// <param name="controller">The controller that triggers the event.</param>
    protected void AxisValuesChange(XRController controller, Vector2 value)
    {
        if (this.isActiveAndEnabled && (controller.controllerNode == UnityEngine.XR.XRNode.LeftHand && useLeftThumbstick || !useLeftThumbstick && controller.controllerNode == UnityEngine.XR.XRNode.RightHand))
        {
            transform.position += CalculateVelocityMultiplicationFactor() * -value.y;
            transform.eulerAngles += CalculateMaxAngleToMoveInFrame() * value.x;
        }
    }
}
