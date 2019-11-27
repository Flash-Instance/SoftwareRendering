using System;
using System.Drawing;
using System.Windows.Forms;

using Engine;

namespace SoftwareRendering
{
    public partial class MainForm : Form
    {

        private readonly Bitmap bitmap;
        private readonly Renderer renderer;

        private Mesh mesh;
        private Matrix4x4 worldMatrix;
        private Matrix4x4 projectionMatrix;
        private float rotation = 0f;

        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            bitmap = new Bitmap(360, 270);
            renderer = new Renderer(bitmap);

            mesh = AssetLoader.LoadMesh("Assets/monkey.obj");
            projectionMatrix = Matrix4x4.CreateProjection(renderer.Width, renderer.Height, 60f, 0.001f, 1000f);

            pb_surface.Image = bitmap;
            t_ticker.Enabled = true;
        }

        private new void Update()
        {
            rotation += 0.8f;
            worldMatrix = Matrix4x4.CreateIdentity() *
                          Matrix4x4.CreateScale(Vector.One * 6f) *
                          Matrix4x4.CreateRotation(MathUtils.Axis.Up, rotation) *
                          Matrix4x4.CreateTranslation(new Vector(0f, 0f, 4f));
        }

        private void Draw()
        {
            renderer.Clear();

            renderer.DrawMeshWireframe(mesh, projectionMatrix * worldMatrix, Color.LightGreen);
        }

        private void t_ticker_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();

            pb_surface.Invalidate();
        }

    }
}