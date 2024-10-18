using System;
using System.Threading;
using UnityEngine;

public class Image
{
    private const int INTERVAL = 5;

    private const int MAXTIME = 500;

    public Texture2D texture = new Texture2D(1, 1);

    public static Image imgTemp;

    public static string filenametemp;

    public static byte[] datatemp;

    public static Image imgSrcTemp;

    public static int xtemp;

    public static int ytemp;

    public static int wtemp;

    public static int htemp;

    public static int transformtemp;

    public int w;

    public int h;

    public static int status;

    public Color colorBlend = Color.black;

    public static Image createEmptyImage()
    {
        return __createEmptyImage();
    }

    public static Image createImageOLD(string filename)
    {
        return __createImage(filename);
    }

    public static Image createImage(string filename)
    {
        return _createImageByPNG(filename);
    }

    public static Image createImageSprite(string filename)
    {
        return _createImageByPNG(filename);
    }

    //public static Image _createImageByPNG(string path)
    //{
    //	Object obj = Resources.Load(path);
    //	Texture2D val = (Texture2D)(object)((obj is Texture2D) ? obj : null);
    //	if ((Object)(object)val == (Object)null)
    //	{
    //		throw new Exception("NULL POINTER EXCEPTION AT Image _createImageByPNG " + path);
    //	}
    //	Image image = new Image();
    //	image.texture = val;
    //	image.w = ((Texture)val).width;
    //	image.h = ((Texture)val).height;
    //	return image;
    //}


    public static Image _createImageByPNG(string path)
    {
        Texture2D texture2D = Resources.Load(path) as Texture2D;
        if (texture2D == null)
        {
            throw new Exception("NULL POINTER EXCEPTION AT Image _createImageByPNG " + path);
        }
        Image image = new Image();
        image.texture = texture2D;
        image.w = texture2D.width;
        image.h = texture2D.height;
        return image;
    }

    public static Image createImage(byte[] imageData, string path)
    {
        return __createImage(imageData);
    }

    public static Image createImage(Image src, int x, int y, int w, int h, int transform)
    {
        return __createImage(src, x, y, w, h, transform);
    }

    public static Image createImage(int w, int h)
    {
        return __createImage(w, h);
    }

    public static Image createImage(Image img)
    {
        Image image = createImage(img.w, img.h);
        image.texture = img.texture;
        image.texture.Apply();
        return image;
    }

    public static Image createImage(sbyte[] imageData, int offset, int lenght, string path)
    {
        if (offset + lenght > imageData.Length)
        {
            return null;
        }
        byte[] array = new byte[lenght];
        for (int i = 0; i < lenght; i++)
        {
            array[i] = convertSbyteToByte(imageData[i + offset]);
        }
        return createImage(array, path);
    }

    public static byte convertSbyteToByte(sbyte var)
    {
        if (var > 0)
        {
            return (byte)var;
        }
        return (byte)(var + 256);
    }

    public static byte[] convertArrSbyteToArrByte(sbyte[] var)
    {
        byte[] array = new byte[var.Length];
        for (int i = 0; i < var.Length; i++)
        {
            if (var[i] > 0)
            {
                array[i] = (byte)var[i];
            }
            else
            {
                array[i] = (byte)(var[i] + 256);
            }
        }
        return array;
    }

    public static Image createRGBImage(int[] rbg, int w, int h, bool bl)
    {
        //IL_0022: Unknown result type (might be due to invalid IL or missing references)
        //IL_0027: Unknown result type (might be due to invalid IL or missing references)
        Image image = createImage(w, h);
        Color[] array = (Color[])(object)new Color[rbg.Length];
        for (int i = 0; i < array.Length; i++)
        {
            ref Color reference = ref array[i];
            reference = setColorFromRBG(rbg[i]);
        }
        image.texture.SetPixels(0, 0, w, h, array);
        image.texture.Apply();
        return image;
    }

    public static Color setColorFromRBG(int rgb)
    {
        int num = rgb & 0xFF;
        int num2 = (rgb >> 8) & 0xFF;
        int num3 = (rgb >> 16) & 0xFF;
        float num4 = (float)num / 256f;
        float num5 = (float)num2 / 256f;
        float num6 = (float)num3 / 256f;
        Color result = new Color(num6, num5, num4);
        return result;
    }

    public static void update()
    {
        if (status == 2)
        {
            status = 1;
            imgTemp = __createEmptyImage();
            status = 0;
        }
        else if (status == 3)
        {
            status = 1;
            imgTemp = __createImage(filenametemp);
            status = 0;
        }
        else if (status == 4)
        {
            status = 1;
            imgTemp = __createImage(datatemp);
            status = 0;
        }
        else if (status == 5)
        {
            status = 1;
            imgTemp = __createImage(imgSrcTemp, xtemp, ytemp, wtemp, htemp, transformtemp);
            status = 0;
        }
        else if (status == 6)
        {
            status = 1;
            imgTemp = __createImage(wtemp, htemp);
            status = 0;
        }
    }

    private static Image _createEmptyImage()
    {
        if (status != 0)
        {
            Cout.LogError("CANNOT CREATE EMPTY IMAGE WHEN CREATING OTHER IMAGE");
            return null;
        }
        imgTemp = null;
        status = 2;
        int i;
        for (i = 0; i < 500; i++)
        {
            Thread.Sleep(5);
            if (status == 0)
            {
                break;
            }
        }
        if (i == 500)
        {
            Cout.LogError("TOO LONG FOR CREATE EMPTY IMAGE");
            status = 0;
        }
        return imgTemp;
    }

    private static Image _createImage(string filename)
    {
        if (status != 0)
        {
            Cout.LogError("CANNOT CREATE IMAGE " + filename + " WHEN CREATING OTHER IMAGE");
            return null;
        }
        imgTemp = null;
        filenametemp = filename;
        status = 3;
        int i;
        for (i = 0; i < 500; i++)
        {
            Thread.Sleep(5);
            if (status == 0)
            {
                break;
            }
        }
        if (i == 500)
        {
            Cout.LogError("TOO LONG FOR CREATE IMAGE " + filename);
            status = 0;
        }
        return imgTemp;
    }

    private static Image _createImage(byte[] imageData)
    {
        if (status != 0)
        {
            Cout.LogError("CANNOT CREATE IMAGE(FromArray) WHEN CREATING OTHER IMAGE");
            return null;
        }
        imgTemp = null;
        datatemp = imageData;
        status = 4;
        int i;
        for (i = 0; i < 500; i++)
        {
            Thread.Sleep(5);
            if (status == 0)
            {
                break;
            }
        }
        if (i == 500)
        {
            Cout.LogError("TOO LONG FOR CREATE IMAGE(FromArray)");
            status = 0;
        }
        return imgTemp;
    }

    private static Image _createImage(Image src, int x, int y, int w, int h, int transform)
    {
        if (status != 0)
        {
            Cout.LogError("CANNOT CREATE IMAGE(FromSrcPart) WHEN CREATING OTHER IMAGE");
            return null;
        }
        imgTemp = null;
        imgSrcTemp = src;
        xtemp = x;
        ytemp = y;
        wtemp = w;
        htemp = h;
        transformtemp = transform;
        status = 5;
        int i;
        for (i = 0; i < 500; i++)
        {
            Thread.Sleep(5);
            if (status == 0)
            {
                break;
            }
        }
        if (i == 500)
        {
            Cout.LogError("TOO LONG FOR CREATE IMAGE(FromSrcPart)");
            status = 0;
        }
        return imgTemp;
    }

    private static Image _createImage(int w, int h)
    {
        if (status != 0)
        {
            Cout.LogError("CANNOT CREATE IMAGE(w,h) WHEN CREATING OTHER IMAGE");
            return null;
        }
        imgTemp = null;
        wtemp = w;
        htemp = h;
        status = 6;
        int i;
        for (i = 0; i < 500; i++)
        {
            Thread.Sleep(5);
            if (status == 0)
            {
                break;
            }
        }
        if (i == 500)
        {
            Cout.LogError("TOO LONG FOR CREATE IMAGE(w,h)");
            status = 0;
        }
        return imgTemp;
    }

    public static byte[] loadData(string filename)
    {
        Image image = new Image();
        TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        if (textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0)
        {
            throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
        }
        sbyte[] array = ArrayCast.cast(textAsset.bytes);
        Debug.LogError("CHIEU DAI MANG BYTE IMAGE CREAT = " + array.Length);
        return textAsset.bytes;
    }

    private static Image __createImage(string filename)
    {
        Image image = new Image();
        TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        if (textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0)
        {
            throw new Exception("NULL POINTER EXCEPTION AT Image __createImage");
        }
        image.texture.LoadImage(textAsset.bytes);
        image.w = image.texture.width;
        image.h = image.texture.height;
        setTextureQuality(image);
        return image;
    }

    private static Image __createImage(byte[] imageData)
    {
        if (imageData == null || imageData.Length == 0)
        {
            Cout.LogError("Create Image from byte array fail");
            return null;
        }
        Image image = new Image();
        try
        {
            image.texture.LoadImage(imageData);
            image.w = ((Texture)image.texture).width;
            image.h = ((Texture)image.texture).height;
            setTextureQuality(image);
        }
        catch (Exception)
        {
            Cout.LogError("CREAT IMAGE FROM ARRAY FAIL \n" + Environment.StackTrace);
        }
        return image;
    }

    private static Image __createImage(Image src, int x, int y, int w, int h, int transform)
    {
        //IL_000a: Unknown result type (might be due to invalid IL or missing references)
        //IL_0014: Expected O, but got Unknown
        //IL_005a: Unknown result type (might be due to invalid IL or missing references)
        Image image = new Image();
        image.texture = new Texture2D(w, h);
        y = ((Texture)src.texture).height - y - h;
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                int num = i;
                if (transform == 2)
                {
                    num = w - i;
                }
                int num2 = j;
                image.texture.SetPixel(i, j, src.texture.GetPixel(x + num, y + num2));
            }
        }
        image.texture.Apply();
        image.w = ((Texture)image.texture).width;
        image.h = ((Texture)image.texture).height;
        setTextureQuality(image);
        return image;
    }

    private static Image __createEmptyImage()
    {
        return new Image();
    }

    public static Image __createImage(int w, int h)
    {
        //IL_000b: Unknown result type (might be due to invalid IL or missing references)
        //IL_0015: Expected O, but got Unknown
        Image image = new Image();
        image.texture = new Texture2D(w, h, (TextureFormat)4, false);
        setTextureQuality(image);
        image.w = w;
        image.h = h;
        image.texture.Apply();
        return image;
    }

    public static int getImageWidth(Image image)
    {
        return image.getWidth();
    }

    public static int getImageHeight(Image image)
    {
        return image.getHeight();
    }

    public int getWidth()
    {
        return w / mGraphics.zoomLevel;
    }

    public int getHeight()
    {
        return h / mGraphics.zoomLevel;
    }

    private static void setTextureQuality(Image img)
    {
        setTextureQuality(img.texture);
    }

    public static void setTextureQuality(Texture2D texture)
    {
        ((Texture)texture).anisoLevel = 0;
        ((Texture)texture).filterMode = (FilterMode)0;
        ((Texture)texture).mipMapBias = 0f;
        ((Texture)texture).wrapMode = (TextureWrapMode)1;
    }

    public Color[] getColor()
    {
        return texture.GetPixels();
    }

    public int getRealImageWidth()
    {
        return w;
    }

    public int getRealImageHeight()
    {
        return h;
    }

    public void getRGB(int[] data, int x1, int x2, int x3, int x4, int x5, int x6)
    {
        Color[] pixels = texture.GetPixels();
    }
}
