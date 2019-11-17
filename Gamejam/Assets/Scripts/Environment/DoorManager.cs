using UnityEngine;

namespace Assets.Scripts.Environment
{
	public class DoorManager : MonoBehaviour
	{
		[SerializeField] private GameObject _door_01;
		[SerializeField] private GameObject _door_02;
		[SerializeField] private GameObject _door_03;
		[SerializeField] private GameObject _door_04;
		[SerializeField] private GameObject _door_05;


		public GameObject GetDoorPrefab()
		{
			var value = Random.value;
			if (value < 0.2)
			{
				return _door_01;
			}
			else if (value < 0.4)
			{
				return _door_02;
			}
			else if (value < 0.6)
			{
				return _door_03;
			}
			else if (value < 0.8)
			{
				return _door_04;
			}
			else
			{
				return _door_05;
			}
		}
	} 
}
