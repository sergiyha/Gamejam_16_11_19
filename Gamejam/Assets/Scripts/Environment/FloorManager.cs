using UnityEngine;

namespace Assets.Scripts.Environment
{
	public class FloorManager : MonoBehaviour
	{

		[SerializeField] private GameObject _floorMain;
		[SerializeField] private GameObject _floor_05;
		[SerializeField] private GameObject _floor_06;

		public GameObject GetFloorPrefab()
		{
			var value = Random.value;
			if (value < 0.98)
			{
				return _floorMain;
			}
			else if (value < 0.99)
			{
				return _floor_05;
			}
			else
			{
				return _floor_06;
			}
		}
	} 
}
