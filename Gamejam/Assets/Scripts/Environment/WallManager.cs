using UnityEngine;

namespace Assets.Scripts.Environment
{
	public class WallManager : MonoBehaviour
	{

		[SerializeField] private GameObject _fense;

		public GameObject GetWallPrefab()
		{
			return _fense;
			//var value = Random.value;
			//if (value < 0.9)
			//{
			//	return _floorMain;
			//}
			//else if (value < 0.95)
			//{
			//	return _floor_05;
			//}
			//else
			//{
			//	return _floor_06;
			//}
		}
	} 
}
