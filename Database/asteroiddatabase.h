#ifndef ASTEROIDDATABASE_H
#define ASTEROIDDATABASE_H

#include <string>
#include <map>
#include <memory>

#include "asteroid.h"


class AsteroidDatabase
{
    typedef std::map<std::string, std::shared_ptr<Asteroid>>::iterator iterator;
public:
    AsteroidDatabase();
    ~AsteroidDatabase();

    void insert(Asteroid *asteroid);
    std::shared_ptr<Asteroid *> getAsteroid(std::string id);

    void print(std::string id);

    int size() { return db.size(); }
    iterator begin() {return db.begin(); }
    iterator end() { return db.end(); }

private:
    std::map<std::string, std::shared_ptr<Asteroid>> db;
};

#endif // ASTEROIDDATABASE_H
