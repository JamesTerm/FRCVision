using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Calibrator
{
    public partial class MainWindow : Form
    {
        private readonly BackgroundWorker bw = new BackgroundWorker();
        private readonly ManagedCvInvoker cvInvoker = new ManagedCvInvoker(0, 60, 480, 600, true);

        public MainWindow()
        {
            InitializeComponent();
            tbarLowerSat.ValueChanged += TbarLowerSat_ValueChanged;
            tbarUpperSat.ValueChanged += TbarUpperSat_ValueChanged;
            tbarLowerHue.ValueChanged += TbarLowerHue_ValueChanged;
            tbarUpperHue.ValueChanged += TbarUpperHue_ValueChanged;
            tbarLowerValue.ValueChanged += TbarLowerValue_ValueChanged;
            tbarUpperValue.ValueChanged += TbarUpperValue_ValueChanged;
            tbarLowerArea.ValueChanged += TbarLowerArea_ValueChanged;
            tbarUpperArea.ValueChanged += TbarUpperArea_ValueChanged;
            tbarMaxObjects.ValueChanged += TbarMaxObjects_ValueChanged;

            tbxLS.TextChanged += TbxLS_TextChanged;
            tbxUS.TextChanged += TbxUS_TextChanged;
            tbxLH.TextChanged += TbxLH_TextChanged;
            tbxUH.TextChanged += TbxUH_TextChanged;
            tbxLV.TextChanged += TbxLV_TextChanged;
            tbxUV.TextChanged += TbxUV_TextChanged;
            tbxLA.TextChanged += TbxLA_TextChanged;
            tbxUA.TextChanged += TbxUA_TextChanged;
            tbxMO.TextChanged += TbxMO_TextChanged;

            cvInvoker.SetLowerHue(0);
            cvInvoker.SetUpperHue(255);
            cvInvoker.SetLowerSaturation(0);
            cvInvoker.SetUpperSaturation(255);
            cvInvoker.SetLowerValue(0);
            cvInvoker.SetUpperValue(255);
            cvInvoker.SetMinimumArea(0);
            cvInvoker.SetMaximumArea(8000);
            cvInvoker.SetMaximumObjects(60);
            cvInvoker.SetLeftBound(0);
            cvInvoker.SetRightBound(1000);
            cvInvoker.SetUpperBound(0);
            cvInvoker.SetLowerBound(1000);

            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }


        private void TbxMO_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxMO.Text, out int n))
            {
                tbarMaxObjects.Value = int.Parse(tbxMO.Text);
            }
        }

        private void TbxUA_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxUA.Text, out int n) && !(int.Parse(tbxLA.Text) < 0 || int.Parse(tbxLA.Text) > 250000))
            {
                tbarUpperArea.Value = int.Parse(tbxUA.Text);
            }
        }

        private void TbxLA_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxLA.Text, out int n) && !(int.Parse(tbxLA.Text) < 0 || int.Parse(tbxLA.Text) > 250000))
            {
                tbarLowerArea.Value = int.Parse(tbxLA.Text);
            }
        }

        private void TbxUV_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxUV.Text, out int n) && !(int.Parse(tbxUV.Text) > 255 || int.Parse(tbxUV.Text) < 0))
            {
                tbarUpperValue.Value = int.Parse(tbxUV.Text);
            }
        }

        private void TbxLV_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxLV.Text, out int n) && !(int.Parse(tbxLV.Text) > 255 || int.Parse(tbxLV.Text) < 0))
            {
                tbarLowerValue.Value = int.Parse(tbxLV.Text);
            }
        }

        private void TbxUH_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxUH.Text, out int n) && !(int.Parse(tbxUH.Text) > 255 || int.Parse(tbxUH.Text) < 0))
            {
                tbarUpperHue.Value = int.Parse(tbxUH.Text);
            }
        }

        private void TbxLH_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxLH.Text, out int n) && !(int.Parse(tbxLH.Text) > 255 || int.Parse(tbxLH.Text) < 0))
            {
                tbarLowerHue.Value = int.Parse(tbxLH.Text);
            }
        }

        private void TbxUS_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxUS.Text, out int n) && !(int.Parse(tbxUS.Text) > 255 || int.Parse(tbxUS.Text) < 0))
            {
                tbarUpperSat.Value = int.Parse(tbxUS.Text);
            }
        }

        private void TbxLS_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbxLS.Text, out int n) && !(int.Parse(tbxLS.Text) > 255 || int.Parse(tbxLS.Text) < 0))
            {
                tbarLowerSat.Value = int.Parse(tbxLS.Text);
            }
        }

        private void TbarMaxObjects_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetMaximumObjects(tbarMaxObjects.Value);
            tbxMO.Text = tbarMaxObjects.Value.ToString();
        }

        private void TbarUpperArea_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetMaximumArea(tbarUpperArea.Value);
            tbxUA.Text = tbarUpperArea.Value.ToString();
        }

        private void TbarUpperValue_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetUpperValue((byte)tbarUpperValue.Value);
            tbxUV.Text = tbarUpperValue.Value.ToString();
        }

        private void TbarLowerValue_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetLowerValue((byte)tbarLowerValue.Value);
            tbxLV.Text = tbarLowerValue.Value.ToString();
        }

        private void TbarUpperHue_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetUpperHue((byte)tbarUpperHue.Value);
            tbxUH.Text = tbarUpperHue.Value.ToString();
        }

        private void TbarLowerHue_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetLowerHue((byte)tbarLowerHue.Value);
            tbxLH.Text = tbarLowerHue.Value.ToString();
        }

        private void TbarUpperSat_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetUpperSaturation((byte)tbarUpperSat.Value);
            tbxUS.Text = tbarUpperSat.Value.ToString();
        }

        private void TbarLowerSat_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetLowerSaturation((byte)tbarLowerSat.Value);
            tbxLS.Text = tbarLowerSat.Value.ToString();
        }

        private void TbarLowerArea_ValueChanged(object sender, EventArgs e)
        {
            cvInvoker.SetMinimumArea(tbarLowerArea.Value);
            tbxLA.Text = tbarLowerArea.Value.ToString();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            cvInvoker.Start();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            tbxLS.Text = "0";
            tbxUS.Text = "255";
            tbxLH.Text = "0";
            tbxUH.Text = "255";
            tbxLV.Text = "0";
            tbxUV.Text = "255";
            tbxLA.Text = "0";
            tbxUA.Text = "8000";
            tbxMO.Text = "60";
        }

        private void tbarLeftBound_Scroll(object sender, EventArgs e)
        {
            cvInvoker.SetLeftBound(tbarLeftBound.Value);
        }

        private void tbarRightBound_Scroll(object sender, EventArgs e)
        {
            cvInvoker.SetRightBound(tbarRightBound.Value);
        }

        private void tbarUpperBound_Scroll(object sender, EventArgs e)
        {
            cvInvoker.SetUpperBound(tbarUpperBound.Value);
        }

        private void tbarLowerBound_Scroll(object sender, EventArgs e)
        {
            cvInvoker.SetLowerBound(tbarLowerBound.Value);
        }
    }
}
