using System.Collections.Generic;
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

	[SerializeField] private bool _meleeAttacking;

	[SerializeField] private UnitMovableContainer _meleeContainer;
	[SerializeField] private UnitMovableContainer _rangeContainer;

	private void Start()
	{
		_meleeContainer = new UnitMovableContainer(this.transform);
		_rangeContainer = new UnitMovableContainer(this.transform);

		_meleeContainer.AddAgents(_meleAgents);
		_meleeContainer.AddTargets(_meleAgentsPositions);

		_rangeContainer.AddAgents(_rangeAgents);
		_rangeContainer.AddTargets(_rangedAgentsPositions);
	}

	// Start is called before the first frame update

	private void Update()
	{
		FindDirection();
		MoveAgents();

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
		//MoveMelee();
		_meleeContainer.Move();
		_rangeContainer.Move();
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

	private List<Character> _meleEnemies = null;
	private List<Character> _rangeEnemies = null;

	public void OnStartMeleeAttack(List<CharacterAndDistance> enemy)
	{
	//	Debug.LogError(enemy);
		_meleeContainer.SetEnemies(enemy);
		if (enemy != null)
			_meleeContainer.Unattach();
		else
			_meleeContainer.Attach();
	}

	public void OnStartRangeAttack(List<CharacterAndDistance> enemy)
	{

	}


}


