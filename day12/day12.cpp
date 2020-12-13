#include <fstream>
#include <iostream>
#include <set>
#include <string>

int main()
{
	// Part 1
	{
		std::ifstream file("input.txt");
		std::string line;
		int x = 0;
		int y = 0;
		int dir = 0;
		while (std::getline(file, line))
		{
			auto act = line[0];
			auto value = std::atoi(line.c_str() + 1);
			if (act == 'N') y -= value;
			else if (act == 'S') y += value;
			else if (act == 'W') x -= value;
			else if (act == 'E') x += value;

			else if (act == 'L' || act == 'R')
			{
				if (value == 90)
				{
					dir += act == 'L' ? -1 : 1;
					if (dir < 0) dir = 3;
					if (dir > 3) dir = 0;
				}
				else if (value == 270)
				{
					dir += act == 'L' ? 1 : -1;
					if (dir < 0) dir = 3;
					if (dir > 3) dir = 0;
				}
				else
				{
					dir += 2;
					dir %= 4;
				}
			}
			else if (act == 'F')
			{
				if (dir == 0 || dir == 2)
					x = dir == 0 ? x + value : x - value;
				else if (dir == 1 || dir == 3)
					y = dir == 1 ? y + value : y - value;
			}
		}
		std::cout << "Part 1 " << std::abs(x) + std::abs(y) << "\n";
	}

	// Part 2
	{
		std::ifstream file("input.txt");
		std::string line;
		int waypointx = 10;
		int waypointy = -1;
		int shipx = 0;
		int shipy = 0;
		while (std::getline(file, line))
		{
			auto act = line[0];
			auto value = std::atoi(line.c_str() + 1);
			if (act == 'N') waypointy -= value;
			else if (act == 'S') waypointy += value;
			else if (act == 'W') waypointx -= value;
			else if (act == 'E') waypointx += value;
			else if (act == 'L')
			{
				int newx = 0;
				int newy = 0;
				if (value == 90)
				{
					newx = waypointx * 0 + waypointy * 1;
					newy = waypointx * -1 + waypointy * 0;
				}
				else if (value == 180)
				{
					newx = waypointx * -1 + waypointy * 0;
					newy = waypointx * 0 + waypointy * -1;
				}
				else if (value == 270)
				{
					newx = waypointx * 0 + waypointy * -1;
					newy = waypointx * 1 + waypointy * 0;
				}
				waypointx = newx;
				waypointy = newy;
			}
			else if (act == 'R')
			{
				int newx = 0;
				int newy = 0;
				if (value == 90)
				{
					newx = waypointx * 0 + waypointy * -1;
					newy = waypointx * 1 + waypointy * 0;
				}
				else if (value == 180)
				{
					newx = waypointx * -1 + waypointy * 0;
					newy = waypointx * 0 + waypointy * -1;
				}
				else if (value == 270)
				{
					newx = waypointx * 0 + waypointy * 1;
					newy = waypointx * -1 + waypointy * 0;
				}
				waypointx = newx;
				waypointy = newy;
			}
			else if (act == 'F')
			{
				shipx += waypointx * value;
				shipy += waypointy * value;
			}
		}
		std::cout << "Part 2 " << std::abs(shipx) + std::abs(shipy) << "\n";
	}
}
