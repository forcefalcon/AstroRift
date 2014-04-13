#ifndef ASTEROID_H
#define ASTEROID_H

#include <string>
#include "picojson.h"
#include "orbitsData.h"

class Asteroid
{
    enum Type {
    };

public:
    Asteroid();
    Asteroid(picojson::value const & jsonItem);
    void print();

    std::string designation;
    std::string epoch;
    std::string name;
    std::string reference;

    double magnitude;
    double slope;
    double epochAnomaly;
    double perihelion;
    double ascendingNode;
    double inclination;
    double eccentricity;
    double meanDailyMotion;
    double semiMajorAxis;

    double numObservations;

    void computeDate();
    int yearOfDiscovery;
};

#endif // ASTEROID_H
