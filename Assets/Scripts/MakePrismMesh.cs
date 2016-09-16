using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakePrismMesh : MonoBehaviour {
    [SerializeField]
    private Material material;
    [SerializeField]
    private float yRotation = 30;

    private List<Vector3> vertices;
    private Vector2[] uv;
    private List<int> triangles;

    private float rad = 0.0174532925f;

    private float heigth;
    private float width = 3f;

    [SerializeField]
    private int sides = 6;

	public void MeshSetup(float maxheight)
    {
        //Sets up the prism just below the ground. When spawning is called, the prism will rise above to its height.
        heigth = Random.Range(1,maxheight);
        transform.position = new Vector3(transform.position.x, transform.position.y - heigth, transform.position.z);

        //The coordinates for the points are given by the HexMetrics.
        vertices = new List<Vector3>{};

        //points for the bottom side
        for (int i = 0; i < sides; i++)
        {
            vertices.Add(new Vector3(Mathf.Cos((yRotation+ (360/sides) * i )* rad) * width, 0f, Mathf.Sin((yRotation+ (360 / sides) * i) * rad) * width));
        }
        //points for the top side
        for (int i = 0; i < sides; i++)
        {
            vertices.Add(new Vector3(Mathf.Cos((yRotation+ (360 / sides) * i) * rad) * width, heigth, Mathf.Sin((yRotation+ (360 / sides) * i )* rad) * width));
        }
        //middle points for top and bottom side
        vertices.Add(new Vector3(0, heigth, 0));
        vertices.Add(new Vector3(0, 0, 0));


        //the triangles will be drawn through the three points given here. Remember, go anti-clockwise.
        //They are drawn with the middle verice on the top and bottom of the prism, drawing at every side.
        triangles = new List<int>{};
        for (int i = 0; i < sides; i++)
        {

            //bottom of the prism
            triangles.Add(sides * 2 + 1);
            triangles.Add(i);
            if(i < sides-1)
                triangles.Add(i + 1);
            else
                triangles.Add(i +1-sides);

            //top of the prism
            triangles.Add(sides * 2 + 0);
            if (i < sides-1)
                triangles.Add(sides + i + 1);
            else
                triangles.Add(i + 1);
            triangles.Add(sides + i);

            //the sides of the prism.
            if (i ==sides -1)
            {
                triangles.Add(i);
                triangles.Add(i+sides);
                triangles.Add(sides);

                triangles.Add(i);
                triangles.Add(sides);
                triangles.Add(0);
            }
            else
            {
                triangles.Add(i);
                triangles.Add(i + sides);
                triangles.Add(i + sides + 1);

                triangles.Add(i);
                triangles.Add(i + sides + 1);
                triangles.Add(i + 1);
            }


        }
        
        //uv is defined so that the texture gets arrount the right points.(limited to six sides sadly)
        uv = new Vector2[]
        {
            new Vector2(0,0.25f),
            new Vector2(0,0.75f),
            new Vector2(0.5f,1),
            new Vector2(1,0.75f),
            new Vector2(1,0.25f),
            new Vector2(0.5f,0),

            new Vector2(0,.25f),
            new Vector2(0,0.75f),
            new Vector2(0.5f,1),
            new Vector2(1,0.75f),
            new Vector2(1,0.25f),
            new Vector2(0.5f,0),

            new Vector2(0.5f,0.5f),
            new Vector2(0.5f,0.5f)
        };
        DrawMesh();

    }
    void DrawMesh()
    {
        //the meshfilter and renderer is added to the gameobject that will draw the mesh.
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();

        //The vertices and triangles iformation are given to the mesh.
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        //uv isn't algarbratic generated and is only capable with only six sides.
        if(sides ==6)
            mesh.uv = uv;

        //recalculating the normals so that the lightning gets the right input making it look nicer.
        mesh.RecalculateNormals();

        //mesh is defined and drawn.
        meshFilter.mesh = mesh;

        //material is given to the object.
        GetComponent<MeshRenderer>().material = material;

        StartCoroutine(Spawning());
    }
    public void Destroy()
    {
        //destroys the gameobject attached to this script.
        Destroy(this.gameObject);
    }
    IEnumerator Spawning()
    {
        while( transform.position.y <-0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Time.deltaTime * 5);
            yield return new WaitForFixedUpdate();
        }
    }
    public float Width
    {
        //returns the width with an extra space.
        get { return (width*1.70f) ; }
    }
    public int Sides
    {
        set { sides = value; }
    }

}
