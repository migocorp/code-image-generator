using System;

using ThoughtWorks.QRCode.Codec;
using System.Drawing.Imaging;

public partial class create : System.Web.UI.Page
{
    string strCode;

    #region QRCode Var
   
    string strQRCodeScale;
    string strQRCodeVersion;
    int intQRCodeSize = 7;//QR Code 图片面积大小
    int intQRCodeVersion = 2;// QR Code编码密度，密度越大，信息容量大。取值范围1到40.
   
    #endregion

    private void Page_Load(object sender, EventArgs e)
    {
        strCode = GetUrlParm("code");

        strQRCodeScale = GetUrlParm("size");
        strQRCodeVersion = GetUrlParm("version");


        if (strCode.Length == 0)
        {
            Response.Write("参数(code)值不存在");
            return;
        }

        if (strQRCodeScale.Length > 0)
        {
            if (Int32.TryParse(strQRCodeScale, out intQRCodeSize))
            {
            }
        }

        if (strQRCodeVersion.Length > 0)
        {
            if (Int32.TryParse(strQRCodeVersion, out intQRCodeVersion))
            {
            }
        }
     
         
      QRCode();
       

    }

    private void QRCode()
    {
        try
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//AlphaNumeric Numeric Byte（字母，数字，字节）
            qrCodeEncoder.QRCodeScale = intQRCodeSize;
            qrCodeEncoder.QRCodeVersion = intQRCodeVersion;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;//L,M,Q,H

            System.Drawing.Image image = qrCodeEncoder.Encode(strCode, System.Text.Encoding.UTF8);
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