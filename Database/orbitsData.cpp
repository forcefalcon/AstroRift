// orbitsData.cpp : définit les fonctions exportées pour l'application DLL.
//

#include "stdafx.h"
#include "orbitsData.h"
#include <cmath>
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
static const char* jsonFilename = "C:/Users/Kharth/Documents/Visual Studio 2010/Projects/testDLL/Debug/mpcorb.dat.json";


#ifdef __cplusplus
extern "C" {
#endif

static s_coordinates * planetsCoordinates = NULL;
static s_coordinates * asteroidsCoordinates = NULL;
static int step = 0;
static unsigned int start = 0;
static AsteroidDatabase db;
static int dbIsNotInit = 1;

__declspec(dllexport) int initializeDatabase(unsigned int timestamp) {
	step = 1;
	start = 0;
	int result = -1;

	if (dbIsNotInit) {
		Filter filter(Filter::Magnitude, Filter::LESSER, 10);

		//db.loadPolicy(filter);
		db.loadFromJSON(jsonFilename);
		//db.filterAndErase(filter);
		dbIsNotInit = 0;
		result = db.size();
	}
	planetsCoordinates = new s_coordinates[8];
	asteroidsCoordinates = new s_coordinates[db.size()];
	start = timestamp;
	return result;
}

__declspec(dllexport) s_coordinates * getOrbits(unsigned int timestamp) {
	double d = timestamp / 86400;
	int j = 0;
	for (auto i = db.begin(); i != db.end(); i++) {
		get_coord(*(i->second),0,d,asteroidsCoordinates[j]);
		j++;
	}

	return asteroidsCoordinates;
}

__declspec(dllexport) s_coordinates * getPlanetsOrbits(unsigned int timestamp) {
	/* int diff = (timestamp - start) / 86400;
	step = (timestamp % 360) + 1;

	for (int i = 0; i < 8; i++) {
		double degInRad = (180/3.14) * (double) timestamp;

		planetsCoordinates[i].x = cos(degInRad) * planetsCoordinates[i].r;
		planetsCoordinates[i].y = sin(degInRad) * planetsCoordinates[i].r; 
	}
	*/
	double d = timestamp / 86400;
	for (int i = 0; i < 8; i++) {
		Asteroid r;
		get_coord(r,i+1,d,planetsCoordinates[i]);
	}

	return planetsCoordinates;
}

__declspec(dllexport) void uninitializeDatabase() {
	if (planetsCoordinates) {
		delete[] planetsCoordinates;
		planetsCoordinates = 0;
	}
	if (asteroidsCoordinates) {
		delete[] asteroidsCoordinates;
		asteroidsCoordinates = 0;
	}
	step = 0;
	start = 0;
}

#ifdef __cplusplus
}
#endif