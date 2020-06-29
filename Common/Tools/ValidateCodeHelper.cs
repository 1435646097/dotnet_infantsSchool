using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Common.Tools
{
    public class ValidateCodeHelper
    {
        /// <summary>
        /// Validation Code generated fromt these charaters.
        /// Note: l,L 1(number), o, O, 0(number) are removed
        /// </summary>
        private const string strValidateCodeBound = "abcdefghijkmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ23456789";

        private static string[] Fonts = new string[] {  "Helvetica",
                                                 "Geneva",
                                                 "sans-serif",
                                                 "Verdana",
                                                 "Times New Roman",
                                                 "Courier New",
                                                 "Arial"
                                             };

        /// <summary>
        /// Generate random string
        /// </summary>
        public static string GetRandomString(int numberLength)
        {
            string valString = string.Empty;
            Random theRandomNumber = new Random((int)DateTime.Now.Ticks);

            for (int int_index = 0; int_index < numberLength; int_index++)
                valString += strValidateCodeBound[theRandomNumber.Next(strValidateCodeBound.Length - 1)].ToString();

            return valString;
        }

        /// <summary>
        /// Generate random Color
        /// </summary>
        private static Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);

            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }

        public static string CreateBase64ImageSrc(string validateCode)
        {
            int int_ImageWidth = validateCode.Length * 22;
            Random newRandom = new Random();

            Bitmap theBitmap = new Bitmap(int_ImageWidth + 6, 38);
            Graphics theGraphics = Graphics.FromImage(theBitmap);

            theGraphics.Clear(Color.White);

            drawLine(theGraphics, theBitmap, newRandom);
            theGraphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, theBitmap.Width - 1, theBitmap.Height - 1);

            for (int int_index = 0; int_index < validateCode.Length; int_index++)
            {
                Matrix X = new Matrix();
                X.Shear((float)newRandom.Next(0, 300) / 1000 - 0.25f, (float)newRandom.Next(0, 100) / 1000 - 0.05f);
                theGraphics.Transform = X;
                string str_char = validateCode.Substring(int_index, 1);
                var startFontColor = Color.FromArgb(newRandom.Next(255, 256), newRandom.Next(0, 256), newRandom.Next(0, 256), newRandom.Next(0, 256));
                var endFontColor = Color.FromArgb(newRandom.Next(255, 256), newRandom.Next(0, 256), newRandom.Next(0, 256), newRandom.Next(0, 256));
                System.Drawing.Drawing2D.LinearGradientBrush newBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, theBitmap.Width, theBitmap.Height), startFontColor, endFontColor, 1.2f, true);
                Point thePos = new Point(int_index * 21 + 1 + newRandom.Next(3), 1 + newRandom.Next(13));

                Font theFont = new Font(Fonts[newRandom.Next(Fonts.Length - 1)], newRandom.Next(14, 18), FontStyle.Bold);

                theGraphics.DrawString(str_char, theFont, newBrush, thePos);
            }

            drawPoint(theBitmap, newRandom);

            var ms = new MemoryStream();
            theBitmap.Save(ms, ImageFormat.Png);
            return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()));
        }

        /// <summary>
        /// Draw Line for noise
        /// </summary>
        private static void drawLine(Graphics gfc, Bitmap img, Random ran)
        {
            var rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                var c = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                int x1 = ran.Next(img.Width);
                int y1 = ran.Next(img.Height);
                int x2 = ran.Next(img.Width);
                int y2 = ran.Next(img.Height);
                gfc.DrawLine(new Pen(c), x1, y1, x2, y2);
            }
        }

        /// <summary>
        /// Draw Point for noise
        /// </summary>
        private static void drawPoint(Bitmap img, Random ran)
        {
            for (int i = 0; i < 30; i++)
            {
                int x = ran.Next(img.Width);
                int y = ran.Next(img.Height);
                img.SetPixel(x, y, Color.FromArgb(ran.Next()));
            }
        }
    }
}