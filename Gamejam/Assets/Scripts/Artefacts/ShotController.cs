using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
	public static ShotController Instance;
	private List<TrailStruct> _effects = new List<TrailStruct>();
	public GameObject Parabola;

	private void Awake()
	{
		Instance = this;
	}

	public void AddShotWithParabola(GameObject effect, ParabolaController parabolaController, Transform Chartransform)
	{
		var newParabola = Instantiate(Parabola, Chartransform.transform.position,Quaternion.identity);
		var _effect = Instantiate(effect,Vector3.zero,Quaternion.identity);
		//var duration =10;
		//var _effect = Instantiate(effect, parabolaController.GetPositionAtTime(0), Quaternion.identity);


		
	}

	private void Update()
	{
		for (int i = 0; i < _effects.Count; i++)
		{
			_effects[i].Time -= _effects[i].GetDecrement();
			_effects[i].Effect.transform.position = _effects[i].ParabolaController.GetPositionAtTime(_effects[i].Time);
		}
	}
}

public class TrailStruct
{
	public float GetDecrement() => Decrement * UnityEngine.Time.deltaTime;
	public float Decrement;
	public float Time;
	public GameObject Effect;
	public ParabolaController ParabolaController;
}
