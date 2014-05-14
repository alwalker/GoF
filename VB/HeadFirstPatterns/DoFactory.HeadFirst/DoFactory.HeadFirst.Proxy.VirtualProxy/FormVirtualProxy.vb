Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Threading
Imports System.Net

Namespace DoFactory.HeadFirst.Proxy.VirtualProxy
	''' <summary>
	''' Summary description for FormVirtualProxy.
	''' </summary>
	Public Class FormVirtualProxy
		Inherits System.Windows.Forms.Form
		Private WithEvents buttonTestImageProxy As System.Windows.Forms.Button
		Private WithEvents pictureBox As System.Windows.Forms.PictureBox
		Private WithEvents label1 As System.Windows.Forms.Label
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
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
			Me.buttonTestImageProxy = New System.Windows.Forms.Button()
			Me.pictureBox = New System.Windows.Forms.PictureBox()
			Me.label1 = New System.Windows.Forms.Label()
			CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' buttonTestImageProxy
			' 
			Me.buttonTestImageProxy.Location = New System.Drawing.Point(197, 25)
			Me.buttonTestImageProxy.Name = "buttonTestImageProxy"
			Me.buttonTestImageProxy.Size = New System.Drawing.Size(105, 23)
			Me.buttonTestImageProxy.TabIndex = 0
			Me.buttonTestImageProxy.Text = "Test Image Proxy"
'			Me.buttonTestImageProxy.Click += New System.EventHandler(Me.buttonTestImageProxy_Click);
			' 
			' pictureBox
			' 
			Me.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
			Me.pictureBox.Location = New System.Drawing.Point(31, 24)
			Me.pictureBox.Name = "pictureBox"
			Me.pictureBox.Size = New System.Drawing.Size(147, 159)
			Me.pictureBox.TabIndex = 1
			Me.pictureBox.TabStop = False
'			Me.pictureBox.Click += New System.EventHandler(Me.pictureBox_Click);
			' 
			' label1
			' 
			Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label1.Location = New System.Drawing.Point(194, 51)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(118, 20)
			Me.label1.TabIndex = 2
			Me.label1.Text = "Click button twice"
'			Me.label1.Click += New System.EventHandler(Me.label1_Click);
			' 
			' FormVirtualProxy
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(323, 217)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.pictureBox)
			Me.Controls.Add(Me.buttonTestImageProxy)
			Me.Name = "FormVirtualProxy"
			Me.Text = "Virtual Proxy Test"
			CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New FormVirtualProxy())
		End Sub

		Private Sub buttonTestImageProxy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonTestImageProxy.Click
			Me.pictureBox.Image = New ImageProxy().Image
		End Sub

		Private Sub pictureBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pictureBox.Click

		End Sub

		Private Sub label1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles label1.Click

		End Sub

		Private Class ImageProxy
			Private Shared _image As Image = Nothing
			Private _width As Integer = 133
			Private _height As Integer = 154
			Private _retrieving As Boolean = False

			Public ReadOnly Property Width() As Integer
				Get
					Return If(_image Is Nothing, _width, _image.Width)
				End Get
			End Property
			Public ReadOnly Property Height() As Integer
				Get
					Return If(_image Is Nothing, _height, _image.Height)
				End Get
			End Property

			Public ReadOnly Property Image() As Image
				Get
					If _image IsNot Nothing Then
						Return _image
					Else
						If (Not _retrieving) Then
							_retrieving = True
							Dim retrievalThread As New Thread(New ThreadStart(AddressOf RetrieveImage))
							retrievalThread.Start()
						End If
						Return PlaceHolderImage()
					End If
				End Get
			End Property

			Public Function PlaceHolderImage() As Image
				Return New Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PlaceHolder.jpg"))
			End Function

			Public Sub RetrieveImage()
				' Book image from amazon
				Dim url As String = "http://images.amazon.com/images/P/0596007124.01._PE34_SCMZZZZZZZ_.jpg"

				Dim request As HttpWebRequest = CType(HttpWebRequest.Create(url), HttpWebRequest)
				Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
				_image = Image.FromStream(response.GetResponseStream())
			End Sub
		End Class
	End Class
End Namespace
