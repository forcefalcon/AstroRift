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

    void computeDate(){
//        1996 Oct. 1    = J96A1
//        2001 Oct. 22   = K01AM
        if(epoch.size() != 5)
            return;

        int YY = atoi(epoch.substr(1, 2).c_str());
        if(YY > 20){
            yearOfDiscovery = 1900+YY;
        }else{
            yearOfDiscovery = 2000+YY;
        }
    }

    int yearOfDiscovery;
};

#endif // ASTEROID_H
