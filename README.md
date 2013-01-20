# DeveMazeGenerator
=================

My maze generator written in C#, highly optimized to generate huge mazes (128.000x128.000 for example)

## Current functionality:
* Generate mazes
* See them being generated live
* See pathfinding live
* Save them with or without path (to png, bmp, jpeg, etc)
* Calculate walls (can be used to draw maze in 3d, I also have a sample of this if anyone is interested)
* A GUI application with all kinds of buttons that do things
* Different inner storage formats (Highly optimized BitArray, the normal .net BitArray, a Boolean array or directly mapped on the hard drive)
* On a hard drive with 2TB you could theoretically generate a maze of the following size:
```
SQRT(2000000000000*8)=4000000
So a maze of about 4 million * 4 million
```
* See the % of a maze being generated by using the callback (See the GUI application for an example, The HUUUGE maze button)
* Added button to get the walls from a maze and draw them, I sort this list in very unique ways for cool effects :P

## Todo: (If you want me to work on any of these just throw me an email)
* Add like 8 other maze generation algorithms (Kruskal, prim, eller, division, etc)

## Things this project tought me:
* Enums are default ints, this means they take up 32-bit (or 4 bytes) in memory when placed in an array.
* Booleans are 1 byte in memory size.
* A BitArray can be used to store 8 booleans in 1 byte.
* How to implement different maze generation algorithms.
* How to profile code in Visual Studio.
* How to create images without wasting memory when creating them.
* Writing callbacks with Actions.
* That the Random class is not Thread safe :/.
* How to write my own pathfinder that's faster then A* (It finds "a path" which is always the shortest since the mazes have only 1 path)
* How the garbage collector won't collect all garbage sometimes (when generating loads of mazes multithreaded for a long time)