// fftsupport.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <fftw3.h>
#include <iostream>
#include <fstream>
#include <cmath>
typedef struct {
	double input[1024];
	double output[1024];
	int len;
	fftw_plan plan;
	std::ofstream* ofs;
} FFTBUF;

extern "C" __declspec( dllexport ) FFTBUF* CreateFFTBuffer(int len)
{
	FFTBUF* r = new FFTBUF();
	r->plan = fftw_plan_r2r_1d(len,r->input,r->output,FFTW_DHT,FFTW_ESTIMATE);
	r->ofs = new std::ofstream("E:\\test.txt");
	return r;
}

extern "C" __declspec(dllexport) void DoFFT(FFTBUF* fftbuf, Int16 input[], int inlen,int inoffset ,UInt8 out[])
{
	
	double max = 0, t;
	for(int i = 0;i<inlen;i++)
	{
		fftbuf->input[i] = input[i+inoffset] / 65536.0;
	}
	
	fftw_execute(fftbuf->plan);
	
	for(int i = 1 ; i < 101 ; i++)
	{
		t = log(abs(fftbuf->output[i]));
		fftbuf->output[i] = t;
		if(t > max) max = t;
	}
	(*(fftbuf->ofs))<<max<<std::endl;

	max /= 255;
	for(int i = 0 ; i < 100;i++)
	{
		t = fftbuf->output[i/2+1]*63;
			out[i] = (UInt8)(t<=255?t:255);
	}
}