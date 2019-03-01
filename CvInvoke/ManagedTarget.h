#pragma once

#include "Structures.h"

using namespace System;

public ref class ManagedTarget : EventArgs
{
public:
	bool valid;
	bool hasTarget;
	int x, y;
	int area;
	int radius;
	int width, height;
	ManagedTarget(Target);
};

