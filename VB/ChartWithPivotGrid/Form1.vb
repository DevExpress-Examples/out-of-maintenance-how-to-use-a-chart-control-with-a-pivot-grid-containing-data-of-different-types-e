Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Namespace ChartWithPivotGrid
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'nwindDataSet.SalesPerson' table. You can move, or remove it, as needed.
			Me.salesPersonTableAdapter.Fill(Me.nwindDataSet.SalesPerson)
			chartControl1.DataSource = Me.pivotGridControl1
			chartControl1.SeriesDataMember = "Series"
			chartControl1.SeriesTemplate.ArgumentDataMember = "Arguments"
			chartControl1.SeriesTemplate.ValueDataMembers.AddRange(New String() { "Values" })
		End Sub

		Private Sub pivotGridControl1_FieldAreaChanged(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotFieldEventArgs) Handles pivotGridControl1.FieldAreaChanged
			If fieldOrderDate.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea Then
				chartControl1.SeriesTemplate.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime
				chartControl1.DataSource = pivotGridControl1
			End If
			If fieldQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea Then
				chartControl1.SeriesTemplate.ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical
				chartControl1.DataSource = pivotGridControl1
			End If
		End Sub

		Private Sub pivotGridControl1_FieldAreaChanging(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotAreaChangingEventArgs) Handles pivotGridControl1.FieldAreaChanging
			chartControl1.DataSource = Nothing
			If e.Field Is fieldOrderDate AndAlso e.NewArea = DevExpress.XtraPivotGrid.PivotArea.DataArea AndAlso fieldQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea Then
				e.Allow = False
			End If
			If e.Field Is fieldQuantity AndAlso e.NewArea = DevExpress.XtraPivotGrid.PivotArea.DataArea AndAlso fieldOrderDate.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea Then
				e.Allow = False
			End If
		End Sub
	End Class
End Namespace