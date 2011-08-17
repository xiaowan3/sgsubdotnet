//------------------------------------------------------------------------------
// File: PlayWnd.h
//
// Desc: DirectShow sample code - header file for video in window movie
//       player application.
//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------------------------

#include "stdafx.h"
//
// Function prototypes
//

HRESULT PlayMovieInWindow(LPCWSTR file);
HRESULT InitVideoWindow(int nMultiplier, int nDivider);
HRESULT _handleGraphEvent(void);
HRESULT StepOneFrame(void);
HRESULT StepFrames(int nFramesToStep);
HRESULT ModifyRate(double dRateAdjust);
HRESULT SetRate(double dRate);

BOOL GetFrameStepInterface(void);
BOOL GetClipFileName(LPTSTR szName);


void MoveVideoWindow(void);
void CheckVisibility(void);
void CloseInterfaces(void);


void PauseClip(void);
void SeekClip(double);
void StopClip(void);
void CloseClip(void);
double GetTime(void);
double GetDur(void);
bool _canStep();
HRESULT set_volume(double volume);
int GrabImg(LPCWSTR filename);

void Msg(TCHAR *szFormat, ...);

HRESULT AddGraphToRot(IUnknown *pUnkGraph, DWORD *pdwRegister);
void RemoveGraphFromRot(DWORD pdwRegister);

//
// Constants
//
#define VOLUME_FULL     0L
#define VOLUME_SILENCE  -10000L


// Begin default media search at root directory
#define DEFAULT_MEDIA_PATH  TEXT("\\\0")

// Defaults used with audio-only files
#define DEFAULT_AUDIO_WIDTH     240
#define DEFAULT_AUDIO_HEIGHT    120
#define DEFAULT_VIDEO_WIDTH     320
#define DEFAULT_VIDEO_HEIGHT    240
#define MINIMUM_VIDEO_WIDTH     200
#define MINIMUM_VIDEO_HEIGHT    120



#define WM_GRAPHNOTIFY  WM_USER+13

enum PLAYSTATE {Stopped = 0, Paused = 1, Running = 2, Init = 3};

//
// Macros
//
#define SAFE_RELEASE(x) { if (x) x->Release(); x = NULL; }

#define JIF(x) if (FAILED(hr=(x))) \
    {Msg(TEXT("FAILED(hr=0x%x) in ") TEXT(#x) TEXT("\n\0"), hr); return hr;}

#define LIF(x) if (FAILED(hr=(x))) \
    {Msg(TEXT("FAILED(hr=0x%x) in ") TEXT(#x) TEXT("\n\0"), hr);}
