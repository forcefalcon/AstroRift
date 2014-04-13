#include "asteroiddatabase.h"
#include "asteroid.h"

#include <string>
#include <map>
#include <iostream>
#include <fstream>
#include <sstream>

#include "picojson.h"

using std::shared_ptr;

const float Filter::equalityMarginUp=1.05;
const float Filter::equalityMarginLow=0.95;

AsteroidDatabase::AsteroidDatabase()
{
}

AsteroidDatabase::~AsteroidDatabase()
{
    db.clear();
}

int AsteroidDatabase::loadFromFile(const char *filename)
{
    std::ifstream f;
    static float expected=637000.;
    int current=0;

    f.open(filename);

    float dummy=0;
    std::string line;

    while(getline(f, line)){
        current++;
        if(current%100 == 0)
            printf("\rLoading: %.1f%%", (current/expected)*100);

        std::istringstream iss(line);
        Asteroid *a = new Asteroid;

        iss >> a->designation >> a->magnitude
            >> a->slope >> a->epoch
            >> a->epochAnomaly >> a->perihelion
            >> a->ascendingNode >> a->inclination
            >> a->eccentricity >> a->meanDailyMotion
            >> a->semiMajorAxis >> dummy;
        a->computeDate();

        if(loadFilter.matches(a))
            this->insert(a);
        else
            delete a;
    }

    printf("\rLoading finished\n");

    f.close();

    return db.size();
}

int AsteroidDatabase::loadFromJSON(const char *filename)
{
    std::ifstream f;
    picojson::value v;
    f.open(filename);
    if(!f.is_open()){
        std::cerr << "Could not open file: " << filename << std::endl;
        return -1;
    }

    std::string jsonErr = picojson::parse(v, f);
    std::cout << std::endl << "Done... " << std::endl;
    f.close();

    if (v.is<picojson::array>()) {

        const picojson::array& a = v.get<picojson::array>();
        std::cout << "Found " << a.size() << " items." << std::endl;

        double total = a.size()/100.;
        unsigned long treated = 0;
        for(auto item: a){
            if(treated++%100 == 0){
                printf("\rCreating database : %.2f%%", treated/total);
            }
            Asteroid *a = new Asteroid(item);

            if(loadFilter.matches(a))
                this->insert(a);
            else
                delete a;
        }
        std::cout << std::endl << "Done... " << std::endl;

    }

    return 0;
}

void AsteroidDatabase::insert(Asteroid *asteroid)
{
    db[asteroid->designation] = shared_ptr<Asteroid>(asteroid);
}

int AsteroidDatabase::filterAndErase(Filter filter)
{
    for(auto item: db){
        if(!filter.matches(item.second)){
            db.erase(item.first);
        }
    }
    return db.size();
}

void AsteroidDatabase::print(std::string id)
{
    if(db.find(id) != db.end()){
        db[id]->print();
    }
}


bool Filter::matches(const Asteroid *a)
{
    if(type == Filter::None){
        return true;
    }

    if(type == Filter::Magnitude){
        switch(cmp) {
        case Filter::EQUAL:
            return (a->magnitude <= value.magnitude*(equalityMarginUp) && a->magnitude >= value.magnitude*equalityMarginLow);

        case Filter::GREATER:
            return a->magnitude >= value.magnitude;

        case Filter::LESSER:
            return a->magnitude <= value.magnitude;

        default:
            return true;
        }
    }

    if(type == Filter::YearOfDiscovery){
        switch (cmp) {
        case Filter::EQUAL:
            return a->yearOfDiscovery == value.year;

        case Filter::GREATER:
            return a->yearOfDiscovery >= value.year;

        case Filter::LESSER:
            return a->yearOfDiscovery <= value.year;

        default:
            return true;
        }
    }

    return true;
}
