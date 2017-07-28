// OkCardSDK.h : OkCardSDK DLL 的主头文件
//

#pragma once

#ifdef OKCARD_SDK_EXPORTS
#define OKCARD_SDK_API extern "C" __declspec(dllexport) 
#else
#define OKCARD_SDK_API extern "C" __declspec(dllimport) 
#endif

#ifndef IN
#define IN
#endif

#ifndef OUT
#define OUT
#endif

#ifndef INOUT
#define INOUT
#endif


namespace OkCardSDK {
	typedef unsigned long long Handle;
	typedef char* LPBitmapStream;

	enum ErrorCode {
		EC_NO_ERROR = 0,
		EC_SYS_ERROR = 1,
	};

	struct Image
	{
		Image() : pData(nullptr), nWidth(0), nHeight(0), nChannels(0) {}
		Image(char* p, int w, int h, int c) : pData(p), nWidth(w), nHeight(h), nChannels(c) {}

		char* pData;
		int nWidth;
		int nHeight;
		int nChannels;
	};

	OKCARD_SDK_API ErrorCode Init(INOUT Handle& hHandle, OUT Image& image);
	OKCARD_SDK_API ErrorCode Release(INOUT Handle& hHandle);

	OKCARD_SDK_API ErrorCode CaptureImage(IN Handle hHandle, OUT Image& image);

	OKCARD_SDK_API ErrorCode GetLastImageBitmapStream(IN Handle hHandle, OUT char* &pBitmapStream, OUT long &nBitmapStreamLen);
}
