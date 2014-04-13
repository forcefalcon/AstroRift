#ifndef ASTEROIDDATABASE_H
#define ASTEROIDDATABASE_H

#include <string>
#include <map>
#include <memory>
#include <iostream>

#include "asteroid.h"

/**
 * @brief The Filter struct
 */
struct Filter {
    enum Type {
        None,
        Magnitude,
        YearOfDiscovery,
        Name,
    };
    enum Compare {
        EQUAL,
        LESSER,
        GREATER
    };

    Filter(): type(Type::None), cmp(Compare::EQUAL) {}

    Filter(Filter::Type _type, Filter::Compare _cmp, char const * _y):type(_type), cmp(_cmp) {
        if(_type == Type::Name){
            memcpy(value.name, _y, (int)strlen(_y));
        }
    }
    Filter(Filter::Type _type, Filter::Compare _cmp, float _y):type(_type), cmp(_cmp) {
        value.magnitude = _y;
    }
    Filter(Filter::Type _type, Filter::Compare _cmp, int _y):type(_type), cmp(_cmp) {
        value.year = _y;
    }


    bool matches(Asteroid const *a);
    bool matches(std::shared_ptr<Asteroid> const &a) { return matches(a.get()); }

    Type type;
    Compare cmp;
    union {
        int year;
        float magnitude;
        char name[32];
    } value;

    /**
     * @brief equalityMargin*
     *  Used to give some tolerance while comparing floats
     */
    static const float equalityMarginUp;
    static const float equalityMarginLow;
};

/**
 * @brief The AsteroidDatabase class
 */
class AsteroidDatabase
{
    typedef std::map<std::string, std::shared_ptr<Asteroid>>::iterator iterator;
public:
    AsteroidDatabase();
    ~AsteroidDatabase();

    void loadPolicy(Filter filter) { loadFilter = filter; }
    int loadFromFile(char const* filename);
    int loadFromJSON(char const* filename);


    void insert(Asteroid *asteroid);
    std::shared_ptr<Asteroid> getAsteroidByDesignation(std::string id);
    std::shared_ptr<Asteroid> getAsteroidByName(std::string name);
    std::vector<std::shared_ptr<Asteroid>> find(Filter filter);

    int filterAndErase(Filter filter);
    void print(std::string id);
    void print();

    int size() { return db.size(); }
    iterator begin() {return db.begin(); }
    iterator end() { return db.end(); }

private:
    std::map<std::string, std::shared_ptr<Asteroid>> db;
    Filter loadFilter;
};

#endif // ASTEROIDDATABASE_H
