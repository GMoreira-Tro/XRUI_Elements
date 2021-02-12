using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// For fun class. My first class on VR. This class changes the color of an object using the index trigger of Oculus joysticks. By: Guilherme Moreira.
/// </summary>
[RequireComponent(typeof(XRGrabInteractable), typeof(MeshRenderer))]
public class ChangeColor : MonoBehaviour
{
    /// <summary>
    /// The XRGrabInteractable component of the object.
    /// </summary>
    private XRGrabInteractable grabInteractable;
    /// <summary>
    /// The initial color of the object.
    /// </summary>
    private Color initialColor;
    /// <summary>
    /// The material of the object.
    /// </summary>
    private Material rendererMaterial;

    /// <summary>
    /// Default MonoBehavior Start().
    /// </summary>
    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.onActivate.AddListener(SetColor);

        grabInteractable.onHoverExit.AddListener((interactor) =>
            {
                ResetColor();
            }
        );

        initialColor = GetComponent<MeshRenderer>().material.color;

        rendererMaterial = GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// Default MonoBehavior OnDestroy().
    /// </summary>
    private void OnDestroy()
    {
        grabInteractable.onActivate.RemoveListener(SetColor);
    }

    /// <summary>
    /// Set a diferent color for the object (will always be a diferent color).
    /// </summary>
    /// <param name="interactor">The interactor reference.</param>
    public void SetColor(XRBaseInteractor interactor)
    {
        int randomInt;

        if(interactor.GetComponent<XRController>().controllerNode == UnityEngine.XR.XRNode.RightHand)
        {
            if (rendererMaterial.color.Equals(Color.green))
            {
                randomInt = new System.Random().Next(1, 4);
            }
            else
            {
                System.Collections.Generic.List<int> lb = new System.Collections.Generic.List<int> { 0, 1, 2, 3, 4 };
                if (rendererMaterial.color.Equals(Color.yellow))
                {
                    lb.Remove(lb[1]);
                    randomInt = lb[new System.Random().Next(4)];
                }
                else if (rendererMaterial.color.Equals(Color.blue))
                {
                    lb.Remove(lb[2]);
                    randomInt = lb[new System.Random().Next(4)];
                }
                else if (rendererMaterial.color.Equals(Color.red))
                {
                    lb.Remove(lb[3]);
                    randomInt = lb[new System.Random().Next(4)];
                }
                else
                {
                    lb.Remove(lb[4]);
                    randomInt = lb[new System.Random().Next(4)];
                }
            }

            switch (randomInt)
            {
                case 0:
                    rendererMaterial.color = Color.green;
                    break;
                case 1:
                    rendererMaterial.color = Color.yellow;
                    break;
                case 2:
                    rendererMaterial.color = Color.blue;
                    break;
                case 3:
                    rendererMaterial.color = Color.red;
                    break;
                default:
                    rendererMaterial.color = Color.black;
                    break;
            }
        }
        else
        {
            if (rendererMaterial.color.Equals(Color.cyan))
            {
                randomInt = new System.Random().Next(1, 4);
            }
            else
            {
                System.Collections.Generic.List<int> lb = new System.Collections.Generic.List<int> { 0, 1, 2, 3, 4 };
                if (rendererMaterial.color.Equals(Color.gray))
                {
                    lb.Remove(lb[1]);
                    randomInt = lb[new System.Random().Next(4)];
                }
                else if (rendererMaterial.color.Equals(Color.magenta))
                {
                    lb.Remove(lb[2]);
                    randomInt = lb[new System.Random().Next(4)];
                }
                else if (rendererMaterial.color.Equals(Color.clear))
                {
                    lb.Remove(lb[3]);
                    randomInt = lb[new System.Random().Next(4)];
                }
                else
                {
                    lb.Remove(lb[4]);
                    randomInt = lb[new System.Random().Next(4)];
                }
            }

            switch (randomInt)
            {
                case 0:
                    rendererMaterial.color = Color.cyan;
                    break;
                case 1:
                    rendererMaterial.color = Color.gray;
                    break;
                case 2:
                    rendererMaterial.color = Color.magenta;
                    break;
                case 3:
                    rendererMaterial.color = Color.clear;
                    break;
                default:
                    rendererMaterial.color = new Color(0.7f,0.2f,0.5f);
                    break;
            }
        }
    }

    /// <summary>
    /// Reset to initial color.
    /// </summary>
    public void ResetColor()
    {
        rendererMaterial.color = initialColor;
    }
}
