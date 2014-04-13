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
    Filter filter{Filter::YearOfDiscovery, Filter::EQUAL, 2008};

    db.loadPolicy(filter);
    db.loadFromJSON(jsonFilename);

//    db.filterAndErase(filter);

    cout << "Created "<< db.size() << " asteroids." << endl;

    db.print();

    return 0;
}

