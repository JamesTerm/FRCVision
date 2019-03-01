#pragma once

#include <math.h>
#include <vector>
#include <iostream>
#include <opencv2/opencv.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>

#include "CvInvoke.h"
#include "ManagedTarget.h"
#include "ManagedBitmap.h"

using namespace cv;
using namespace std;
using namespace System;
using namespace System::Drawing;

public ref class ManagedCvInvoker
{
public:
	ManagedCvInvoker(int i, int fps, int width, int height, bool displayFeed);
	ManagedCvInvoker(System::String^ addr, int fps, int width, int height, bool displayFeed);

	event EventHandler<ManagedBitmap^>^ FrameUpdated;
	event EventHandler<ManagedTarget^>^ TargetUpdated;

	void Stop();
	void Start();
	void Start(double);
	void SetMinimumArea(int val) { invoker->SetMinimumArea(val); }
	void SetMaximumArea(int val) { invoker->SetMaximumArea(val); }
	void SetLowerHue(uint8_t val) { invoker->SetLowerHue(val); }
	void SetUpperHue(uint8_t val) { invoker->SetUpperHue(val); }
	void SetLowerValue(uint8_t val) { invoker->SetLowerValue(val); }
	void SetUpperValue(uint8_t val) { invoker->SetUpperValue(val); }
	void SetMaximumObjects(int val) { invoker->SetMaximumObjects(val); }
	void SetLowerSaturation(uint8_t val) { invoker->SetLowerSaturation(val); }
	void SetUpperSaturation(uint8_t val) { invoker->SetUpperSaturation(val); }
	void SetLeftBound(int val) { invoker->SetLeftBound(val); }
	void SetRightBound(int val) { invoker->SetRightBound(val); }
	void SetUpperBound(int val) { invoker->SetUpperBound(val); }
	void SetLowerBound(int val) { invoker->SetLowerBound(val); }

private:
	bool enabled;
	CvInvoke* invoker;
};

