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
		int sum = 0;
		std::set<char> answers;
		while (std::getline(file, line))
		{
			if (line.size() != 0)
			{
				for (int i = 0; i < line.size(); i++)
				{
					answers.insert(line[i]);
				}
			}
			else
			{
				sum += answers.size();
				answers.clear();
			}
		}

		sum += answers.size();
		std::cout << "Part 1 " << sum << "\n";
	}

	// Part 2
	{
		std::ifstream file("input.txt");
		std::string line;
		int sum = 0;
		auto a = { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };
		std::set<char> allAnswers = a;
		while (std::getline(file, line))
		{
			if (line.size() != 0)
			{
				for (auto i = allAnswers.begin(); i != allAnswers.end();)
				{
					if (line.find(*i) != std::string::npos)
						i++;
					else
						i = allAnswers.erase(i);
				}
			}
			else
			{
				sum += allAnswers.size();
				allAnswers = a;
			}
		}

		sum += allAnswers.size();
		std::cout << "Part 2 " << sum << "\n";
	}
}
