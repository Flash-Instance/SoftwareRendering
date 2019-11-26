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

        private float angle = 0f;
        private float timer = 0f;
        private float invertTimer = 0f;
        private Vector center;
        private int colorOffset = 0;
        private int colorInvertedIndex = 0;
        private Color[] colors = new Color[]
        {
            Color.IndianRed,
            Color.DarkRed,
            Color.PaleVioletRed,
            Color.OrangeRed,
            Color.PaleVioletRed,
            Color.DarkRed
        };

        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            bitmap = new Bitmap(480, 360);
            renderer = new Renderer(bitmap);

            center = new Vector(renderer.Width / 2f, renderer.Height / 2f);

            Mesh testMesh = AssetLoader.LoadMesh("Assets/cube.obj");

            pb_surface.Image = bitmap;
            t_ticker.Enabled = true;
        }

        private new void Update()
        {
            angle += 0.6f;
            timer += 0.0166f;
            invertTimer += 0.0166f;

            if (timer >= 0.15f)
            {
                timer = 0f;
                colorOffset = (colorOffset + 1) % colors.Length;
            }

            if (invertTimer >= 0.05f)
            {
                invertTimer = 0f;
                colorInvertedIndex = (colorInvertedIndex + 1) % 10;
            }
        }

        private void Draw()
        {
            renderer.Clear(Color.Black);

            for(int i = 0;i < 10;i++)
            {
                float angleOffset = (i + 1) * 0.2f;
                if(i % 2 == 0)
                {
                    angleOffset *= -1f;
                }
                DrawCircleRing(i, 4 * (i + 1), 28 * (i + 1), 1f * (i + 4), angleOffset);
            }
        }

        private void DrawCircleRing(int ringIndex, int circleCount, float radius, float circleRadius, float angleOffset)
        {
            float step = 360f / (float)circleCount;
            for (int i = 0; i < circleCount; i++)
            {
                Vector pos = Vector.FromAngle(angle * angleOffset + (i * step));
                pos *= radius;
                pos += center;

                Color c = colors[((i + colorOffset + ringIndex) % colors.Length)];
                renderer.FillCircle(pos, circleRadius, (colorInvertedIndex == ringIndex) ? ColorUtils.Invert(c) : c);
            }
        }

        private void t_ticker_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();

            pb_surface.Invalidate();
        }

    }
}