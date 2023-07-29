// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Text.Json;
using ThreadHomeWork2;

internal class Program
{
    
    private static void Main(string[] args)
    {
        Console.WriteLine("hello");
        List<ColorRGB> colors = new List<ColorRGB>();
        object locker = new();
        using (Bitmap bitmap = new Bitmap(200, 200))
        {
            Random r = new Random();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int k = 0; k < bitmap.Height; k++)
                {

                    Color color = Color.FromArgb(255, r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                    bitmap.SetPixel(i, k, color);
                    Color pixelColor = bitmap.GetPixel(i, k);
                    //Console.WriteLine(pixelColor.ToString() /*+ "  " + pixelColor.G.ToString() + "  " + pixelColor.B.ToString()*/);
                }
               
            }
            //bitmap.Save("image.png", System.Drawing.Imaging.ImageFormat.Png);
            
        }
        Thread th1 = new Thread(() => GetImageFromFile(colors, locker));
        th1.Start();
        Thread th2 = new Thread(() => TheSaim(colors));
        th2.Start();
        th2.Join();
    }
    public static void GetImageFromFile(List<ColorRGB> colors, object locker)
    {
        Console.WriteLine("th1 start");
        Bitmap newimage = new("image.png", true);
        lock (colors)
        {
            for (int i = 0; i < newimage.Width; i++)
            {
                for (int k = 0; k < newimage.Height; k++)
                {
                    colors.Add(new ColorRGB(newimage.GetPixel(i, k), i, k));
                    //Console.WriteLine(colors.Last().ToString());
                }
            }
        }
        
        Console.WriteLine("th1 end");
    }

    public static void TheSaim(List<ColorRGB> colors)
    {
        Thread.Sleep(1000);
        Console.WriteLine("th2 start");
        List<ColorRGB> all = new List<ColorRGB>();
        int i = 1;
        foreach (var item in colors)
        {
            //Console.WriteLine(item);
            string t = i.ToString();
            if (!all.Contains(item))
            {
                
                var saim = colors.Where(x => AreSimilar(item._ItemColor, x._ItemColor, 10, "D65"));
                using (FileStream fs1 = new FileStream(t, FileMode.OpenOrCreate))
                {
                    foreach (var color in saim)
                    {
                        JsonSerializer.Serialize(fs1, color);
                    }
                    
                }
                using (FileStream fs = new FileStream("Color.json", FileMode.Append))
                {
                    foreach(var color in saim)
                    {
                        //Console.WriteLine("serilaize");
                        JsonSerializer.Serialize(fs, color);
                        all.Add(color);
                    }
                }
                i++;
            }
        }
        Console.WriteLine("th2 end");
    }

    public static bool AreSimilar(Color color1, Color color2, float delta, string d)
    {
        return (new LAB(new XYZ(color1, d))).DeltaE(new LAB(new XYZ(color2, d))) <= delta;
    }
}