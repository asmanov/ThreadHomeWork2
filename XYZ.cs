using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadHomeWork2
{
    public class XYZ
    {
        private static readonly List<float> D50 = new List<float> { 0.966797f, 1.0f, 0.825188f };
        private static readonly List<float> D65 = new List<float> { 0.95047f, 1.0f, 1.0883f };

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public XYZ(Color color, string D)
        {
            float r = LinearToSRGB(color.R);
            float g = LinearToSRGB(color.G);
            float b = LinearToSRGB(color.B);

            switch (D)
            {
                case "D50":
                    x = 0.4360747f * r + 0.3850649f * g + 0.1430804f * b;
                    y = 0.2225045f * r + 0.7168786f * g + 0.0606169f * b;
                    z = 0.0139322f * r + 0.0971045f * g + 0.7141733f * b;

                    float D50x = D50[0];
                    float D50y = D50[1];
                    float D50z = D50[2];

                    x = Math.Clamp(x, 0f, D50x) / D50x;
                    y = Math.Clamp(y, 0f, D50y) / D50y;
                    z = Math.Clamp(z, 0f, D50z) / D50z;
                    break;
                case "D65":
                    x = 0.4124564f * r + 0.3575761f * g + 0.1804375f * b;
                    y = 0.2126729f * r + 0.7151522f * g + 0.0721750f * b;
                    z = 0.0193339f * r + 0.1191920f * g + 0.9503041f * b;

                    float D65x = D65[0];
                    float D65y = D65[1];
                    float D65z = D65[2];

                    x = Math.Clamp(x, 0f, D65x) / D65x;
                    y = Math.Clamp(y, 0f, D65y) / D65y;
                    z = Math.Clamp(z, 0f, D65z) / D65z;
                    break;

            }

        }

        private static float LinearToSRGB(float channel)
        {
            if (channel > 0.0031308f)
            {
                channel = (float)(1.055f * Math.Pow(channel, 1 / 2.4f) - 0.055f);
            }
            else
            {
                channel *= 12.92f;
            }
            return channel;
        }
    }    
}
