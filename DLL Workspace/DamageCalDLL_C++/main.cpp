#include <iostream>

#include "DamageCalculation.h"

using namespace std;

extern "C" {
	EXPORT int calculateDamage(PlaneType type, int altitude, AmmoType ammoType)
	{
		return 100;
	}
}

int main()
{
	return 0;
}