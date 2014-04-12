#include "asteroid.h"

#include <iostream>

Asteroid::Asteroid()
{
}

void Asteroid::print()
{
    std::cout << "Asteroid: " << designation << std::endl;
}
