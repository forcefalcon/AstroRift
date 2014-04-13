#ifndef _ORBITSDATA_H_
#define _ORBITSDATA_H_

#ifdef __cplusplus
extern "C" {
#endif

typedef struct coordinates {
	double idx;
	double H;
	double x;
	double y;
	double z;
	double r;
} s_coordinates;

__declspec(dllimport) int initializeDatabase(unsigned int timestamp);
__declspec(dllimport) s_coordinates * getOrbits(unsigned int timestamp);
__declspec(dllimport) s_coordinates * getPlanetsOrbits(unsigned int timestamp);
__declspec(dllimport) void uninitializeDatabase();

#ifdef __cplusplus
}
#endif

#endif /* _ORBITSDATA_H_ */