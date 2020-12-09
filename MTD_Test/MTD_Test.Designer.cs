namespace MTD_TEST
{
    partial class fMTD_TEST
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bLogin_OOB = new System.Windows.Forms.Button();
            this.tbOOB_Code = new System.Windows.Forms.TextBox();
            this.bAuth_OOB_Code = new System.Windows.Forms.Button();
            this.bIsLogin = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bGetObligations = new System.Windows.Forms.Button();
            this.bLogin = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.bGetReturns = new System.Windows.Forms.Button();
            this.lbNetVatDue = new System.Windows.Forms.Label();
            this.lbTotalValueSalesExVAT = new System.Windows.Forms.Label();
            this.lbTotalAcquisitionsExVAT = new System.Windows.Forms.Label();
            this.lbPeriodKey = new System.Windows.Forms.Label();
            this.lbVatDueSales = new System.Windows.Forms.Label();
            this.lbDueAcquisitions = new System.Windows.Forms.Label();
            this.lbTotalVatDue = new System.Windows.Forms.Label();
            this.lbVatReclaimedCurrPeriod = new System.Windows.Forms.Label();
            this.lbTotalValuePurchasesExVAT = new System.Windows.Forms.Label();
            this.lbTotalValueGoodsSuppliedExVAT = new System.Windows.Forms.Label();
            this.tbPeriodKey = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bCalc = new System.Windows.Forms.Button();
            this.bSubmit = new System.Windows.Forms.Button();
            this.tbTotalAcquisitionsExVAT = new MTD_TEST.Controls.NumericTextBox();
            this.tbVatDueSales = new MTD_TEST.Controls.NumericTextBox();
            this.tbatDueAcquisitions = new MTD_TEST.Controls.NumericTextBox();
            this.tbTotalVatDue = new MTD_TEST.Controls.NumericTextBox();
            this.tbVatReclaimedCurrPeriod = new MTD_TEST.Controls.NumericTextBox();
            this.tbTotalValueGoodsSuppliedExVAT = new MTD_TEST.Controls.NumericTextBox();
            this.tbNetVatDue = new MTD_TEST.Controls.NumericTextBox();
            this.tbTotalValueSalesExVAT = new MTD_TEST.Controls.NumericTextBox();
            this.tbTotalValuePurchasesExVAT = new MTD_TEST.Controls.NumericTextBox();
            this.bGetLiabilities = new System.Windows.Forms.Button();
            this.bGetPayments = new System.Windows.Forms.Button();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.bTestFraud = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bLogin_OOB
            // 
            this.bLogin_OOB.Location = new System.Drawing.Point(367, 3);
            this.bLogin_OOB.Name = "bLogin_OOB";
            this.bLogin_OOB.Size = new System.Drawing.Size(122, 23);
            this.bLogin_OOB.TabIndex = 0;
            this.bLogin_OOB.Text = "Manual Login";
            this.bLogin_OOB.UseVisualStyleBackColor = true;
            this.bLogin_OOB.Click += new System.EventHandler(this.bLogin_OOB_Click);
            // 
            // tbOOB_Code
            // 
            this.tbOOB_Code.Location = new System.Drawing.Point(368, 32);
            this.tbOOB_Code.Name = "tbOOB_Code";
            this.tbOOB_Code.Size = new System.Drawing.Size(121, 20);
            this.tbOOB_Code.TabIndex = 1;
            this.tbOOB_Code.TextChanged += new System.EventHandler(this.tbOOB_Code_TextChanged);
            // 
            // bAuth_OOB_Code
            // 
            this.bAuth_OOB_Code.Enabled = false;
            this.bAuth_OOB_Code.Location = new System.Drawing.Point(495, 32);
            this.bAuth_OOB_Code.Name = "bAuth_OOB_Code";
            this.bAuth_OOB_Code.Size = new System.Drawing.Size(94, 20);
            this.bAuth_OOB_Code.TabIndex = 2;
            this.bAuth_OOB_Code.Text = "Process Code";
            this.bAuth_OOB_Code.UseVisualStyleBackColor = true;
            this.bAuth_OOB_Code.Click += new System.EventHandler(this.bAuth_OOB_Code_Click);
            // 
            // bIsLogin
            // 
            this.bIsLogin.Location = new System.Drawing.Point(140, 2);
            this.bIsLogin.Name = "bIsLogin";
            this.bIsLogin.Size = new System.Drawing.Size(75, 23);
            this.bIsLogin.TabIndex = 3;
            this.bIsLogin.Text = "Is Login";
            this.bIsLogin.UseVisualStyleBackColor = true;
            this.bIsLogin.Click += new System.EventHandler(this.bIsLogin_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(222, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(66, 20);
            this.textBox2.TabIndex = 4;
            // 
            // bGetObligations
            // 
            this.bGetObligations.Location = new System.Drawing.Point(12, 28);
            this.bGetObligations.Name = "bGetObligations";
            this.bGetObligations.Size = new System.Drawing.Size(84, 23);
            this.bGetObligations.TabIndex = 5;
            this.bGetObligations.Text = "Obligations";
            this.bGetObligations.UseVisualStyleBackColor = true;
            this.bGetObligations.Click += new System.EventHandler(this.bGetObligations_Click);
            // 
            // bLogin
            // 
            this.bLogin.Location = new System.Drawing.Point(12, 3);
            this.bLogin.Name = "bLogin";
            this.bLogin.Size = new System.Drawing.Size(122, 23);
            this.bLogin.TabIndex = 9;
            this.bLogin.Text = "Auto Login";
            this.bLogin.UseVisualStyleBackColor = true;
            this.bLogin.Click += new System.EventHandler(this.bLogin_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 57);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(548, 281);
            this.dataGridView2.TabIndex = 12;
            // 
            // bGetReturns
            // 
            this.bGetReturns.Location = new System.Drawing.Point(6, 34);
            this.bGetReturns.Name = "bGetReturns";
            this.bGetReturns.Size = new System.Drawing.Size(60, 23);
            this.bGetReturns.TabIndex = 11;
            this.bGetReturns.Text = "Read";
            this.bGetReturns.UseVisualStyleBackColor = true;
            this.bGetReturns.Click += new System.EventHandler(this.bGetReturns_Click);
            // 
            // lbNetVatDue
            // 
            this.lbNetVatDue.AutoSize = true;
            this.lbNetVatDue.Location = new System.Drawing.Point(114, 121);
            this.lbNetVatDue.Name = "lbNetVatDue";
            this.lbNetVatDue.Size = new System.Drawing.Size(66, 13);
            this.lbNetVatDue.TabIndex = 26;
            this.lbNetVatDue.Text = "Net Vat Due";
            // 
            // lbTotalValueSalesExVAT
            // 
            this.lbTotalValueSalesExVAT.AutoSize = true;
            this.lbTotalValueSalesExVAT.Location = new System.Drawing.Point(54, 143);
            this.lbTotalValueSalesExVAT.Name = "lbTotalValueSalesExVAT";
            this.lbTotalValueSalesExVAT.Size = new System.Drawing.Size(126, 13);
            this.lbTotalValueSalesExVAT.TabIndex = 27;
            this.lbTotalValueSalesExVAT.Text = "Total Value Sales ExVAT";
            // 
            // lbTotalAcquisitionsExVAT
            // 
            this.lbTotalAcquisitionsExVAT.AutoSize = true;
            this.lbTotalAcquisitionsExVAT.Location = new System.Drawing.Point(54, 219);
            this.lbTotalAcquisitionsExVAT.Name = "lbTotalAcquisitionsExVAT";
            this.lbTotalAcquisitionsExVAT.Size = new System.Drawing.Size(126, 13);
            this.lbTotalAcquisitionsExVAT.TabIndex = 33;
            this.lbTotalAcquisitionsExVAT.Text = "Total Acquisitions ExVAT";
            // 
            // lbPeriodKey
            // 
            this.lbPeriodKey.AutoSize = true;
            this.lbPeriodKey.Location = new System.Drawing.Point(3, 11);
            this.lbPeriodKey.Name = "lbPeriodKey";
            this.lbPeriodKey.Size = new System.Drawing.Size(55, 13);
            this.lbPeriodKey.TabIndex = 21;
            this.lbPeriodKey.Text = "PeriodKey";
            // 
            // lbVatDueSales
            // 
            this.lbVatDueSales.AutoSize = true;
            this.lbVatDueSales.Location = new System.Drawing.Point(127, 11);
            this.lbVatDueSales.Name = "lbVatDueSales";
            this.lbVatDueSales.Size = new System.Drawing.Size(53, 13);
            this.lbVatDueSales.TabIndex = 22;
            this.lbVatDueSales.Text = "DueSales";
            // 
            // lbDueAcquisitions
            // 
            this.lbDueAcquisitions.AutoSize = true;
            this.lbDueAcquisitions.Location = new System.Drawing.Point(94, 37);
            this.lbDueAcquisitions.Name = "lbDueAcquisitions";
            this.lbDueAcquisitions.Size = new System.Drawing.Size(86, 13);
            this.lbDueAcquisitions.TabIndex = 23;
            this.lbDueAcquisitions.Text = "Due Acquisitions";
            // 
            // lbTotalVatDue
            // 
            this.lbTotalVatDue.AutoSize = true;
            this.lbTotalVatDue.Location = new System.Drawing.Point(107, 65);
            this.lbTotalVatDue.Name = "lbTotalVatDue";
            this.lbTotalVatDue.Size = new System.Drawing.Size(73, 13);
            this.lbTotalVatDue.TabIndex = 24;
            this.lbTotalVatDue.Text = "Total Vat Due";
            // 
            // lbVatReclaimedCurrPeriod
            // 
            this.lbVatReclaimedCurrPeriod.AutoSize = true;
            this.lbVatReclaimedCurrPeriod.Location = new System.Drawing.Point(71, 95);
            this.lbVatReclaimedCurrPeriod.Name = "lbVatReclaimedCurrPeriod";
            this.lbVatReclaimedCurrPeriod.Size = new System.Drawing.Size(109, 13);
            this.lbVatReclaimedCurrPeriod.TabIndex = 25;
            this.lbVatReclaimedCurrPeriod.Text = "Reclaimed CurrPeriod";
            // 
            // lbTotalValuePurchasesExVAT
            // 
            this.lbTotalValuePurchasesExVAT.AutoSize = true;
            this.lbTotalValuePurchasesExVAT.Location = new System.Drawing.Point(30, 169);
            this.lbTotalValuePurchasesExVAT.Name = "lbTotalValuePurchasesExVAT";
            this.lbTotalValuePurchasesExVAT.Size = new System.Drawing.Size(150, 13);
            this.lbTotalValuePurchasesExVAT.TabIndex = 29;
            this.lbTotalValuePurchasesExVAT.Text = "Total Value Purchases ExVAT";
            // 
            // lbTotalValueGoodsSuppliedExVAT
            // 
            this.lbTotalValueGoodsSuppliedExVAT.AutoSize = true;
            this.lbTotalValueGoodsSuppliedExVAT.Location = new System.Drawing.Point(5, 192);
            this.lbTotalValueGoodsSuppliedExVAT.Name = "lbTotalValueGoodsSuppliedExVAT";
            this.lbTotalValueGoodsSuppliedExVAT.Size = new System.Drawing.Size(175, 13);
            this.lbTotalValueGoodsSuppliedExVAT.TabIndex = 31;
            this.lbTotalValueGoodsSuppliedExVAT.Text = "Total Value Goods Supplied ExVAT";
            // 
            // tbPeriodKey
            // 
            this.tbPeriodKey.Location = new System.Drawing.Point(57, 8);
            this.tbPeriodKey.Name = "tbPeriodKey";
            this.tbPeriodKey.Size = new System.Drawing.Size(56, 20);
            this.tbPeriodKey.TabIndex = 14;
            this.tbPeriodKey.Text = "18AG";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.bCalc);
            this.panel1.Controls.Add(this.lbTotalAcquisitionsExVAT);
            this.panel1.Controls.Add(this.bSubmit);
            this.panel1.Controls.Add(this.tbPeriodKey);
            this.panel1.Controls.Add(this.bGetReturns);
            this.panel1.Controls.Add(this.tbTotalAcquisitionsExVAT);
            this.panel1.Controls.Add(this.tbVatDueSales);
            this.panel1.Controls.Add(this.tbatDueAcquisitions);
            this.panel1.Controls.Add(this.tbTotalVatDue);
            this.panel1.Controls.Add(this.lbTotalValueGoodsSuppliedExVAT);
            this.panel1.Controls.Add(this.tbVatReclaimedCurrPeriod);
            this.panel1.Controls.Add(this.tbTotalValueGoodsSuppliedExVAT);
            this.panel1.Controls.Add(this.tbNetVatDue);
            this.panel1.Controls.Add(this.lbTotalValuePurchasesExVAT);
            this.panel1.Controls.Add(this.tbTotalValueSalesExVAT);
            this.panel1.Controls.Add(this.tbTotalValuePurchasesExVAT);
            this.panel1.Controls.Add(this.lbPeriodKey);
            this.panel1.Controls.Add(this.lbTotalValueSalesExVAT);
            this.panel1.Controls.Add(this.lbVatDueSales);
            this.panel1.Controls.Add(this.lbNetVatDue);
            this.panel1.Controls.Add(this.lbDueAcquisitions);
            this.panel1.Controls.Add(this.lbVatReclaimedCurrPeriod);
            this.panel1.Controls.Add(this.lbTotalVatDue);
            this.panel1.Location = new System.Drawing.Point(566, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 281);
            this.panel1.TabIndex = 36;
            // 
            // bCalc
            // 
            this.bCalc.Location = new System.Drawing.Point(6, 246);
            this.bCalc.Name = "bCalc";
            this.bCalc.Size = new System.Drawing.Size(63, 23);
            this.bCalc.TabIndex = 38;
            this.bCalc.Text = "Calc";
            this.bCalc.UseVisualStyleBackColor = true;
            this.bCalc.Click += new System.EventHandler(this.bCalc_Click);
            // 
            // bSubmit
            // 
            this.bSubmit.Location = new System.Drawing.Point(178, 245);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.Size = new System.Drawing.Size(97, 23);
            this.bSubmit.TabIndex = 37;
            this.bSubmit.Text = "Submmit";
            this.bSubmit.UseVisualStyleBackColor = true;
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // tbTotalAcquisitionsExVAT
            // 
            this.tbTotalAcquisitionsExVAT.AllowNegative = true;
            this.tbTotalAcquisitionsExVAT.Location = new System.Drawing.Point(188, 219);
            this.tbTotalAcquisitionsExVAT.Name = "tbTotalAcquisitionsExVAT";
            this.tbTotalAcquisitionsExVAT.NumericPrecision = 9;
            this.tbTotalAcquisitionsExVAT.NumericScaleOnFocus = 0;
            this.tbTotalAcquisitionsExVAT.NumericScaleOnLostFocus = 0;
            this.tbTotalAcquisitionsExVAT.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbTotalAcquisitionsExVAT.Size = new System.Drawing.Size(71, 20);
            this.tbTotalAcquisitionsExVAT.TabIndex = 34;
            this.tbTotalAcquisitionsExVAT.Text = "0";
            this.tbTotalAcquisitionsExVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalAcquisitionsExVAT.ZeroIsValid = true;
            // 
            // tbVatDueSales
            // 
            this.tbVatDueSales.AllowNegative = true;
            this.tbVatDueSales.Location = new System.Drawing.Point(188, 8);
            this.tbVatDueSales.Name = "tbVatDueSales";
            this.tbVatDueSales.NumericPrecision = 9;
            this.tbVatDueSales.NumericScaleOnFocus = 2;
            this.tbVatDueSales.NumericScaleOnLostFocus = 2;
            this.tbVatDueSales.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbVatDueSales.Size = new System.Drawing.Size(87, 20);
            this.tbVatDueSales.TabIndex = 15;
            this.tbVatDueSales.Text = "0.00";
            this.tbVatDueSales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbVatDueSales.ZeroIsValid = true;
            // 
            // tbatDueAcquisitions
            // 
            this.tbatDueAcquisitions.AllowNegative = true;
            this.tbatDueAcquisitions.Location = new System.Drawing.Point(188, 34);
            this.tbatDueAcquisitions.Name = "tbatDueAcquisitions";
            this.tbatDueAcquisitions.NumericPrecision = 9;
            this.tbatDueAcquisitions.NumericScaleOnFocus = 2;
            this.tbatDueAcquisitions.NumericScaleOnLostFocus = 2;
            this.tbatDueAcquisitions.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbatDueAcquisitions.Size = new System.Drawing.Size(87, 20);
            this.tbatDueAcquisitions.TabIndex = 16;
            this.tbatDueAcquisitions.Text = "0.00";
            this.tbatDueAcquisitions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbatDueAcquisitions.ZeroIsValid = true;
            // 
            // tbTotalVatDue
            // 
            this.tbTotalVatDue.AllowNegative = true;
            this.tbTotalVatDue.Location = new System.Drawing.Point(188, 62);
            this.tbTotalVatDue.Name = "tbTotalVatDue";
            this.tbTotalVatDue.NumericPrecision = 9;
            this.tbTotalVatDue.NumericScaleOnFocus = 2;
            this.tbTotalVatDue.NumericScaleOnLostFocus = 2;
            this.tbTotalVatDue.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbTotalVatDue.ReadOnly = true;
            this.tbTotalVatDue.Size = new System.Drawing.Size(87, 20);
            this.tbTotalVatDue.TabIndex = 17;
            this.tbTotalVatDue.Text = "0.00";
            this.tbTotalVatDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalVatDue.ZeroIsValid = true;
            // 
            // tbVatReclaimedCurrPeriod
            // 
            this.tbVatReclaimedCurrPeriod.AllowNegative = true;
            this.tbVatReclaimedCurrPeriod.Location = new System.Drawing.Point(188, 88);
            this.tbVatReclaimedCurrPeriod.Name = "tbVatReclaimedCurrPeriod";
            this.tbVatReclaimedCurrPeriod.NumericPrecision = 9;
            this.tbVatReclaimedCurrPeriod.NumericScaleOnFocus = 2;
            this.tbVatReclaimedCurrPeriod.NumericScaleOnLostFocus = 2;
            this.tbVatReclaimedCurrPeriod.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbVatReclaimedCurrPeriod.Size = new System.Drawing.Size(87, 20);
            this.tbVatReclaimedCurrPeriod.TabIndex = 18;
            this.tbVatReclaimedCurrPeriod.Text = "0.00";
            this.tbVatReclaimedCurrPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbVatReclaimedCurrPeriod.ZeroIsValid = true;
            // 
            // tbTotalValueGoodsSuppliedExVAT
            // 
            this.tbTotalValueGoodsSuppliedExVAT.AllowNegative = true;
            this.tbTotalValueGoodsSuppliedExVAT.Location = new System.Drawing.Point(188, 192);
            this.tbTotalValueGoodsSuppliedExVAT.Name = "tbTotalValueGoodsSuppliedExVAT";
            this.tbTotalValueGoodsSuppliedExVAT.NumericPrecision = 9;
            this.tbTotalValueGoodsSuppliedExVAT.NumericScaleOnFocus = 0;
            this.tbTotalValueGoodsSuppliedExVAT.NumericScaleOnLostFocus = 0;
            this.tbTotalValueGoodsSuppliedExVAT.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbTotalValueGoodsSuppliedExVAT.Size = new System.Drawing.Size(71, 20);
            this.tbTotalValueGoodsSuppliedExVAT.TabIndex = 30;
            this.tbTotalValueGoodsSuppliedExVAT.Text = "0";
            this.tbTotalValueGoodsSuppliedExVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalValueGoodsSuppliedExVAT.ZeroIsValid = true;
            // 
            // tbNetVatDue
            // 
            this.tbNetVatDue.AllowNegative = true;
            this.tbNetVatDue.Location = new System.Drawing.Point(188, 114);
            this.tbNetVatDue.Name = "tbNetVatDue";
            this.tbNetVatDue.NumericPrecision = 9;
            this.tbNetVatDue.NumericScaleOnFocus = 2;
            this.tbNetVatDue.NumericScaleOnLostFocus = 2;
            this.tbNetVatDue.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbNetVatDue.ReadOnly = true;
            this.tbNetVatDue.Size = new System.Drawing.Size(87, 20);
            this.tbNetVatDue.TabIndex = 19;
            this.tbNetVatDue.Text = "0.00";
            this.tbNetVatDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbNetVatDue.ZeroIsValid = true;
            // 
            // tbTotalValueSalesExVAT
            // 
            this.tbTotalValueSalesExVAT.AllowNegative = true;
            this.tbTotalValueSalesExVAT.Location = new System.Drawing.Point(188, 140);
            this.tbTotalValueSalesExVAT.Name = "tbTotalValueSalesExVAT";
            this.tbTotalValueSalesExVAT.NumericPrecision = 9;
            this.tbTotalValueSalesExVAT.NumericScaleOnFocus = 0;
            this.tbTotalValueSalesExVAT.NumericScaleOnLostFocus = 0;
            this.tbTotalValueSalesExVAT.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbTotalValueSalesExVAT.Size = new System.Drawing.Size(71, 20);
            this.tbTotalValueSalesExVAT.TabIndex = 20;
            this.tbTotalValueSalesExVAT.Text = "0";
            this.tbTotalValueSalesExVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalValueSalesExVAT.ZeroIsValid = true;
            // 
            // tbTotalValuePurchasesExVAT
            // 
            this.tbTotalValuePurchasesExVAT.AllowNegative = true;
            this.tbTotalValuePurchasesExVAT.Location = new System.Drawing.Point(188, 166);
            this.tbTotalValuePurchasesExVAT.Name = "tbTotalValuePurchasesExVAT";
            this.tbTotalValuePurchasesExVAT.NumericPrecision = 9;
            this.tbTotalValuePurchasesExVAT.NumericScaleOnFocus = 0;
            this.tbTotalValuePurchasesExVAT.NumericScaleOnLostFocus = 0;
            this.tbTotalValuePurchasesExVAT.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbTotalValuePurchasesExVAT.Size = new System.Drawing.Size(71, 20);
            this.tbTotalValuePurchasesExVAT.TabIndex = 28;
            this.tbTotalValuePurchasesExVAT.Text = "0";
            this.tbTotalValuePurchasesExVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalValuePurchasesExVAT.ZeroIsValid = true;
            // 
            // bGetLiabilities
            // 
            this.bGetLiabilities.Location = new System.Drawing.Point(101, 29);
            this.bGetLiabilities.Name = "bGetLiabilities";
            this.bGetLiabilities.Size = new System.Drawing.Size(84, 23);
            this.bGetLiabilities.TabIndex = 38;
            this.bGetLiabilities.Text = "Liabilities";
            this.bGetLiabilities.UseVisualStyleBackColor = true;
            this.bGetLiabilities.Click += new System.EventHandler(this.bGetLiabilities_Click);
            // 
            // bGetPayments
            // 
            this.bGetPayments.Location = new System.Drawing.Point(191, 28);
            this.bGetPayments.Name = "bGetPayments";
            this.bGetPayments.Size = new System.Drawing.Size(84, 23);
            this.bGetPayments.TabIndex = 39;
            this.bGetPayments.Text = "Payments";
            this.bGetPayments.UseVisualStyleBackColor = true;
            this.bGetPayments.Click += new System.EventHandler(this.bGetPayments_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Auth Code";
            // 
            // bTestFraud
            // 
            this.bTestFraud.Location = new System.Drawing.Point(770, 30);
            this.bTestFraud.Name = "bTestFraud";
            this.bTestFraud.Size = new System.Drawing.Size(72, 23);
            this.bTestFraud.TabIndex = 42;
            this.bTestFraud.Text = "Test Fraud";
            this.bTestFraud.UseVisualStyleBackColor = true;
            this.bTestFraud.Click += new System.EventHandler(this.bTestFraud_Click);
            // 
            // fMTD_TEST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(862, 341);
            this.Controls.Add(this.bTestFraud);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bGetPayments);
            this.Controls.Add(this.bGetLiabilities);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.bLogin);
            this.Controls.Add(this.bGetObligations);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.bIsLogin);
            this.Controls.Add(this.bAuth_OOB_Code);
            this.Controls.Add(this.tbOOB_Code);
            this.Controls.Add(this.bLogin_OOB);
            this.Name = "fMTD_TEST";
            this.Text = "MTD TEST";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.fMTD_TEST_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bLogin_OOB;
        private System.Windows.Forms.TextBox tbOOB_Code;
        private System.Windows.Forms.Button bAuth_OOB_Code;
        private System.Windows.Forms.Button bIsLogin;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bGetObligations;
        private System.Windows.Forms.Button bLogin;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button bGetReturns;
        private System.Windows.Forms.TextBox tbPeriodKey;
        private Controls.NumericTextBox tbVatDueSales;
        private Controls.NumericTextBox tbatDueAcquisitions;
        private Controls.NumericTextBox tbTotalVatDue;
        private Controls.NumericTextBox tbVatReclaimedCurrPeriod;
        private Controls.NumericTextBox tbNetVatDue;
        private Controls.NumericTextBox tbTotalValueSalesExVAT;
        private Controls.NumericTextBox tbTotalValuePurchasesExVAT;
        private Controls.NumericTextBox tbTotalValueGoodsSuppliedExVAT;
        private Controls.NumericTextBox tbTotalAcquisitionsExVAT;
        private System.Windows.Forms.Label lbPeriodKey;
        private System.Windows.Forms.Label lbVatDueSales;
        private System.Windows.Forms.Label lbDueAcquisitions;
        private System.Windows.Forms.Label lbTotalVatDue;
        private System.Windows.Forms.Label lbVatReclaimedCurrPeriod;
        private System.Windows.Forms.Label lbNetVatDue;
        private System.Windows.Forms.Label lbTotalValueSalesExVAT;
        private System.Windows.Forms.Label lbTotalValuePurchasesExVAT;
        private System.Windows.Forms.Label lbTotalValueGoodsSuppliedExVAT;
        private System.Windows.Forms.Label lbTotalAcquisitionsExVAT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bSubmit;
        private System.Windows.Forms.Button bGetLiabilities;
        private System.Windows.Forms.Button bGetPayments;
        private System.Windows.Forms.Button bCalc;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bTestFraud;
    }
}