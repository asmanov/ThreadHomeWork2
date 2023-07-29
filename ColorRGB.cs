using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadHomeWork2
{
    public class ColorRGB
    {
        public Color _ItemColor { get; set; }
        public int _x { get; set; }
        public int _y { get; set; }

        public ColorRGB(Color itemColor, int x, int y)
        {
            _ItemColor = itemColor;
            this._x = x;
            this._y = y;
        }

        public override string ToString()
        {
            return $"Color: {_ItemColor.R},{_ItemColor.G},{_ItemColor.B} x: {_x} : y: {_y}";
        }
    }
}
