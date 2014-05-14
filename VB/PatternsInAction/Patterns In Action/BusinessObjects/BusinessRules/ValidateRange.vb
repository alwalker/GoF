Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace BusinessObjects.BusinessRules
	''' <summary>
	''' Validates a range (min and max) for a given data type.
	''' </summary>
	Public Class ValidateRange
		Inherits BusinessRule
        Private _dataType As ValidationDataType
        Private Property DataType() As ValidationDataType
            Get
                Return _dataType
            End Get
            Set(ByVal value As ValidationDataType)
                _dataType = value
            End Set
        End Property
        Private _operator As ValidationOperator
        Private Property TheOperator() As ValidationOperator
            Get
                Return _operator
            End Get
            Set(ByVal value As ValidationOperator)
                _operator = value
            End Set
        End Property

        Private _min As Object
        Private Property Min() As Object
            Get
                Return _min
            End Get
            Set(ByVal value As Object)
                _min = value
            End Set
        End Property
        Private _max As Object
        Private Property Max() As Object
            Get
                Return _max
            End Get
            Set(ByVal value As Object)
                _max = value
            End Set
        End Property

        Public Sub New(ByVal propertyName As String, ByVal min As Object, ByVal max As Object, ByVal theOperator As ValidationOperator, ByVal dataType As ValidationDataType)
            MyBase.New(propertyName)
            Me.Min = min
            Me.Max = max

            Me.TheOperator = theOperator
            Me.DataType = dataType

            ErrorMessage = propertyName & " must be between " & CStr(Me.Min) & " and " & CStr(Me.Max)
        End Sub

        Public Sub New(ByVal propertyName As String, ByVal errorMessage As String, ByVal min As Object, ByVal max As Object, ByVal theOperator As ValidationOperator, ByVal dataType As ValidationDataType)
            Me.New(propertyName, min, max, theOperator, dataType)
            Me.ErrorMessage = errorMessage
        End Sub

		Public Overrides Function Validate(ByVal businessObject As BusinessObject) As Boolean
			Try
				Dim value As String = GetPropertyValue(businessObject).ToString()

				Select Case DataType
					Case ValidationDataType.Integer

						Dim imin As Integer = Integer.Parse(Min.ToString())
						Dim imax As Integer = Integer.Parse(Max.ToString())
						Dim ival As Integer = Integer.Parse(value)

						Return (ival >= imin AndAlso ival <= imax)

					Case ValidationDataType.Double
						Dim dmin As Double = Double.Parse(Min.ToString())
						Dim dmax As Double = Double.Parse(Max.ToString())
						Dim dval As Double = Double.Parse(value)

						Return (dval >= dmin AndAlso dval <= dmax)

					Case ValidationDataType.Decimal
						Dim cmin As Decimal = Decimal.Parse(Min.ToString())
						Dim cmax As Decimal = Decimal.Parse(Max.ToString())
						Dim cval As Decimal = Decimal.Parse(value)

						Return (cval >= cmin AndAlso cval <= cmax)

					Case ValidationDataType.Date
						Dim tmin As DateTime = DateTime.Parse(Min.ToString())
						Dim tmax As DateTime = DateTime.Parse(Max.ToString())
						Dim tval As DateTime = DateTime.Parse(value)

						Return (tval >= tmin AndAlso tval <= tmax)

					Case ValidationDataType.String

						Dim smin As String = Min.ToString()
						Dim smax As String = Max.ToString()

						Dim result1 As Integer = String.Compare(smin, value)
						Dim result2 As Integer = String.Compare(value, smax)

						Return result1 >= 0 AndAlso result2 <= 0
				End Select
				Return False
			Catch
				Return False
			End Try
		End Function
	End Class
End Namespace
