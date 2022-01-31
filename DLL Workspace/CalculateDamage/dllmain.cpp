// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"

#define EXPORT __declspec(dllexport)

extern "C" {

	EXPORT int* getVAL(int a) {
		return &a;
	}
}

int main()
{
	return 0;
}
