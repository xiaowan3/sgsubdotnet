/* Copyright 2001 Matt Flax <flatmax@ieee.org>
This file is part of MFFM Time Scale Modification for Audio.

MFFM Time Scale Modification for Audio is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

MFFM Time Scale Modification for Audio is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You have received a copy of the GNU General Public License
along with MFFM Time Scale Modification for Audio
*/
#ifndef WSOLA_H_
#define WSOLA_H_

///Use this to debug general
//#define W_DEBUG
#ifdef W_DEBUG
#include <fstream>
#endif

#define VERSION_NUM "4.9"

#ifndef WIN32
#include <values.h>
#else
#include "win32fix.H"
#endif

//This function is for scientific rounding
#ifndef roundD
//#define roundD(a) ((a-(int)a>=0.5) ? ceil(a) : floor(a))
#define roundD(a) rint(a)
#endif



#define c_re(c) ((c)[0])
#define c_im(c) ((c)[1])

#include <string.h>
#include <iostream>
using namespace std;
#include "hanning.H"

//#include "estimate.H"

//From the MFFM FFTw C++ wrapper available at sourceforge.net
#include "mffm/complexFFT.H"
#include <complex>

//From the MFFM_TimeCode library available at sourceforge.net
#include "mffm/timeCode.H"

#ifndef ATYPE
#define ATYPE short int
#endif

extern double hanningDur; //HANNING_DURATION 0.1
extern double hanningOverlap; //HANNING_OVERLAP 0.3
extern double deltaDiv;   //DELTA_DIVISOR 8.0 
extern double sampFre;		//SAMPLE_FREQUENCY 44100

#define SAMPLE_FREQUENCY sampFre ///The default sample frequency


///The hanning window duration in s
#define HANNING_DURATION hanningDur
//The proportional hanning overlap
#define HANNING_OVERLAP hanningOverlap
///The sample length of the hanning window
#define HANNING_LENGTH(sampFreq) (int)roundD(HANNING_DURATION*(double)sampFreq)

//The pitch (Hz) which we don't want to exceed the period of durin matching
#define DELTA_DIVISOR sampFre

/* reference :
An Overlap Add Technique Based on Waveform Similarity (WSOLA)
For High Quality Time-Scale Modification of Speech
Werner Verhelst and Marc Roelands
IEEE publication
*/

///The time code structures used within
#define MASTERCOUNTERTYPE_W MasterCounter<field, 1>
#define TIMECODETYPE_W TimeCode<MASTERCOUNTERTYPE_W, ATYPE>

class WSOLA : public Hanning 
{
	//double avBestMatch; ///The average best match
	long int frameCnt; // The frame count
	TIMECODETYPE_W sFrame, dFrame; //Source and desired frames
	ATYPE *output; // The timescale modified audio

	int N; //The total size of the fft data structures
	///The fft structure used to implement the convolution
	complexFFT *fft;
	///The fft data structures
	complexFFTData *sFrameFFTData, *dFrameFFTData, *hanningFFTData,  *normWindowData;

	int hanningLength, deltaMax, sampleFrequency;

	///Inits the fft structures
	void initFFTStructures(int count);

	/// De-Inits the FFT structures
	void deInitFFTStructures(void);
	/* call this to init structures which aren't FFT based */
	int newInit(void);

	///This function returns the maximum similarity location
	int findSimilarityFFTNEW();

	int processFrameFFT(void);

	int findBestMatch(void);

	void copyBestMatch(ATYPE *extOutput, int bestLocation);

public:
	//Labeled errors
	typedef enum _errors {DEFAULT_ERR=-1, INPUT_READ2DF_ERR=-2, INPUT_READ2SF_ERR=-3, WRITE_ERR=-4, PROCFFT_ERR=-5, FINISHED_NORMALLY=-6} errors;

	int channels;
	/**Constructor ...
	This constructor takes the length of the hanning window, the sample
	frequency, the channel count (default=1)
	*/
	WSOLA(int hl=HANNING_LENGTH(SAMPLE_FREQUENCY), int sf=SAMPLE_FREQUENCY, int ch=1);

	///Destructor
	~WSOLA(void);

	void initProcess(const ATYPE*inputSrc, double tau);

	/** Called by the user to process a frame */
	int processFrame(ATYPE *extOutput, double tau);

	///This is the size of the modified (output) data
	int getCount(void);

	///This is the index in input stream to read from
	int getSourceIndex(void);
	///This is the length of elements required by the input stream to read
	int getSourceLength(void);
	///Loads an external memory source stream to correct locations in WSOLA
	void loadSourceInput(ATYPE *inSrc);
	///Loads an external memory source stream to correct locations in WSOLA
	void loadDesiredInput(ATYPE *inDes);
	///This is the index in input stream to read from
	int getDesiredIndex(void);
	///This is the length of elements required by the desired frame
	int getDesiredLength(void);

	void reset(void);

	//Returns the number of bytes in a frame
	int getFrameSize(void);

	//sets the source position - leaves the desired position
	void setPosition(int pos);

	void checkPositions(void);

	///This function is now public to that the audio can bypass WSOLA for tau=1.0
	void shiftOn(double tau);
};
#endif //WSOLA_H_
