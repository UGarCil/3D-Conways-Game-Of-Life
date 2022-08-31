# 3D-Conways-Game-Of-Life
A tridimensional implementation of Conway's classic algorithm on cellular automata using C# and Unity

![alt text](https://github.com/UGarCil/3D-Conways-Game-Of-Life/blob/main/demo.gif?raw=true)

### The rules of the 3D grid of cells (voxels) are as follows:  
if the cell is alive:  
&nbsp; if there are less than 4 neighbors, OR more than 5 neighbors, cell dies  
if the cell is dead:  
&nbsp; if there are exactly 5 neighbors, cell is born  

To implement this visualization in Unity:  
- Create a new primitive geometry, make prefab and add the Tile.cs as a component
- Create a new empty object (the manager) and add the Manager.cs file as a component
- Set the dimensions of the cube from the Manager
