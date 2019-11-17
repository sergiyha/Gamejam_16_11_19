
using System;
using System.Collections.Generic;
using System.Linq;
using DigitalOpus.MB.Core;
using UnityEngine;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Environment
{
	public class MapGenerator : MonoBehaviour
	{
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private RoomCreator _roomCreator;
        [SerializeField] private Transform _players;

        private int _minRoomSize = 1;
		private int _maxRoomSizeExclude = 4;
		private int _emptyMap = -1;
		private int _roomSize = 10;

        public Vector3 StartRoomCenter { get; private set; }
        public Vector3 EndRoomCenter { get; private set; }


        // Start is called before the first frame update
        void Start()
		{
			GenerateMap(out var map, out var rooms);

			var adjacencyMatrix = BuildAdjacencyMatrix(rooms, map);

			var startX = Random.value < 0.5 ? 0 : _width - 1;
			var startY = Random.value < 0.5 ? 0 : _height - 1;
			int endX;
			int endY;
			do
			{
				endX = Random.value < 0.5 ? 0 : _width - 1;
				endY = Random.value < 0.5 ? 0 : _height - 1;
			} while (startX == endX && startY == endY);

			var startRoom = map[startX + startY * _width];
			var endRoom = map[endX + endY * _width];

            

            var path = GetLongestPath(rooms, startRoom, endRoom);
			Debug.Log(String.Join(" ", path));
			var doors = new int[rooms.Count, rooms.Count];
			var prevRoom = path[0];
			for (var index = 1; index < path.Count; index++)
			{
				doors[prevRoom, path[index]] = 1;
				doors[path[index], prevRoom] = 1;
				prevRoom = path[index];
			}
			doors[prevRoom, endRoom] = 1;
			doors[endRoom, prevRoom] = 1;

			for (int i = 0; i < doors.GetLength(0); i++)
			{
				rooms[i].DoorToRooms = new List<int>();
				for (int j = 0; j < doors.GetLength(1); j++)
				{
					if (doors[i,j] != 0)
					{
						rooms[i].DoorToRooms.Add(j);
					}
				}
			}

			Debug.Log("Doors");
			LogMatrix(doors);


			foreach (var room in rooms)
			{
				room.GameObject = new GameObject("Room " + room.Number);
				room.GameObject.transform.parent = transform;

				_roomCreator.Build(_roomSize,
					transform.position, room, rooms, map, room.GameObject.transform);

				MeshFilter[] meshFilters = room.GameObject.GetComponentsInChildren<MeshFilter>();
				var meshBacker = room.GameObject.AddComponent<MB3_MeshBaker>();

				meshBacker.ClearMesh();
				meshBacker.meshCombiner.resultSceneObject = room.GameObject;
				meshBacker.resultPrefab = room.GameObject;
				var gos = meshFilters.Select(s => s.gameObject).ToArray();
				foreach (var gop in gos.Select(s => s.transform.parent).Distinct())
				{
					Destroy(gop.gameObject);
				}

				meshBacker.AddDeleteGameObjects(gos, null, true);

				meshBacker.meshCombiner.Apply();

                room.GameObject.transform.GetChild(room.GameObject.transform.childCount - 1).gameObject.AddComponent<NavMeshSourceTag>();

                //StartRoomCenter = rooms[startRoom].StartPosition + rooms[startRoom].SceneSize / 2;

            }
            StartRoomCenter = rooms[startRoom].StartPosition + rooms[startRoom].SceneSize / 2;

            EndRoomCenter = rooms[endRoom].StartPosition + rooms[endRoom].SceneSize / 2;
            Debug.LogError(EndRoomCenter);

            _players.transform.position = StartRoomCenter;
        }

		private List<int> GetLongestPath(List<Room> rooms, int startRoom, int endRoom)
		{
			List<List<int>> pathes = new List<List<int>>();
			bool[] visited = new bool[rooms.Count];
			List<int> prev = new List<int>();

			FindPath(pathes, rooms, visited, prev, startRoom, endRoom);

			return pathes.OrderBy(o => o.Count).First(); //.Last();
		}

		private void FindPath(List<List<int>> pathes, List<Room> rooms, bool[] visited, List<int> prev, int start,
			int goal)
		{
			if (start == goal)
			{
				pathes.Add(prev);
				return;
			}
			visited[start] = true;
			foreach (var r in rooms[start].AdjacentRooms)
			{
				if (!visited[r])
				{
					prev.Add(start);
					FindPath(pathes, rooms, visited, prev.ToArray().ToList(), r, goal);
					prev.RemoveAt(prev.Count - 1);
				}
			}
		}

		private int[,] BuildAdjacencyMatrix(List<Room> rooms, int[] map)
		{
			var adjacencyMatrix = new int[rooms.Count, rooms.Count];

			// horizontal adjacency
			for (int y = 0; y < _height; y++)
			{
				var prevRoomNum = map[0 + y * _width];
				for (int x = 1; x < _width; x++)
				{
					var roomNumber = map[x + y * _width];
					if (prevRoomNum != roomNumber)
					{
						adjacencyMatrix[roomNumber, prevRoomNum] = 1;
						adjacencyMatrix[prevRoomNum, roomNumber] = 1;
					}

					prevRoomNum = roomNumber;
				}
			}

			for (int x = 1; x < _width; x++)
			{
				var prevRoomNum = map[x + 0 * _width];
				for (int y = 0; y < _height; y++)
				{
					var roomNumber = map[x + y * _width];
					if (prevRoomNum != roomNumber)
					{
						adjacencyMatrix[roomNumber, prevRoomNum] = 1;
						adjacencyMatrix[prevRoomNum, roomNumber] = 1;
					}

					prevRoomNum = roomNumber;
				}
			}

			for (int i = 0; i < rooms.Count; i++)
			{
				rooms[i].AdjacentRooms = new List<int>();
				for (int j = 0; j < rooms.Count; j++)
				{
					if (adjacencyMatrix[i, j] != 0)
					{
						rooms[i].AdjacentRooms.Add(j);
					}
				}
			}

			LogMatrix(adjacencyMatrix);
			return adjacencyMatrix;
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
				Number = currentRoomNumber,
				MapWidth = roomWidth,
				MapHeight = roomHeight,
				OnMapPositionX = 0,
				OnMapPositionY = 0,
			});
			for (int roomY = rooms[currentRoomNumber].OnMapPositionY;
				roomY < rooms[currentRoomNumber].OnMapPositionY + rooms[currentRoomNumber].MapHeight;
				roomY++)
			{
				for (int roomX = rooms[currentRoomNumber].OnMapPositionX;
					roomX < rooms[currentRoomNumber].OnMapPositionX + rooms[currentRoomNumber].MapWidth;
					roomX++)
				{
					map[roomX + roomY * _width] = rooms[currentRoomNumber].Number;
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
						Number = ++currentRoomNumber,
						MapWidth = roomWidth,
						MapHeight = roomHeight,
						OnMapPositionX = x,
						OnMapPositionY = y,
					});

					// create new room
					for (int roomY = rooms[currentRoomNumber].OnMapPositionY;
						roomY < rooms[currentRoomNumber].OnMapPositionY + rooms[currentRoomNumber].MapHeight;
						roomY++)
					{
						for (int roomX = rooms[currentRoomNumber].OnMapPositionX;
							roomX < rooms[currentRoomNumber].OnMapPositionX + rooms[currentRoomNumber].MapWidth;
							roomX++)
						{
							map[roomX + roomY * _width] = rooms[currentRoomNumber].Number;
						}
					}
				}
			}
			LogMap(map);
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

		private void LogMatrix(int[,] matrix)
		{
			Debug.Log("Matrix: ");
			Debug.Log("   " + String.Join(" ", Enumerable.Range(0, matrix.GetLength(0))));
			for (int j = 0; j < matrix.GetLength(0); j++)
			{
				string line = j + " ";
				for (int i = 0; i < matrix.GetLength(1); i++)
				{
					line += matrix[i, j] + " ";
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