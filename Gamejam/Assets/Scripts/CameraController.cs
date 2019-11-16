using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class CameraController : MonoBehaviour
{
	private PlayerController _playerController;

	[SerializeField] private Camera _camera;

	[SerializeField]
	private float CameraInvesremultiplier;

	[SerializeField] private Vector3 _cameraDif;
	// Start is called before the first frame update
	void Start()
	{
		_playerController = GetComponent<PlayerController>();
		_cameraDif = _camera.transform.position - _playerController.transform.position;
	}


	// Update is called once per frame
	void Update()
	{
		var cameraDirection = _playerController.GetDirection() * -1;
		Debug.DrawRay(_playerController.transform.position, cameraDirection, Color.blue);

		var playerPosition = _playerController.transform.position;
		_camera.transform.position = playerPosition + _cameraDif - cameraDirection.normalized * CameraInvesremultiplier;
	}

	private Vector3 _currentCameraInversion = Vector3.zero;
	private float _lerp = 0;

	//private void LerpCameraInversion(Vector3 futureInversion)
	//{
	//	_currentCameraInversion = Vector3.Lerp(_currentCameraInversion, futureInversion, _lerp);
		
	//}
}
