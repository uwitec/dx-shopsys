using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;


using System.Drawing;

/// <summary>
/// 生成缩略图、为图片添加文字水印、图片水印的类
/// </summary>
namespace web
{
    public class ThumNail
    {
        public ThumNail()
        {

        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">
        /// 生成缩略图的方式
        /// "HW"://指定高宽缩放（可能变形）
        /// "HW2"://指定高宽自适应缩放（不变形）
        /// "W"://指定宽，高按比例 
        /// "H"://指定高，宽按比例
        /// "Cut"://指定高宽裁减（不变形）
        /// "HWCut"://指定高宽裁减（不变形,防止空白）
        /// </param>    
        public static void MakeThumNail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "HW2"://指定高宽自适应缩放（不变形）
                    toheight = originalImage.Height * width / originalImage.Width;
                    towidth = originalImage.Width * height / originalImage.Height;
                    if (toheight > height)
                    {
                        toheight = height;
                    }
                    if (towidth > width)
                    {
                        towidth = width;
                    }
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        //原图比缩略图更宽，则按高度裁切
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        //原图比缩略图更高，则按宽度裁切
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;

                
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }

        /// <summary>    
        /// 在图片上添加文字水印    
        /// </summary>    
        /// <param name="path">要添加水印的图片路径</param>    
        /// <param name="syPath">生成的水印图片存放的位置</param>    
        public static void AddWaterWord(string path, string syPath)
        {
            string syWord = Constants.DOMAIN;
            System.Drawing.Image image = System.Drawing.Image.FromFile(path);        
            
            //新建一个画板        
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(image);
            graphic.DrawImage(image, 0, 0, image.Width, image.Height);  
      
            //设置字体        
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 6); 
            
            //设置字体颜色        
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            graphic.DrawString(syWord, f, b, 35, 35);
            graphic.Dispose();        
            
            //保存文字水印图片        
            image.Save(syPath);
            image.Dispose();
        }

        /// <summary>    
        /// 在图片上添加图片水印    
        /// </summary>    
        /// <param name="path">原服务器上的图片路径</param>    
        /// <param name="syPicPath">水印图片的路径</param>    
        /// <param name="waterPicPath">生成的水印图片存放路径</param>    
        public static void AddWaterPic(string path, string syPicPath, string waterPicPath)
        { 
            System.Drawing.Image image = System.Drawing.Image.FromFile(path); 
            System.Drawing.Image waterImage = System.Drawing.Image.FromFile(syPicPath); 
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(image);
            graphic.DrawImage(waterImage, new System.Drawing.Rectangle(image.Width - waterImage.Width, image.Height - waterImage.Height, waterImage.Width, waterImage.Height), 0, 0, waterImage.Width, waterImage.Height, System.Drawing.GraphicsUnit.Pixel); 
            graphic.Dispose(); 
            image.Save(waterPicPath); 
            image.Dispose(); 
        }

    }
}
