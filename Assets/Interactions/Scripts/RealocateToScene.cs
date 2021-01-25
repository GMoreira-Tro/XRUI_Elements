using System.Collections;
using UnityEngine;

/// <summary>
/// Class to realocate an object out of a defined bounds back to its original position.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RealocateToScene : MonoBehaviour
{
    /// <summary>
    /// The limits field.
    /// </summary>
    public MeshRenderer meshLimits;

    /// <summary>
    /// Initial position of the object.
    /// </summary>
    private Vector3 initialPosition;
    /// <summary>
    /// The limits bounds.
    /// </summary>
    private Vector3 limitPosition;
    /// <summary>
    /// Constant time in seconds to verify object again.
    /// </summary>
    private const int waitSeconds = 2;
    /// <summary>
    /// The proper rigidbody of the object.
    /// </summary>
    private new Rigidbody rigidbody;

    /// <summary>
    /// Default MonoBehavior Start().
    /// </summary>
    private void Start()
    {
        initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();

        //Seting limit position
        limitPosition = meshLimits.bounds.extents;

        StartCoroutine(VerifyObject(waitSeconds));
    }

    /// <summary>
    /// Verify if the object is out of the bounds and realocate it if it is.
    /// </summary>
    /// <param name="waitSeconds">Time in seconds to redo the coroutine.</param>
    /// <returns>WaitForSeconds(waitSeconds) to wait to redo the coroutine.</returns>
    private IEnumerator VerifyObject(int waitSeconds)
    {
        if (SurpassLimits())
        {
            transform.position = initialPosition;
            rigidbody.velocity = Vector3.zero;
        }

        yield return new WaitForSeconds(waitSeconds);


        StartCoroutine(VerifyObject(waitSeconds));
    }

    /// <summary>
    /// Verify if the object surpassed the limits field, considering the offset in relation to the world.
    /// </summary>
    /// <returns>True if the object surpassed the limits field, false if it doesn't.</returns>
    private bool SurpassLimits()
    {
        //Always consider the offset related to the world.
        return (transform.position.x > (limitPosition.x + meshLimits.transform.position.x) ||
            transform.position.x < (-limitPosition.x + meshLimits.transform.position.x) ||
            transform.position.y > (limitPosition.y + meshLimits.transform.position.y) ||
            transform.position.y < (-limitPosition.y + meshLimits.transform.position.y) ||
            transform.position.z > (limitPosition.x + meshLimits.transform.position.z) ||
            transform.position.z < (-limitPosition.x + meshLimits.transform.position.z));
    }
}
