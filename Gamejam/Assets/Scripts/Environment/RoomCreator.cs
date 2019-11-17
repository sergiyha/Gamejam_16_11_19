using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Environment
{

	public class Room
	{
		public int MapWidth;
		public int MapHeight;
		public int Number;
		public int OnMapPositionX;
		public int OnMapPositionY;

		public List<int> AdjacentRooms;
		public List<int> DoorToRooms;
		public Door[] Doors;

		public GameObject GameObject;
		public Vector3 StartPosition;
		public Vector3 SceneSize;
		public List<Vector3> DoorsPosition;
	}

	public class Door
	{
		public Room[] Rooms;
		public Vector2Int[] PositionOnTile;
		public Vector3[] Position;
	}

	public class RoomCreator : MonoBehaviour
	{
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private float _tileSize;

		[SerializeField] private FloorManager _floorManager;
		[SerializeField] private WallManager _wallManager;
		[SerializeField] private GameObject _corner;
		[SerializeField] private DoorManager _doorManager;

		[Flags]
		public enum TileType
		{
			Floor,
			Wall,
			Corner,
			Door
		}

		/*private void Start()
		{
			Build(_width, _height, Vector3.zero, parent);
		}*/

		public void Build(int tilesPerRoom, Vector3 mapPosition, Room room, List<Room> rooms, int[] map,
			Transform parent)
		{
			int width = room.MapWidth * tilesPerRoom;
			int height = room.MapHeight * tilesPerRoom;

			room.StartPosition = mapPosition + new Vector3(room.OnMapPositionX * tilesPerRoom * _tileSize, 0,
				                     -room.OnMapPositionY * tilesPerRoom * _tileSize);
			room.SceneSize = new Vector3(room.MapWidth * tilesPerRoom * _tileSize, 0,
				-room.MapHeight * tilesPerRoom * _tileSize);


			// TODO: door position, not corner, connection direction
			if (room.Doors == null)
			{
				room.Doors = new Door[room.DoorToRooms.Count];
			}
			for (int i = 0; i < room.DoorToRooms.Count; i++)
			{
				if (room.Doors[i] != null)	// already filled from another room
				{
					continue;
				}
				var connectedRoom = rooms[room.DoorToRooms[i]];


				Vector2Int doorOnTiles = Vector2Int.zero;
				Vector2Int connectedDoorOnTiles = Vector2Int.zero;

				// find common wall
				//// find common side
				//// find common tiles
				if (room.OnMapPositionY >= connectedRoom.OnMapPositionY + connectedRoom.MapHeight)
				{
					// current room is below! door must be on top
					var commonSideBeginX = room.OnMapPositionX > connectedRoom.OnMapPositionX
						? room.OnMapPositionX : connectedRoom.OnMapPositionX;
					var roomRightX = room.OnMapPositionX + room.MapWidth;
					var connectedRightX = connectedRoom.OnMapPositionX + connectedRoom.MapWidth;
					var commonSideEndX = roomRightX < connectedRightX ? roomRightX : connectedRightX;

					var commonSideWidthInTiles = Math.Abs(commonSideEndX - commonSideBeginX);
					var randomTile = Random.Range(1, commonSideWidthInTiles * tilesPerRoom - 1);
					// convert to tiles position
					doorOnTiles = new Vector2Int((commonSideBeginX - room.OnMapPositionX) * tilesPerRoom + randomTile,
						0);
					connectedDoorOnTiles = new Vector2Int((commonSideBeginX - connectedRoom.OnMapPositionX) * tilesPerRoom + randomTile,
						connectedRoom.MapHeight * tilesPerRoom - 1);
					/*doorOnTiles = new Vector2Int((commonSideBeginX - room.OnMapPositionY) * tilesPerRoom + randomTile,
						room.OnMapPositionY * tilesPerRoom - 1);
					connectedDoorOnTiles = new Vector2Int(randomTile,
						connectedRoom.OnMapPositionY * tilesPerRoom - 1);*/

				}
				else if (room.OnMapPositionX >= connectedRoom.OnMapPositionX + connectedRoom.MapWidth)
				{
					// current room is right from connected, door on left side

					var commonSideBeginY = room.OnMapPositionY > connectedRoom.OnMapPositionY
						? room.OnMapPositionY : connectedRoom.OnMapPositionY;
					var roomBottomY = room.OnMapPositionY + room.MapHeight;
					var connectedBottomY = connectedRoom.OnMapPositionY + connectedRoom.MapHeight;
					var commonSideEndY = roomBottomY < connectedBottomY ? roomBottomY : connectedBottomY;

					var commonSideWidthInTiles = Math.Abs(commonSideEndY - commonSideBeginY);
					var randomTile = Random.Range(1, commonSideWidthInTiles * tilesPerRoom - 1);
					//var randomTile = Random.Range(commonSideBeginY * tilesPerRoom + 1 , commonSideEndY * tilesPerRoom - 1);
					// convert to tiles position
					doorOnTiles = new Vector2Int(0,
						(commonSideBeginY - room.OnMapPositionY ) * tilesPerRoom + randomTile);
					connectedDoorOnTiles = new Vector2Int(connectedRoom.MapWidth * tilesPerRoom - 1,
						(commonSideBeginY - connectedRoom.OnMapPositionY) * tilesPerRoom + randomTile);

					/*doorOnTiles = new Vector2Int( room.OnMapPositionX * tilesPerRoom - 1,
						randomTile);
					connectedDoorOnTiles = new Vector2Int(connectedRoom.OnMapPositionX * tilesPerRoom - 1,
						randomTile);*/
				}
				else if (room.OnMapPositionX + room.MapWidth <= connectedRoom.OnMapPositionX)
				{
					// current room is left from connected, door on right side

					var commonSideBeginY = room.OnMapPositionY > connectedRoom.OnMapPositionY
						? room.OnMapPositionY : connectedRoom.OnMapPositionY;
					var roomBottomY = room.OnMapPositionY + room.MapHeight;
					var connectedBottomY = connectedRoom.OnMapPositionY + connectedRoom.MapHeight;
					var commonSideEndY = roomBottomY < connectedBottomY ? roomBottomY : connectedBottomY;

					var commonSideWidthInTiles = Math.Abs(commonSideEndY - commonSideBeginY);
					var randomTile = Random.Range(1, commonSideWidthInTiles * tilesPerRoom - 1);
					//var randomTile = Random.Range(commonSideBeginY * tilesPerRoom + 1, commonSideEndY * tilesPerRoom - 1);
					// convert to tiles position
					doorOnTiles = new Vector2Int(room.MapWidth * tilesPerRoom - 1,
						(commonSideBeginY - room.OnMapPositionY) * tilesPerRoom + randomTile);
					connectedDoorOnTiles = new Vector2Int(0,
						(commonSideBeginY - connectedRoom.OnMapPositionY) * tilesPerRoom + randomTile);


					/*doorOnTiles = new Vector2Int((room.OnMapPositionX + room.MapWidth) * tilesPerRoom - 1,
						randomTile);
					connectedDoorOnTiles = new Vector2Int((connectedRoom.OnMapPositionX + connectedRoom.MapWidth) * tilesPerRoom - 1,
						randomTile);*/
				}
				else if (room.OnMapPositionY + room.MapHeight <= connectedRoom.OnMapPositionY)
				{
					// current room is on top from connected, door on bottom side

					var commonSideBeginX = room.OnMapPositionX > connectedRoom.OnMapPositionX
						? room.OnMapPositionX : connectedRoom.OnMapPositionX;
					var roomRightX = room.OnMapPositionX + room.MapWidth;
					var connectedRightX = connectedRoom.OnMapPositionX + connectedRoom.MapWidth;
					var commonSideEndX = roomRightX < connectedRightX ? roomRightX : connectedRightX;

					var commonSideWidthInTiles = Math.Abs(commonSideEndX - commonSideBeginX);
					var randomTile = Random.Range(1, commonSideWidthInTiles * tilesPerRoom - 1);
					//var randomTile = Random.Range(commonSideBeginX * tilesPerRoom + 1, commonSideEndX * tilesPerRoom - 1);
					// convert to tiles position
					doorOnTiles = new Vector2Int((commonSideBeginX - room.OnMapPositionX) * tilesPerRoom + randomTile,
						room.MapHeight * tilesPerRoom - 1);
					connectedDoorOnTiles = new Vector2Int((commonSideBeginX - connectedRoom.OnMapPositionX) * tilesPerRoom + randomTile,
						0);
				}
				else
				{
					Debug.LogError("Am I stupid?");
				}



				var newDoor = new Door
				{
					Rooms = new []{ room, connectedRoom },
					PositionOnTile = new[] {doorOnTiles, connectedDoorOnTiles},
				};

				room.Doors[i] = newDoor;
				if (connectedRoom.Doors == null)
				{
					connectedRoom.Doors = new Door[connectedRoom.DoorToRooms.Count];
				}

				var doorIndex = connectedRoom.DoorToRooms.FindIndex(f=> f == room.Number);
				connectedRoom.Doors[doorIndex] = newDoor;

			}

			var tiles = new TileType[width, height];
			var wall = _wallManager.GetWallPrefab();
			var door = _doorManager.GetDoorPrefab();
			GameObject border;

			Debug.Log($"Room {room.Number} will have {room.Doors.Length} rooms");
			for (int i = 0; i < room.Doors.Length; i++)
			{
				Debug.Log($"Door in room {room.Number} to rooms {String.Join(" ", room.Doors[i].Rooms.Select(s=>s.Number))} is drawn in positions {room.Doors[i].PositionOnTile[0]} and {room.Doors[i].PositionOnTile[1]}");
			}


			//var goToRemove = 
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Vector3 position = room.StartPosition + new Vector3(_tileSize * x, 0, -_tileSize * y);

					// flour
					tiles[x, y] |= TileType.Floor; 
					Instantiate(_floorManager.GetFloorPrefab(), position, Quaternion.identity, parent);


					// door
					bool isDoor = false;
					for (int i = 0; i < room.Doors.Length; i++)
					{
						var currentRoomIndex = Array.IndexOf(room.Doors[i].Rooms, room);
						var doorPosition = room.Doors[i].PositionOnTile[currentRoomIndex];
						if (doorPosition.x == x && doorPosition.y == y)
						{
							isDoor = true;
							Debug.Log($"Door in room {room.Number} to room {room.Doors[i].Rooms[Math.Abs(currentRoomIndex-1)].Number} is drawn in position {doorPosition}");
						}
					}

					border = isDoor ? door : wall;

                    if(isDoor)
                        continue;

					// wall
					if (y == 0)
					{
						if ((tiles[x, y] & TileType.Wall) != 0)
						{
							tiles[y, x] &= ~TileType.Wall;
							tiles[y, x] |= TileType.Corner;

						}
						Instantiate(border, position, Quaternion.Euler(0, -90, 0), parent);
					}

					if (x == 0)
					{
						if ((tiles[x, y] & TileType.Wall) != 0)
						{
							tiles[y, x] &= ~TileType.Wall;
							tiles[y, x] |= TileType.Corner;

						}
						Instantiate(border, position, Quaternion.Euler(0, 180, 0), parent);
					}

					if (y == height - 1)
					{
						if ((tiles[x, y] & TileType.Wall) != 0)
						{
							tiles[y, x] &= ~TileType.Wall;
							tiles[y, x] |= TileType.Corner;

						}
						Instantiate(border, position, Quaternion.Euler(0, 90, 0), parent);
					}

					if (x == width - 1)
					{
						if ((tiles[x, y] & TileType.Wall) != 0)
						{
							tiles[y, x] &= ~TileType.Wall;
							tiles[y, x] |= TileType.Corner;

						}
						Instantiate(border, position, Quaternion.identity, parent);
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

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{

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