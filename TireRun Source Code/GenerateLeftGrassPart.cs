using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLeftGrassPart : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs; //use if your material is a picture and you want to map the picture to the terrian
    MeshFilter meshfilter;
    MeshCollider meshcollider;


    public int Speed = 20;
    int[] triangles;
    public int xSize = 5;
    public int zSize = 2;
    private float Globalz;

    public Globalz g;
    void Start()
    {
        mesh = new Mesh();
        meshfilter = GetComponent<MeshFilter>();
        meshcollider = GetComponent<MeshCollider>();
        CreateShape();
        meshfilter.mesh = mesh;
        meshcollider.sharedMesh = mesh;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        g = GetComponentInParent<Globalz>();
        Globalz = g.globalz;
        g.globalz += 1;
    }
    void CreateShape() {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
            {
                vertices[i] = new Vector3(0f, 0.5f, z);
                i++;
                for (int x = 1; x <= xSize - 1; x++)
                {
                    float y = Mathf.PerlinNoise(x * .3f, Globalz * .3f);
                    vertices[i] = new Vector3(x, y, z);
                    i++;
                }
                vertices[i] = new Vector3(xSize, 0.5f, z);
                i++;
            }
        //Debug.Log(vertices.Length);
            //verticesList = new List<Vector3>(vertices);
            triangles = new int[xSize * zSize * 6];

            int vert = 0;
            int tris = 0;
            for (int z = 0; z < zSize; z++)
            {
                for (int x = 0; x < xSize; x++)
                {
                //Debug.Log(x);
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + xSize + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + xSize + 1;
                    triangles[tris + 5] = vert + xSize + 2;

                    vert++;
                    tris += 6;

                    //yield return new WaitForSeconds(0.1f);
                }
                vert++;
            }
            uvs = new Vector2[vertices.Length];
            for (int i = 0, z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    //float height = Mathf.InverseLerp(MinTerrianHeight, MaxTerrianHeight, vertices[i].y);
                    uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                    i++;
                }
            }
        }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back);
        if(transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }
}
