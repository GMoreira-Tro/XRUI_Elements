using InputManager;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable;

/// <summary>
/// Class that permits a bunch of interactions with a joystick in an object.
/// </summary>
[RequireComponent(typeof(XRGrabInteractable))]
public class MoveWithJoystick : MonoBehaviour
{
    /// <summary>
    /// The player's GameObject.
    /// </summary>
    public GameObject player;
    /// <summary>
    /// Material to set the object on hover enter it.
    /// </summary>
    public Material onHoverEnterMaterial;
    /// <summary>
    /// Material to set the object on select enter it.
    /// </summary>
    public Material onSelectEnterMaterial;

    /// <summary>
    /// Will you use the translation interaction?
    /// </summary>
    public bool useTranslationInteraction;
    /// <summary>
    /// Will you use the rotation interaction?
    /// </summary>
    public bool useRotationInteraction;
    /// <summary>
    /// Will you use the scale interaction?
    /// </summary>
    public bool useScaleInteraction;

    /// <summary>
    /// Speed the object uses to get towards the controller.
    /// </summary>
    [ShowIf("useTranslationInteraction")]
    public float speedTowards = -4f;

    /// <summary>
    /// Speed of rotation.
    /// </summary>
    [ShowIf("useRotationInteraction")]
    public float rotationSpeed = 10f;
    /// <summary>
    /// The GameObject to be shown when the object is on rotation interaction;
    /// </summary>
    [ShowIf("useRotationInteraction")]
    public GameObject rotationMarkerGameObject;

    /// <summary>
    /// Speed to scale.
    /// </summary>
    [ShowIf("useScaleInteraction")]
    public float scalingSpeed = 10f;
    /// <summary>
    /// The minimum scale magnitude of the object.
    /// </summary>
    [ShowIf("useScaleInteraction")]
    public float minScaleMagnitude = 0.5f;
    /// <summary>
    /// The maximum scale magnitude of the object.
    /// </summary>
    [ShowIf("useScaleInteraction")]
    public float maxScaleMagnitude = 5f;
    /// <summary>
    /// The GameObject to be shown when the object is on scale interaction;
    /// </summary>
    [ShowIf("useScaleInteraction")]
    public GameObject scaleMarkerGameObject;

    /// <summary>
    /// Multiplication factor of the vector direction when the object is following the player.
    /// </summary>
    public float vectorDirectionMultiplicationFactor = 1.5f;

    /// <summary>
    /// The minimum distance to attach an object in a controller.
    /// </summary>
    public float minDistanceToAttach = 0.1f;

    [Header("Input buttons")]
    [Required]
    /// <summary>
    /// Axis handle 2d equivalent to both hands.
    /// </summary>
    public AxisHandler2D axixHandle2D = null;
    /// <summary>
    /// Button to change the interaction of the object.
    /// </summary>
    public ButtonHandler changeInteractionButton = null;
    /// <summary>
    /// Button handle to control axis rotation.
    /// </summary>
    public ButtonHandler changeRotationAxisButton = null;

    /// <summary>
    /// Enum of possible interactions types.
    /// </summary>
    private enum InteractionStates
    {
        ROTATING,
        SCALING,
        ANY
    };
    /// <summary>
    /// Instance of InteractionStates enum.
    /// </summary>
    private InteractionStates states = 0;
    /// <summary>
    /// Enum of axis.
    /// </summary>
    private enum AxisToRotate
    {
        X,
        Y,
        Z
    };
    /// <summary>
    /// Instance of AxisToRotate enum.
    /// </summary>
    private AxisToRotate axisToRotate = AxisToRotate.Y;
    /// <summary>
    /// A SnapTurnProvider reference from XRRig.
    /// </summary>
    private SnapTurnProvider snap;
    /// <summary>
    /// A Walk reference from XRRig.
    /// </summary>
    private WalkSystemBase walk;

    /// <summary>
    /// The controller XRGrabInteractable reference.
    /// </summary>
    private XRGrabInteractable grabInteractable;
    /// <summary>
    /// The controller transform.
    /// </summary>
    private Transform controllerTransform;
    /// <summary>
    /// Is the controller the left hand input?
    /// </summary>
    private bool isInputLeft;
    /// <summary>
    /// The vertical axis reference.
    /// </summary>
    private float verticalAxis;
    /// <summary>
    /// The horizontal axis reference.
    /// </summary>
    private float horizontalAxis;
    /// <summary>
    /// Is the controller interactor a ray interactor?
    /// </summary>
    private bool isRayInteractor;
    /// <summary>
    /// MovementType of the controller.
    /// </summary>
    private MovementType movementType;
    /// <summary>
    /// Line length of a ray interactor.
    /// </summary>
    private float lineLength;
    /// <summary>
    /// The object has started with track position?
    /// </summary>
    private bool startsTrackedPosition;
    /// <summary>
    /// The starter scale of the object.
    /// </summary>
    private Vector3 starterScale;
    /// <summary>
    /// The position of the controller at the start of frame.
    /// </summary>
    private Vector3 controllerPositionOnStartOfFrame;    
    /// <summary>
    /// The local position of the controller at the start of frame.
    /// </summary>
    private Vector3 controllerLocalPositionOnStartOfFrame;    
    /// <summary>
    /// The y euler angle of the controller at the start of frame.
    /// </summary>
    private float controllerYEulerAngleOnStartOfFrame;
    /// <summary>
    /// The position of the object when it is selected.
    /// </summary>
    private Vector3 transformPositionOnSelect;
    /// <summary>
    /// The proper object's meshRenderer.
    /// </summary>
    private MeshRenderer meshRenderer;
    /// <summary>
    /// The original material of the object.
    /// </summary>
    private Material originalMaterial;
    /// <summary>
    /// Is the position of the object being interpolated?
    /// </summary>
    private bool isInterpolatingPosition;

    /// <summary>
    /// Default MonoBehavior Start().
    /// </summary>
    private void Start()
    {
        walk = player.GetComponent<WalkSystemBase>();
        snap = player.GetComponent<SnapTurnProvider>();

        if (useRotationInteraction)
        {
            states = InteractionStates.ROTATING;
        }
        else if (useScaleInteraction)
        {
            states = InteractionStates.SCALING;
        }
        else
        {
            states = InteractionStates.ANY;
        }

        grabInteractable = GetComponent<XRGrabInteractable>();

        startsTrackedPosition = grabInteractable.trackPosition;
        starterScale = gameObject.transform.localScale;

        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        grabInteractable.onHoverEnter.AddListener(SetOnHoverEnterMaterial);
        grabInteractable.onHoverExit.AddListener((interactor) =>
        {
            if (grabInteractable.isSelected)
            {
                return;
            }
            SetToOriginalMaterial(interactor);
        });

        grabInteractable.onSelectEnter.AddListener(OnSelectEnterConfiguration);
        grabInteractable.onSelectExit.AddListener(OnSelectExitConfiguration);

        if (changeRotationAxisButton)
        {
            changeRotationAxisButton.OnButtonUp += ChangeRotationAxisButtonUp;
        }
        if (changeInteractionButton)
        {
            changeInteractionButton.OnButtonUp += ChangeInteractionButtonUp;
        }
        axixHandle2D.OnValueChange += AxisValuesChange;
    }

    /// <summary>
    /// Method to set the material of the object in to the onHoverEnterMaterial.
    /// </summary>
    /// <param name="interactor">The interactor that hovers the material.</param>
    private void SetOnHoverEnterMaterial(XRBaseInteractor interactor)
    {
        meshRenderer.material = onHoverEnterMaterial;
    }
    /// <summary>
    /// Set the object to its original material.
    /// </summary>
    /// <param name="interactor">The interactor that hovers the material.</param>
    private void SetToOriginalMaterial(XRBaseInteractor interactor)
    {
        meshRenderer.material = originalMaterial;
    }

    /// <summary>
    /// Main configuration of OnSelectEnter of the grabInteractable.
    /// </summary>
    /// <param name="interactor">The interactor that selects the interactable.</param>
    private void OnSelectEnterConfiguration(XRBaseInteractor interactor)
    {
        if(states == InteractionStates.ROTATING)
        {
            rotationMarkerGameObject.SetActive(true);
        }
        else if(states == InteractionStates.SCALING)
        {
            scaleMarkerGameObject.SetActive(true);
        }
        meshRenderer.material = onSelectEnterMaterial;

        controllerTransform = interactor.transform;
        transformPositionOnSelect = transform.position;

        isInputLeft = interactor.GetComponent<XRController>().controllerNode == UnityEngine.XR.XRNode.LeftHand;

        isRayInteractor = interactor as XRRayInteractor;

        if (isRayInteractor)
        {
            if (!startsTrackedPosition)
            {
                SetInteractions(false);
            }

            var ray = interactor.GetComponent<XRRayInteractor>();
            lineLength = ray.maxRaycastDistance;

            ray.maxRaycastDistance = 0;
        }
        else
        {
            movementType = grabInteractable.movementType;
            grabInteractable.movementType = MovementType.Kinematic;
        }
    }
    /// <summary>
    /// Main configuration of OnSelectExit of the grabInteractable.
    /// </summary>
    /// <param name="interactor">The interactor that selects the interactable.</param>
    private void OnSelectExitConfiguration(XRBaseInteractor interactor)
    {
        if (states == InteractionStates.ROTATING)
        {
            rotationMarkerGameObject.SetActive(false);
        }
        else if (states == InteractionStates.SCALING)
        {
            scaleMarkerGameObject.SetActive(false);
        }
        meshRenderer.material = originalMaterial;

        grabInteractable.trackPosition = startsTrackedPosition;

        if (isRayInteractor)
        {
            interactor.GetComponent<XRRayInteractor>().maxRaycastDistance = lineLength;
        }
        else
        {
            grabInteractable.movementType = movementType;
        }

        SetInteractions(true);
    }

    /// <summary>
    /// Default MonoBehavior FixedUpdate().
    /// </summary>
    private void FixedUpdate()
    {
        if (!isInterpolatingPosition)
        {
            if (controllerTransform != null)
            {
                controllerPositionOnStartOfFrame = controllerTransform.position;
                controllerLocalPositionOnStartOfFrame = controllerTransform.localPosition;
                controllerYEulerAngleOnStartOfFrame = player.transform.eulerAngles.y;
            }

            if (grabInteractable.isSelected && !grabInteractable.trackPosition)
            {
                StartCoroutine(InterpolatePosition());
            }
        }
    }
    /// <summary>
    ///  changeRotationAxisButton handler Button up event. 
    /// </summary>
    /// <param name="controller">The controller that triggers the event.</param>
    private void ChangeRotationAxisButtonUp(XRController controller)
    {
        if (!grabInteractable.isSelected || states != InteractionStates.ROTATING
            || grabInteractable.selectingInteractor != 
            controller.GetComponent<XRBaseInteractor>())
        {
            return;
        }

        if (axisToRotate == AxisToRotate.Z)
        {
            axisToRotate = AxisToRotate.X;
            rotationMarkerGameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        else if (axisToRotate == AxisToRotate.Y)
        {
            axisToRotate = AxisToRotate.Z;
            rotationMarkerGameObject.transform.localEulerAngles = new Vector3(0,0,-45);
        }
        else
        {
            axisToRotate = AxisToRotate.Y;
            rotationMarkerGameObject.transform.localEulerAngles = Vector3.zero;
        }
    }
    /// <summary>
    /// Change interaction button up event.
    /// </summary>
    /// <param name="controller">The controller that triggers the event.</param>
    private void ChangeInteractionButtonUp(XRController controller)
    {
        if (!grabInteractable.isSelected || grabInteractable.selectingInteractor !=
            controller.GetComponent<XRBaseInteractor>())
        {
            return;
        }

        switch (states)
        {
            case InteractionStates.ROTATING:
                if (useScaleInteraction)
                {
                    states = InteractionStates.SCALING;
                    scaleMarkerGameObject.SetActive(true);
                    rotationMarkerGameObject.SetActive(false);
                }
                break;
            case InteractionStates.SCALING:
                RecheckScale();
                if (useRotationInteraction)
                {
                    states = InteractionStates.ROTATING;
                    scaleMarkerGameObject.SetActive(false);
                    rotationMarkerGameObject.SetActive(true);
                }
                break;
        }
    }
    /// <summary>
    /// The event called when values of the 2d axis stick have changed.
    /// </summary>
    /// <param name="controller">The controller that triggers the event.</param>
    private void AxisValuesChange(XRController controller, Vector2 value)
    {
        if (!grabInteractable.isSelected || grabInteractable.trackPosition)
        {
            return;
        }

        if (controller.controllerNode == UnityEngine.XR.XRNode.LeftHand && isInputLeft || !isInputLeft && controller.controllerNode == UnityEngine.XR.XRNode.RightHand)
        {
            verticalAxis = value.y;
            horizontalAxis = value.x;
        }

        if (useTranslationInteraction)
        {
            if (Mathf.Abs(verticalAxis) > 0.7f)
            {
                TransladingInteraction();
            }
        }

        if (states == InteractionStates.ROTATING)
        {
            if (Mathf.Abs(horizontalAxis) > 0.7f)
            {
                RotatingInteraction();
            }
        }
        else if (states == InteractionStates.SCALING)
        {
            if (Mathf.Abs(horizontalAxis) > 0.7f)
            {
                ScaleInteraction();
            }
        }
        AdjustMovements();
    }

    /// <summary>
    /// Set grabInteractable.trackPosition then call SetInteraction(bool state) with a proper parameter.
    /// </summary>
    private void AdjustMovements()
    {
        if (!startsTrackedPosition && !grabInteractable.trackPosition)
        {
            grabInteractable.trackPosition = (Vector3.Distance(gameObject.transform.position, controllerTransform.position) <= minDistanceToAttach)
                || !isRayInteractor;
        }

        SetInteractions(grabInteractable.trackPosition && states == InteractionStates.ROTATING);
    }

    /// <summary>
    /// Moving the object based on vertical and horizontal axis values. 
    /// </summary>
    private void TransladingInteraction()
    {
        transform.position = Vector3.MoveTowards(transform.position, controllerTransform.position, Time.deltaTime * speedTowards *
                verticalAxis);
    }
    /// <summary>
    /// Set transform's euler angles.
    /// </summary>
    private void RotatingInteraction()
    {
        switch (axisToRotate)
        {
            case AxisToRotate.X:
                transform.RotateAround(transform.localPosition, transform.right, 
                    rotationSpeed * horizontalAxis * Time.deltaTime);
                break;
            case AxisToRotate.Y:
                transform.RotateAround(transform.localPosition, transform.up,
                    rotationSpeed * horizontalAxis * Time.deltaTime);
                break;
            case AxisToRotate.Z:
                transform.RotateAround(transform.localPosition, transform.forward,
                    rotationSpeed * horizontalAxis * Time.deltaTime);
                break;
        }
    }
    /// <summary>
    /// Scale the object with the joystick.
    /// </summary>
    private void ScaleInteraction()
    {
        if (horizontalAxis < 0)
        {
            if (transform.localScale.magnitude > minScaleMagnitude && transform.localScale.x > 0 &&
                transform.localScale.y > 0 &&
                transform.localScale.z > 0)
            {

                transform.localScale += Vector3.one * horizontalAxis * Time.deltaTime * scalingSpeed;
            }
            else
            {
                transform.localScale = starterScale * (minScaleMagnitude / starterScale.magnitude);
            }
        }
        else
        {
            if (transform.localScale.magnitude < maxScaleMagnitude)
            {
                transform.localScale += Vector3.one * horizontalAxis * Time.deltaTime * scalingSpeed;
            }
            else
            {
                transform.localScale = starterScale * (maxScaleMagnitude / starterScale.magnitude);
            }
        }
    }

    /// <summary>
    /// Method to recheck the scale of the object after leave scale interaction.
    /// </summary>
    private void RecheckScale()
    {
        if (transform.localScale.magnitude < minScaleMagnitude || transform.localScale.x < 0 ||
                transform.localScale.y < 0 ||
                transform.localScale.z < 0)
        {
            transform.localScale = starterScale * (minScaleMagnitude / starterScale.magnitude);
        }
        else if (transform.localScale.magnitude > maxScaleMagnitude)
        {
            transform.localScale = starterScale * (maxScaleMagnitude / starterScale.magnitude);
        }
    }

    /// <summary>
    /// Enable/Disable WalkSystemBase or (exclusive) SnapTurnProvider component.
    /// </summary>
    /// <param name="state">Do you want to enable(true) or disable(false) a component?</param>
    private void SetInteractions(bool state)
    {
        if (isInputLeft)
        {
            if (walk)
            {
                if (walk.useLeftThumbstick)
                {
                    walk.enabled = state;
                }
                else if (snap)
                {
                    snap.enabled = state;
                }
            }
            else if (snap)
            {
                snap.enabled = state;
            }
        }
        else
        {
            if (walk)
            {
                if (walk.useLeftThumbstick && snap != null)
                {
                    snap.enabled = state;
                }
                else
                {
                    walk.enabled = state;
                }
            }
            else if (snap)
            {
                snap.enabled = state;
            }
        }
    }

    /// <summary>
    /// Make the object mirror the controller movements.
    /// </summary>
    private IEnumerator InterpolatePosition()
    {
        isInterpolatingPosition = true;
        yield return new WaitForEndOfFrame();

        //Calculating vector direction
        Vector3 vectorDirection = controllerTransform.localPosition - 
            controllerLocalPositionOnStartOfFrame;
        transform.position += vectorDirection * vectorDirectionMultiplicationFactor;

        vectorDirection = controllerTransform.position - 
            controllerPositionOnStartOfFrame;
        transform.position += vectorDirection;

        float yDirection = player.transform.eulerAngles.y -
            controllerYEulerAngleOnStartOfFrame;
        transform.RotateAround(controllerTransform.position,
            transform.up, yDirection);

        isInterpolatingPosition = false;
    }
}
