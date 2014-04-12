#ifndef ASTEROIDDATABASE_H
#define ASTEROIDDATABASE_H

#include <string>
#include <map>
#include <memory>

#include "asteroid.h"

/**
 * @brief The Filter struct
 */
struct Filter {
    enum Type {
        None,
        Magnitude,
        YearOfDiscovery
    };
    enum Compare {
        EQUAL,
        LESSER,
        GREATER
    };

    Filter(): type(Type::None) {}
    Filter(Filter::Type _type, Filter::Compare _cmp, float _y)
        :type(_type), cmp(_cmp), value{_y} {}
    bool matches(Asteroid const *a);
//    bool matches(std::shared_ptr<Asteroid> const a);

    Type type;
    Compare cmp;
    union {
        int year;
        float magnitude;
    } value;

    /**
     * @brief equalityMargin*
     *  Used to give some tolerance while comparing floats
     */
    static constexpr float equalityMarginUp=1.05;
    static constexpr float equalityMarginLow=0.95;
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


    void insert(Asteroid *asteroid);
    std::shared_ptr<Asteroid *> getAsteroid(std::string id);

    int filterInplace(Filter filter) {}

    void print(std::string id);

    int size() { return db.size(); }
    iterator begin() {return db.begin(); }
    iterator end() { return db.end(); }

private:
    std::map<std::string, std::shared_ptr<Asteroid>> db;
    Filter loadFilter;
};

#endif // ASTEROIDDATABASE_H
