#include <iostream>
#include <fstream>
#include <map>
#include <utility>
#include <sstream>
#include <regex>

#include "asteroid.h"
#include "asteroiddatabase.h"

using namespace std;

static const char* filename = "mpcorb.dat";

void readCsvDatabase(char const* filename, AsteroidDatabase &db)
{
    ifstream f;
    static float expected=637000.;
    int current=0;

    f.open(filename);
    string pdes;

    float dummy=0;
    string provDesignation, line, epoch;

    while(getline(f, line)){
        current++;
        if(current%100 == 0)
            printf("\rLoading: %.1f%%", (current/expected)*100);

        istringstream iss(line);
        Asteroid *a = new Asteroid;

        iss >> a->designation >> a->magnitude
            >> a->slope >> a->epoch
            >> a->epochAnomaly >> a->perihelion
            >> a->ascendingNode >> a->inclination
            >> a->eccentricity >> a->meanDailyMotion
            >> a->semiMajorAxis >> dummy;

        db.insert(a);
    }

    printf("\rLoading finished\n");

    f.close();
}


int main()
{
    AsteroidDatabase db;
    readCsvDatabase(filename, db);

    cout << "Created "<< db.size() << " asteroids." << endl;


    db.print("b1319");

    return 0;
}

