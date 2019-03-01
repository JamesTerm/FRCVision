#include "ManagedCvInvoker.h"

using namespace System::Runtime::InteropServices;

//this is broken
static Bitmap^ ConvertMatToBitmap(cv::Mat matToConvert)
{
	return gcnew Bitmap(matToConvert.rows, matToConvert.cols, 4 * matToConvert.step1(), System::Drawing::Imaging::PixelFormat::Format4bppIndexed, IntPtr(matToConvert.data));
}

ManagedCvInvoker::ManagedCvInvoker(int i, int fps, int width, int height, bool displayFeed)
{
	invoker = new CvInvoke(i, fps, width, height, displayFeed);
}

ManagedCvInvoker::ManagedCvInvoker(System::String^ addr, int fps, int width, int height, bool displayFeed)
{
	const char* str = (const char*)(void*)Marshal::StringToHGlobalAnsi(addr);
	invoker = new CvInvoke(str, fps, width, height, displayFeed);
	Marshal::FreeHGlobal((IntPtr)(void*)str);
}

void ManagedCvInvoker::Stop()
{
	enabled = false;
}

void ManagedCvInvoker::Start()
{
	enabled = true;

	while (enabled)
	{
		TargetUpdated(this, gcnew ManagedTarget(invoker->Process()));
		//FrameUpdated(this, gcnew ManagedBitmap(ConvertMatToBitmap(invoker->GetLastFrame())));		
	}
}

void ManagedCvInvoker::Start(double ms)
{
	enabled = true;

	while (enabled)
	{
		TargetUpdated(this, gcnew ManagedTarget(invoker->Process()));
		//FrameUpdated(this, gcnew ManagedBitmap(ConvertMatToBitmap(invoker->GetLastFrame())));
		Threading::Thread::Sleep(ms);
	}
}
