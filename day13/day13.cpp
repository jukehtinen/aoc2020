#include <algorithm>
#include <fstream>
#include <iostream>
#include <string>
#include <vector>
#include <sstream>

std::vector<std::string> split(const std::string& s, char delimiter)
{
	std::vector<std::string> tokens;
	std::string token;
	std::istringstream tokenStream(s);
	while (std::getline(tokenStream, token, delimiter))
	{
		tokens.push_back(token);
	}
	return tokens;
}

int main()
{
	// Part 1
	std::ifstream file("input.txt");
	std::string line;

	std::getline(file, line);
	int target = std::atoi(line.c_str());
	std::getline(file, line);
	const auto tokens = split(line, ',');

	int best = 9999;
	int result = 0;
	for (const auto& t : tokens)
	{
		if (t == "x") continue;

		auto time = std::atoi(t.c_str());

		auto near = std::ceil((double)target / (double)time) * time;
		auto wait = near - target;
		if (wait < best)
		{
			best = wait;
			result = time * wait;
		}
	}

	std::cout << "Part 1 " << result << "\n";

	// Part 2
	std::vector<int> ids(tokens.size());
	for (int i = 0; i < tokens.size(); i++)
		ids[i] = tokens[i] == "x" ? 1 : std::atoi(tokens[i].c_str());

	int64_t ts = 0;
	int64_t n = ids[0];
	for (int i = 1; i < ids.size(); i++)
	{
		while (true)
		{
			if (ids[i] == 1)
				break;

			ts += n;
			if ((ts + i) % ids[i] == 0)
			{
				n *= ids[i];
				break;
			}
		}
	}

	std::cout << "Part 2 " << ts << "\n";
}
