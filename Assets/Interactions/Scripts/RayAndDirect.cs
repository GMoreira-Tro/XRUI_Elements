using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Class to reference direct interactor components and ray interactor components.
/// </summary>
public class RayAndDirect : MonoBehaviour
{
    /// <summary>
    /// The XRInteractorLineVIsual component.
    /// </summary>
    public XRInteractorLineVisual line;
    /// <summary>
    /// The XRRayInteractor component.
    /// </summary>
    public XRRayInteractor ray;
    /// <summary>
    /// The XRDirectInteractor component.
    /// </summary>
    public XRDirectInteractor direct;

    /// <summary>
    /// Transform a XRRayInteractor component in to a shallow copy of the ray variable.
    /// </summary>
    /// <param name="rayCopy">The XRRayInteractor to be turned in to a copy.</param>
    public void RayCopy(XRRayInteractor rayCopy)
    {
        rayCopy.InteractionLayerMask = ray.InteractionLayerMask;
        //Attach transform is readonly
        //Starting Selected Interactable is readonly
        rayCopy.selectActionTrigger = ray.selectActionTrigger;
        rayCopy.hideControllerOnSelect = ray.hideControllerOnSelect;
        rayCopy.lineType = ray.lineType;
        rayCopy.maxRaycastDistance = ray.maxRaycastDistance;
        rayCopy.hitDetectionType = ray.hitDetectionType;
        rayCopy.sphereCastRadius = ray.sphereCastRadius;
        rayCopy.raycastMask = ray.raycastMask;
        rayCopy.raycastTriggerInteraction = ray.raycastTriggerInteraction;
        rayCopy.hoverToSelect = ray.hoverToSelect;
        //rayCopy.enableInteractions = ray.enableInteractions;

        //Sound Events
        rayCopy.playAudioClipOnHoverEnter = ray.playAudioClipOnHoverEnter;
        rayCopy.playAudioClipOnHoverExit = ray.playAudioClipOnHoverExit;
        rayCopy.playAudioClipOnSelectEnter = ray.playAudioClipOnSelectEnter;
        rayCopy.playAudioClipOnSelectExit = ray.playAudioClipOnSelectExit;

        //Haptic Events
        rayCopy.playHapticsOnHoverEnter = ray.playHapticsOnHoverEnter;
        rayCopy.playHapticsOnHoverExit = ray.playHapticsOnHoverExit;
        rayCopy.playHapticsOnSelectEnter = ray.playHapticsOnSelectEnter;
        rayCopy.playHapticsOnSelectExit = ray.playHapticsOnSelectExit;

        //Interactor Events
        rayCopy.onHoverEnter = ray.onHoverEnter;
        rayCopy.onHoverExit = ray.onHoverExit;
        rayCopy.onSelectEnter = ray.onSelectEnter;
        rayCopy.onSelectExit = ray.onSelectExit;
    }

    /// <summary>
    /// Transform a XRInteractorLineVisual component in to a shallow copy of the line variable.
    /// </summary>
    /// <param name="lineCopy">The XRInteractorLineVisual to be turned in to a copy.</param>
    public void LineVisualCopy(XRInteractorLineVisual lineCopy)
    {
        lineCopy.lineWidth = line.lineWidth;
        lineCopy.validColorGradient = line.validColorGradient;
        lineCopy.invalidColorGradient = line.invalidColorGradient;
        lineCopy.smoothMovement = line.smoothMovement;
        lineCopy.followTightness = line.followTightness;
        lineCopy.snapThresholdDistance = line.snapThresholdDistance;
        lineCopy.overrideInteractorLineLength = line.overrideInteractorLineLength;
        lineCopy.lineLength = line.lineLength;
        lineCopy.reticle = line.reticle;
        lineCopy.stopLineAtFirstRaycastHit = line.stopLineAtFirstRaycastHit;
    }

    /// <summary>
    /// Transform a XRDirectInteractor component in to a shallow copy of the direct variable.
    /// </summary>
    /// <param name="directCopy">The XRDirectInteractor to be turned in to a copy.</param>
    public void DirectCopy(XRDirectInteractor directCopy)
    {
        directCopy.InteractionLayerMask = direct.InteractionLayerMask;
        //Attach Transform is readonly
        //Starting Selected Interactable is readonly
        directCopy.selectActionTrigger = direct.selectActionTrigger;
        directCopy.hideControllerOnSelect = direct.hideControllerOnSelect;

        //Sound Events
        directCopy.playAudioClipOnHoverEnter = direct.playAudioClipOnHoverEnter;
        directCopy.playAudioClipOnHoverExit = direct.playAudioClipOnHoverExit;
        directCopy.playAudioClipOnSelectEnter = direct.playAudioClipOnSelectEnter;
        directCopy.playAudioClipOnSelectExit = direct.playAudioClipOnSelectExit;

        //Haptic Events
        directCopy.playHapticsOnHoverEnter = direct.playHapticsOnHoverEnter;
        directCopy.playHapticsOnHoverExit = direct.playHapticsOnHoverExit;
        directCopy.playHapticsOnSelectEnter = direct.playHapticsOnSelectEnter;
        direct.playHapticsOnSelectExit = direct.playHapticsOnSelectExit;

        //Interactor Events
        directCopy.onHoverEnter = direct.onHoverEnter;
        directCopy.onHoverExit = direct.onHoverExit;
        directCopy.onSelectEnter = direct.onSelectEnter;
        directCopy.onSelectExit = direct.onSelectExit;
    }
}
