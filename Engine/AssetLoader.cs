using System;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Loads different assets into memory
    /// </summary>
    public static class AssetLoader
    {

        private static readonly string programPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Loads the mesh (OBJ file) at the given path
        /// </summary>
        /// <param name="relativePath">The relative file path to the mesh</param>
        /// <returns>The loaded mesh</returns>
        public static Mesh LoadMesh(string relativePath)
        {
            if(File.Exists(relativePath))
            {
                try
                {
                    // Load the model file
                    string data;
                    using (StreamReader sr = new StreamReader(GetAbsolutePath(relativePath)))
                    {
                        data = sr.ReadToEnd();
                    }

                    // Parse the data
                    string meshName = "";
                    List<Vector> vertices = new List<Vector>();
                    List<Vector> normals = new List<Vector>();
                    List<Vector> uvs = new List<Vector>();
                    List<int> vertexIndices = new List<int>();
                    List<int> normalIndices = new List<int>();
                    List<int> uvIndices = new List<int>();

                    string[] lines = data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for(int i = 0;i < lines.Length;i++)
                    {
                        string line = lines[i];
                        switch(line[0])
                        {
                            default:
                            case '#':
                                // This is a comment, do nothing
                                break;

                            case 'o':
                                // This is the name of the mesh
                                meshName = line.Substring(2).Trim();
                                break;

                            case 'v':
                                // This is either a vertex, UV, or normal
                                if(line[1] == 't')
                                {
                                    // This is a UV coordinate
                                    string[] uvRawValues = line.Substring(3).Split(' ');
                                    uvs.Add(new Vector(float.Parse(uvRawValues[0], CultureInfo.InvariantCulture), float.Parse(uvRawValues[1], CultureInfo.InvariantCulture)));
                                }
                                else if(line[1] == 'n')
                                {
                                    // This is a normal
                                    string[] normalRawValues = line.Substring(3).Split(' ');
                                    normals.Add(new Vector(float.Parse(normalRawValues[0], CultureInfo.InvariantCulture), float.Parse(normalRawValues[1], CultureInfo.InvariantCulture), float.Parse(normalRawValues[2], CultureInfo.InvariantCulture)));
                                }
                                else
                                {
                                    // This is a vertex
                                    string[] vertexRawValues = line.Substring(2).Split(' ');
                                    vertices.Add(new Vector(float.Parse(vertexRawValues[0], CultureInfo.InvariantCulture), float.Parse(vertexRawValues[1], CultureInfo.InvariantCulture), float.Parse(vertexRawValues[2], CultureInfo.InvariantCulture)));
                                }
                                break;

                            case 's':
                                // This is a smoothing group, we ignore these
                                break;

                            case 'f':
                                // This is a face
                                string[] faceIndexGroups = line.Substring(2).Split(' ');
                                for(int j = 0;j < faceIndexGroups.Length;j++)
                                {
                                    string[] indexRawValues = faceIndexGroups[j].Split('/');
                                    vertexIndices.Add(int.Parse(indexRawValues[0]) - 1);
                                    uvIndices.Add(int.Parse(indexRawValues[1]) - 1);
                                    normalIndices.Add(int.Parse(indexRawValues[2]) - 1);
                                }
                                break;
                        }
                    }

                    return new Mesh(meshName, vertices.ToArray(), normals.ToArray(), uvs.ToArray(), vertexIndices.ToArray(), normalIndices.ToArray(), uvIndices.ToArray());
                }
                catch(Exception e)
                {
                    throw new Exception("Unable to parse OBJ model file");
                }
            }

            return null;
        }

        private static string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(programPath, relativePath);
        }

    }
}