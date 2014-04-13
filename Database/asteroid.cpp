#include "asteroid.h"

#include <iostream>

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
    std::cout << "Asteroid: " << designation << std::endl;
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
