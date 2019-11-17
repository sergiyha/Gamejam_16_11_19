using UnityEngine;

[ExecuteInEditMode]
public class ParabolaConstrain : MonoBehaviour
{
	public Transform StartTarget;
	public Transform MiddleTarget;
	public Transform EndPoint;
	public float height;

	public void Update()
	{
		UpdatePoints();
	}

	public void UpdatePoints()
	{
		if (StartTarget && MiddleTarget && EndPoint)
		{
			var direction = EndPoint.position - StartTarget.position;
			var halfDirection = direction / 2;
			MiddleTarget.position = StartTarget.position + halfDirection;
			var middleposition = transform.InverseTransformPoint(MiddleTarget.position);
			MiddleTarget.localPosition = new Vector3(middleposition.x, height, middleposition.z);
		}
	}

}
