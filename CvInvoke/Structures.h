#pragma once

#include <opencv2/opencv.hpp>

using namespace cv;

typedef struct
{
	Point2f centre;
	double area;
	float radius;
}CircleF;

typedef struct
{
	bool valid;
	bool hasTarget;
	int x, y;
	int area;
	int radius;
	int width, height;
}Target;