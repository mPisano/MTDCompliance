using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using PX.HMRC;
using PX.HMRC.DAC;
using PX.HMRC.Model;
using Microsoft.Win32;

//https://developer.service.hmrc.gov.uk/api-documentation/docs/api/service/vat-api/1.0#_retrieve-vat-obligations_get_accordion

namespace MTD_TEST
{
    public partial class fMTD_TEST : Form
    {
        public fMTD_TEST()
        {
            InitializeComponent();
            this.toolTips.SetToolTip(this.lbVatDueSales, Messages.vatDueSales_Notes);
            this.toolTips.SetToolTip(this.lbDueAcquisitions, Messages.vatDueAcquisitions_Notes);
            this.toolTips.SetToolTip(this.lbTotalVatDue, Messages.totalVatDue_Notes);
            this.toolTips.SetToolTip(this.lbVatReclaimedCurrPeriod, Messages.vatReclaimedCurrPeriod_Notes);
            this.toolTips.SetToolTip(this.lbNetVatDue, Messages.netVatDue_Notes);
            this.toolTips.SetToolTip(this.lbTotalValueSalesExVAT, Messages.totalValueSalesExVAT_Notes);
            this.toolTips.SetToolTip(this.lbTotalValuePurchasesExVAT, Messages.totalValuePurchasesExVAT_Notes);
            this.toolTips.SetToolTip(this.lbTotalValueGoodsSuppliedExVAT, Messages.totalValueGoodsSuppliedExVAT_Notes);
            this.toolTips.SetToolTip(this.lbTotalAcquisitionsExVAT, Messages.totalAcquisitionsExVAT_Notes);

            this.toolTips.SetToolTip(this.tbVatDueSales, Messages.vatDueSales);
            this.toolTips.SetToolTip(this.tbDueAcquisitions, Messages.vatDueAcquisitions);
            this.toolTips.SetToolTip(this.tbTotalVatDue, Messages.totalVatDue);
            this.toolTips.SetToolTip(this.tbVatReclaimedCurrPeriod, Messages.vatReclaimedCurrPeriod);
            this.toolTips.SetToolTip(this.tbNetVatDue, Messages.netVatDue);
            this.toolTips.SetToolTip(this.tbTotalValueSalesExVAT, Messages.totalValueSalesExVAT);
            this.toolTips.SetToolTip(this.tbTotalValuePurchasesExVAT, Messages.totalValuePurchasesExVAT);
            this.toolTips.SetToolTip(this.tbTotalValueGoodsSuppliedExVAT, Messages.totalValueGoodsSuppliedExVAT);
            this.toolTips.SetToolTip(this.tbTotalAcquisitionsExVAT, Messages.totalAcquisitionsExVAT);
        }

        OAuthSettings oa = new OAuthSettings();
        VATMaint vATMaint;
        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO Set Below for Test or Production in .Config File
            oa.ApplicationName = MTD_Test.Properties.Settings.Default.ApplicationName;
            oa.ApplicationID = MTD_Test.Properties.Settings.Default.ApplicationID;
            oa.ClientID = MTD_Test.Properties.Settings.Default.ClientID;
            oa.ClientSecret = MTD_Test.Properties.Settings.Default.ClientSecret;
            oa.VRN = MTD_Test.Properties.Settings.Default.VRN;

            vATMaint = new VATMaint(oa, GetFraudHeaders());
        }

        private void fMTD_TEST_Shown(object sender, EventArgs e)
        {

            if (vATMaint.VATProvider.IsSignedIn())
                GetObligations();
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            try
            {
                vATMaint.VATProvider.SignIn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bLogin_OOB_Click(object sender, EventArgs e)
        {
            vATMaint.VATProvider.SignIn(true);
        }

        private void bAuth_OOB_Code_Click(object sender, EventArgs e)
        {
            vATMaint.VATProvider.ProcessAuthorizationCode(tbOOB_Code.Text);
        }

        private void tbOOB_Code_TextChanged(object sender, EventArgs e)
        {
            bAuth_OOB_Code.Enabled = !String.IsNullOrWhiteSpace(tbOOB_Code.Text);
        }

        private void bIsLogin_Click(object sender, EventArgs e)
        {
            textBox2.Text = vATMaint.VATProvider.IsSignedIn().ToString();
        }

        private void bGetObligations_Click(object sender, EventArgs e)
        {
            GetObligations();
        }

        private void GetObligations()
        {
            try
            {
                VATMaint.GetVATObligationsProc(vATMaint, DateTime.UtcNow.AddDays(-365), DateTime.UtcNow, testScenario: "MONTHLY_THREE_MET");
                dataGridView2.DataSource = vATMaint.Obligations;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bGetReturns_Click(object sender, EventArgs e)
        {
            try
            {
                VATMaint.CheckVATReturnProc(vATMaint, tbPeriodKey.Text);
                ScatterVatReturn(vATMaint.VATreturn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ScatterVatReturn(VATreturn v)
        {
            tbPeriodKey.Text = v.periodKey;
            tbVatDueSales.NumericValue = v.vatDueSales;
            tbDueAcquisitions.NumericValue = v.vatDueAcquisitions;
            tbTotalVatDue.NumericValue = v.totalVatDue;
            tbVatReclaimedCurrPeriod.NumericValue = v.vatReclaimedCurrPeriod;
            tbNetVatDue.NumericValue = v.netVatDue;
            tbTotalValueSalesExVAT.NumericValue = v.totalValueSalesExVAT;
            tbTotalValuePurchasesExVAT.NumericValue = v.totalValuePurchasesExVAT;
            tbTotalValueGoodsSuppliedExVAT.NumericValue = v.totalValueGoodsSuppliedExVAT;
            tbTotalAcquisitionsExVAT.NumericValue = v.totalAcquisitionsExVAT;
        }

        private void GatherVatReturn(VATreturn v)
        {
            v.periodKey = tbPeriodKey.Text;
            v.vatDueSales = tbVatDueSales.DecimalValue;
            v.vatDueAcquisitions = tbDueAcquisitions.DecimalValue;
            v.totalVatDue = tbTotalVatDue.DecimalValue;
            v.vatReclaimedCurrPeriod = tbVatReclaimedCurrPeriod.DecimalValue;
            v.netVatDue = tbNetVatDue.DecimalValue;
            v.totalValueSalesExVAT = tbTotalValueSalesExVAT.IntegerValue;
            v.totalValuePurchasesExVAT = tbTotalValuePurchasesExVAT.IntegerValue;
            v.totalValueGoodsSuppliedExVAT = tbTotalValueGoodsSuppliedExVAT.IntegerValue;
            v.totalAcquisitionsExVAT = tbTotalAcquisitionsExVAT.IntegerValue;
        }

        private void Calc(VATreturn v)
        {
            GatherVatReturn(v);
            v.totalVatDue = v.vatDueSales + v.vatDueAcquisitions;
            v.netVatDue = v.totalVatDue - v.vatReclaimedCurrPeriod;
            ScatterVatReturn(v);
        }

        private void bSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Calc(vATMaint.VATreturn);
                GatherVatReturn(vATMaint.VATreturn);
                VATMaint.SendVATReturnProc(vATMaint, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            var X = vATMaint.VATReturnResponse;
        }

        private void bGetLiabilities_Click(object sender, EventArgs e)
        {
            try
            {
                VATMaint.GetVATLiabilitiesProc(vATMaint, DateTime.UtcNow.AddDays(-900), DateTime.UtcNow.AddDays(-600), "MULTIPLE_LIABILITIES");
                dataGridView2.DataSource = vATMaint.Liabilities;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bGetPayments_Click(object sender, EventArgs e)
        {
            try
            {
                VATMaint.GetVATPaymentsProc(vATMaint, DateTime.UtcNow.AddDays(-900), DateTime.UtcNow.AddDays(-600), "MULTIPLE_PAYMENTS");
                dataGridView2.DataSource = vATMaint.Payments;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bCalc_Click(object sender, EventArgs e)
        {
            Calc(vATMaint.VATreturn);
        }

        public Dictionary<string, string> GetFraudHeaders()
        {
            Dictionary<string, string> Headers = new Dictionary<string, string>();

            Headers.Add("Gov-Client-Connection-Method", @"DESKTOP_APP_DIRECT");

            string DeviceId = Uri.EscapeDataString(GetMachineGuid());
            Headers.Add("Gov-Client-Device-ID", DeviceId);

            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            int slash = username.IndexOf(@"\");
            if (slash != 0)
                username = username.Substring(slash + 1);
            Headers.Add("Gov-Client-User-IDs", "os=" + username);


            var offset = DateTimeOffset.Now.Offset;
            string sign = offset < TimeSpan.Zero ? "-" : "+";
            string output = "UTC" + sign + offset.ToString(@"hh\:mm");
            Headers.Add("Gov-Client-Timezone", output);

            string macAddresses = "";
            string ipAddresses = "";

            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    if (!string.IsNullOrWhiteSpace(macAddresses))
                    {
                        macAddresses += ",";
                        ipAddresses += ",";
                    }
                    var macAddr = nic.GetPhysicalAddress().GetAddressBytes();
                    var formattedMacAddr = string.Join(":", (from z in macAddr select z.ToString("X2")).ToArray());
                    macAddresses += formattedMacAddr;
                    var ipProperties = nic.GetIPProperties().UnicastAddresses;
                    var ipAddress = ipProperties.Where(z => z.PrefixLength != 64).FirstOrDefault().Address.ToString();
                    ipAddresses += ipAddress;
                    break;
                }
            }

            Headers.Add("Gov-Client-Local-IPs", ipAddresses);
            var ipts = Uri.EscapeDataString(DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss.sssZ"));
            Headers.Add("Gov-Client-Local-IPs-Timestamp", ipts);
            Headers.Add("Gov-Client-MAC-Addresses", Uri.EscapeDataString(macAddresses));

            var w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width.ToString();
            var h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height.ToString();
            var bbp = System.Windows.Forms.Screen.PrimaryScreen.BitsPerPixel.ToString();
            var screen = @"width=" + w + "&height=" + h + "&scaling-factor=1&colour-depth=" + bbp;
            Headers.Add("Gov-Client-Screens", screen);
            Headers.Add("Gov-Client-Window-Size", @"width=" + this.Width.ToString() + "&height=" + this.Height.ToString());

            var os = "os-family=Windows";
            var version = "os-version=" + Uri.EscapeDataString(System.Runtime.InteropServices.RuntimeInformation.OSDescription);
            var mb = "device-manufacturer=" + Uri.EscapeDataString(Manufacturer);
            var model = "device-model=" + Uri.EscapeDataString(Product);
            Headers.Add("Gov-Client-User-Agent", os + "&" + version + "&" + mb + "&" + model);

            Headers.Add("Gov-Client-Multi-Factor", "");

            var m = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductName;
            var v1 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductMajorPart.ToString();
            var v2 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductMinorPart.ToString();

            Headers.Add("Gov-Vendor-Version", m + "=" + v1 + "." + v2);
            Headers.Add("Gov-Vendor-License-IDs", "");
            Headers.Add("Gov-Vendor-Product-Name", m);
            return Headers;
        }

        private static ManagementObjectSearcher baseboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

        static public string Manufacturer
        {
            get
            {
                try
                {
                    foreach (ManagementObject queryObj in baseboardSearcher.Get())
                    {
                        return queryObj["Manufacturer"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
        static public string Product
        {
            get
            {
                try
                {
                    foreach (ManagementObject queryObj in baseboardSearcher.Get())
                    {
                        return queryObj["Product"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public string GetMachineGuid()
        {
            string x64Result = string.Empty;
            string x86Result = string.Empty;
            RegistryKey keyBaseX64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey keyBaseX86 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey keyX64 = keyBaseX64.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey keyX86 = keyBaseX86.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadSubTree);
            object resultObjX64 = keyX64.GetValue("MachineGuid", (object)"default");
            object resultObjX86 = keyX86.GetValue("MachineGuid", (object)"default");
            keyX64.Close();
            keyX86.Close();
            keyBaseX64.Close();
            keyBaseX86.Close();
            keyX64.Dispose();
            keyX86.Dispose();
            keyBaseX64.Dispose();
            keyBaseX86.Dispose();
            keyX64 = null;
            keyX86 = null;
            keyBaseX64 = null;
            keyBaseX86 = null;
            if (resultObjX64 != null && resultObjX64.ToString() != "default")
            {
                return resultObjX64.ToString();
            }
            if (resultObjX86 != null && resultObjX86.ToString() != "default")
            {
                return resultObjX86.ToString();
            }
            return "";
        }
        private void bTestFraud_Click(object sender, EventArgs e)
        {
            string buffer = "";
            foreach (var item in vATMaint.VATProvider.FraudHeaders)
            {
                buffer += item.Key + "=" + item.Value + Environment.NewLine;
            }
            MessageBox.Show(buffer, "Headers");


            var r = vATMaint.VATProvider.TestFraud();
            MessageBox.Show(r, "Result");
        }
    }
}