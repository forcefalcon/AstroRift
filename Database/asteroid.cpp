#include "asteroid.h"

#include <iostream>

const double pi   = 3.141592653589793238463;
const double degs = 180 / pi;
const double rads = pi / 180;
const double eps  = 1.0e-12;

// return the integer part of a number
int abs_floor( double x ) {
    return ((x >= 0.0) ? (int) floor(x) : (int) ceil(x));
}

// day number to/from J2000 (Jan 1.5, 2000)
inline double day_number(int y, int m, int d, int hour, int mins) {
    double h = hour + mins / 60;
    return 367*y - 7*(y + (m + 9) / 12) / 4 + 275*m / 9 + d - 730531.5 + h / 24;
}

// return an angle in the range 0 to 2pi
// radians
inline double mod2pi( double x ) {
    double b = x / (2*pi);
    double a = (2*pi)*(b - abs_floor(b));
    
    return ((a < 0) ? (2*pi) + a : a);
}

Asteroid::Asteroid()
{
}

Asteroid::Asteroid(const picojson::value &jsonItem)
{
    try{
        if(jsonItem.get("readable_des").is<std::string>()){
            name = jsonItem.get("readable_des").get<std::string>();
        }
        if(jsonItem.get("epoch").is<std::string>()){
            epoch = jsonItem.get("epoch").get<std::string>();
        }
        if(jsonItem.get("des").is<std::string>()){
            designation = jsonItem.get("des").get<std::string>();
        }
        if(jsonItem.get("ref").is<std::string>()){
            reference = jsonItem.get("ref").get<std::string>();
        }
        if(jsonItem.get("H").is<double>()){
            magnitude = jsonItem.get("H").get<double>();
        }
        if(jsonItem.get("G").is<double>()){
            slope = jsonItem.get("G").get<double>();
        }
        if(jsonItem.get("M").is<double>()){
            epochAnomaly = jsonItem.get("M").get<double>();
        }
        if(jsonItem.get("w").is<double>()){
            perihelion = jsonItem.get("w").get<double>();
        }
        if(jsonItem.get("om").is<double>()){
            ascendingNode = jsonItem.get("om").get<double>();
        }
        if(jsonItem.get("i").is<double>()){
            inclination = jsonItem.get("i").get<double>();
        }
        if(jsonItem.get("e").is<double>()){
            eccentricity = jsonItem.get("e").get<double>();
        }
        if(jsonItem.get("d").is<double>()){
            meanDailyMotion = jsonItem.get("d").get<double>();
        }
        if(jsonItem.get("a").is<double>()){
            semiMajorAxis = jsonItem.get("a").get<double>();
        }
        if(jsonItem.get("num_obs").is<double>()){
            numObservations = jsonItem.get("num_obs").get<double>();
        }
    }catch(...){
        std::cerr << "Err parsing item" << std::endl;
    }

    computeDate();
}

void Asteroid::print()
{
    std::cout << "Asteroid: " << name << ", discovered in " << yearOfDiscovery << std::endl;
}

void Asteroid::computeDate()
{
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

// Compute the elements of the orbit for planet-i at day number-d
// result is returned in structure p
void initMeanOrbitalElementsPlanets( double d, int index, Asteroid & planet ) {
    double cy = d / 36525;         // centuries since J2000

    switch(index) {
	case 1:
		planet.semiMajorAxis = 0.38709893 + 0.00000066*cy;
        planet.eccentricity = 0.20563069 + 0.00002527*cy;
        planet.inclination = ( 7.00487  -  23.51*cy/3600)*rads;
        planet.ascendingNode = (48.33167  - 446.30*cy/3600)*rads;
        planet.perihelion = (77.45645  + 573.57*cy/3600)*rads;
        planet.meanDailyMotion = mod2pi((252.25084 + 538101628.29*cy/3600)*rads);
		break;
	case 2:
		planet.semiMajorAxis = 0.72333199 + 0.00000092*cy;
        planet.eccentricity = 0.00677323 - 0.00004938*cy;
        planet.inclination = (  3.39471 -   2.86*cy/3600)*rads;
        planet.ascendingNode = ( 76.68069 - 996.89*cy/3600)*rads;
        planet.perihelion = (131.53298 - 108.80*cy/3600)*rads;
        planet.meanDailyMotion = mod2pi((181.97973 + 210664136.06*cy/3600)*rads);
		break;
	case 3:
		planet.semiMajorAxis = 1.00000011 - 0.00000005*cy;
        planet.eccentricity = 0.01671022 - 0.00003804*cy;
        planet.inclination = (  0.00005 -    46.94*cy/3600)*rads;
        planet.ascendingNode = (-11.26064 - 18228.25*cy/3600)*rads;
        planet.perihelion = (102.94719 +  1198.28*cy/3600)*rads;
        planet.meanDailyMotion = mod2pi((100.46435 + 129597740.63*cy/3600)*rads);
		break;
	case 4:
		planet.semiMajorAxis = 1.52366231 - 0.00007221*cy;
        planet.eccentricity = 0.09341233 + 0.00011902*cy;
        planet.inclination = (  1.85061 -   25.47*cy/3600)*rads;
        planet.ascendingNode = ( 49.57854 - 1020.19*cy/3600)*rads;
        planet.perihelion = (336.04084 + 1560.78*cy/3600)*rads;
        planet.meanDailyMotion = mod2pi((355.45332 + 68905103.78*cy/3600)*rads);
		break;
	case 5:
		planet.semiMajorAxis = 5.20336301 + 0.00060737*cy;
        planet.eccentricity = 0.04839266 - 0.00012880*cy;
        planet.inclination = (  1.30530 -    4.15*cy/3600)*rads;
        planet.ascendingNode = (100.55615 + 1217.17*cy/3600)*rads;
        planet.perihelion = ( 14.75385 +  839.93*cy/3600)*rads;
        planet.meanDailyMotion = mod2pi((34.40438 + 10925078.35*cy/3600)*rads);
		break;
	case 6:
		planet.semiMajorAxis = 9.53707032 - 0.00301530*cy;
        planet.eccentricity = 0.05415060 - 0.00036762*cy;
        planet.inclination = (  2.48446 +    6.11*cy/3600)*rads;
        planet.ascendingNode = (113.71504 - 1591.05*cy/3600)*rads;
        planet.perihelion = ( 92.43194 - 1948.89*cy/3600)*rads;
        planet.meanDailyMotion = mod2pi((49.94432 + 4401052.95*cy/3600)*rads);
		break;
	case 7:
		planet.semiMajorAxis = 19.19126393 + 0.00152025*cy;
        planet.eccentricity =  0.04716771 - 0.00019150*cy;
        planet.inclination = (  0.76986  -    2.09*cy/3600)*rads;
        planet.ascendingNode = ( 74.22988  - 1681.40*cy/3600)*rads;
        planet.perihelion = (170.96424  + 1312.56*cy/3600)*rads;
        planet.meanDailyMotion = mod2pi((313.23218 + 1542547.79*cy/3600)*rads);
		break;
	case 8:
		planet.semiMajorAxis = 30.06896348 - 0.00125196*cy;
        planet.eccentricity =  0.00858587 + 0.00002510*cy;
        planet.inclination = (  1.76917  -   3.64*cy/3600)*rads;
        planet.ascendingNode = (131.72169  - 151.25*cy/3600)*rads;
        planet.perihelion = ( 44.97135  - 844.43*cy/3600)*rads;
        planet.meanDailyMotion = mod2pi((304.88003 + 786449.21*cy/3600)*rads);
		break;
	default:
		break;
	}
}

// compute the true anomaly from mean anomaly using iteration
//  M - mean anomaly in radians
//  e - orbit eccentricity
double true_anomaly( double M, double e ) {
    double V, E, E1 = 0.0;

    // initial approximation of eccentric anomaly
    E = M + e*sin(M)*(1.0 + e*cos(M));

    // iterate to improve accuracy
	do {
		E1 = E;
        E = E1 - (E1 - e*sin(E1) - M)/(1 - e*cos(E1));
	} while (!(abs( E - E1 ) < eps));

    // convert eccentric anomaly to true anomaly
    V = 2*atan(sqrt((1 + e)/(1 - e))*tan(0.5*E));

    // modulo 2pi
    if (V < 0)
        V = V + (2*pi) ;
    
    return V;
}

// get coordinates for objet a,
void get_coord( Asteroid & elm, int p, double d, s_coordinates & coords ) {
	if (p != 0)
		initMeanOrbitalElementsPlanets( d, p, elm );
    double ap = elm.semiMajorAxis;
	double ep = elm.eccentricity;
    double ip = elm.inclination;
    double op = elm.ascendingNode;
    double pp = elm.perihelion;
    double lp = elm.meanDailyMotion;

    // position of planet in its orbit
    double mp = mod2pi(lp - pp);
    double vp = true_anomaly(mp, elm.eccentricity);
    double rp = ap*(1 - ep*ep) / (1 + ep*cos(vp));
    
    // heliocentric rectangular coordinates of planet
    double xh = rp*(cos(op)*cos(vp + pp - op) - sin(op)*sin(vp + pp - op)*cos(ip));
    double yh = rp*(sin(op)*cos(vp + pp - op) + cos(op)*sin(vp + pp - op)*cos(ip));
    double zh = rp*(sin(vp + pp - op)*sin(ip));

	coords.idx = 0;
	coords.H = elm.magnitude;
	coords.x = xh;
	coords.y = yh;
	coords.z = zh;
	coords.r = (elm.ascendingNode + elm.perihelion) / 2;
}