using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{


    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs; //use if your material is a picture and you want to map the picture to the terrian
    MeshFilter meshfilter;
    MeshCollider meshcollider;


    public int Speed = 20;
    int[] triangles;
    public int xSize = 5;
    public int zSize = 100;
    private float Globalz;
    //private List<Vector3> verticesList;

    private int d;
    void Start()
    {
        transform.position = new Vector3(7.5f, 0f, -3f);
        mesh = new Mesh();
        meshfilter = GetComponent<MeshFilter>();
        meshcollider = GetComponent<MeshCollider>();
        CreateShape();
        meshfilter.mesh = mesh;
        meshcollider.sharedMesh = mesh;
        UpdateMesh();
        mesh.triangles = triangles;
        mesh.uv = uvs;
        Globalz = zSize;
        d = (xSize + 1) * (zSize);

}
    //IEnumerator CreateShape()
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i=0, z = 0; z <= zSize; z++)
        {
            vertices[i] = new Vector3(0f, 0.5f, z);
            i++;
            for (int x = 1; x <= xSize -1; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z *.3f);
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
            vertices[i] = new Vector3(xSize, 0.5f, z);
            i++;
        }
        //verticesList = new List<Vector3>(vertices);
        triangles = new int[xSize *zSize *6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
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
    void UpdateMesh()
    {
        //mesh.Clear();
        //mesh.vertices = verticesList.ToArray();
        mesh.vertices = vertices;
        //mesh.uv = uvs;
        mesh.RecalculateNormals();
        //mesh.RecalculateBounds();
        // mesh.RecalculateTangents();
        //meshfilter.mesh = mesh;
        meshcollider.sharedMesh = mesh;
    }
    void FixedUpdate()
    {
        //transform.Translate(Vector3.back * (1));
        GenerateNextPart();

    }
    void GenerateNextPart()
    {

        /*Globalz += 1;
        int d = xSize + 1;
        verticesList.RemoveRange(0, d);
        verticesList.Add(new Vector3(0, 0.5f, Globalz));
        for (int x = 1; x <= xSize; x++)
        {
            float y = Mathf.PerlinNoise(x * .3f, Globalz * .3f);
            verticesList.Add(new Vector3(x, y, Globalz));
        }


        UpdateMesh();*/

        Globalz += 1;
        for (int j = xSize + 1, i = 0; i < d; j++, i++)
        {
            vertices[i].Set(vertices[j].x, vertices[j].y, vertices[j].z - 1);
        }
        //Debug.Log(w);
        //Debug.Log(d);
        vertices[d] = new Vector3(0, 0.5f, zSize + 1);
        for (int i = d + 1, x = 1; x < xSize; i++, x++)
        {
            //Debug.Log(vertices[i].x);
            float y = Mathf.PerlinNoise(x * .3f, Globalz * .3f);
            vertices[i].Set(x, y, zSize + 1);
        }
        vertices[d + xSize].Set(xSize, 0.5f, zSize + 1);
        UpdateMesh();
    }
}
