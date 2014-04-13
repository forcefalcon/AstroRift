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
static const char* jsonFilename = "mpcorb.dat.json";

int main()
{
    AsteroidDatabase db;
    Filter filter{Filter::Magnitude, Filter::LESSER, 10};

//    db.loadPolicy(filter);
    db.loadFromJSON(jsonFilename);
    db.filterAndErase(filter);

    cout << "Created "<< db.size() << " asteroids." << endl;


    return 0;
}

