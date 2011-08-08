// fftsupport.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <fftw3.h>
#include <cmath>

void ddct(int n, int isgn, double *a, int *ip, double *w);
typedef struct {
#ifdef _FFTW_
	double* input;
	double* output;
	int len;
	fftw_plan plan;
#else
	int len;
	int *ip;
	double *t;
	double *w;
	double *a;
#endif
} FFTBUF;

extern "C" __declspec( dllexport ) FFTBUF* CreateFFTBuffer(int len)
{
#ifdef _FFTW_
	FFTBUF* r = new FFTBUF();
	r->input = new double[len * 2];
	r->output = new double[len * 2];
	r->len = len;
	r->plan = fftw_plan_r2r_1d(len * 2,r->input,r->output,FFTW_REDFT10,FFTW_MEASURE);
	memset(r->input,0,len * 2 * sizeof(double));
	return r;
#else
	FFTBUF* r = new FFTBUF();
	r->len = 1024;
	r->a = new double [len*2];
	r->t = new double [len*2];
	r->w = new double [len*2];
	r->ip = new int[len*2];
	r->ip[0] = 0;
	return r;
#endif
}

extern "C" __declspec(dllexport) void DoFFT(FFTBUF* fftbuf, Int16 input[], int inlen,int inoffset ,UInt8 out[])
{
#ifdef _FFTW_
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
#else
	double max = 0, t;
	for(int i = 0;i<inlen;i++)
	{
		fftbuf->a[i + inlen] = input[i+inoffset] / 65536.0;
	}
	memcpy(fftbuf->t,fftbuf->a+inlen,inlen * sizeof(double));
	memcpy(	fftbuf->a,fftbuf->t,inlen * sizeof(double));

	ddct(fftbuf->len, 1, fftbuf->a, fftbuf->ip, fftbuf->w);
	for(int i = 1 ; i < 101 ; i++)
	{
		t = abs(fftbuf->a[i]);
		fftbuf->a[i] = t;
		if(t > max && i < 80) max = t;
	}
	max /= 255;
	if(max<0.1) max = 0.1;
	for(int i = 0 ; i < 100;i++)
	{
		t = (fftbuf->a[i+1]/2+fftbuf->a[i+2]+fftbuf->a[i+3]/2)/(2*max);
			out[99-i] = (UInt8)(t<=255?t:255);
	}
#endif
}

extern "C" __declspec(dllexport) void ReleaseBuffer(FFTBUF* fftbuf)
{
#ifdef _FFTW_
	delete fftbuf->input;
	delete fftbuf->output;
	fftw_destroy_plan(fftbuf->plan);
	delete fftbuf;	
#else
#endif
}
