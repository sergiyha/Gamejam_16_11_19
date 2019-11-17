using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PlayerAimModule : MonoBehaviour
{
	public ParabolaController ParabolaController;
	private Vector3 _tagerPos;
	public void Aim(Transform target)
	{
		ParabolaController.ParabolaConstrain.EndPoint.position = Vector3.Lerp(_tagerPos, target.transform.position,Time.deltaTime *20 );
		if (!ParabolaController.isActiveAndEnabled) ParabolaController.gameObject.SetActive(true);

		_tagerPos = target.transform.position;
	}

	public void Disable()
	{
		if(ParabolaController.isActiveAndEnabled) ParabolaController.gameObject.SetActive(false);
	}

	public void Fire()
	{

	}

}
