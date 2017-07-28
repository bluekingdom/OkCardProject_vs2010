// OkCardSDK.cpp : 定义 DLL 的初始化例程。
//

#include "stdafx.h"
#include "OkCardSDK.h"

#include "Okapi64.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

namespace OkCardSDK {

	struct CPrivateMember {
		CPrivateMember(): m_hBoard(0), m_pBitmapStream(NULL), m_nBitmapStreamLength(0) 
		{ memset(&m_blk, 0, sizeof(m_blk)); }

		HANDLE m_hBoard;
		BLOCKINFO m_blk;
		char* m_pBitmapStream;
		int m_nBitmapStreamLength;
	};

#define DECLARE_MEMBER() \
	auto& m_blk = pMember->m_blk; \
	auto& m_hBoard = pMember->m_hBoard; \
	auto& m_pBitmapStream = pMember->m_pBitmapStream; \
	auto& m_nBitmapStreamLength = pMember->m_nBitmapStreamLength;

#define HANDLE_TO_MEMBER() \
	auto pMember = reinterpret_cast<CPrivateMember*>(hHandle); \
	if (!pMember) return EC_SYS_ERROR; \
	DECLARE_MEMBER()

#define MEMBER_TO_HANDLE() \
	hHandle = reinterpret_cast<Handle>(pMember); \
	if (!pMember) { return EC_SYS_ERROR; }

	long exSetBitmapHeader(LPBITMAPINFOHEADER lpbi,short width,short height, short bits, short form)
	{
		long	wbytes;

		if(!lpbi)
			return FALSE;


		if(form==FORM_RGB555)
			bits=16;
		wbytes=((((width*bits)+31)&~31)>>3);//bmp need 4 byte align

		lpbi->biWidth=width;
		lpbi->biHeight=height;

		lpbi->biSize=sizeof(BITMAPINFOHEADER);
		lpbi->biPlanes=1;

		lpbi->biBitCount=bits;
		lpbi->biSizeImage=wbytes*height;


		lpbi->biClrUsed=0;
		//special format for 555,565 & 32 
		if(form==FORM_RGB555 || form==FORM_RGB565 || form==FORM_RGB8888 ) 
			lpbi->biCompression=BI_BITFIELDS;
		else
			lpbi->biCompression=0;
		if(lpbi->biCompression==BI_BITFIELDS ) {//
			DWORD	*lpmask;
			lpmask=(DWORD *)((LPSTR)lpbi+lpbi->biSize);

			if(form==FORM_RGB555) {
				lpmask[2]=0x001f; //blue
				lpmask[1]=0x03e0;
				lpmask[0]=0x7c00;
			}
			else if(form==FORM_RGB565) {
				lpmask[2]=0x001f; //blue
				lpmask[1]=0x07e0;
				lpmask[0]=0xf800;
			}
			else if(bits==32 ) {
				lpmask[2]=0x0000ff;
				lpmask[1]=0x00ff00;
				lpmask[0]=0xff0000;
			}
		}
		else if(bits<=14) { // 8,10,12
			int		i,ratio;
			RGBQUAD	*lpRGB=(RGBQUAD *)((LPSTR)lpbi+lpbi->biSize);

			lpbi->biClrUsed=(1<<bits);
			ratio=lpbi->biClrUsed/256;

			for(i=0; i<(short)lpbi->biClrUsed;i++) {
				lpRGB[i].rgbBlue=i/ratio;
				lpRGB[i].rgbGreen=i/ratio;
				lpRGB[i].rgbRed=i/ratio;
				lpRGB[i].rgbReserved=0;
			}

		} 
		lpbi->biClrImportant=lpbi->biClrUsed;

		return lpbi->biClrUsed;
	}

	OKCARD_SDK_API ErrorCode Init(INOUT Handle& hHandle, OUT Image& image)
	{
		CPrivateMember *pMember = new CPrivateMember;

		MEMBER_TO_HANDLE();
		DECLARE_MEMBER();

		MLONG	lIndex=-1;

		if (m_pBitmapStream) {
			return EC_SYS_ERROR;
		}

		m_hBoard=okOpenBoard(&lIndex);

		if (m_hBoard == 0)
		{
			return EC_SYS_ERROR;
		}

		RECT	rect;
		rect.right=-1;
		okSetTargetRect(m_hBoard,BUFFER,&rect);

		long form=okSetCaptureParam(m_hBoard, CAPTURE_SCRRGBFORMAT, GETCURRPARAM);

		m_blk.iBitCount= HIWORD(form);
		m_blk.iHeight = (rect.bottom - rect.top);
		m_blk.iWidth =(rect.right - rect.left);

		const int nBitmapHeaderSize = 5120; // magic number, haha
		m_nBitmapStreamLength = m_blk.iHeight * m_blk.iWidth * m_blk.iBitCount / 8 + nBitmapHeaderSize;
		m_pBitmapStream = (char*)new char[m_nBitmapStreamLength];
		m_blk.lpBits = (LPBYTE)(m_pBitmapStream + nBitmapHeaderSize);
		memset(m_pBitmapStream, 0, m_nBitmapStreamLength);

		exSetBitmapHeader((LPBITMAPINFOHEADER)m_pBitmapStream, (short)m_blk.iWidth, (short)m_blk.iHeight, m_blk.iBitCount, LOWORD(form) );

		if (!m_blk.lpBits)
		{
			return EC_SYS_ERROR;
		}

		int info= okSetCaptureParam(m_hBoard,CAPTURE_MISCCONTROL,-1);

		info = info | 0x00000001; //设置为中断方式
		okSetCaptureParam(m_hBoard,CAPTURE_MISCCONTROL,info);

		int no=okGetSeqCapture(m_hBoard,0,0); //开始采集第一帧

		image.nChannels = m_blk.iBitCount / 8;
		image.nWidth = m_blk.iWidth;
		image.nHeight = m_blk.iHeight;
		image.pData = (char*)m_blk.lpBits;

		// in order to flip bitmap from up to down
		m_blk.iHeight = -m_blk.iHeight;

		return EC_NO_ERROR;
	}

	OKCARD_SDK_API ErrorCode Release(INOUT Handle& hHandle)
	{
		HANDLE_TO_MEMBER();

		if (m_hBoard)
		{
			okGetSeqCapture(m_hBoard,NULL,-1);//结束采集
			okStopCapture(m_hBoard);
			okCloseBoard(m_hBoard);
		}
		m_hBoard = 0;

		if (m_pBitmapStream)
		{
			delete[] m_pBitmapStream;
		}
		m_pBitmapStream = NULL;
		memset(&m_blk, 0, sizeof(m_blk));

		return EC_NO_ERROR;
	}

	OKCARD_SDK_API ErrorCode CaptureImage(IN Handle hHandle, OUT Image& image)
	{
		HANDLE_TO_MEMBER();

		int no=okGetSeqCapture(m_hBoard,0,1);//开始采集第一帧
		okConvertRect(m_hBoard,(TARGET)&m_blk,0,BUFFER,(short)no,1);
		image.nChannels = m_blk.iBitCount / 8;
		image.nWidth = m_blk.iWidth;
		image.nHeight = -m_blk.iHeight;
		image.pData = (char*)m_blk.lpBits;

		return EC_NO_ERROR;
	}


	OKCARD_SDK_API ErrorCode GetLastImageBitmapStream(IN Handle hHandle, OUT char* &pBitmapStream, OUT long &nBitmapStreamLen)
	{
		HANDLE_TO_MEMBER();

		pBitmapStream = (char*)m_pBitmapStream;
		nBitmapStreamLen = m_nBitmapStreamLength;

		return EC_NO_ERROR;
	}

}


