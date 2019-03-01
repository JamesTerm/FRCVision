#pragma once


using namespace System;
using namespace System::Drawing;

public ref class ManagedBitmap : EventArgs
{
public:
	Bitmap^ Bitmap;
	ManagedBitmap(System::Drawing::Bitmap^);
};

