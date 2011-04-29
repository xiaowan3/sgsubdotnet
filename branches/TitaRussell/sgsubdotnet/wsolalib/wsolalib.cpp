// wsolalib.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "WSOLA.H"
typedef short int Int16;
typedef struct
{
	int chns;
	int fs;
	double tau;
	int winSize;
	WSOLA *wsola;
} WsolaObj;

double hanningDur; //HANNING_DURATION 0.1
double hanningOverlap; //HANNING_OVERLAP 0.3
double deltaDiv;   //DELTA_DIVISOR 8.0 
double sampFre;		//SAMPLE_FREQUENCY 44100
WsolaObj w;

extern "C" __declspec( dllexport ) void setWSOLAPara(
	double Hanning_Duration,
	double Hanning_Overlap,
	double Delta_Divisor,
	double Sample_Frequency)
{
	hanningDur = Hanning_Duration;
	hanningOverlap = Hanning_Overlap;
	deltaDiv = Delta_Divisor;
	sampFre = Sample_Frequency;
}

extern "C" __declspec( dllexport ) void initWSOLA(int channels, int frequency, double tau)
{

	w.chns = channels;
	w.fs = frequency;
	w.tau = tau;
	w.winSize = channels*HANNING_LENGTH(frequency);
	w.wsola = new WSOLA(w.winSize, frequency, channels);

}

extern "C" __declspec( dllexport ) int getOutputSize()
{
	return w.winSize * 2; //to Byte
}

extern "C" __declspec( dllexport ) int getInputSize()
{
	return w.wsola->getSourceLength() * 2; //to Byte
}

extern "C" __declspec( dllexport ) void initProcess(Int16* input)
{
	w.wsola->initProcess(input, w.tau);
}

extern "C" __declspec( dllexport ) void prereadSrc(int* srcIndex, int* srcLen)
{
	*srcIndex = w.wsola ->getSourceIndex() * 2;//to byte
	*srcLen = w.wsola->getSourceLength() * 2;//to byte
}

extern "C" __declspec( dllexport ) void prereadDes(int *desIndex, int *desLen)
{
	*desIndex = w.wsola->getDesiredIndex() * 2;//to byte
	*desLen = w.wsola->getDesiredLength() * 2;//tobyte
}
extern "C" __declspec( dllexport ) void loadSource(Int16* input)
{
	w.wsola->loadSourceInput(input);
}

extern "C" __declspec( dllexport ) void loadDesire(Int16* input)
{
	w.wsola->loadDesiredInput(input);
}

extern "C" __declspec( dllexport ) void process(Int16* output)
{
	w.wsola->processFrame(output,w.tau);
}

extern "C" __declspec( dllexport ) void destroyWsola()
{
	delete w.wsola;
}
