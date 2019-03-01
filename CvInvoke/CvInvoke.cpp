#pragma once

#include "CvInvoke.h"

CvInvoke::CvInvoke(int i, int fps, int width, int height, bool displayFeed)
{
	cap = new VideoCapture(i);
	cap->set(CAP_PROP_FPS, fps);
	cap->set(CAP_PROP_FRAME_WIDTH, width);
	cap->set(CAP_PROP_FRAME_HEIGHT, height);

	this->displayFeed = displayFeed;
}

CvInvoke::CvInvoke(const char* addr, int fps, int width, int height, bool displayFeed)
{
	cap = new VideoCapture(addr);

	cap->set(CAP_PROP_FPS, fps);
	cap->set(CAP_PROP_FRAME_WIDTH, width);
	cap->set(CAP_PROP_FRAME_HEIGHT, height);

	this->displayFeed = displayFeed;
}

Target CvInvoke::Process()
{
	//clear the vectors
	hierarchy.clear();
	contours.clear();

	//set defaults
	_circle.area = 0;
	_circle.radius = 0;
	target.valid = true;
	target.hasTarget = false;

	//query frame
	(*cap) >> frame;

	//check if frame is empty
	if (frame.empty())
	{
		target.valid = false;
		return target;
	}
	
	//set hsv mask and apply to hsvImg
	Scalar lowerLimit(lower_hue, lower_saturation, lower_value);
	Scalar upperLimit(upper_hue, upper_saturation, upper_value);
	cvtColor(frame, hsvImg, cv::COLOR_BGR2HSV);
	inRange(hsvImg, lowerLimit, upperLimit, hsvImg);
	for (int r = 0; r < hsvImg.rows; r++)
	{
		for (int c = 0; c < hsvImg.cols; c++)
		{
			if (r < upper_bound || r > lower_bound || c < left_bound || c > right_bound)
				hsvImg.at<bool>(Point(c,r)) = 0;
		}
	}
	//for display if it's enabled
	Mat imgProc_disp;
	imgProc_disp = hsvImg.clone();


	findContours(hsvImg, contours, hierarchy, 1, 2);

	int index = 0;
	int objects = 0;

	if (contours.size() > 0)
		for (; index >= 0; index = hierarchy[index][0])
		{
			if (objects >= max_objects)
				break;
			//line(frame, Point(_circle.centre.x, 0), Point(_circle.centre.x, hsvImg.rows), Scalar(255, 0, 255), 4); //debug
			
			Point2f point_tmp;
			float radius_tmp;
			int area_tmp;
			
			minEnclosingCircle(contours[index], point_tmp, radius_tmp);
			area_tmp = pow(_PI*radius_tmp, 2.0);
			if ((int)radius_tmp >= _circle.radius && area_tmp >= min_area && area_tmp <= max_area)
			{
				target.hasTarget = true;
				_circle.area = area_tmp;
				_circle.radius = radius_tmp;
				_circle.centre = point_tmp;
			}

			objects++;
		}


	if (displayFeed)
	{
		if (_circle.area > 0 && _circle.radius > 0 && target.valid)
		{
			Scalar red(0, 0, 255);
			circle(frame, _circle.centre, _circle.radius, Scalar(0, 0, 255), 4);
		}
		//line(frame, Point(left_bound, 0), Point(left_bound, hsvImg.rows), Scalar(0, 255, 0), 4);
		rectangle(frame, Point(left_bound, upper_bound), Point(right_bound, lower_bound), Scalar(200, 200, 200), 4);
		namedWindow("imgPreProc", WINDOW_AUTOSIZE);
		imshow("imgPreProc", frame);

		namedWindow("imgPostProc", WINDOW_AUTOSIZE);
		imshow("imgPostProc", imgProc_disp);
		waitKey(1);
	}

	target.x = _circle.centre.x;
	target.y = _circle.centre.y;
	target.area = _circle.area;
	target.radius = _circle.radius;
	target.width = frame.size().width;
	target.height = frame.size().height;
	return target;
}

