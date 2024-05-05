﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace MUDMapBuilder
{
	public static class MapBuilder
	{
		public static MMBGrid BuildGrid(IMMBRoom[] sourceRooms, int? maxSteps = null)
		{
			var toProcess = new List<MMBRoom>();
			var firstRoom = new MMBRoom(sourceRooms[0])
			{
				Position = new Point(0, 0)
			};
			toProcess.Add(firstRoom);

			var rooms = new RoomsCollection
			{
				firstRoom
			};

			// First run: we move through room's exits, assigning each room a 2d coordinate
			// If there are overlaps, then we expand the grid in the direction of movement
			var step = 1;

			Point pos;
			while (toProcess.Count > 0 && (maxSteps == null || maxSteps.Value > step))
			{
				var room = toProcess[0];
				toProcess.RemoveAt(0);

				var exitDirs = room.Room.ExitsDirections;
				for (var i = 0; i < exitDirs.Length; ++i)
				{
					var exitDir = exitDirs[i];
					var exitRoom = room.Room.GetRoomByExit(exitDir);
					if (rooms.GetRoomById(exitRoom.Id) != null)
					{
						continue;
					}

					pos = room.Position;
					var delta = exitDir.GetDelta();
					var newPos = new Point(pos.X + delta.X, pos.Y + delta.Y);

					while (true)
					{
						// Check if this pos is used already
						if (rooms.GetRoomByPosition(newPos) == null)
						{
							break;
						}

						rooms.PushRoom(newPos, exitDir);
					}

					var mbRoom = new MMBRoom(exitRoom)
					{
						Position = newPos
					};
					toProcess.Add(mbRoom);
					rooms.Add(mbRoom);
				}

				++step;
			}

			// Second run: if it is possible to place interconnected rooms with single exits next to each other, do it
			foreach (var room in rooms)
			{
				pos = room.Position;
				var exitDirs = room.Room.ExitsDirections;
				for (var i = 0; i < exitDirs.Length; ++i)
				{
					var exitDir = exitDirs[i];
					var exitRoom = room.Room.GetRoomByExit(exitDir);

					var targetRoom = rooms.GetRoomById(exitRoom.Id);
					if (targetRoom == null)
					{
						continue;
					}

					var delta = exitDir.GetDelta();
					var desiredPos = new Point(pos.X + delta.X, pos.Y + delta.Y);
					if (targetRoom.Position == desiredPos)
					{
						// The target room is already at desired pos
						continue;
					}

					if (exitRoom.ExitsDirections.Length > 1)
					{
						continue;
					}

					var targetPos = targetRoom.Position;

					// Check if the spot is free
					var usedByRoom = rooms.GetRoomByPosition(desiredPos);
					if (usedByRoom != null)
					{
						// Spot is occupied
						continue;
					}

					// Place target room next to source
					targetRoom.Position = desiredPos;
				}
			}

			// Third run: Try to make the map more compact
			for (var it = 0; it < 10; ++it)
			{
				// 10 compact iterations
				foreach (var room in rooms)
				{
					pos = room.Position;
					var exitDirs = room.Room.ExitsDirections;
					for (var i = 0; i < exitDirs.Length; ++i)
					{
						var exitDir = exitDirs[i];
						var exitRoom = room.Room.GetRoomByExit(exitDir);
						var targetRoom = rooms.GetRoomById(exitRoom.Id);
						if (targetRoom == null)
						{
							continue;
						}

						var delta = exitDir.GetDelta();
						var desiredPos = new Point(pos.X + delta.X, pos.Y + delta.Y);
						if (targetRoom.Position == desiredPos)
						{
							// The target room is already at desired pos
							continue;
						}

						// Skip if direction is Up/Down or the target room isn't straighly positioned
						var steps = 0;
						switch (exitDir)
						{
							case MMBDirection.North:
							case MMBDirection.South:
								if (targetRoom.Position.X - pos.X != 0)
								{
									continue;
								}

								steps = Math.Abs(targetRoom.Position.Y - pos.Y) - 1;
								break;
							case MMBDirection.West:
							case MMBDirection.East:
								if (targetRoom.Position.Y - pos.Y != 0)
								{
									continue;
								}

								steps = Math.Abs(targetRoom.Position.X - pos.X) - 1;
								break;
							case MMBDirection.Up:
							case MMBDirection.Down:
								// Skip Up/Down for now
								continue;
						}

						while (steps > 0)
						{
							var clone = rooms.Clone();
							var targetRoomClone = targetRoom.Clone();
							clone.PullRoom(targetRoomClone, exitDir.GetOppositeDirection(), steps);

							if (!clone.HasOverlaps())
							{
								rooms = clone;
								goto nextiter;
							}

							--steps;
						}
					}
				}

			nextiter:;
			}

			// Determine minimum point
			var min = new Point();
			var minSet = false;
			foreach (var room in rooms)
			{
				pos = room.Position;
				if (!minSet)
				{
					min = new Point(pos.X, pos.Y);
					minSet = true;
				}

				if (pos.X < min.X)
				{
					min.X = pos.X;
				}

				if (pos.Y < min.Y)
				{
					min.Y = pos.Y;
				}
			}

			// Shift everything so it begins from 0,0
			foreach (var room in rooms)
			{
				pos = room.Position;

				pos.X -= min.X;
				pos.Y -= min.Y;

				room.Position = pos;
			}

			// Determine size
			Point max = new Point(0, 0);
			foreach (var room in rooms)
			{
				pos = room.Position;

				if (pos.X > max.X)
				{
					max.X = pos.X;
				}

				if (pos.Y > max.Y)
				{
					max.Y = pos.Y;
				}
			}

			++max.X;
			++max.Y;

			var grid = new MMBGrid(max, step);
			for (var x = 0; x < max.X; ++x)
			{
				for (var y = 0; y < max.Y; ++y)
				{
					var room = rooms.GetRoomByPosition(new Point(x, y));
					if (room == null)
					{
						continue;
					}

					grid[x, y] = room;
				}
			}

			return grid;
		}
	}

	internal static class MapBuilderExtensions
	{
		public static Point GetDelta(this MMBDirection direction)
		{
			switch (direction)
			{
				case MMBDirection.East:
					return new Point(1, 0);
				case MMBDirection.West:
					return new Point(-1, 0);
				case MMBDirection.North:
					return new Point(0, -1);
				case MMBDirection.South:
					return new Point(0, 1);
				case MMBDirection.Up:
					return new Point(1, -1);
				case MMBDirection.Down:
					return new Point(-1, 1);
			}

			throw new Exception($"Unknown direction {direction}");
		}

		public static MMBDirection GetOppositeDirection(this MMBDirection direction)
		{
			switch (direction)
			{
				case MMBDirection.East:
					return MMBDirection.West;
				case MMBDirection.West:
					return MMBDirection.East;
				case MMBDirection.North:
					return MMBDirection.South;
				case MMBDirection.South:
					return MMBDirection.North;
				case MMBDirection.Up:
					return MMBDirection.Down;
				default:
					return MMBDirection.Up;
			}
		}
	}
}