using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using SYY;
using Utils;

namespace AI_Analysis_GUI
{
    public partial class Form1 : Form
    {
        public ulong hAnalysisHandle;
        public ulong hVideoHandle;
        public ulong hOkCardSDKHandle;
        public ErrorCode error_code = ErrorCode.SYY_NO_ERROR;
        Bitmap m_Bitmap;
        CImage m_cVideoFrame;
        public Thread m_videoCaptureThread { get; set; }
        public bool m_bIsCapture { get; set; }
        public Thread m_okCardCaptrueThread { get; set; }
        public bool m_bIsAnalysisOkCardCapture { get; set; }

        public Form1()
        {
            InitializeComponent();
            error_code = SDK.InitSDK();
            if (error_code != ErrorCode.SYY_NO_ERROR)
            {
                MessageBox.Show("初始化sdk失败.");
                return;
            }
            error_code = SDK.InitBUAnalysis(ref hAnalysisHandle);
            if (error_code != ErrorCode.SYY_NO_ERROR)
            {
                MessageBox.Show("初始化模型失败.");
                return;
            }
        }

        private void ShowImage(Bitmap bitmap)
        {
            PictureBox pb = (PictureBox)this.Controls.Find("MainImageBox", true)[0];
            pb.Image = bitmap;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            //pb.Refresh();
        }
        private void ShowImage(CImage image)
        {
            Bitmap cvt_bitmap = new Bitmap(image.nWidth, image.nHeight, image.nChannels * image.nWidth,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb, image.pData);

            ShowImage(cvt_bitmap);
        }

        private void ButtonOpenImage_Click(object sender, EventArgs e)
        {
            string img_file = FileSystem.OpenImageFile();
            if (img_file.Length == 0)
                return;

            m_Bitmap = Bitmap.FromFile(img_file) as Bitmap;
            ShowImage(m_Bitmap);
        }

        private void ButtonAnalysisImage_Click(object sender, EventArgs e)
        {
            CImage image = new CImage();

            if (false == SDK.CvtBitmap2CImage(m_Bitmap, ref image))
            {
                MessageBox.Show("CvtBitmap2CImage出错！");
                return;
            }

            BUAnalysisResult result = new BUAnalysisResult();
            error_code = SDK.ExecuteBUAnalysisFromImage(hAnalysisHandle, image, ref result);
            if (error_code != ErrorCode.SYY_NO_ERROR)
            {
                MessageBox.Show("运行分析出错，请查看log文件。");
                return;
            }

            error_code = SDK.DrawResult2Image(ref image, result);
            if (error_code != ErrorCode.SYY_NO_ERROR)
            {
                MessageBox.Show("运行转换出错，请查看log文件。");
                return;
            }


            ShowImage(image);
        }

        private void CaptureVideoThread()
        {
            ErrorCode error_code;
            BUAnalysisResult result = new BUAnalysisResult();

            ulong hHandle = 0;

            error_code = SDK.InitBUAnalysis(ref hHandle);
            if (error_code != ErrorCode.SYY_NO_ERROR)
            {
                MessageBox.Show("初始化模型出错，请查看log文件。");
                return;
            }

            while (m_bIsCapture)
            {
                error_code = SDK.GetVideoFrame(hVideoHandle, ref m_cVideoFrame);
                if (error_code != ErrorCode.SYY_NO_ERROR)
                {
                    if (error_code == ErrorCode.SYY_SYS_ERROR)
                    {
                        MessageBox.Show("获取视频帧失败.");
                    }
                    if (error_code == ErrorCode.SYY_VIDEO_END_FRAME)
                    {
                        MessageBox.Show("视频帧已读取完.");
                    }

                    break;
                }

                error_code = SDK.ExecuteBUAnalysisFromImage(hHandle, m_cVideoFrame, ref result);
                if (error_code != ErrorCode.SYY_NO_ERROR)
                {
                    MessageBox.Show("运行分析出错，请查看log文件。");
                    break;
                }

                error_code = SDK.DrawResult2Image(ref m_cVideoFrame, result);
                if (error_code != ErrorCode.SYY_NO_ERROR)
                {
                    MessageBox.Show("运行转换出错，请查看log文件。");
                    break;
                }

                Bitmap cvt_bitmap = new Bitmap(m_cVideoFrame.nWidth, m_cVideoFrame.nHeight, m_cVideoFrame.nChannels * m_cVideoFrame.nWidth,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb, m_cVideoFrame.pData);

                ShowImage(cvt_bitmap);
                Thread.Sleep(25);
            }

            m_bIsCapture = false;
            SDK.ReleaseBUAnalysis(ref hHandle);
            SDK.ReleaseVideo(ref hVideoHandle);
        }

        private void ButtonOpenVideo_Click(object sender, EventArgs e)
        {
            string video_file = FileSystem.OpenVideoFile();
            if (video_file.Length == 0)
                return;

            m_cVideoFrame = new CImage();
            IntPtr strPtr = Marshal.StringToHGlobalAnsi(video_file);

            error_code = SDK.LoadVideo(strPtr, video_file.Length, ref hVideoHandle, ref m_cVideoFrame);
            if (error_code != ErrorCode.SYY_NO_ERROR)
            {
                MessageBox.Show("加载视频出错.");
                return;
            }

            m_cVideoFrame.pData = Marshal.AllocHGlobal(m_cVideoFrame.nHeight * m_cVideoFrame.nWidth * m_cVideoFrame.nChannels);

        }

        private void ButtonAnalysisVideo_Click(object sender, EventArgs e)
        {
            m_bIsCapture = true;
            m_videoCaptureThread = new Thread(new ThreadStart(CaptureVideoThread));
            m_videoCaptureThread.Start();
        }

        private void ButtonStopVideo_Click(object sender, EventArgs e)
        {
            m_bIsCapture = false;
        }

        private void OkCardCaptureThread()
        {
            ErrorCode error_code;
            BUAnalysisResult result = new BUAnalysisResult();
            CImage image = new CImage();

            ulong hHandle = 0;

            error_code = SDK.InitBUAnalysis(ref hHandle);
            if (error_code != ErrorCode.SYY_NO_ERROR)
            {
                MessageBox.Show("初始化模型出错，请查看log文件。");
                return;
            }

            while (m_bIsCapture)
            {
                error_code = OkCardSDK.CaptureImage(hOkCardSDKHandle, ref image);
                if (error_code != ErrorCode.SYY_NO_ERROR)
                {
                    MessageBox.Show("初始化ok卡失败.");
                    break;
                }

                if (m_bIsAnalysisOkCardCapture)
                {
                    error_code = SDK.ExecuteBUAnalysisFromImage(hHandle, image, ref result);
                    if (error_code != ErrorCode.SYY_NO_ERROR)
                    {
                        MessageBox.Show("运行分析出错，请查看log文件。");
                        break;
                    }

                    error_code = SDK.DrawResult2Image(ref image, result);
                    if (error_code != ErrorCode.SYY_NO_ERROR)
                    {
                        MessageBox.Show("运行转换出错，请查看log文件。");
                        break;
                    }
                }

                Bitmap cvt_bitmap = new Bitmap(image.nWidth, image.nHeight, image.nChannels * image.nWidth,
                    System.Drawing.Imaging.PixelFormat.Format32bppRgb, image.pData);

                cvt_bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                ShowImage(cvt_bitmap);
                Thread.Sleep(33);
            }

            m_bIsCapture = false;
            m_bIsAnalysisOkCardCapture = false;
            SDK.ReleaseBUAnalysis(ref hHandle);
            OkCardSDK.Release(ref hOkCardSDKHandle);
        }

        private void ButtonOpenOkCard_Click(object sender, EventArgs e)
        {
            CImage image = new CImage();
            ErrorCode error_code;
            error_code = OkCardSDK.Init(ref hOkCardSDKHandle, ref image);

            if (error_code != ErrorCode.SYY_NO_ERROR)
            {
                MessageBox.Show("初始化ok卡失败.");
                return;
            }

            m_bIsCapture = true;
            m_bIsAnalysisOkCardCapture = false;

            m_okCardCaptrueThread = new Thread(new ThreadStart(OkCardCaptureThread));
            m_okCardCaptrueThread.Start();
        }

        private void ButtonAnalysisOkCard_Click(object sender, EventArgs e)
        {
            m_bIsAnalysisOkCardCapture = true;
        }

        private void ButtonCloseOkCard_Click(object sender, EventArgs e)
        {
            m_bIsCapture = false;
            m_bIsAnalysisOkCardCapture = false;
        }


    }
}
