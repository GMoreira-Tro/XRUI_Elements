using UnityEngine;

/// <summary>
/// Base class for Walk Systems implementations.
/// </summary>
public abstract class WalkSystemBase : MonoBehaviour
{
    /// <summary>
    /// Speed for translation movement
    /// </summary>
    public short translationSpeed = 250;
    /// <summary>
    /// Speed for rotation movement
    /// </summary>
    public short rotationSpeed = 50;
    /// <summary>
    /// The transform of the xrRig camera
    /// </summary>
    public Transform cameraTransform;
    /// <summary>
    /// Do you wanna use left thumbstick? If it is false, use right thumbstick
    /// </summary>
    public bool useLeftThumbstick;

    /// <summary>
    /// The vector direction pointed according to 'y' euler angle of the camera transform.
    /// </summary>
    /// <returns></returns>
    protected virtual Vector3 DirectionVector()
    {
        return new Vector3(Mathf.Sin(cameraTransform.eulerAngles.y * Mathf.Deg2Rad), 0,
              Mathf.Cos(cameraTransform.eulerAngles.y * Mathf.Deg2Rad));
    }

    /// <summary>
    /// Calculates the factor to multiplicate with rigidbody's velocity.
    /// </summary>
    /// <returns>The factor calculated.</returns>
    protected virtual Vector3 CalculateVelocityMultiplicationFactor()
    {
        return DirectionVector() * Time.deltaTime * translationSpeed;
    }
    /// <summary>
    /// Calculates the max angle that a single frame cans rotate.
    /// </summary>
    /// <returns>The angle calculated.</returns>
    protected virtual Vector3 CalculateMaxAngleToMoveInFrame()
    {
        return Vector3.up * Time.deltaTime * rotationSpeed;
    }
}
