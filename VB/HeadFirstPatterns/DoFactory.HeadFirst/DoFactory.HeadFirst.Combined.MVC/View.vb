Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace DoFactory.HeadFirst.Combined.MVC
	''' <summary>
	''' The View class in the MVC pattern
	''' </summary>
	Public Class FormMain
        Inherits System.Windows.Forms.Form

		Private textBoxBPM As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label

		Private WithEvents trackBarBPM As System.Windows.Forms.TrackBar
		Private WithEvents buttonSet As System.Windows.Forms.Button
		Private labelMVC As System.Windows.Forms.Label
        Private WithEvents buttonStop As System.Windows.Forms.Button

        Private components As System.ComponentModel.Container = Nothing

        Private groupBoxControl As System.Windows.Forms.GroupBox
        Private groupBoxView As System.Windows.Forms.GroupBox

        ' Two 'beatable' observer controls
        Private textBoxCurrentBPM As DoFactory.HeadFirst.Combined.MVC.BeatTextBox
        Private panelColor As DoFactory.HeadFirst.Combined.MVC.BeatPanel

        Private _controller As Controller

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

        End Sub

        ' Make stop button accessible to controller
        Public ReadOnly Property TheButtonStop() As Button
            Get
                Return buttonStop
            End Get
        End Property

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.textBoxBPM = New System.Windows.Forms.TextBox
            Me.label1 = New System.Windows.Forms.Label
            Me.buttonSet = New System.Windows.Forms.Button
            Me.groupBoxControl = New System.Windows.Forms.GroupBox
            Me.trackBarBPM = New System.Windows.Forms.TrackBar
            Me.groupBoxView = New System.Windows.Forms.GroupBox
            Me.label2 = New System.Windows.Forms.Label
            Me.labelMVC = New System.Windows.Forms.Label
            Me.buttonStop = New System.Windows.Forms.Button

            Me.textBoxCurrentBPM = New DoFactory.HeadFirst.Combined.MVC.BeatTextBox
            Me.panelColor = New DoFactory.HeadFirst.Combined.MVC.BeatPanel



            Me.groupBoxControl.SuspendLayout()
            CType(Me.trackBarBPM, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.groupBoxView.SuspendLayout()
            Me.SuspendLayout()
            '
            'textBoxBPM
            '
            Me.textBoxBPM.Location = New System.Drawing.Point(90, 24)
            Me.textBoxBPM.Name = "textBoxBPM"
            Me.textBoxBPM.Size = New System.Drawing.Size(68, 20)
            Me.textBoxBPM.TabIndex = 0
            Me.textBoxBPM.Text = "120"
            '
            'label1
            '
            Me.label1.Location = New System.Drawing.Point(23, 27)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(65, 19)
            Me.label1.TabIndex = 1
            Me.label1.Text = "Enter BPM:"
            '
            'buttonSet
            '
            Me.buttonSet.Location = New System.Drawing.Point(173, 24)
            Me.buttonSet.Name = "buttonSet"
            Me.buttonSet.Size = New System.Drawing.Size(75, 23)
            Me.buttonSet.TabIndex = 2
            Me.buttonSet.Text = "Set"
            '
            'groupBoxControl
            '
            Me.groupBoxControl.Controls.Add(Me.trackBarBPM)
            Me.groupBoxControl.Controls.Add(Me.label1)
            Me.groupBoxControl.Controls.Add(Me.textBoxBPM)
            Me.groupBoxControl.Controls.Add(Me.buttonSet)
            Me.groupBoxControl.Location = New System.Drawing.Point(19, 48)
            Me.groupBoxControl.Name = "groupBoxControl"
            Me.groupBoxControl.Size = New System.Drawing.Size(281, 116)
            Me.groupBoxControl.TabIndex = 4
            Me.groupBoxControl.TabStop = False
            Me.groupBoxControl.Text = "DJ Control"
            '
            'trackBarBPM
            '
            Me.trackBarBPM.LargeChange = 10
            Me.trackBarBPM.Location = New System.Drawing.Point(26, 61)
            Me.trackBarBPM.Maximum = 200
            Me.trackBarBPM.Minimum = 1
            Me.trackBarBPM.Name = "trackBarBPM"
            Me.trackBarBPM.Size = New System.Drawing.Size(233, 45)
            Me.trackBarBPM.TabIndex = 3
            Me.trackBarBPM.TickFrequency = 10
            Me.trackBarBPM.Value = 120
            '
            'groupBoxView
            '
            Me.groupBoxView.Controls.Add(Me.panelColor)
            Me.groupBoxView.Controls.Add(Me.textBoxCurrentBPM)
            Me.groupBoxView.Controls.Add(Me.label2)
            Me.groupBoxView.Location = New System.Drawing.Point(19, 183)
            Me.groupBoxView.Name = "groupBoxView"
            Me.groupBoxView.Size = New System.Drawing.Size(280, 116)
            Me.groupBoxView.TabIndex = 5
            Me.groupBoxView.TabStop = False
            Me.groupBoxView.Text = "DJ View"
            '
            'label2
            '
            Me.label2.Location = New System.Drawing.Point(23, 30)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(77, 19)
            Me.label2.TabIndex = 4
            Me.label2.Text = "Current BPM:"
            '
            'labelMVC
            '
            Me.labelMVC.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.labelMVC.ForeColor = System.Drawing.Color.Red
            Me.labelMVC.Location = New System.Drawing.Point(19, 13)
            Me.labelMVC.Name = "labelMVC"
            Me.labelMVC.Size = New System.Drawing.Size(173, 23)
            Me.labelMVC.TabIndex = 6
            Me.labelMVC.Text = "Model  View  Controller"
            '
            'buttonStop
            '
            Me.buttonStop.Enabled = False
            Me.buttonStop.Location = New System.Drawing.Point(225, 13)
            Me.buttonStop.Name = "buttonStop"
            Me.buttonStop.Size = New System.Drawing.Size(75, 23)
            Me.buttonStop.TabIndex = 7
            Me.buttonStop.Text = "Stop"
            '
            'panelColor
            '
            Me.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.panelColor.Location = New System.Drawing.Point(26, 64)
            Me.panelColor.Name = "panelColor"
            Me.panelColor.Size = New System.Drawing.Size(222, 30)
            Me.panelColor.TabIndex = 6
            '
            'textBoxCurrentBPM
            '
            Me.textBoxCurrentBPM.BackColor = System.Drawing.SystemColors.Control
            Me.textBoxCurrentBPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.textBoxCurrentBPM.ForeColor = System.Drawing.Color.Red
            Me.textBoxCurrentBPM.Location = New System.Drawing.Point(102, 27)
            Me.textBoxCurrentBPM.Name = "textBoxCurrentBPM"
            Me.textBoxCurrentBPM.Size = New System.Drawing.Size(49, 20)
            Me.textBoxCurrentBPM.TabIndex = 5
            Me.textBoxCurrentBPM.Text = "120"
            Me.textBoxCurrentBPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'FormMain
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(319, 319)
            Me.Controls.Add(Me.buttonStop)
            Me.Controls.Add(Me.labelMVC)
            Me.Controls.Add(Me.groupBoxControl)
            Me.Controls.Add(Me.groupBoxView)
            Me.Name = "FormMain"
            Me.Text = "DJ View"
            Me.groupBoxControl.ResumeLayout(False)
            Me.groupBoxControl.PerformLayout()
            CType(Me.trackBarBPM, System.ComponentModel.ISupportInitialize).EndInit()
            Me.groupBoxView.ResumeLayout(False)
            Me.groupBoxView.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread()> _
        Shared Sub Main()
            Application.Run(New FormMain())
        End Sub

        Private Sub FormMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            _controller = New Controller(Me)

            _controller.Attach(Me.textBoxCurrentBPM)
            _controller.Attach(Me.panelColor)
        End Sub

        ' Set and Start the beat
        Private Sub buttonSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSet.Click
            _controller.BeatsPerMinute = Integer.Parse(textBoxBPM.Text)
            _controller.Start()
        End Sub

        ' Update the pulse
        Private Sub trackBarBPM_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackBarBPM.Scroll
            _controller.BeatsPerMinute = Me.trackBarBPM.Value
            Me.textBoxBPM.Text = Me.trackBarBPM.Value.ToString()
        End Sub

        ' Stop the beat
        Private Sub buttonStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonStop.Click
            _controller.Stop()
        End Sub
    End Class
End Namespace
