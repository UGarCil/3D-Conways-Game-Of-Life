using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    float timer;
    public float setTimer;
    public GameObject tile;
    public int[] DIMS;
    private List<List<List<GameObject>>> cube = new List<List<List<GameObject>>>();
    // public List<Color> colors = new List<Color>();
    public int chanceAlive;
    // Start is called before the first frame update
    void Start()
    {
        for (int z = 0; z<DIMS[2]; z++){
            List<List<GameObject>> grid = new List<List<GameObject>>();
            // Initiaize the grid in the Y axis
            for (int y=0; y< DIMS[1]; y++){
                List<GameObject> gameObjectRow = new List<GameObject>();
                // Initiaize the grid in the X axis
                for (int x=0; x< DIMS[0]; x++){
                    // Create a new tile
                    GameObject newTile = Instantiate(tile, new Vector3(x,z,y), Quaternion.identity);
                    int randomNum = Random.Range(0,100);
                    if (randomNum <= chanceAlive){
                        newTile.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        newTile.GetComponent<Tile>().state = 1;
                    } else {
                        newTile.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                        newTile.SetActive(false);
                        // newTile.GetComponent<MeshRenderer>().enabled = false;
                    }
                    gameObjectRow.Add(newTile);
                }
                grid.Add(gameObjectRow);
            }
            cube.Add(grid);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer<=0){
            getNeighbors();  //Get the total number of neighbors that each cell has alive
            updateState();   //Update the state of each tile based on the total number of alive neighbors
            timer = setTimer;
        } else {
            timer -= Time.deltaTime;
        }
        
    }


    void getNeighbors(){
        // Update the grid
        for (int h=0; h<cube.Count; h++){
            for (int r = 0; r<cube[0].Count;r++){
                for (int c = 0; c<cube[0][0].Count; c++){
                    Tile tile = cube[h][r][c].GetComponent<Tile>();
                    int total = 0;
                    // print(mod(-1,5));
                    // print((r-1)%10);
                    // print((c-1)%10);
                    // print(cube[(r-1)%DIMS[1]][(c-1)%DIMS[0]]);
                    GameObject[] neighbors = 
                        {
                            // XXX
                            // XXX
                            // XXX
                            cube[mod(h-1,DIMS[2])][mod(r-1,DIMS[1])][mod(c-1,DIMS[0])],  cube[mod(h-1,DIMS[2])][mod(r-1,DIMS[1])][c], cube[mod(h-1,DIMS[2])][mod(r-1,DIMS[1])][mod(c+1,DIMS[0])],
                            cube[mod(h-1,DIMS[2])][r][mod(c-1,DIMS[0])],cube[mod(h-1,DIMS[2])][r][c],cube[mod(h-1,DIMS[2])][r][mod(c+1,DIMS[0])],
                            cube[mod(h-1,DIMS[2])][mod(r+1,DIMS[1])][mod(c-1,DIMS[0])],  cube[mod(h-1,DIMS[2])][mod(r+1,DIMS[1])][c], cube[mod(h-1,DIMS[2])][mod(r+1,DIMS[1])][mod(c+1,DIMS[0])],


                            // XXX
                            // X X
                            // XXX
                            cube[h][mod(r-1,DIMS[1])][mod(c-1,DIMS[0])],  cube[h][mod(r-1,DIMS[1])][c], cube[h][mod(r-1,DIMS[1])][mod(c+1,DIMS[0])],
                            cube[h][r][mod(c-1,DIMS[0])],cube[h][r][mod(c+1,DIMS[0])],
                            cube[h][mod(r+1,DIMS[1])][mod(c-1,DIMS[0])],  cube[h][mod(r+1,DIMS[1])][c], cube[h][mod(r+1,DIMS[1])][mod(c+1,DIMS[0])],

                            // XXX
                            // XXX
                            // XXX
                            cube[mod(h+1,DIMS[2])][mod(r-1,DIMS[1])][mod(c-1,DIMS[0])],  cube[mod(h+1,DIMS[2])][mod(r-1,DIMS[1])][c], cube[mod(h+1,DIMS[2])][mod(r-1,DIMS[1])][mod(c+1,DIMS[0])],
                            cube[mod(h+1,DIMS[2])][r][mod(c-1,DIMS[0])],cube[mod(h+1,DIMS[2])][r][c],cube[mod(h+1,DIMS[2])][r][mod(c+1,DIMS[0])],
                            cube[mod(h+1,DIMS[2])][mod(r+1,DIMS[1])][mod(c-1,DIMS[0])],  cube[mod(h+1,DIMS[2])][mod(r+1,DIMS[1])][c], cube[mod(h+1,DIMS[2])][mod(r+1,DIMS[1])][mod(c+1,DIMS[0])],
                        };
                    
                    foreach (GameObject neigh in neighbors)
                    {
                    total += neigh.GetComponent<Tile>().state;
                    }
                    
                    tile.neighbors = total;
                }
            }
        }
        
    }


    void updateState(){
        for (int h = 0; h<cube.Count; h++){
            for (int r = 0; r<cube[0].Count;r++){
                for (int c = 0; c<cube[0][0].Count; c++){
                    Tile tile = cube[h][r][c].GetComponent<Tile>();
                    // if the cell is alive
                    if (tile.state == 1){
                        // if there are less than 4 OR more than 5 neighbors alive, cell dies
                        if (tile.neighbors < 4 || tile.neighbors > 5){
                            tile.state = 0;
                            // cube[h][r][c].GetComponent<Renderer>().material.SetColor("_Color",Color.black);
                            // cube[h][r][c].GetComponent<MeshRenderer>().enabled = false;
                            cube[h][r][c].SetActive(false);
                        }
                    }
                    // if vell is dead 
                    else {
                        // if there are exactly 5 neighbors alive, cell is born
                        if (tile.neighbors == 5){
                            tile.state = 1;
                            cube[h][r][c].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                            // cube[h][r][c].GetComponent<MeshRenderer>().enabled = true;
                            cube[h][r][c].SetActive(true);

                        }
                    } 
                }
            }
        }
        
        


    } 

    int mod(int x, int m) {
        return (x%m + m)%m;
    }
}
