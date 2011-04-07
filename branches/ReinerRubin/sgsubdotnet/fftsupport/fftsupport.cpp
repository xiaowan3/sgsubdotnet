// fftsupport.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <fftw3.h>
#include <cmath>
typedef struct {
	double* input;
	double* output;
	int len;
	fftw_plan plan;
} FFTBUF;

extern "C" __declspec( dllexport ) FFTBUF* CreateFFTBuffer(int len)
{
	FFTBUF* r = new FFTBUF();
	r->input = new double[len * 2];
	r->output = new double[len * 2];
	r->len = len;
	r->plan = fftw_plan_r2r_1d(len * 2,r->input,r->output,FFTW_DHT,FFTW_MEASURE);
	memset(r->input,0,len * 2 * sizeof(double));
	return r;
}

extern "C" __declspec(dllexport) void DoFFT(FFTBUF* fftbuf, Int16 input[], int inlen,int inoffset ,UInt8 out[])
{
	double max = 0, t;
	for(int i = 0;i<inlen;i++)
	{
		fftbuf->input[i + fftbuf->len] = input[i+inoffset] / 65536.0;
	}
	
	fftw_execute(fftbuf->plan);


	memcpy(	fftbuf->input,
			fftbuf->input + fftbuf->len,
			fftbuf->len * sizeof(double));

	for(int i = 1 ; i < 101 ; i++)
	{
		t = abs(fftbuf->output[i]);
		fftbuf->output[i] = t;
		if(t > max && i < 80) max = t;
	}

	max /= 255;
	if(max<0.1) max = 0.1;
	for(int i = 0 ; i < 100;i++)
	{
		t = (fftbuf->output[i+1]/2+fftbuf->output[i+2]+fftbuf->output[i+3]/2)/(2*max);
			out[99-i] = (UInt8)(t<=255?t:255);
	}
}