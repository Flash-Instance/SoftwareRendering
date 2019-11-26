using System;

namespace Engine
{
    public struct Vertex
    {

        public readonly Vector Position;
        public readonly Vector Normal;
        public readonly Vector UV;

        public Vertex(Vector position, Vector normal, Vector uv)
        {
            Position = position;
            Normal = normal;
            UV = uv;
        }

    }

    /// <summary>
    /// Represents a 3D model
    /// </summary>
    public class Mesh
    {

        public Vertex this[int index]
        {
            get
            {
                if(index >= 0 && index < vertexIndices.Length)
                {
                    return new Vertex(vertices[vertexIndices[index]], normals[normalIndices[index]], uvs[uvIndices[index]]);
                }

                throw new IndexOutOfRangeException();
            }
        }

        public int TriangleCount
        {
            get { return vertexIndices.Length / 3; }
        }

        public int FaceCount
        {
            get { return TriangleCount / 2; }
        }

        public readonly string Name;

        private readonly Vector[] vertices;
        private readonly Vector[] normals;
        private readonly Vector[] uvs;
        private readonly int[] vertexIndices;
        private readonly int[] normalIndices;
        private readonly int[] uvIndices;

        public Mesh(string name, Vector[] vertices, Vector[] normals, Vector[] uvs, int[] vertexIndices, int[] normalIndices, int[] uvIndices)
        {
            Name = name;

            this.vertices = vertices;
            this.normals = normals;
            this.uvs = uvs;
            this.vertexIndices = vertexIndices;
            this.normalIndices = normalIndices;
            this.uvIndices = uvIndices;
        }

    }
}