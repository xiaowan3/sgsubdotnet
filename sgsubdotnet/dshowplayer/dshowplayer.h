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

#define APPLICATIONNAME TEXT("PlayWnd Media Player\0")
#define CLASSNAME       TEXT("PlayWndMediaPlayer\0")

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

//
// Resource constants
//
//#define IDI_PLAYWND                     100
//#define IDR_MENU                        101
//#define IDD_ABOUTBOX                    200
//#define ID_FILE_OPENCLIP                40001
//#define ID_FILE_EXIT                    40002
//#define ID_FILE_PAUSE                   40003
//#define ID_FILE_STOP                    40004
//#define ID_FILE_CLOSE                   40005
//#define ID_FILE_MUTE                    40006
//#define ID_FILE_FULLSCREEN              40007
//#define ID_FILE_SIZE_NORMAL             40008
//#define ID_FILE_SIZE_HALF               40009
//#define ID_FILE_SIZE_DOUBLE             40010
//#define ID_FILE_SIZE_QUARTER            40011
//#define ID_FILE_SIZE_THREEQUARTER       40012
//#define ID_HELP_ABOUT                   40014
//#define ID_RATE_INCREASE                40020
//#define ID_RATE_DECREASE                40021
//#define ID_RATE_NORMAL                  40022
//#define ID_RATE_DOUBLE                  40023
//#define ID_RATE_HALF                    40024
//#define ID_SINGLE_STEP                  40025

