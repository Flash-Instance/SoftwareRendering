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

        private float sine = 0f;
        private float sine2 = 0f;
        private Color[] colors = new Color[]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Yellow,
            Color.Magenta
        };

        public MainForm()
        {
            InitializeComponent();

            bitmap = new Bitmap(480, 360);
            renderer = new Renderer(bitmap);

            pb_surface.Image = bitmap;
            t_ticker.Enabled = true;
        }

        private void Update()
        {
            sine += 0.0166f;
            sine2 += 0.0332f;
        }

        private void Draw()
        {
            renderer.Clear(Color.Black);

            int circleCount = 10;
            float offset = (float)((Math.Sin(sine2) + 1f) / 2f);
            offset = 1f - (offset * offset * offset);
            float offset2 = 120f * (float)(Math.PI / 180f);
            float step = (360f / (float)circleCount) * (float)(Math.PI / 180f);
            for(int i = 0;i < circleCount; i++)
            {
                float sX = (float)Math.Cos(sine * 4f + (i * step * offset) + offset2);
                float sY = (float)Math.Sin(sine * 4f + (i * step * offset) + offset2);
                int x = (int)(sX * 100f + (renderer.Width / 2f));
                int y = (int)(sY * 100f + (renderer.Height / 2f));

                Color c = colors[(i % colors.Length)];
                renderer.FillCircle(x, y, 10, c);
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