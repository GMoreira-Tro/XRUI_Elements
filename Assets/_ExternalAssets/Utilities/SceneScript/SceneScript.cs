using System;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;


namespace Atlas
{
	/// <summary>
	/// The class that binds all SceneScripts and their functions to Unity loop.
	/// This happens through the <see cref="SceneScriptManager"/> instance.
	/// </summary>
	internal class SceneScriptBinder
	{
		/// <summary>
		/// A reference for the SceneScripManager in Unity scene.
		/// </summary>
		public static SceneScriptManager component;

		/// <summary>
		/// List of GameObjects containing Components requested by an script.
		/// </summary>
		private static List<GameObject> componentsGameObjects = new List<GameObject>();

		/// <summary>
		/// Function called every time a Scene is loaded in Unity. This is what instantiates
		/// an instance of the SceneScript and binds it to the SceneScriptManager (the MonoBehaviour 
		/// instance of the SeceneScript system).
		/// </summary>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void OnSceneLoad()
		{
			GameObject gameObject = new GameObject("Scene Script Manager");
			GameObject.DontDestroyOnLoad(gameObject);
			component = gameObject.AddComponent<SceneScriptManager>();

			foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (Type type in ass.GetTypes())
				{
					object[] attributes = type.GetCustomAttributes(typeof(SceneScriptAttribute), true);
					if (attributes != null && attributes.Length > 0)
					{
						SceneScriptAttribute sceneAttribute = (attributes[0] as SceneScriptAttribute);

						MethodInfo currentMethod = type.GetMethod("Setup", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
						if (currentMethod != null)
						{
							GameObject goTemp = new GameObject(type.Name);
							goTemp.transform.parent = gameObject.transform;
                            if(sceneAttribute.RequiredComponents != null)
                            {
                                foreach (Type item in sceneAttribute.RequiredComponents)
                                {
                                    goTemp.AddComponent(item);
                                }
                            }
							componentsGameObjects.Add(goTemp);
							currentMethod.Invoke(null, new object[] { goTemp });
						}

						currentMethod = type.GetMethod("Start", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
						if (currentMethod != null)
						{
							component.On_Start += (Action)Delegate.CreateDelegate(typeof(Action), null, currentMethod);
						}

						currentMethod = type.GetMethod("Update", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
						if (currentMethod != null)
						{
							component.On_Update += (Action)Delegate.CreateDelegate(typeof(Action), null, currentMethod);
						}

						currentMethod = type.GetMethod("OnGUI", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
						if (currentMethod != null)
						{
							component.On_OnGUI += (Action)Delegate.CreateDelegate(typeof(Action), null, currentMethod);
						}

                        currentMethod = type.GetMethod("OnApplicationQuit", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                        if (currentMethod != null)
                        {
                            component.On_ApplicationQuit += (Action)Delegate.CreateDelegate(typeof(Action), null, currentMethod);
                        }


                        //Create instance of given type or, if static only, continues
                        if (type.IsAbstract && type.IsSealed) { continue; }
                        object instance;
                        if(type.BaseType == typeof(ScriptableObject))
                        {
                            instance = ScriptableObject.CreateInstance(type);
                        }
                        else
                        {
                            instance = Activator.CreateInstance(type);
                        }

                        //Search for every Scene Script Method and set Callback
                        currentMethod = type.GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
						if (currentMethod != null)
						{
							component.On_Start += (Action)Delegate.CreateDelegate(typeof(Action), instance, currentMethod);
						}

						currentMethod = type.GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
						if (currentMethod != null)
						{
							component.On_Update += (Action)Delegate.CreateDelegate(typeof(Action), instance, currentMethod);
						}

						currentMethod = type.GetMethod("OnGUI", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
						if (currentMethod != null)
						{
							component.On_OnGUI += (Action)Delegate.CreateDelegate(typeof(Action), instance, currentMethod);
						}

                        currentMethod = type.GetMethod("OnApplicationQuit", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        if (currentMethod != null)
                        {
                            component.On_ApplicationQuit += (Action)Delegate.CreateDelegate(typeof(Action), instance, currentMethod);
                        }
                    }
				}
			}
		}
	}

}
