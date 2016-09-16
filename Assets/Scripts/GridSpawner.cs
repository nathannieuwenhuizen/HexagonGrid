using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExtentionMethods;

public class GridSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject hexagonTile;
    [SerializeField]
    private List<GameObject> hexagonTilesList;

    [SerializeField]
    public float tilesPersSide;
    private float maxHeight;

    void Start () {
        //sets up the list for all the tiles being made.
        hexagonTilesList = new List<GameObject>();
        
	}

    //makes a triangle based grid.
    public void MakeTriangleShapedGrid()
    {
        int xLength = (int)tilesPersSide;
        float xPos = 0;
        float yPos = 0;
        float hexWidth = hexagonTile.GetComponent<MakePrismMesh>().Width;
        for (int i = 0; i < tilesPersSide.SumofAllIntegersBetweeOneAndValue(); i++)
        {
            
            hexagonTilesList.Add(hexagonTile);
            if (xPos > xLength - 1)
            {
                xPos = 0;
                xLength--;
                yPos++;
            }
            xPos++;
            hexagonTilesList[i] = Instantiate(hexagonTilesList[i], new Vector3((xPos + yPos / 2) * hexWidth, 0, yPos * (hexWidth * 0.866f)), Quaternion.identity) as GameObject;
            hexagonTilesList[i].GetComponent<MakePrismMesh>().MeshSetup(maxHeight);
        }
        CameraUpdate();

    }

    //makes a hexgon-shaped grid.
    public void MakeHexagonShapedGrid()
    {
        int xLength = (int)tilesPersSide;
        float xPos = 0;
        float yPos = 0;
        float decreasingYPos = 0;
        float hexWidth = hexagonTile.GetComponent<MakePrismMesh>().Width;


        for (int i = 0; i < 2*(tilesPersSide* 2 -2).SumofAllIntegersBetweeOneAndValue() - 2* (tilesPersSide-1).SumofAllIntegersBetweeOneAndValue()+ (tilesPersSide*2 -1); i++)
        {
            hexagonTilesList.Add(hexagonTile);
            
            if (xPos > xLength - 1)
            {
                
                xPos = 0;
                yPos++;
                if (yPos<tilesPersSide)
                {
                    xLength++;
                }
                else
                {
                    xLength--;
                    decreasingYPos-= 2;
                }
            }
            xPos++;
            hexagonTilesList[i] = Instantiate(hexagonTilesList[i], new Vector3((xPos - (yPos+ decreasingYPos) / 2) * hexWidth, 0, yPos * (hexWidth * 0.866f)), Quaternion.identity) as GameObject;
            hexagonTilesList[i].GetComponent<MakePrismMesh>().MeshSetup(maxHeight);
        }
        CameraUpdate();
        
    }

    //makes a parralel- shaped grid.
    public void MakeParralelShapedGrid()
    {
        int xLength = (int)tilesPersSide;
        float xPos = 0;
        float yPos = 0;
        float hexWidth = hexagonTile.GetComponent<MakePrismMesh>().Width;
        for (int i = 0; i < tilesPersSide.PowerOfTwo(); i++)
        {
            hexagonTilesList.Add(hexagonTile);

            if (xPos > xLength - 1)
            {
                xPos = 0;
                yPos++;
            }
            xPos++;
            hexagonTilesList[i] = Instantiate(hexagonTilesList[i], new Vector3((xPos + -yPos / 2) * hexWidth, 0, yPos * (hexWidth * 0.866f)), Quaternion.identity) as GameObject;
            hexagonTilesList[i].GetComponent<MakePrismMesh>().MeshSetup(maxHeight);
        }
        CameraUpdate();
    }

    //destroys the grid.
    public void ClearGrid()
    {
        //destroys every object in the list.
        foreach (GameObject tile in hexagonTilesList) tile.GetComponent<MakePrismMesh>().Destroy() ;
        //clears the gird making it empty.
        hexagonTilesList.Clear();
    }

    void CameraUpdate()
    {
        //Let the camera focus on the middle tile, so that you can see the different sizes of grids.
        Vector3 destinationpos = new Vector3(hexagonTilesList[(int)tilesPersSide / 2].transform.position.x, 30f + tilesPersSide * 1.5f, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, destinationpos, 2f);
    }
    public float MaxHeight
    {
        set { maxHeight = value; }
    }

}
