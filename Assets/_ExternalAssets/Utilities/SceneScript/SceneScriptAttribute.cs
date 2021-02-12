using System;

namespace Atlas
{
	/// <summary>
	/// SceneScript Attribute that indicates that this class is a SceneScript class.
	/// <para>It has a constructor that can receive Unity Components, Later if the class has a 'Setup' method 
	/// will receive a GameObject containing the Components.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public class SceneScriptAttribute : Attribute
	{
		public Type[] RequiredComponents { get; set; }

		public SceneScriptAttribute() { }

		public SceneScriptAttribute(params Type[] requiredComponents)
		{
			RequiredComponents = requiredComponents;
		}
	}
}