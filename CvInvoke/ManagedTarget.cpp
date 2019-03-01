#include "ManagedTarget.h"

ManagedTarget::ManagedTarget(Target target)
{
	x = target.x;
	y = target.y;
	area = target.area;
	width = target.width;
	valid = target.valid;
	height = target.height;
	radius = target.radius;
	hasTarget = target.hasTarget;
}