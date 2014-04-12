#ifndef ASTEROID_H
#define ASTEROID_H

#include <string>

class Asteroid
{
    enum Type {
    };

public:
    Asteroid();
    void print();

    std::string designation;
    std::string epoch;

    float magnitude;
    float slope;
    float epochAnomaly;
    float perihelion;
    float ascendingNode;
    float inclination;
    float eccentricity;
    float meanDailyMotion;
    float semiMajorAxis;
};

#endif // ASTEROID_H
