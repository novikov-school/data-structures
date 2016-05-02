#include <iostream>
#include <algorithm>
#include <list>

int main()
{
	std::list<int> list;
	list.push_back(3);
	list.push_back(5);
	list.push_back(7);

	std::for_each(list.begin(), list.end(), [](int value) {
		std::cout << value << std::endl;
	});
}