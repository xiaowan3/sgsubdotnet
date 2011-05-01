/* Copyright 2001,2002 Matt Flax <flatmax@ieee.org>
   This file is part of the MFFM FFTw Wrapper library.

   MFFM MFFM FFTw Wrapper library is free software; you can 
   redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation; either version 2 of the License, or
   (at your option) any later version.
   
   MFFM FFTw Wrapper library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.
   
   You have received a copy of the GNU General Public License
   along with the MFFM FFTw Wrapper library
*/
#ifndef COMPLEXFFT_H_
#define COMPLEXFFT_H_

#include <fftw3.h>

#ifndef fftw_real
#define fftw_real double
#endif
#define c_re(c) ((c)[0])
#define c_im(c) ((c)[1])

#include <iomanip>
using namespace std;

#define PLANTYPE FFTW_ESTIMATE

/// class complexFFTData controls and manipulates complex fft data
class complexFFTData {
public:
  /// Specifies the size of the data array
  int size;
  /// the input and output arrays
  fftw_complex *in, *out;
  /// the power_spectrum array
  fftw_real *power_spectrum;
  /// The total power (summed) of the power spectrum as used in the method compPowerSpec
  double totalPower;

  /// Constructor with all memory to be allocated internally
  complexFFTData(int sz);
  /// Deconstructor
  ~complexFFTData(void);

  /// Use this to change associated fft data (for fft'ing)
  void switchData(complexFFTData *d);

  /// Limits the maximum to 'lim' and returns the last fft bin with max  
  int limitHalfPowerSpec(double lim);

  /// Returns the number of elements in the input and output arrays
  int getSize(){return size;}
  //  int getHalfSize(){ if (!(size%2)) return size/2; else return size/2+1;}

  /// This function computes the power spectrum and returns the max bin
  int compPowerSpec();
  //  int powerSpecDeriv(); // Find the derivative of the power spectrum
};

///class complexFFT controls fftw plans and executes fwd/inv transforms
class complexFFT {
  /// The fwd/inv plans
  fftw_plan fwdPlan, invPlan;
  /// Method to create the plans
  void createPlan(void);
  /// Method to destroy the plans
  void destroyPlan(void);
protected:
  //  int size;
  /// The pointer to the relevant data
  complexFFTData *data;
public:

  //  complexFFT(int sz, char *ws=NULL);
  /// fft init ... data pointed to by 'd'
  complexFFT(complexFFTData *d);
  /// fft deconstructor
  ~complexFFT();

  /// Use this to change associated fft data (for fft'ing)
  void switchData(complexFFTData *d);

  /// Forward transform the data (in to out)
  void fwdTransform(); // Forward fft
  /// Inverse transform the data (out to in)
  void invTransform(); // Inverse fft
};
/** \example complexFFTExample.cc
 * This is an example of how to use the class.
 */
#endif // COMPLEXFFT_H_
