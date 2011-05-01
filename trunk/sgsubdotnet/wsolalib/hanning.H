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
#ifndef HANNING_H_
#define HANNING_H_

#include <iostream>
#include <math.h>


class Hanning {
protected:
	int cnt, wnd_cnt;
	double *wnd;

public:
	Hanning(int windowSize){
		cnt=0;
		wnd=NULL;
		reset(windowSize);
	}

	~Hanning(void){
		if (wnd) delete [] wnd;
		wnd=NULL;
	}

	void reset(int count){
		wnd_cnt=count;
		if (count>cnt){
			cnt=count;
			if (wnd)
				delete [] wnd;
			wnd=NULL;
			if (!(wnd=new double[wnd_cnt])){
				std::cerr<<"Hanning::Hanning : hanning window malloc fail"<<std::endl;
				exit(-1);
			}
		}
		for (int i=0;i<wnd_cnt;i++)
			wnd[i]=0.5-0.5*cos(2.0*M_PI*i/(wnd_cnt-1));
	}

	int getCount(){return wnd_cnt;}

	double operator[](int i){return wnd[i];}
};

template <class DATATYPE>
class HanningApplyer : public Hanning {
public:
	HanningApplyer(int windowSize) : Hanning(windowSize){}

	void process(DATATYPE *data){

		for (int i=0;i<wnd_cnt;i++)
			data[i]=(DATATYPE)((double)data[i]*wnd[i]);
	}
};
#endif //HANNING_H_
