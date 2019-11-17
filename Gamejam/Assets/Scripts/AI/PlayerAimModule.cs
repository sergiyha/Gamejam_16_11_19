using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAimModule : MonoBehaviour
{
	public ParabolaController ParabolaController;
	private Vector3 _tagerPos;

	private List<AnimEffectLerp> _effectLerp = new List<AnimEffectLerp>();
	public void Aim(Transform target, BowScriptableObj data, Character character)
	{
		if (_effectLerp.Any(e => e.Character == character)) return;


		_effectLerp.Add(new AnimEffectLerp()
		{
			Lerp = 0,
			Finish = target,
			Start = character.transform.position,
			Effect = Instantiate(data.ps.transform, character.transform.position, Quaternion.identity),
			CD = data.Cooldown/2,
			Character = character
		});

	}

	private bool f = false;


	private void InitParticle()
	{

	}

	public void Update()
	{
		for (int i = 0; i < _effectLerp.Count; i++)
		{
			_effectLerp[i].CD -= Time.deltaTime;

			_effectLerp[i].Lerp += Time.deltaTime;
			Vector3 position = Vector3.Lerp(_effectLerp[i].Start, _effectLerp[i].Finish ? _effectLerp[i].Finish.position : _effectLerp[i].Start, _effectLerp[i].Lerp);
			_effectLerp[i].Effect.position = position;

			float halfLerp = 0;
			if (_effectLerp[i].Lerp < 0.5)
			{
				halfLerp = _effectLerp[i].Lerp / 0.5f;
			}
			else if (_effectLerp[i].Lerp > 0.5 && _effectLerp[i].Lerp < 1)
			{
				var lerp = Mathf.Abs(1 - _effectLerp[i].Lerp);
				halfLerp = lerp / 0.5f;
			}
			if (_effectLerp[i].Lerp > 0)
				_effectLerp[i].Effect.position = new Vector3(position.x, halfLerp, position.z);
		}
		_effectLerp.Where(e => e.CD <= 0).ToList().ForEach((s) => { Destroy(s.Effect.gameObject); });
		_effectLerp = _effectLerp.Where(e => e?.CD >= 0).ToList();
	}



	public void Disable()
	{
		//if (ParabolaController.isActiveAndEnabled) ParabolaController.gameObject.SetActive(false);
	}

	public void Fire()
	{

	}

	public class AnimEffectLerp
	{
		public float CD;
		public float Lerp = 0;
		public Vector3 Start;
		public Transform Finish;
		public Transform Effect;
		public Character Character;
	}


}
