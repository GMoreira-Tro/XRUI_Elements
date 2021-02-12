
//System Includes
using System;

//Unity Includes
using UnityEngine;

namespace Atlas
{
    /// <summary>
    /// MonoBehaviour instance for the SceneManager static class binding
    /// with Unity component system calls/loop.
    /// </summary>
    internal class SceneScriptManager : MonoBehaviour
    {
        /// <summary>
        /// OnAwake callback for the OnAwake bind.
        /// </summary>
        public Action On_Awake;
        private void Awake()
        {
            On_Awake?.Invoke();
        }

        /// <summary>
        /// OnStart callback for the OnStart bind.
        /// </summary>
        public Action On_Start;
        private void Start()
        {
            On_Start?.Invoke();
        }

        /// <summary>
        /// OnUpdate callback for the OnUpdate bind.
        /// </summary>
        public Action On_Update;
        private void Update()
        {
            On_Update?.Invoke();
        }

        /// <summary>
        /// OnGui callback for the OnGUI bind.
        /// </summary>
        public Action On_OnGUI;
        private void OnGUI()
        {
            On_OnGUI?.Invoke();
        }

        /// <summary>
        /// OnApplicationQuit callback for the OnApplicationQuit bind.
        /// </summary>
        public Action On_ApplicationQuit;
        private void OnApplicationQuit()
        {
            On_ApplicationQuit?.Invoke();
        }
    }
}
