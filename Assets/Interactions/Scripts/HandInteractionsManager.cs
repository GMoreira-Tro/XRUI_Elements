using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UserController;

/// <summary>
/// Class that permits the swap from a XRDirectInteractor in to a XRRayInteractor of a hand.
/// </summary>
[RequireComponent(typeof(RayAndDirect))]
public class HandInteractionsManager : MonoBehaviour
{
    /// <summary>
    /// Is the object the left hand?
    /// </summary>
    public bool isLeftHand;
    /// <summary>
    /// HandLPController of the hand.
    /// </summary>
    public HandLPController handLPController;
    /// <summary>
    /// The point transform to attach the ray.
    /// </summary>
    public Transform attachRayTransform;
    /// <summary>
    /// The reticle of the line visual string.
    /// </summary>
    public GameObject lineVisualReticle;
    /// <summary>
    /// The scale factor of the line visual's reticle.
    /// </summary>
    public float lineVisualReticleScaleFactor = 0.01f;
    /// <summary>
    /// The point transform to attach direct interactor.
    /// </summary>
    public Transform attachDirectTransform;
    /// <summary>
    /// The name of controller attach.
    /// </summary>
    public string controllerAttachName;

    /// <summary>
    /// Enum of possible commands to execute in the next frame.
    /// </summary>
    private enum NextFrameCommands
    {
        NOTHING,
        ADD_RAY,
        ADD_DIRECT
    };
    /// <summary>
    /// The instance of nextFrameCommands enum.
    /// </summary>
    private NextFrameCommands nextFrameCommands;
    /// <summary>
    /// Ray and Direct scripts that keeps the SOs.
    /// </summary>
    private RayAndDirect rayAndDirect;
    /// <summary>
    /// The ray component reference.
    /// </summary>
    private XRRayInteractor ray;
    /// <summary>
    /// The line visual component reference.
    /// </summary>
    private XRInteractorLineVisual lineVisual;
    /// <summary>
    /// The direct component reference.
    /// </summary>
    private XRDirectInteractor direct;
    /// <summary>
    /// The string of the indexTouch input.
    /// </summary>
    private string indexTouchString;

    /// <summary>
    /// Default MonoBehavior Start().
    /// </summary>
    private void Start()
    {
        rayAndDirect = gameObject.GetComponent<RayAndDirect>();
        indexTouchString = isLeftHand ? "XRI_Left_IndexTouch" : "XRI_Right_IndexTouch";

        try
        {
            direct = gameObject.GetComponent<XRDirectInteractor>();
        }
        catch (System.NullReferenceException)
        {
            ray = gameObject.GetComponent<XRRayInteractor>();
            lineVisual = gameObject.GetComponent<XRInteractorLineVisual>();
        }
    }

    /// <summary>
    /// Default MonoBehavior FixedUpdate().
    /// </summary>
    private void FixedUpdate()
    {
        if (Input.GetAxis(indexTouchString) == 1)
        {
            if (ray)
            {
                if (lineVisual.reticle.activeSelf || ray.isSelectActive)
                {
                    return;
                }
                Destroy(ray);
                Destroy(lineVisual);
                nextFrameCommands = NextFrameCommands.ADD_DIRECT;
            }
        }
        else
        {
            if (direct)
            {
                if (direct.isSelectActive)
                {
                    return;
                }
                Destroy(direct);
                nextFrameCommands = NextFrameCommands.ADD_RAY;
            }
        }
    }
    /// <summary>
    /// Default MonoBehavior Update().
    /// </summary>
    private void Update()
    {
        if (ray)
        {
            if (ray.GetCurrentRaycastHit(out RaycastHit raycastHit))
            {
                const float scaleOffset = 0.015f;
                lineVisual.reticle.transform.localScale = new Vector3(
                    scaleOffset, scaleOffset, scaleOffset) + Vector3.one * raycastHit.distance
                    * lineVisualReticleScaleFactor;
            }
        }
    }
    /// <summary>
    /// Default MonoBehavior LateUpdate().
    /// </summary>
    private void LateUpdate()
    {
        if (nextFrameCommands != NextFrameCommands.NOTHING)
        {
            switch (nextFrameCommands)
            {
                case NextFrameCommands.ADD_DIRECT:
                    AddDirect();
                    break;

                case NextFrameCommands.ADD_RAY:
                    AddRay();
                    break;
            }

            nextFrameCommands = NextFrameCommands.NOTHING;
            Destroy(GameObject.Find(controllerAttachName));
        }
    }

    /// <summary>
    /// Setup to add a XRRayInteraction and a XRLineInteraction component.
    /// </summary>
    private void AddRay()
    {
        ray = gameObject.AddComponent<XRRayInteractor>();
        lineVisual = gameObject.AddComponent<XRInteractorLineVisual>();

        rayAndDirect.RayCopy(ray);
        rayAndDirect.LineVisualCopy(lineVisual);
        lineVisual.reticle = lineVisualReticle;

        gameObject.GetComponent<LineRenderer>().enabled = true;

        SetLineAttachTransform(ray.attachTransform);
    }

    /// <summary>
    /// Setup to add a XRDirectInteraction component.
    /// </summary>
    public void AddDirect()
    {
        lineVisualReticle.SetActive(false);

        direct = gameObject.AddComponent<XRDirectInteractor>();
        rayAndDirect.DirectCopy(direct);

        SetHandAttachTransform(direct.attachTransform);
    }

    /// <summary>
    /// Send to a receptor the attach ray transform members.
    /// </summary>
    /// <param name="receptor">The transform that will receive the members.</param>
    private void SetLineAttachTransform(Transform receptor)
    {
        if (attachRayTransform != null)
        {
            receptor.position = attachRayTransform.position;
            receptor.rotation = attachRayTransform.rotation;
            receptor.localScale = attachRayTransform.localScale;
        }
    }

    /// <summary>
    /// Send to a receptor the attach hand point transform members.
    /// </summary>
    /// <param name="receptor">The transform that will receive the members.</param>
    private void SetHandAttachTransform(Transform receptor)
    {
        if (attachDirectTransform != null)
        {
            receptor.position = attachDirectTransform.position;
            receptor.rotation = attachDirectTransform.rotation;
            receptor.localScale = attachDirectTransform.localScale;
        }
    }
}
