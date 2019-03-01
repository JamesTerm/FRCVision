using NetworkTables;
using NetworkTables.Tables;
using System;

namespace VisionServer
{
    public class Server
    {
        private readonly ManagedCvInvoker cvInvoker;
        private readonly NetworkTable table;
        public Server(string ip, string tableName, int updateTime, int cameraIndex, int fps, int width, int height, bool enableDisplay = false)
        {
            NetworkTable.SetClientMode();
            NetworkTable.SetIPAddress(ip);
            table = NetworkTable.GetTable(tableName);

            cvInvoker = new ManagedCvInvoker(cameraIndex, fps, width, height, enableDisplay);
            cvInvoker.TargetUpdated += CvInvoker_TargetUpdated;

            table.AddTableListener(new CommListener(cvInvoker), true);

            cvInvoker.Start(updateTime);
        }

        private void CvInvoker_TargetUpdated(object sender, ManagedTarget e)
        {
            table.PutNumber("RADIUS", e.radius);
            table.PutNumber("X", e.x);
            table.PutNumber("Y", e.y);
            table.PutNumber("AREA", e.area);
            table.PutNumber("HEIGHT", e.height);
            table.PutNumber("WIDTH", e.width);
            table.PutBoolean("HASTARGET", e.hasTarget);
            //throw new NotImplementedException();
        }
    }


    public class CommListener : ITableListener
    {
        private readonly ManagedCvInvoker cvInvoker;
        public CommListener(ManagedCvInvoker invoker)
        {
            cvInvoker = invoker;
        }

        public void ValueChanged(ITable source, string key, Value value, NotifyFlags flags)
        {
            Console.WriteLine($@"{key}: {value.GetObjectValue()}");
            switch (key)
            {
                case @"LH":
                    cvInvoker.SetLowerHue((byte)value.GetDouble());
                    break;
                case @"UH":
                    cvInvoker.SetUpperHue((byte)value.GetDouble());
                    break;
                case @"LS":
                    cvInvoker.SetLowerSaturation((byte)value.GetDouble());
                    break;
                case @"US":
                    cvInvoker.SetUpperSaturation((byte)value.GetDouble());
                    break;
                case @"LV":
                    cvInvoker.SetLowerValue((byte)value.GetDouble());
                    break;
                case @"UV":
                    cvInvoker.SetUpperValue((byte)value.GetDouble());
                    break;
                case @"MinA":
                    cvInvoker.SetMinimumArea((int)value.GetDouble());
                    break;
                case @"MaxA":
                    cvInvoker.SetMaximumArea((int)value.GetDouble());
                    break;
                case @"MaxO":
                    cvInvoker.SetMaximumObjects((int)value.GetDouble());
                    break;
                case @"LOWER_BOUND":
                    cvInvoker.SetLowerBound((int)value.GetDouble());
                    break;
                case @"UPPER_BOUND":
                    cvInvoker.SetUpperBound((int)value.GetDouble());
                    break;
                case @"LEFT_BOUND":
                    cvInvoker.SetLeftBound((int)value.GetDouble());
                    break;
                case @"RIGHT_BOUND":
                    cvInvoker.SetRightBound((int)value.GetDouble());
                    break;
                default:
                    return;
                    //do rest of parameters
            }
        }
    }
}
