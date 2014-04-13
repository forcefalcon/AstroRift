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

#ifdef __cplusplus
}
#endif

#endif /* _ORBITSDATA_H_ */