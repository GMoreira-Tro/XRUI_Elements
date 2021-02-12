/// Copyright (c) 2011, Ken Rockot  <k-e-n-oz.gs>.  All rights reserved.
/// Everyone is granted non-exclusive license to do anything at all with this code.

//System Include
using System.Collections;

namespace Atlas
{
	/// <summary>
	/// A singleton that runs coroutines for other scripts.
	/// </summary>
	class UnityTaskManager : Singleton<UnityTaskManager>
	{
		public class TaskState
		{
			/// <summary>
			/// Returns true if and only if the coroutine is running.
			/// <para>Paused tasks are considered to be running.</para>
			/// </summary>
			public bool Running { get; private set; }

			/// <summary>
			/// Returns true if and only if the coroutine is currently paused.
			/// </summary>
			public bool Paused { get; private set; }

			/// <summary>
			/// Termination event.  
			/// <para>Triggered when the coroutine completes execution.</para>
			/// </summary>
			/// <param name="manual">Returns true if the coroutine was stopped and false if naturaly completed.</param>
			public delegate void FinishedHandler(bool manual);

			/// <summary>
			/// Termination event.  
			/// <para>Triggered when the coroutine completes execution.</para>
			/// </summary>
			public event FinishedHandler Finished;

			/// <summary>
			/// The coroutine that will be called.
			/// </summary>
			private IEnumerator coroutine;

			/// <summary>
			/// If the coroutine is stopped.
			/// </summary>
			private bool stopped;

			/// <summary>
			/// Default constructor receiving the coroutine to start.
			/// </summary>
			/// <param name="coroutine">The coroutine to start.</param>
			public TaskState(IEnumerator coroutine)
			{
				this.coroutine = coroutine;
			}

			/// <summary>
			/// Pauses the execution of a coroutine.
			/// </summary>
			public void Pause()
			{
				Paused = true;
			}

			/// <summary>
			/// Unpauses a coroutine.
			/// </summary>
			public void Unpause()
			{
				Paused = false;
			}

			/// <summary>
			/// Calls the StartCoroutine in the MonoBehaviour.
			/// </summary>
			public void Start()
			{
				Running = true;
				Instance().StartCoroutine(CallWrapper());
			}

			/// <summary>
			/// Discontinues execution of the coroutine at its next yield.
			/// </summary>
			public void Stop()
			{
				stopped = true;
				Running = false;
			}

			/// <summary>
			/// Wraps the call of a coroutine allowing to be paused and stopped.
			/// </summary>
			private IEnumerator CallWrapper()
			{
				yield return null;
				IEnumerator e = coroutine;
				while (Running)
				{
					if (Paused)
					{
						yield return null;
					}
					else
					{
						if (e != null && e.MoveNext())
						{
							yield return e.Current;
						}
						else
						{
							Running = false;
						}
					}
				}

				FinishedHandler handler = Finished;
				if (handler != null)
				{
					handler(stopped);
				}
			}
		}

		/// <summary>
		/// Creates a new Coroutine.
		/// </summary>
		/// <param name="coroutine">The coroutine that will be run.</param>
		/// <returns>Returns the coroutine state.</returns>
		public static TaskState CreateTask(IEnumerator coroutine)
		{
			return new TaskState(coroutine);
		}
	}
}