using CommanApi.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace CommanApi.Models
{
    public class Common
    {
       
        public Common()
        {
        }
        public static int ResizeFile(int Width, int Height, IFormFile sourcePath, string saveFilePath)
        {
            int intReturnValue = 0;
            try
            {
                Stream fileStream = sourcePath.OpenReadStream();
                // variable for percentage resize 
                float percentageResize = 0;
                float percentageResizeW = 0;
                float percentageResizeH = 0;

                // variables for the dimension of source and cropped image 
                int sourceX = 0;
                int sourceY = 0;
                int destX = 0;
                int destY = 0;

                // Create a bitmap object file from source file 
                Bitmap sourceImage = new Bitmap(fileStream);

                // Set the source dimension to the variables 
                int sourceWidth = sourceImage.Width;
                int sourceHeight = sourceImage.Height;

                // Calculate the percentage resize 
                percentageResizeW = Width / (float)sourceWidth;
                percentageResizeH = Height / (float)sourceHeight;

                // Checking the resize percentage 
                if (percentageResizeH < percentageResizeW)
                {
                    percentageResize = percentageResizeW;
                    destY = Convert.ToInt16((Height - sourceHeight * percentageResize) / 2);
                }
                else
                {
                    percentageResize = percentageResizeH;
                    destX = Convert.ToInt16((Width - sourceWidth * percentageResize) / 2);
                }

                // Set the new cropped percentage image
                int destWidth = (int)Math.Round(sourceWidth * percentageResize);
                int destHeight = (int)Math.Round(sourceHeight * percentageResize);

                // Create the image object 
                using (Bitmap objBitmap = new Bitmap(Width, Height))
                {
                    objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                    using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                    {
                        // Set the graphic format for better result cropping 
                        objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        objGraphics.DrawImage(sourceImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

                        // Save the file path, note we use png format to support png file 
                        objBitmap.Save(saveFilePath, ImageFormat.Jpeg);
                    }
                }
                intReturnValue = 1;
            }
            catch
            {
                intReturnValue = 0;
            }
            return intReturnValue;
        }

        public static string StringChange(string Title)
        {
            string strReturnLink = "";
            try
            {
                string strTitle = Title.ToString();

                strTitle = strTitle.Trim();

                //Trim "-" Hyphen
                strTitle = strTitle.Trim('-');

                strTitle = strTitle.ToLower();
                char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();

                //Replace . with - hyphen
                strTitle = strTitle.Replace(".", "-");

                //Replace Special-Characters
                for (int i = 0; i < chars.Length; i++)
                {
                    string strChar = chars.GetValue(i).ToString();
                    if (strTitle.Contains(strChar))
                    {
                        strTitle = strTitle.Replace(strChar, string.Empty);
                    }
                }

                //Replace all spaces with one "-" hyphen
                strTitle = strTitle.Replace(" ", "-");

                //Replace multiple "-" hyphen with single "-" hyphen.
                strTitle = strTitle.Replace("_", "-");
                strTitle = strTitle.Replace("__", "-");

                //Trim Start and End Spaces.
                strTitle = strTitle.Trim();

                //Trim "-" Hyphen
                strTitle = strTitle.Trim('-');

                strReturnLink = strTitle;
            }
            catch (Exception exMessage)
            {
                //  Common.WriteError(exMessage.Message);
            }
            return strReturnLink;
        }
        public static void WriteError(string errorMessage, string url)
        {
            try
            {

                var folderName = Path.Combine("Error");
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", folderName);
                string path = Path.Combine(uploadsFolder, DateTime.Today.ToString("dd-mm-yy") + ".txt");
                //string path = "~/Error/" + DateTime.Today.ToString("dd-mm-yy") + ".txt";
                if (!File.Exists(Path.Combine(path)))
                {
                    File.Create(Path.Combine(path)).Close();
                }
                using (StreamWriter w = File.AppendText(Path.Combine(path)))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in: " + url +
                                  ". Error Message:" + errorMessage;
                    w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message, "Error.cs");
            }

        }

       
        public static string StringChange2(string Title)
        {
            return Title?.Replace("-", " ");
        }
    }
}
