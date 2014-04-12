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

int main()
{
    AsteroidDatabase db;
    db.loadPolicy( {Filter::Magnitude, Filter::LESSER, 20.} );
    db.loadFromFile(filename);

    cout << "Created "<< db.size() << " asteroids." << endl;

    db.print("b1319");

    return 0;
}

