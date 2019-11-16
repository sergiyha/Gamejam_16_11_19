using UnityEngine;

namespace Assets.Scripts.Environment
{

	public class Room
	{
		public int RoomWidth;
		public int RoomHeight;
		public int RoomNumber;

		public int RoomInMapPositionX;
		public int RoomInMapPositionY;
		public GameObject GameObject;
	}

	public class RoomCreator : MonoBehaviour
	{
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private float _tileSize;

		[SerializeField] private FloorManager _floorManager;
		[SerializeField] private WallManager _wallManager;
		[SerializeField] private GameObject _corner;

		public enum TileType
		{
			Floor,
			Wall,
			Corner
		}

		/*private void Start()
		{
			Build(_width, _height, Vector3.zero, parent);
		}*/

		public void Build(int width, int height, Vector3 mapPosition, Room room, int roomOnMapSize, Transform parent)
		{
			var startPosition = mapPosition + new Vector3(room.RoomInMapPositionX * roomOnMapSize * _tileSize, 0,
				                    room.RoomInMapPositionY * roomOnMapSize * _tileSize);
			var map = new TileType[width * height];
			//var goToRemove = 
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Vector3 position = startPosition + new Vector3(_tileSize * x, 0, _tileSize * y);

					// flour
					Instantiate(_floorManager.GetFloorPrefab(), position, Quaternion.identity, parent);

					// walls
					var wall = _wallManager.GetWallPrefab();
					if (y == 0)
					{
						Instantiate(wall, position, Quaternion.Euler(0, 90, 0), parent);
					}
					if (x == 0)
					{
						Instantiate(wall, position, Quaternion.Euler(0, 180, 0), parent);
					}
					if (y == height - 1)
					{
						Instantiate(wall, position, Quaternion.Euler(0, -90, 0), parent);
					}
					if (x == width - 1)
					{
						Instantiate(wall, position, Quaternion.identity, parent);
					}

					/*
					// corners
					if (y == 0 && x == 0)
					{
						Instantiate(_corner, position, Quaternion.Euler(0, 90, 0), parent);
						continue;
					}
					else if (y == 0 && x == width - 1)
					{
						Instantiate(_corner, position, Quaternion.Euler(0, 0, 0), parent);
						continue;
					}
					else if (y == height - 1 && x == 0)
					{
						Instantiate(_corner, position, Quaternion.Euler(0, 180, 0), parent);
						continue;
					}
					else if (y == height - 1 && x == width - 1)
					{
						Instantiate(_corner, position, Quaternion.Euler(0, -90, 0), parent);
						continue;
					}*/

				}
			}

		}



		/*
		public void Build(int width, int height, Vector3 roomPosition, Transform parent)
		{
			var map = new TileType[width * height];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					// corners
					Vector3 position = roomPosition + new Vector3(_tileSize * x, 0, _tileSize * y);
					if (y == 0 && x == 0)
					{
						Instantiate(_corner, position, Quaternion.Euler(0, 90, 0), parent);
						continue;
					}
					else if (y == 0 && x == width - 1)
					{
						Instantiate(_corner, position, Quaternion.Euler(0, 0, 0), parent);
						continue;
					}
					else if (y == height - 1 && x == 0)
					{
						Instantiate(_corner, position, Quaternion.Euler(0, 180, 0), parent);
						continue;
					}
					else if (y == height - 1 && x == width - 1)
					{
						Instantiate(_corner, position, Quaternion.Euler(0, -90, 0), parent);
						continue;
					}

					// walls
					if (y == 0)
					{
						Instantiate(_wall, position, Quaternion.Euler(0, 90, 0), parent);
						continue;
					}
					else if (x == 0)
					{
						Instantiate(_wall, position, Quaternion.Euler(0, 180, 0), parent);
						continue;
					}
					else if (y == height - 1)
					{
						Instantiate(_wall, position, Quaternion.Euler(0, -90, 0), parent);
						continue;
					}
					else if (x == width - 1)
					{
						Instantiate(_wall, position, Quaternion.identity, parent);
						continue;
					}

					// flour
					Instantiate(_floorManager.GetWallPrefab(), position, Quaternion.identity, parent);
				}
			}

		}*/
	}
}