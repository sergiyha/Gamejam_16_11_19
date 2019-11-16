
using System;
using System.Collections.Generic;
using System.Linq;
using DigitalOpus.MB.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Environment
{
	public class MapGenerator : MonoBehaviour
	{
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private RoomCreator _roomCreator;

		private int _minRoomSize = 1;
		private int _maxRoomSizeExclude = 4;
		private int _emptyMap = -1;
		private int roomSize = 10;


		// Start is called before the first frame update
		void Start()
		{
			GenerateMap(out var map, out var rooms);
			LogMap(map);

			foreach (var room in rooms)
			{
				room.GameObject = new GameObject("Room " + room.RoomNumber);
				room.GameObject.transform.parent = transform;

				_roomCreator.Build(room.RoomWidth * roomSize, room.RoomHeight * roomSize,
					transform.position, room, roomSize, room.GameObject.transform);

				MeshFilter[] meshFilters = room.GameObject.GetComponentsInChildren<MeshFilter>();
				var meshBacker = room.GameObject.AddComponent<MB3_MeshBaker>();

				meshBacker.ClearMesh();
				meshBacker.meshCombiner.resultSceneObject = room.GameObject;
				meshBacker.resultPrefab = room.GameObject;
				var gos = meshFilters.Select(s => s.gameObject).ToArray();
				/*foreach (var gop in gos.Select(s => s.transform.parent).Distinct())
				{
					Destroy(gop.gameObject);
				}*/

				//meshBacker.AddDeleteGameObjects(gos, null, true);

				//meshBacker.meshCombiner.Apply();
			}

			return;
		}

		private void GenerateMap(out int[] map, out List<Room> rooms)
		{
			map = Enumerable.Repeat(_emptyMap, _width * _height).ToArray();


			rooms = new List<Room>(map.Length);
			int currentRoomNumber = 0;
			int roomWidth = Random.Range(_minRoomSize, _maxRoomSizeExclude);
			int roomHeight = Random.Range(_minRoomSize, _maxRoomSizeExclude);
			rooms.Add(new Room
			{
				RoomNumber = currentRoomNumber,
				RoomWidth = roomWidth,
				RoomHeight = roomHeight,
				RoomInMapPositionX = 0,
				RoomInMapPositionY = 0,
			});
			for (int roomY = rooms[currentRoomNumber].RoomInMapPositionY;
				roomY < rooms[currentRoomNumber].RoomInMapPositionY + rooms[currentRoomNumber].RoomHeight;
				roomY++)
			{
				for (int roomX = rooms[currentRoomNumber].RoomInMapPositionX;
					roomX < rooms[currentRoomNumber].RoomInMapPositionX + rooms[currentRoomNumber].RoomWidth;
					roomX++)
				{
					map[roomX + roomY * _width] = rooms[currentRoomNumber].RoomNumber;
				}
			}

			for (int y = 0; y < _height; y++)
			{
				for (int x = 0; x < _width; x++)
				{
					if (map[x + y * _width] != _emptyMap) // room exist
					{
						continue;
					}

					// match new room
					bool isMatch = false;
					while (!isMatch)
					{
						roomWidth = Random.Range(_minRoomSize, _maxRoomSizeExclude);
						roomHeight = Random.Range(_minRoomSize, _maxRoomSizeExclude);
						isMatch = IsNewRoomPossible(x, y, roomWidth, roomHeight, map);
					}

					// add new room
					rooms.Add(new Room
					{
						RoomNumber = ++currentRoomNumber,
						RoomWidth = roomWidth,
						RoomHeight = roomHeight,
						RoomInMapPositionX = x,
						RoomInMapPositionY = y,
					});

					// create new room
					for (int roomY = rooms[currentRoomNumber].RoomInMapPositionY;
						roomY < rooms[currentRoomNumber].RoomInMapPositionY + rooms[currentRoomNumber].RoomHeight;
						roomY++)
					{
						for (int roomX = rooms[currentRoomNumber].RoomInMapPositionX;
							roomX < rooms[currentRoomNumber].RoomInMapPositionX + rooms[currentRoomNumber].RoomWidth;
							roomX++)
						{
							map[roomX + roomY * _width] = rooms[currentRoomNumber].RoomNumber;
						}
					}
				}
			}
		}

		private bool IsNewRoomPossible(int x, int y, int roomWidth, int roomHeight, int[] map)
		{
			var roomEndX = x + roomWidth;
			var roomEndY = y + roomHeight;
			if (roomEndX > _width || roomEndY > _height)
			{
				return false;
			}

			for (int newRoomY = y; newRoomY < roomEndY; newRoomY++)
			{
				for (int newRoomX = x; newRoomX < roomEndX; newRoomX++)
				{
					if (map[newRoomX + newRoomY * _width] != _emptyMap)
					{
						return false;
					}
				}
			}

			return true;
		}

		private void LogMap(int[] map)
		{
			Debug.Log("Map: ");
			for (int j = 0; j < _height; j++)
			{
				string line = String.Empty;
				for (int i = 0; i < _width; i++)
				{
					line += map[i + j * _width] + " ";
				}

				Debug.Log(line);
			}
		}

		// Update is called once per frame
		void Update()
		{

		}

	}
}