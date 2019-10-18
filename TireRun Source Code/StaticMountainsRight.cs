using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMountainsRight : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs; //use if your material is a picture and you want to map the picture to the terrian
    MeshFilter meshfilter;
    MeshCollider meshcollider;


    public int Speed = 20;
    int[] triangles;
    public int xSize = 10;
    public int zSize = 10;
    void Start()
    {
        transform.position = new Vector3(2f + 7.5f+11.5f, -0.5f, -3f);
        mesh = new Mesh();
        meshfilter = GetComponent<MeshFilter>();
        CreateShape();
        meshfilter.mesh = mesh;
        UpdateMesh();

    }
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            vertices[i] = new Vector3(0f, 0.5f, z);
            i++;
            for (int x = 1; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 7f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
        triangles = new int[xSize * zSize * 6];

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
        mesh.vertices = vertices;
        //mesh.uv = uvs;
        // mesh.RecalculateTangents();
        meshfilter.mesh = mesh;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

}
