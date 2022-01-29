#ifndef DAMAGECALC_H	
	#define EXPORT __declspec(dllexport)
	
	using namespace std;

	extern "C"
	{
		EXPORT enum PlaneType
		{
			fighter,
			bomber,
			transport,
		};

		EXPORT enum AmmoType
		{
			light,
			heavy,
		};
	}
#endif
