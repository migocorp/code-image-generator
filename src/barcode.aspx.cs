using System;
using BarcodeLib;

using System.Drawing.Imaging;

public partial class BarcodeImg : System.Web.UI.Page
{
    string strCode;

    #region BarCode Var
   
    string strBarCodeWidth;
    string strBarCodeHeight;
    int intBarCodeWidth = 220;
    int intBarCodeHeight = 80;
    string strBarCodeIncludeText;
    Barcode b = new Barcode();
   
    #endregion

    private void Page_Load(object sender, EventArgs e)
    {
        strCode = GetUrlParm("code");
   
        strBarCodeHeight = GetUrlParm("height");
        strBarCodeWidth = GetUrlParm("width");
        strBarCodeIncludeText = GetUrlParm("text");

        if (strCode.Length == 0)
        {
            Response.Write("参数(code)值不存在");
            return;
        }

      

        if (strBarCodeHeight.Length > 0)
        {
            if (Int32.TryParse(strBarCodeHeight, out intBarCodeHeight))
            {
            }
        }

        if (strBarCodeWidth.Length > 0)
        {
            if (Int32.TryParse(strBarCodeWidth, out intBarCodeWidth))
            {
            }
        }

         BarCode();
      
    }

    private void BarCode()
    {
        b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
        b.IncludeLabel = false;
        BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
        if (strBarCodeIncludeText == "1")
        {
            b.IncludeLabel = true;
        }

        try
        {
            System.Drawing.Image image = b.Encode(type, strCode, intBarCodeWidth, intBarCodeHeight);
            image.Save(Response.OutputStream, ImageFormat.Jpeg);
            Response.ContentType = "image/Jpeg";
            Response.Flush();
            image.Dispose();
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private string GetUrlParm(string paramName)
    {
        string param = Request.Params[paramName] as string;

        return param != null && paramName.Trim().Length > 0 ? param : "";
    }

    
    
}