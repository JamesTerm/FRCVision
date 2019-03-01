#pragma once

#include <math.h>
#include <vector>
#include <iostream>
#include <opencv2/opencv.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>

#include "Structures.h"


#define DEFAULT_MIN_AREA 8000
#define DEFAULT_MAX_AREA 250000
#define DEFAULT_MAX_OBJECTS 128

//Detect blue(ish) colour be default/for testing
#define DEFAULT_LOWER_HUE 100
#define DEFAULT_LOWER_SATURATION 110
#define DEFAULT_LOWER_VALUE 130
#define DEFAULT_UPPER_HUE 140
#define DEFAULT_UPPER_SATURATION 190
#define DEFAULT_UPPER_VALUE 255

#define DEFAULT_LEFT_BOUND 0
#define DEFAULT_RIGHT_BOUND 1000
#define DEFAULT_UPPER_BOUND 0
#define DEFAULT_LOWER_BOUND 1000

using namespace cv;
using namespace std;

static const double _PI = atan(1) * 4;

class CvInvoke
{

public:
	Target Process();
	Mat GetLastFrame() { return frame; }
	void SetMinimumArea(int val) { min_area = val; }
	void SetMaximumArea(int val) { max_area = val; }
	void SetLowerHue(uint8_t val) { lower_hue = val; }
	void SetUpperHue(uint8_t val) { upper_hue = val; }
	void SetLowerValue(uint8_t val) { lower_value = val; }
	void SetUpperValue(uint8_t val) { upper_value = val; }
	void SetMaximumObjects(int val) { max_objects = val; }
	void SetLowerSaturation(uint8_t val) { lower_saturation = val; }
	void SetUpperSaturation(uint8_t val) { upper_saturation = val; }
	void SetLeftBound(int val) { left_bound = val; }
	void SetRightBound(int val) { right_bound = val; }
	void SetUpperBound(int val) { upper_bound = val; }
	void SetLowerBound(int val) { lower_bound = val; }

	CvInvoke(int i, int fps, int width, int height, bool displayFeed);
	CvInvoke(const char* addr, int fps, int width, int height, bool displayFeed);

private:
	Target target;
	CircleF _circle;
	bool displayFeed;
	Mat frame, hsvImg;
	vector<Vec4i> hierarchy;
	vector<vector<cv::Point>> contours;

	VideoCapture* cap;
	int min_area = DEFAULT_MIN_AREA;
	int max_area = DEFAULT_MAX_AREA;
	int max_objects = DEFAULT_MAX_OBJECTS;
	uint8_t lower_hue = DEFAULT_LOWER_HUE;
	uint8_t upper_hue = DEFAULT_UPPER_HUE;
	uint8_t lower_value = DEFAULT_LOWER_VALUE;
	uint8_t upper_value = DEFAULT_UPPER_VALUE;
	uint8_t lower_saturation = DEFAULT_LOWER_SATURATION;
	uint8_t upper_saturation = DEFAULT_UPPER_SATURATION;

	int left_bound = DEFAULT_LEFT_BOUND;
	int right_bound = DEFAULT_RIGHT_BOUND;
	int upper_bound = DEFAULT_UPPER_BOUND;
	int lower_bound = DEFAULT_LOWER_BOUND;

};