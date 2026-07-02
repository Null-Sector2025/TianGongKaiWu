cmake_minimum_required(VERSION 3.18.1)
project("tiangongndk")
add_library(tiangongndk SHARED ndk_translator.cpp)
target_link_libraries(tiangongndk android log)
