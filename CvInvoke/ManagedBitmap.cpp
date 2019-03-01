#include "ManagedBitmap.h"



ManagedBitmap::ManagedBitmap(System::Drawing::Bitmap^ bitmap)
{
	Bitmap = bitmap;
}
