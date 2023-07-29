using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ThreadHomeWork2
{
    public class LAB
    {
        private const float e = 0.008856f;
        private const float k = 903.3f;

        public float l { get; set; }
        public float a { get; set; }
        public float b { get; set; }

        public LAB(XYZ col)
        {
            Vector3 lab = XYZtoLAB(col);
            l = lab.X;
            a = lab.Y;
            b = lab.Z;
        }

        private static Vector3 XYZtoLAB(XYZ col)
        {
            float x = col.x;
            float y = col.y;
            float z = col.z;

            float fx = ApplyLABconversion(x);
            float fy = ApplyLABconversion(y);
            float fz = ApplyLABconversion(z);

            return new Vector3(
                     116.0f * fy - 16.0f,
                     500.0f * (fx - fy),
                     200.0f * (fy - fz)
                 );
        }

        private static float ApplyLABconversion(float value)
        {
            if (value > e)
            {
                value = (float)Math.Pow(value, 1.0f / 3.0f);
            }
            else
            {
                value = (k * value + 16) / 116;
            }
            return value;
        }

        public float DeltaE(LAB color)
        {
            return (float)Math.Sqrt(Math.Pow((this.l - color.l), 2f) + Math.Pow((this.a - color.a), 2f) + Math.Pow((this.b - color.b), 2f));
        }
    }
}
