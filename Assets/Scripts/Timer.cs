using UnityEngine;
using System.Collections;

public static class Timer
{
	static MonoBehaviour behaviour;
	public delegate void Task();

	public static void Schedule(MonoBehaviour _behaviour, float delay, Task task)
	{
		behaviour = _behaviour;
		behaviour.StartCoroutine(DoTask(task, delay));
	}

	static IEnumerator DoTask(Task task, float delay)
	{
		yield return new WaitForSeconds(delay);
		task();
	}
}
