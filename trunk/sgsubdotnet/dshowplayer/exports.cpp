#include"stdafx.h"
#include "dshowplayer.h"

extern HWND ghWnd;
extern PLAYSTATE g_psCurrent;
extern "C" __declspec( dllexport ) bool InitPlayer(HWND hWnd)
{
	if(FAILED(CoInitializeEx(NULL, COINIT_APARTMENTTHREADED)))
    {
        return false;
    }
	ghWnd = hWnd;
	return true;
}

//
//[DllImport("kernel32.dll", CharSet=Unicode)]
//static extern bool Foo([In] string stuff);
//

extern "C" __declspec( dllexport ) void PlayMovie(LPCWSTR filename)
{
	PlayMovieInWindow(filename);
}

extern "C" __declspec( dllexport ) void UninitPlayer()
{
	CloseInterfaces();
	CoUninitialize();
}

extern "C"  __declspec( dllexport ) long HandleGraphEvent()
{
	return _handleGraphEvent();
}

extern "C"  __declspec( dllexport ) double GetPlayerPos()
{
	return GetTime();
}

extern "C"  __declspec( dllexport ) double GetDuration()
{
	return GetDur();
}

extern "C" __declspec( dllexport ) int GrabImage(LPCWSTR filename)
{
	return GrabImg(filename);
}

extern "C" __declspec( dllexport ) int GetPlayState()
{
	return g_psCurrent;
}

extern "C" __declspec( dllexport ) void Resize()
{
	MoveVideoWindow();
}

extern "C" __declspec( dllexport ) void TogglePause()
{
	PauseClip();
}

extern "C" __declspec( dllexport ) void Seek(double pos)
{
	SeekClip(pos);
}
extern "C" __declspec( dllexport ) void SetVolume(double vol)
{
	set_volume(vol);
}
