#include "asteroiddatabase.h"
#include "asteroid.h"

#include <string>
#include <map>

using std::shared_ptr;

AsteroidDatabase::AsteroidDatabase()
{
}

AsteroidDatabase::~AsteroidDatabase()
{
    db.clear();
}

void AsteroidDatabase::insert(Asteroid *asteroid)
{
    db[asteroid->designation] = shared_ptr<Asteroid>(asteroid);
}

void AsteroidDatabase::print(std::string id)
{
    if(db.find(id) != db.end()){
        db[id]->print();
    }
}
