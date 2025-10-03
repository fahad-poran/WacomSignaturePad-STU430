using System;
using System.Windows.Forms;
using FlSigCaptLib;   // COM Interop for signature capture
using FLSIGCTLLib;   // COM Interop for control

namespace WacomSTUSimple
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Create the signature control
                SigCtl sigCtl = new SigCtl();
                sigCtl.Licence = "eyJhbGciOiJSUzUxMiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJMTVMiLCJleHAiOjE3NjcwODk4MTQsImlhdCI6MTc1OTIyNzQxNCwic2VhdHMiOjAsInJpZ2h0cyI6WyJTSUdfU0RLX0NPUkUiLCJUT1VDSF9TSUdOQVRVUkVfRU5BQkxFRCIsIlNJR0NBUFRYX0FDQ0VTUyIsIlNJR19TREtfSVNPIiwiU0lHX1NES19FTkNSWVBUSU9OIl0sImRldmljZXMiOltdLCJ0eXBlIjoiZXZhbCIsImxpY19uYW1lIjoiV2Fjb21fSW5rX1NES19mb3Jfc2lnbmF0dXJlIiwid2Fjb21faWQiOiIxMzhiNzgwNTFhZjU0MTlkYjk4NTY1MDUzZGY5MWRkZSIsImxpY191aWQiOiI4ZDhlMjE5Yi1iZDg1LTRlZDYtOTliYi03YzQ1ZTkzYjM0MDkiLCJhcHBzX3dpbmRvd3MiOltdLCJhcHBzX2lvcyI6W10sImFwcHNfYW5kcm9pZCI6W10sIm1hY2hpbmVfaWRzIjpbXSwid3d3IjpbXSwiYmFja2VuZF9pZHMiOltdfQ.D4IL9ubHEsvcywlX2N4MzCoKMgqKoXLDK_HyCkHfBEnC-mqD6v30xqSsFTpLpyU10mtrM4L1s2ULMcOHncFsN5QzQXtGb6U3IFzKrBPb74D7gn7oEpGsKH2LXS0Q7engRYgM_3cqs-r8Y9Ynoa55C-lwp11ewXgsito9CkBi6ov2M_y9w9Sfa2jAQgFbI0NThlQGffbVjoqNfZ0j8yzFz1-Cv_YoyTGIADZX-vo8idWQVVdjgeV-bf0dV2_uBcDBpPdVQmyJFEwagPT2d4wkixMlGaiuOq79CsJHhBvXS3MGQxsGjIL-Z1kSATYmMhlT3W8FKj7cffVntZwCLsvaWg";  // some SDKs need a license string, some don’t

                // Create the dynamic capture
                DynamicCapture dc = new DynamicCaptureClass();

                // Start capture
                DynamicCaptureResult res = dc.Capture(sigCtl, "Fahad Abdullah", "Signature Pad Test", null, null);

                if (res == DynamicCaptureResult.DynCaptOK)
                {
                    MessageBox.Show("Signature captured successfully!");
                }
                else
                {
                    MessageBox.Show("Signature capture failed: " + res.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}