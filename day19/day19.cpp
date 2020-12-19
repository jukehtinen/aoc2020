#include <algorithm>
#include <fstream>
#include <iostream>
#include <map>
#include <string>
#include <sstream>
#include <regex>

std::vector<std::string> split(const std::string& s, char delimiter)
{
	std::vector<std::string> tokens;
	std::string token;
	std::istringstream tokenStream(s);
	while (std::getline(tokenStream, token, delimiter))
	{
		if (token.length() != 0)
			tokens.push_back(token);
	}
	return tokens;
}

std::string build(const std::map<int, std::string>& rules, const std::string& r)
{
	std::string out;
	if (r.find('|') != std::string::npos)
	{
		out += "(";
		for (const auto& t : split(r, '|'))
		{
			out += build(rules, t);
			out += "|";
		}
		out.pop_back();
		out += ")";
	}
	else if (r.find('"') != std::string::npos)
	{
		out = r;
		out.erase(std::remove(out.begin(), out.end(), '\"'), out.end());
	}
	else
	{
		for (const auto& t : split(r, ' '))
		{
			auto rule = std::atoi(t.c_str());
			out += build(rules, rules.at(rule));
		}
	}
	return out;
}

int main()
{
	std::map<int, std::string> rules;

	std::ifstream file("input.txt");
	std::string line;
	while (std::getline(file, line) && line.length() != 0)
	{
		std::smatch m;
		std::regex_search(line, m, std::regex("(\\d+): (.+)"));
		rules[std::atoi(m[1].str().c_str())] = m[2];
	}

	// Part 1
	const auto& r = rules[0];
	std::regex rx("^" + build(rules, r) + "$");
	auto matches = 0;
	auto filepos = file.tellg();
	while (std::getline(file, line))
	{
		if (std::regex_match(line, rx))
			matches++;
	}
	std::cout << "Part 1 " << matches << "\n";

	// Part 2
	// hax solution - unroll the loops few times to extra rules...
	for (int i = 0; i < 20; i++)
	{
		rules[1000 + i] = " 42 | 42 " + std::to_string(1000 + i + 1);
		rules[1100 + i] = " 42 31 | 42 " + std::to_string(1100 + i + 1) + " 31";
	}
	rules[1020] = " 42";
	rules[1120] = " 42 31";
	rules[8] = " 42 | 42 1000";
	rules[11] = " 42 31 | 42 1100 31";

	const auto& r2 = rules[0];
	std::regex rx2("^" + build(rules, r2) + "$");
	matches = 0;
	file.clear();
	file.seekg(filepos, std::ios_base::beg);
	while (std::getline(file, line))
	{
		if (std::regex_match(line, rx2))
			matches++;
	}
	std::cout << "Part 2 " << matches << "\n";
}
