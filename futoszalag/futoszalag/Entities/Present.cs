using futoszalag.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futoszalag.Entities
{
    public class Present : Toy
    {
   
        public Present(Color ribbon, Color box)
        {

        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Green), 0, 0, Width, Height);
            g.FillRectangle(new SolidBrush(Color.Black), 0, Height / 3, Width, 2 * Height / 3);
            g.FillRectangle(new SolidBrush(Color.Black), Width / 3, 0, 2 * Width / 3, Height);
        }

    }
}
