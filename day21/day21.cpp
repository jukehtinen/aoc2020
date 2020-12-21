#include <algorithm>
#include <fstream>
#include <iostream>
#include <map>
#include <set>
#include <string>
#include <sstream>
#include <regex>
#include <vector>

std::string trim(const std::string& str)
{
	size_t first = str.find_first_not_of(' ');
	if (std::string::npos == first)
	{
		return str;
	}
	size_t last = str.find_last_not_of(' ');
	return str.substr(first, (last - first + 1));
}

std::vector<std::string> split(const std::string& s, char delimiter)
{
	std::vector<std::string> tokens;
	std::string token;
	std::istringstream tokenStream(s);
	while (std::getline(tokenStream, token, delimiter))
	{
		if (token.length() != 0)
			tokens.push_back(trim(token));
	}
	return tokens;
}

int main()
{
	std::map<std::string, std::vector<std::string>> allergMapping;
	std::vector<std::string> lines;
	std::ifstream file("input.txt");
	std::string line;
	while (std::getline(file, line))
	{
		std::smatch m;
		std::regex_search(line, m, std::regex("([\\w\\s]+)\\(contains ([\\w\\s,]+)\\)"));

		const auto allergs = split(m[2], ',');
		for (auto& a : allergs)
		{
			allergMapping[a].push_back(m[1]);
		}
		lines.push_back(m[1]);
	}

	std::map<std::string, std::string> hits;
	for (const auto& a : allergMapping)
	{
		std::map<std::string, int> occurs;
		for (const auto& ing : a.second)
		{
			auto items = split(ing, ' ');
			for (const auto& i : items)
			{
				if (hits.find(i) != hits.end())
					continue;

				occurs[i]++;
			}
		}
		auto max = std::max_element(occurs.begin(), occurs.end(), [](const auto& a, const auto& b) -> bool { return a.second < b.second; });
		hits[max->first] = a.first;
	}

	int count = 0;
	for (const auto& l : lines)
	{
		auto items = split(l, ' ');
		for (const auto& i : items)
		{
			if (hits.find(i) != hits.end())
				continue;
			count++;
		}
	}
	std::cout << "Part 1 " << count << "\n";

	// Part 2
	std::set<std::string> done;
	std::map<std::string, std::string> results;
	while (results.size() != allergMapping.size())
	{
		for (const auto& a : allergMapping)
		{
			std::set<std::string> uniq;
			for (const auto& ing : a.second)
			{
				for (const auto& t : split(ing, ' '))
				{
					if (done.find(t) != done.end()) continue;

					bool found = true;
					for (const auto& ing2 : a.second)
					{
						auto toks2 = split(ing2, ' ');

						if (std::find_if(toks2.begin(), toks2.end(), [t](const std::string& t2) { return t == t2; }) == toks2.end())
							found = false;
					}
					if (found)
						uniq.insert(t);
				}
			}
			if (uniq.size() == 1)
			{
				results[a.first] = *uniq.begin();
				done.insert(*uniq.begin());
			}
		}
	}

	std::string out;
	for (const auto& v : results)
	{
		out += v.second + ",";
	}
	out.pop_back();
	std::cout << "Part 2 " << out << "\n";
}
