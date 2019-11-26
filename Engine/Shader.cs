using System.Drawing;

namespace Engine
{
    public struct VertexInfo
    {

        public readonly Vector Position;
        public readonly Vector Normal;
        public readonly Vector UV;

        public VertexInfo(Vector position, Vector normal, Vector uv)
        {
            Position = position;
            Normal = normal;
            UV = uv;
        }

    }

    public abstract class Shader
    {

        public abstract VertexInfo Vertex(Vector position, Vector normal, Vector uv);
        public abstract Color Fragment(VertexInfo vertex);

    }
}