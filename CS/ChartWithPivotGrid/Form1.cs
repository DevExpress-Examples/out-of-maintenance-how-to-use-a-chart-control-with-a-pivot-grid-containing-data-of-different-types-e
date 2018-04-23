using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChartWithPivotGrid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nwindDataSet.SalesPerson' table. You can move, or remove it, as needed.
            this.salesPersonTableAdapter.Fill(this.nwindDataSet.SalesPerson);
            chartControl1.DataSource = this.pivotGridControl1;
            chartControl1.SeriesDataMember = "Series";
            chartControl1.SeriesTemplate.ArgumentDataMember = "Arguments";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Values" });
        }

        private void pivotGridControl1_FieldAreaChanged(object sender, DevExpress.XtraPivotGrid.PivotFieldEventArgs e)
        {
            if (fieldOrderDate.Area == DevExpress.XtraPivotGrid.PivotArea.DataArea)
            {
                chartControl1.SeriesTemplate.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
                chartControl1.DataSource = pivotGridControl1;
            }
            if (fieldQuantity.Area == DevExpress.XtraPivotGrid.PivotArea.DataArea)
            {
                chartControl1.SeriesTemplate.ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
                chartControl1.DataSource = pivotGridControl1;
            }
        }

        private void pivotGridControl1_FieldAreaChanging(object sender, DevExpress.XtraPivotGrid.PivotAreaChangingEventArgs e)
        {
            chartControl1.DataSource = null;
            if (e.Field == fieldOrderDate && e.NewArea == DevExpress.XtraPivotGrid.PivotArea.DataArea && fieldQuantity.Area == DevExpress.XtraPivotGrid.PivotArea.DataArea)
                e.Allow = false;
            if (e.Field == fieldQuantity && e.NewArea == DevExpress.XtraPivotGrid.PivotArea.DataArea && fieldOrderDate.Area == DevExpress.XtraPivotGrid.PivotArea.DataArea)
                e.Allow = false; 
        }
    }
}