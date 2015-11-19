using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Domain
{
    public class dev_img
    {
        public dev_img()
        {
            //
        }

        public void CreateThumbNails(List<string> smallFilesList, string img_folder, string tm_folder, string small_folder, string tmFileEx, int max_width, int max_height)
        {
            string tm_file = img_folder + tm_folder + "{0}";
            string small_file = img_folder + small_folder + "{0}";

            foreach (string smallFile in smallFilesList)
            {
                CreateThumbNail(
                            string.Format(tm_file, smallFile.Replace(".jpg", "." + tmFileEx))
                        , string.Format(small_file, smallFile)
                        , max_width
                        , max_height
                        , "image/" + tmFileEx
                        );
            }
        }

        public bool CreateThumbNail(string dest_file, string src_file, int max_width, int max_height, string tmFileType)
        {
            try
            {
                Image load_img = Image.FromFile(src_file);

                int height = max_height;
                int width = (load_img.Width * height) / load_img.Height;

                int img_w_ris = (load_img.Width - load_img.Height * max_width / max_height) / 2;
                if (width < max_width)
                {
                    width = max_width;
                    height = (load_img.Height * width) / load_img.Width;
                    img_w_ris = 0;
                }

                Image tm_img = new Bitmap(max_width, max_height);

                Graphics gr = Graphics.FromImage(tm_img);
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                gr.DrawImage(load_img, new Rectangle(0, 0, width, height),
                    img_w_ris, 0, load_img.Width, load_img.Height, GraphicsUnit.Pixel);

                //Encoder myEncoder;
                ImageCodecInfo inf = GetCodecInfo(tmFileType);
                EncoderParameters encParams = new EncoderParameters(1);
                encParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)90);

                tm_img.Save(dest_file, inf, encParams);

                tm_img.Dispose();
                load_img.Dispose();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private ImageCodecInfo GetCodecInfo(string mt)
        {
            ImageCodecInfo[] ici = ImageCodecInfo.GetImageEncoders();
            int idx = 0;
            for (int i = 0; i < ici.Length; i++)
            {
                if (ici[i].MimeType == mt)
                {
                    idx = i;
                    break;
                }
            }
            return ici[idx];
        }

    }
}