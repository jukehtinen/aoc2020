#include <algorithm>
#include <fstream>
#include <iostream>
#include <vector>
#include <string>

int main()
{
	std::ifstream file("input.txt");

	// Part 1
	int highestId = 0;
	std::string line;
	std::vector<int> seats;
	while (std::getline(file, line))
	{
		int lowRow = 0;
		int highRow = 128;
		int lowSeat = 0;
		int highSeat = 8;
		for (int i = 0; i < line.size(); i++)
		{
			auto c = line[i];
			if (c == 'F')
				highRow = lowRow + (highRow - lowRow) / 2;
			if (c == 'B')
				lowRow = lowRow + (highRow - lowRow) / 2;
			if (c == 'L')
				highSeat = lowSeat + (highSeat - lowSeat) / 2;
			if (c == 'R')
				lowSeat = lowSeat + (highSeat - lowSeat) / 2;
		}
		int id = (lowRow * 8 + lowSeat);
		highestId = std::max(highestId, id);
		seats.push_back(id);
	}

	std::cout << "Part 1 " << highestId << "\n";

	// Part 2
	std::sort(std::begin(seats), std::end(seats));
	for (int i = seats[0]; i < seats.size() - 1; i++)
	{
		if (seats[i] + 1 != seats[i + 1])
		{
			std::cout << "Part 2 " << seats[i] + 1 << "\n";
			break;
		}
	}
}
