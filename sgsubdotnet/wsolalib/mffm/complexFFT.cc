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
#include"stdafx.h"
#include "complexFFT.H"
#include<iostream>
//#include <iomanip.h>
using namespace std;

complexFFTData::
complexFFTData(int sz){
  //  std::cout <<"complexFFTData init:"<<this<<std::endl;
  size=sz;
  in = out = NULL;
  power_spectrum = NULL;

  //in = new fftw_complex[size];
  //out = new fftw_complex[size];
  //power_spectrum = new fftw_real[size];
  in = (fftw_complex*)fftw_malloc(size*sizeof(fftw_complex));
  out = (fftw_complex*)fftw_malloc(size*sizeof(fftw_complex));
  power_spectrum = (fftw_real*)fftw_malloc(size*sizeof(fftw_real));
  if (!in || !out || !power_spectrum){
    std::cerr << "Could not allocate enough mem for a complexFFT"<<std::endl;
    //if (in) delete [] in;
    //if (out) delete [] out;
    //if (power_spectrum) delete [] power_spectrum;
    if (in) fftw_free(in); in=NULL;
    if (out) fftw_free(out); out=NULL;
    if (power_spectrum) fftw_free(power_spectrum); power_spectrum=NULL;
    exit(-1);
  }
  totalPower = 0.0;
  //  std::cout <<"complexFFTData exit"<<std::endl;
}

complexFFTData::
~complexFFTData(){
  //if (in) delete [] in;
  //if (out) delete [] out;
  //if (power_spectrum) delete [] power_spectrum;
  if (in) fftw_free(in); in=NULL;
  if (out) fftw_free(out); out=NULL;
  if (power_spectrum) fftw_free(power_spectrum); power_spectrum=NULL;
}

int complexFFTData::
compPowerSpec(){
  int bin;
  totalPower = 0.0;
  double max = power_spectrum[bin=0] = c_re(out[0])*c_re(out[0])+c_im(out[0])*c_im(out[0]);  // DC component
  for (int k = 1; k < getSize(); ++k){
    if ((power_spectrum[k] = c_re(out[k])*c_re(out[k]) + c_im(out[k])*c_im(out[k]))>max){
      max=power_spectrum[bin=k];
    }
    totalPower += power_spectrum[k];
  }
  /*  if (getSize() % 2 == 0){ // N is even
    power_spectrum[getSize()/2] = out[getSize()/2]*out[getSize()/2];  // Nyquist freq.
    if (power_spectrum[getSize()/2]>max)
      max=power_spectrum[bin=getSize()/2];      
    totalPower += power_spectrum[getSize()/2];
    }*/
  return bin;
}

complexFFT::
complexFFT(complexFFTData *d) {
  //  std::cout <<"complexFFT init:"<<this<<std::endl;
  data=d;
  createPlan();
}

complexFFT::
~complexFFT(){
  destroyPlan();
}

void complexFFT::
destroyPlan(void){
  if (data){
    fftw_destroy_plan(fwdPlan);
    fftw_destroy_plan(invPlan);
  }
}

void complexFFT::
createPlan(void){
  if (data){
    //fftw3
    fwdPlan = fftw_plan_dft_1d(data->getSize(), data->in, data->out, FFTW_FORWARD, PLANTYPE);
    invPlan = fftw_plan_dft_1d(data->getSize(), data->in, data->out, FFTW_BACKWARD, PLANTYPE);
  }
}

void complexFFT::
switchData(complexFFTData *d){
  //fftw_cleanup();
  destroyPlan();
  data=d;
  createPlan();
}

void complexFFT::
fwdTransform(){
  if (!data)
    std::cerr<<"complexFFT::fwdTransform : data not present, please switch data"<<std::endl;
  else
    fftw_execute(fwdPlan);
}

void complexFFT::
invTransform(){
  if (!data)
    std::cerr<<"complexFFT::invTransform : data not present, please switch data"<<std::endl;
  else
    fftw_execute(invPlan);
}
