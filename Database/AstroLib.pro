TEMPLATE = app
CONFIG += console
CONFIG -= app_bundle
CONFIG -= qt

SOURCES += main.cpp \
    asteroid.cpp \
    asteroiddatabase.cpp

HEADERS += \
    asteroid.h \
    asteroiddatabase.h

QMAKE_CXXFLAGS += -std=c++11

