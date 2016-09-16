using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Inputs : MonoBehaviour {
    /// <summary>
    /// This script is purely for demonstration of the scene. 
    /// The user can tweak the info of the grid and the mesh, and the Input- script asks the gridspawner to make the grid.
    /// </summary>
    [SerializeField]
    private Slider tilesPerSideSlider;
    [SerializeField]
    private Text tilesPerSideText;
    [SerializeField]
    private Dropdown gridShapeDropDown;
    [SerializeField]
    private Slider sidesOfPrismSlider;
    [SerializeField]
    private Text sidesOfPrismSliderText;
    [SerializeField]
    private Slider maxHeightSlider;
    [SerializeField]
    private Text maxHeightSliderText;

    [SerializeField]
    private GridSpawner gridSpawner;
    [SerializeField]
    private MakePrismMesh prismMesh;

    void Start()
    {
        //to spawn a grid in the beginning of the scene.
        UpdateGridData();
        MakeGrid();
    }
	void Update () {

        //updates the text info for spawning the gird.
        tilesPerSideText.text = "tiles per side: " + tilesPerSideSlider.value;
        sidesOfPrismSliderText.text = "Sides of prism: " + sidesOfPrismSlider.value;
        maxHeightSliderText.text = "Max height of prism: " + maxHeightSlider.value;
	}

    //Changes the variables of the grid and mesh with the inputs of the user.
    public void UpdateGridData()
    {
        prismMesh.Sides = 6;
        gridSpawner.MaxHeight = 10f;
        gridSpawner.tilesPersSide = tilesPerSideSlider.value;
        prismMesh.Sides = (int)sidesOfPrismSlider.value;
        gridSpawner.MaxHeight = maxHeightSlider.value;
    }

    //Makes the grid depending on which kind of grid the user wants.
    public void MakeGrid()
    {
        UpdateGridData();
        switch(gridShapeDropDown.value)
        {
            case 0:
                gridSpawner.MakeTriangleShapedGrid();
                break;

            case 1:
                gridSpawner.MakeHexagonShapedGrid();
                break;

            case 2:
                gridSpawner.MakeParralelShapedGrid();
                break;
        }
    }


}
