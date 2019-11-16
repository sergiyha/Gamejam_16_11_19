using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private Transform[] _startPositions;

	[SerializeField]
	private Camera _camera;

	[SerializeField]
	private Transform _mainPlayerObject;

	[SerializeField]
	private Transform _basePositionsContainer;

	[SerializeField]
	private Transform _directionObject;

	[SerializeField]
	private string DirectionDetectorName = "DirectionDetector";


	[SerializeField]
	private NavMeshAgent[] _rangeAgents;

	[SerializeField]
	private NavMeshAgent[] _meleAgents;

	[SerializeField]
	private Transform[] _meleAgentsPositions;

	[SerializeField]
	private Transform[] _rangedAgentsPositions;

	[SerializeField] private float _speed;

	[SerializeField] private float MeleeRange;
	[SerializeField] private float RangeRange;
	

	// Start is called before the first frame update

	private void Update()
	{
		FindDirection();
		MoveAgents();
		MoveRanged();
	}

	public void AddNewUnit()
	{

	}

	public void RemoveUnit()
	{
	}

	public Vector3 GetDirection()
	{
		return _aimingDirection;
	}

	private Vector3 _aimingDirection;

	private Vector3 MousePositionHit;
	private void FindDirection()
	{
		var mousePosition = Input.mousePosition;

		RaycastHit[] hits;
		var rayMouse = _camera.ScreenPointToRay(mousePosition);
		hits = Physics.RaycastAll(rayMouse.origin, rayMouse.direction, 100000);
		Debug.DrawRay(rayMouse.origin, rayMouse.direction * 10000, Color.black);
		MoveUnits();
		foreach (var raycastHit in hits)
		{
			if (raycastHit.transform.name == DirectionDetectorName)
			{
				MousePositionHit = raycastHit.point;
				var mouseRayHit = raycastHit.point;
				Debug.DrawLine(MousePositionHit, transform.position);
 
				//Debug.DrawRay(rayMouse.origin,mouseRayHit);

				var mouse0Y = new Vector3(mouseRayHit.x, 0, mouseRayHit.z);
				var trans0Y = new Vector3(transform.position.x, 0, transform.position.z);
				_aimingDirection = mouse0Y - trans0Y;
				_directionObject.forward = _aimingDirection;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Break();
		}
	}

	public void AddCharacter()
	{

	}

	public void DeleteCharacter()
	{
		
	}

	private void CheckEnemies()
	{

	}

	private void Attack()
	{
			
	}

	


	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(MousePositionHit, 1);
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(this.transform.position, MeleeRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, RangeRange);
	}

	private void MoveAgents()
	{
		MoveMele();
	}

	private void MoveMele()
	{
		for (var i = 0; i < _meleAgents.Length; i++)
		{
			_meleAgents[i].destination = _meleAgentsPositions[i].transform.position;
		}
	}

	private void MoveRanged()
	{
		for (var i = 0; i < _rangeAgents.Length; i++)
		{
			_rangeAgents[i].destination = _rangedAgentsPositions[i].transform.position;
		}
	}

	private void MoveUnits()
	{
		var y = Input.GetAxis("Vertical") * _speed;
		var x = Input.GetAxis("Horizontal") * _speed;
		y *= Time.deltaTime;
		x *= Time.deltaTime;

		this.transform.position += new Vector3(x, 0, y);
	}

	public float GetRangeDistance()
	{
		return RangeRange;
	}

	public float GetMeleeDistance()
	{
		return MeleeRange;
	}



	public void OnStartMeleeAttack(Character enemy)
	{
		Debug.Log(enemy);
	}


	public void OnStartRangeAttack(Character enemy)
	{
		Debug.Log(enemy);
	}


}


