#include"stdafx.h"
#undef max
#include"win32fix.H"
#include<iostream>
#include <complex>
#include"WSOLA.H"

double rint(double x)
{
	double a = floor(x);
	if(x - a < .5) 
	{
		return a;
	}
	else if(x - a > .5) 
	{
		return a + 1.0;
	} else return(floor(a/2) == a/2)?a:a+1.0;
}


///Inits the fft structures

void WSOLA::initFFTStructures(int count)
{
	if (sFrameFFTData)
		if (sFrameFFTData->getSize()!=count)
			deInitFFTStructures();
		else
			return;

	//Set up the fft structures
	if (!(sFrameFFTData=new complexFFTData(count)))
		std::cerr << "WSOLA::initFFTStructures : couldn't malloc sFrameFFTData of size "<<count<<endl;
	if (!(dFrameFFTData=new complexFFTData(count)))
		std::cerr << "WSOLA::initFFTStructures : couldn't malloc dFrameFFTData of size "<<count<<endl;
	if (!(hanningFFTData=new complexFFTData(count)))
		std::cerr << "WSOLA::initFFTStructures : couldn't malloc hanningFFTData of size "<<count<<endl;
	if (!(normWindowData=new complexFFTData(count)))
		std::cerr << "WSOLA::initFFTStructures : couldn't malloc normWindowData of size "<<count<<endl;
	if (!(fft=new complexFFT(hanningFFTData)))
		std::cerr << "WSOLA::initFFTStructures : couldn't malloc fft"<<endl;
	//set the hanning data and Norm. data here
	for (int i=0;i<hanningLength;i++){
		c_re(hanningFFTData->in[i])=wnd[i];
		c_im(hanningFFTData->in[i])=0.0;
	}
	for (int i=hanningLength;i<count;i++){
		c_re(hanningFFTData->in[i])=c_im(hanningFFTData->in[i])=0.0;
	}
	fft->fwdTransform(); //Find the hanning data DFT
}

/// De-Inits the FFT structures
void WSOLA::deInitFFTStructures(void)
{
	if (fft) delete fft; fft=NULL;
	if (hanningFFTData) delete hanningFFTData; hanningFFTData=NULL;
	if (normWindowData) delete normWindowData; normWindowData=NULL;
	if (sFrameFFTData) delete sFrameFFTData; sFrameFFTData=NULL;
	if (dFrameFFTData) delete dFrameFFTData; dFrameFFTData=NULL;
}

/* call this to init structures which aren't FFT based */
int WSOLA::newInit(void){


	if (output) delete [] output;
	if (!(output=new ATYPE[hanningLength]))
	{
		std::cerr<<"WSOLA::process : output audio array malloc failure"<<endl;
		return -1;
	}
	bzero(output, hanningLength*sizeof(ATYPE));

	reset();//Set time code locations to zero and maximise endpoints



	return 0;
}

///This function returns the maximum similarity location
int WSOLA::findSimilarityFFTNEW()
{
	//time data must be preloaded   
	fft->switchData(sFrameFFTData);
	fft->fwdTransform();
	fft->switchData(normWindowData);
	fft->fwdTransform();
	fft->switchData(dFrameFFTData);
	fft->fwdTransform();

	// apply hanning to next source frame in this domain ...
	//multiply the outputs
	for (int i=0;i<N;i++)
	{
		std::complex<fftw_real> res1=(std::complex<fftw_real>(c_re(sFrameFFTData->out[i]), -c_im(sFrameFFTData->out[i])));

		//Find the Similarity
		std::complex<fftw_real> res2=(std::complex<fftw_real>(c_re(dFrameFFTData->out[i]), c_im(dFrameFFTData->out[i])));
		res2*=res1;
		c_re(dFrameFFTData->out[i])=res2.real();
		c_im(dFrameFFTData->out[i])=res2.imag();

		//Find the Norm
		std::complex<fftw_real> res3=(std::complex<fftw_real>(c_re(hanningFFTData->out[i]), c_im(hanningFFTData->out[i])));
		std::complex<fftw_real> res4=(std::complex<fftw_real>(c_re(normWindowData->out[i]), -c_im(normWindowData->out[i])));
		//      cout<<res3<<'\t'<<res4<<'\t';
		res3*=res4;
		//cout<<res3<<'\n';
		c_re(normWindowData->out[i])=res3.real();
		c_im(normWindowData->out[i])=res3.imag();
	}


	//Make sure we point to the correct output buffer
	fft->switchData(dFrameFFTData);
	//inverse transform ...
	fft->invTransform();

	fft->switchData(normWindowData);
	//inverse transform ...
	fft->invTransform();


	//Scan for the maximum
	double maximum=-MAXDOUBLE, minimum=MAXDOUBLE, tempD;
	int bestLocation=0, bestMin=0, bestMax=0;
	for (int i=0;i<deltaMax;i+=channels)
	{
		tempD=c_re(dFrameFFTData->in[i])/(c_re(normWindowData->in[i])*c_re(normWindowData->in[i]));


		if (tempD>maximum)
		{
			bestMax=i;
			maximum=tempD;
		}
		if (tempD<minimum)
		{
			bestMin=i;
			minimum=tempD;
		}
	}
	bestLocation=bestMax;
	//std::cout<<"bestLocation = "<<bestLocation<<endl;
	return bestLocation;
}

int WSOLA::processFrameFFT(void)
{

	ATYPE* tempDFW=dFrame.window->getDataPtr();
	for (int i=0;i<hanningLength;i++)
	{ //Load the desired range
		c_re(dFrameFFTData->in[i])=(double)tempDFW[i]*wnd[i]*wnd[i];
		c_im(dFrameFFTData->in[i])=0.0;
	}
	for (int i=hanningLength;i<hanningLength+deltaMax;i++)
		c_im(dFrameFFTData->in[i])=c_re(dFrameFFTData->in[i])=0.0;

	ATYPE* tempSFW=sFrame.window->getDataPtr();
	for (int i=0;i<hanningLength+deltaMax;i++)
	{ //Load the search range
		c_re(sFrameFFTData->in[i])=(double)tempSFW[i];
		//c_re(normWindowData->in[i])=(double)(tempSFW[i]*tempSFW[i]);
		if (tempSFW[i]>=0)
			c_re(normWindowData->in[i])=(double)tempSFW[i];
		else
			c_re(normWindowData->in[i])=(double)-tempSFW[i];
		c_im(normWindowData->in[i])=c_im(sFrameFFTData->in[i])=0.0;
	}
	//Search through for the closest match
	int bestLocation=0;
	bestLocation=findSimilarityFFTNEW();

	return bestLocation;
}

int WSOLA::findBestMatch(void)
{
	int ret=0;
	int wndCnt=hanningLength, bestLocation;
	//Set the source frame to its beginning location
	if ((bestLocation=processFrameFFT())<0)
	{
		std::cout<<"error ocurred during processFrameFFT function"<<endl;
		return PROCFFT_ERR;
	}
	return bestLocation;
}

void WSOLA::copyBestMatch(ATYPE *extOutput, int bestLocation)
{
	ATYPE* tempDFW=sFrame.window->getDataPtr();
	for (int i=0;i<hanningLength;i++) // Copy over by windowing and adding
		output[i]+=(ATYPE)roundD((double)tempDFW[i+bestLocation]*wnd[i]);
	for (int i=0;i<hanningLength/2;i++) // Copy over to the external output
		extOutput[i]=output[i];
}

WSOLA::WSOLA(int hl, int sf, int ch) : Hanning(hl)
{

	//avBestMatch=0.0;
	frameCnt=0;
	channels=ch;
	std::cout<<"Assuming "<<channels<<" channels"<<endl;
	std::cout<<"hanning length = "<<hl<<endl;
	hanningLength=hl;
	deltaMax=(int)roundD((double)hanningLength/DELTA_DIVISOR*channels); 
	std::cout<<"deltaMax="<<deltaMax<<endl;    //Make sure we are using a valid deltaMax with respect to channel count
	while (remainder((double)(hanningLength+deltaMax)/(double)channels,floor((double)(hanningLength+deltaMax)/(double)channels))!=0.0)
	{
		std::cout<<"hanning+delta remainder = "<<remainder((double)(hanningLength+deltaMax)/(double)channels,floor((double)(hanningLength+deltaMax)/(double)channels))<<endl;
		deltaMax++;
	}
	std::cout<<"deltaMax="<<deltaMax<<endl;    //Make sure we are using a valid deltaMax with respect to channel count

	//Set up null pointers
	fft=NULL;
	dFrameFFTData=sFrameFFTData=NULL;
	N=deltaMax+hanningLength;
	initFFTStructures(N);

	sampleFrequency=sf;
	output=NULL;

	if (newInit()<0)
	{
		std::cout<<"WSOLA:: error - couldn't init the structures - out of memory ?"<<endl;
		exit(-1);
	}

}

///Destructor
WSOLA::~WSOLA(void)
{

	if (output) delete [] output;
	deInitFFTStructures();

}

void WSOLA::initProcess(const ATYPE*inputSrc, double tau)
{
	int halfWndCnt=(int)((double)hanningLength/2.0);
	std::cout<<"halfWndCnt "<<halfWndCnt<<endl;
	for (int i=0;i<halfWndCnt;i++)
		output[i]=(ATYPE)roundD((double)inputSrc[i]*wnd[i+halfWndCnt]);
}

/** Called by the user to process a frame */
int WSOLA::processFrame(ATYPE *extOutput, double tau){

	int ret=0;
	int bestLoc;
	if ((bestLoc=findBestMatch())<0){
		std::cerr<<"Error findBestMatch HERE"<<endl;
		return bestLoc;
	}
	frameCnt++;

	copyBestMatch(extOutput,bestLoc); //copy the best match to the output

	shiftOn(tau);
	return ret;
}

///This is the size of the modified (output) data
int WSOLA::getCount(void){return dFrame.getEnd();}

///This is the index in input stream to read from
int WSOLA::getSourceIndex(void){return sFrame.getCount();}
///This is the length of elements required by the input stream to read
int WSOLA::getSourceLength(void){return (hanningLength+deltaMax);}
///Loads an external memory source stream to correct locations in WSOLA
void WSOLA::loadSourceInput(ATYPE *inSrc){
	// Copies memory location to memory location
	memcpy(sFrame.window->getDataPtr(), inSrc, (hanningLength+deltaMax)*sizeof(ATYPE));
}

///Loads an external memory source stream to correct locations in WSOLA
void WSOLA::loadDesiredInput(ATYPE *inDes){
	// Copies memory location to memory location
	memcpy(dFrame.window->getDataPtr(), inDes, hanningLength*sizeof(ATYPE));
}
///This is the index in input stream to read from
int WSOLA::getDesiredIndex(void){return dFrame.getCount();}
///This is the length of elements required by the desired frame
int WSOLA::getDesiredLength(void){return dFrame.window->getCount();}

void WSOLA::reset(void){
	//Set up the time codes - default to max int size
	sFrame.init(0,hanningLength); // Dumy inits ... set maximum now
	sFrame.setFinish(MAXINT-1);
	sFrame.setEnd(MAXINT-2); // Make sure you don't try to set the current count to the same as the finish count or it will loop !

	dFrame.init(0,hanningLength); // Dumy inits ... set maximum now
	dFrame.setFinish(MAXINT-1);
	dFrame.setEnd(MAXINT-2);

	//Ensure the array frame and window sizes are correct
	(*sFrame.window)=hanningLength;
	(*dFrame.window)=hanningLength;
	sFrame.window->setFrameSize(sizeof(ATYPE));
	dFrame.window->setFrameSize(sizeof(ATYPE));

	//Process input frame by input frame ....
	sFrame=0;
	dFrame=0;
}

//Returns the number of bytes in a frame
int WSOLA::getFrameSize(void){
	return sFrame.window->getFrameSize();
}

//sets the source position - leaves the desired position
void WSOLA::setPosition(int pos){
	sFrame=pos;
}

void WSOLA::checkPositions(void){
	std::cout<<"sFrame "<<sFrame<<'\n'<<"dFrame "<<dFrame<<endl;
}

///This function is now public to that the audio can bypass WSOLA for tau=1.0
void WSOLA::shiftOn(double tau){
	for (int i=0;i<hanningLength/2.0;i++){
		output[i]=output[i+(int)((double)hanningLength/2.0)];
		output[i+(int)((double)hanningLength/2.0)]=(ATYPE)0.0;
	}

	// Locate to the next desired frame to match to
	dFrame=(int)roundD((double)sFrame.getCount()+(double)hanningLength/2.0);
	// Locate to the corresponding search region
	sFrame=(int)roundD((double)sFrame.getCount()+(double)hanningLength/2.0/tau);
}

