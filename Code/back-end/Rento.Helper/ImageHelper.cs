using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Rento.Helper
{
    public class ImageHelper
    {
        public static void WriteOnImage(string fileImagePath, string text, Stream outPutSream)
        {
            System.Drawing.Image bitmap = (System.Drawing.Image)Bitmap.FromFile(fileImagePath); // set image    
            Graphics graphicsImage = Graphics.FromImage(bitmap);
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;
            if (text.Length > 21)
                text = text.Substring(0, 18) + "...";
            else
            {
                while (text.Length < 21)
                    text += " ";
            }
            graphicsImage.DrawString(text, new Font("arial", 20,
            FontStyle.Bold), new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#ED2226")), new Point(650, 475),
            stringformat);
            bitmap.Save(outPutSream, ImageFormat.Jpeg);

        }
        public static string WriteOnImage(string fileImagePath, string text, string outputPath)
        {
            System.Drawing.Image bitmap = (System.Drawing.Image)Bitmap.FromFile(fileImagePath); // set image    
            Graphics graphicsImage = Graphics.FromImage(bitmap);
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;
            if (text.Length > 21)
                text = text.Substring(0, 18) + "...";
            else
            {
                while (text.Length < 21)
                    text += " ";
            }
            graphicsImage.DrawString( text, new Font("arial", 20,
            FontStyle.Bold), new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#ED2226")), new Point(650, 475),
            stringformat);
            var fileId = Guid.NewGuid().ToString() + ".jpeg";
            string path = Path.Combine(outputPath, fileId);
            using (FileStream file = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                bitmap.Save(file, ImageFormat.Jpeg);
            return fileId;
        }
    }
}
