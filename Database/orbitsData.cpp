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
static const char* jsonFilename = "mpcorb.dat.json";


#ifdef __cplusplus
extern "C" {
#endif

static s_coordinates * planetsCoordinates = NULL;
static int step = 1;
static unsigned int start = 0;

__declspec(dllexport) int initializeDatabase(unsigned int timestamp) {
	step = 1;
	start = 0;

	AsteroidDatabase db;
    Filter filter{Filter::Magnitude, Filter::LESSER, 10};

	//db.loadPolicy(filter);
    db.loadFromJSON(jsonFilename);
    db.filterAndErase(filter);

	if (!planetsCoordinates) {
		planetsCoordinates = new s_coordinates[8];

		planetsCoordinates[0].r = 0.39;
		planetsCoordinates[0].x = 0.39;
		planetsCoordinates[0].y = 0.0;
		planetsCoordinates[0].z = 0.0;

		planetsCoordinates[1].r = 0.72;
		planetsCoordinates[1].x = 0.72;
		planetsCoordinates[1].y = 0.0;
		planetsCoordinates[1].z = 0.0;

		planetsCoordinates[2].r = 1.0;
		planetsCoordinates[2].x = 0.0;
		planetsCoordinates[2].y = 0.0;
		planetsCoordinates[2].z = 0.0;

		planetsCoordinates[3].r = 1.52;
		planetsCoordinates[3].x = 0.0;
		planetsCoordinates[3].y = 0.0;
		planetsCoordinates[3].z = 0.0;

		planetsCoordinates[4].r = 5.20;
		planetsCoordinates[4].x = 0.0;
		planetsCoordinates[4].y = 0.0;
		planetsCoordinates[4].z = 0.0;

		planetsCoordinates[5].r = 9.58;
		planetsCoordinates[5].x = 0.0;
		planetsCoordinates[5].y = 0.0;
		planetsCoordinates[5].z = 0.0;

		planetsCoordinates[6].r = 19.23;
		planetsCoordinates[6].x = 0.0;
		planetsCoordinates[6].y = 0.0;
		planetsCoordinates[6].z = 0.0;

		planetsCoordinates[7].r = 30.10;
		planetsCoordinates[7].x = 0.0;
		planetsCoordinates[7].y = 0.0;
		planetsCoordinates[7].z = 0.0;
	}
	start = timestamp;
	return 0;
}

__declspec(dllexport) s_coordinates * getOrbits(unsigned int timestamp) {
	return NULL;
}

__declspec(dllexport) s_coordinates * getPlanetsOrbits(unsigned int timestamp) {
	int diff = (timestamp - start) / 86400;
	step = (timestamp % 360) + 1;

	for (int i = 0; i < 8; i++) {
		double degInRad = (180/3.14) * (double) timestamp;

		planetsCoordinates[i].x = cos(degInRad) * planetsCoordinates[i].r;
		planetsCoordinates[i].y = sin(degInRad) * planetsCoordinates[i].r; 
	}

	return planetsCoordinates;
}

__declspec(dllexport) void uninitializeDatabase() {
	if (planetsCoordinates) {
		delete[] planetsCoordinates;
		planetsCoordinates = 0;
	}
	step = 0;
	start = 0;
}

#ifdef __cplusplus
}
#endif