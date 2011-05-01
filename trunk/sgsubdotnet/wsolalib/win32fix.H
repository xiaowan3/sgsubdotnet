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
#ifndef WIN32FIX_H_
#define WIN32FIX_H_

#ifndef rint
#include <math.h>
double rint(double x);
#endif

#ifndef round
#define round(a) roundD(a)
#endif

#ifndef bzero
#define bzero(a,c) memset(a,0,c)
#endif

#ifndef remainder
#define remainder(a,b) fmod(a,b)
#endif

#ifndef MAXDOUBLE
#include <limits>
#define MAXDOUBLE (numeric_limits<double>::max( ))
#endif

#ifndef MAXINT
#include <limits>
#define MAXINT (numeric_limits<int>::max( ))
#endif

#ifndef M_PI
#define M_PI       3.14159265358979323846
#endif

#endif
