using System.Windows.Forms;

namespace SoftwareRendering
{
    public class CustomPictureBox : PictureBox
    {

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            base.OnPaint(pe);
        }

    }
}