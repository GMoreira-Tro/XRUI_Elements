# XR_UIElements 0.1 Alpha

XRUI_Elements it's a Element Package to be used on Virtual Reality (and in the near future Augmented Reality) Applications on Unity.

This package's elements was developed to be worked with the package [XR Interaction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@0.9/manual/index.html).

By now, it's possible to download and add the XRUI_Elements on a Unity project by te custom packages system (explained on [Instalation](#installation)).

This project it's part of a Master's Study made by @PedroRossa in partnership wit the Vizlab | X-Reality and GeoInformatics Lab - @vizlab-uni.

<p align="center">
<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/capaProvisoriaXRUIElements.png"  width="700" align="..." alt="Provisory Image">
</p>

# Table of contents
- [Installation](#installation)
- [Usage](#usage)
- [License](#license)
- [Credits](#credits)
- [How to cite](#how-to-cite)

## Installation

Necessary softwares to use the XRUI_Elements Package.

* [Visual Studio](https://visualstudio.microsoft.com/vs/community/) or [Visual Code](https://code.visualstudio.com/docs/other/unity)
* [Unity 2019.4 LTS](https://unity3d.com/unity/qa/lts-releases?_ga=2.178802020.1167371567.1592846982-112079466.1585313065).

# Configuring XR Interaction Toolkit

1. Create a new Unity Project or open an existent one (Unity 2019.4).
2. On Menu Bar, access "Window->PackageManager" - [Package Manager](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@2.0/manual/index.html).
<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/PackageManager_01.png" alt="PackageManager Unity" width="350">
3. Check the option to show PREVIEW Packages
<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/PackageManager_02.png" alt="Preview Packages Option" width="350"">
4. Install the ** XR Interaction Toolkit ** Package
<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/PackageManager_03.png" alt="XR Interaction Toolkit Install" width="350">

That is it! Now the project can use the Unity package that deal with the basics on Virtual and Augmented Reality.

The next step is get and configure the XRUI_Elements.

<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/PackageManager_04.png" alt="XR Interaction Toolkit Instaled." width="350">

# Get XRUI_Elements Pacakge

Access [XRUI_Elements 0.1 Alpha](https://github.com/PedroRossa/XRUI_Elements/releases/tag/v0.1-alpha) and download the XRUI_Elements_01_alpha.unitypackage
On the opened Unity project, go to "Assets -> Import Package -> Custom Package".

<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/CustomPackage.png" alt="Import Custom Package." width="350">

Because of a internal dependecy of the elements, it's necessary import the TextMeshPro package from Unity.
To do that, goes to "Window -> TextMeshPro -> Import TMP Essential Resource"

<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ImportTextMeshPro.png" alt="Import TextMeshPro Package." width="350">

Select the XRUI_Elements_01_alpha.unitypackage and import the files to Project.

Congrats!!!! 
Now you already have all the basics to star te amazing journey on the development to Virtual Reality using this simple but awesome elements created with love, tears and dedication.

<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/XRUI_ElementsMenu.png" alt="XRUI_Elements Menu Bar." width="450">

## Usage

This is the current list of elements implemented on XRUI_Elements;

### Elements List
- 2D Button Sprite
- 2D Button Text
- 2D Progress Bar
- 2D Toggle
- 3D Button Sprite
- 3D Button Text
- 3D Progress Bar
- 3D Toggle
- 3D Slider
- 3D Box Slider
- Manipulables
- Feedback System


### Feedback System

 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_Feedback.png" alt="Feedback System" width="768">

### Buttons

 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_2DButton.png" alt="2D Buttons" width="768">
 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_3DButton.png" alt="3D Buttons" width="768">
 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_3DButtonSprite.png" alt="3D Button Sprite" width="768">

### Progress Bar

 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_2DProgressBar.png" alt="2D ProgressBar" width="768">
 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_2DProgressBar.png" alt="3D ProgressBar" width="768">
 
### Slider

 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_3DSlider.png" alt="3D Slider" width="768">
 
### Toggle

 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_Toggle.png" alt="Toggles" width="768">
 
### Manipulables

 <img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/ElementImages/English_XRUI_Manipulables.png" alt="Manipulables" width="768">

# Class Architecture

The class architecture can be saw on the image below.
It was used a inheritance system to encapsulate elements and facilitate de extension of this elements.

<img src="https://github.com/PedroRossa/XR_UIElements/blob/master/_ReadMe_Resources/Architecture.png" alt="XRUI_Elements Architecture" >

# Demo Scene

The package comes with a Demo Scene where the users can interact with all the existent elements created on XRUI_Elements.

## Credits
 
- [Pedro Rossa](http://lattes.cnpq.br/8600200220209812)
 
 Special thanks to Research and Development Vizlab's Team for all the support, help and tips on development of this package.

## License

```
MIT Licence (https://mit-license.org/)
```

# How to cite

Not published yet

```


```

