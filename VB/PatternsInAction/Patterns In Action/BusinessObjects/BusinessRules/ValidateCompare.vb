Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace BusinessObjects.BusinessRules
	''' <summary>
	''' Compares values of two properties given a data type and operator  (>, ==, etc).
	''' </summary>
	Public Class ValidateCompare
		Inherits BusinessRule
        Private _otherPropertyName As String
        Private Property OtherPropertyName() As String
            Get
                Return _otherPropertyName
            End Get
            Set(ByVal value As String)
                _otherPropertyName = value
            End Set
        End Property
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

        Public Sub New(ByVal propertyName As String, ByVal otherPropertyName As String, ByVal theOperator As ValidationOperator, ByVal dataType As ValidationDataType)
            MyBase.New(propertyName)

            Me.OtherPropertyName = otherPropertyName
            Me.TheOperator = theOperator
            Me.DataType = dataType

            ErrorMessage = propertyName & " must be " & theOperator.ToString() & " than " & otherPropertyName
        End Sub

		Public Sub New(ByVal propertyName As String, ByVal otherPropertyName As String, ByVal errorMessage As String, ByVal [operator] As ValidationOperator, ByVal dataType As ValidationDataType)
			Me.New(propertyName, otherPropertyName, [operator], dataType)
			Me.ErrorMessage = errorMessage
		End Sub

		Public Overrides Function Validate(ByVal businessObject As BusinessObject) As Boolean
			Try
				Dim propValue1 As String = businessObject.GetType().GetProperty(PropertyName).GetValue(businessObject, Nothing).ToString()
				Dim propValue2 As String = businessObject.GetType().GetProperty(OtherPropertyName).GetValue(businessObject, Nothing).ToString()

				Select Case DataType
					Case ValidationDataType.Integer

						Dim ival1 As Integer = Integer.Parse(propValue1)
						Dim ival2 As Integer = Integer.Parse(propValue2)

                        Select Case TheOperator
                            Case ValidationOperator.Equal
                                Return ival1 = ival2
                            Case ValidationOperator.NotEqual
                                Return ival1 <> ival2
                            Case ValidationOperator.GreaterThan
                                Return ival1 > ival2
                            Case ValidationOperator.GreaterThanEqual
                                Return ival1 >= ival2
                            Case ValidationOperator.LessThan
                                Return ival1 < ival2
                            Case ValidationOperator.LessThanEqual
                                Return ival1 <= ival2
                        End Select

					Case ValidationDataType.Double

						Dim dval1 As Double = Double.Parse(propValue1)
						Dim dval2 As Double = Double.Parse(propValue2)

                        Select Case TheOperator
                            Case ValidationOperator.Equal
                                Return dval1 = dval2
                            Case ValidationOperator.NotEqual
                                Return dval1 <> dval2
                            Case ValidationOperator.GreaterThan
                                Return dval1 > dval2
                            Case ValidationOperator.GreaterThanEqual
                                Return dval1 >= dval2
                            Case ValidationOperator.LessThan
                                Return dval1 < dval2
                            Case ValidationOperator.LessThanEqual
                                Return dval1 <= dval2
                        End Select

					Case ValidationDataType.Decimal

						Dim cval1 As Decimal = Decimal.Parse(propValue1)
						Dim cval2 As Decimal = Decimal.Parse(propValue2)

                        Select Case TheOperator
                            Case ValidationOperator.Equal
                                Return cval1 = cval2
                            Case ValidationOperator.NotEqual
                                Return cval1 <> cval2
                            Case ValidationOperator.GreaterThan
                                Return cval1 > cval2
                            Case ValidationOperator.GreaterThanEqual
                                Return cval1 >= cval2
                            Case ValidationOperator.LessThan
                                Return cval1 < cval2
                            Case ValidationOperator.LessThanEqual
                                Return cval1 <= cval2
                        End Select

					Case ValidationDataType.Date

						Dim tval1 As DateTime = DateTime.Parse(propValue1)
						Dim tval2 As DateTime = DateTime.Parse(propValue2)

                        Select Case TheOperator
                            Case ValidationOperator.Equal
                                Return tval1 = tval2
                            Case ValidationOperator.NotEqual
                                Return tval1 <> tval2
                            Case ValidationOperator.GreaterThan
                                Return tval1 > tval2
                            Case ValidationOperator.GreaterThanEqual
                                Return tval1 >= tval2
                            Case ValidationOperator.LessThan
                                Return tval1 < tval2
                            Case ValidationOperator.LessThanEqual
                                Return tval1 <= tval2
                        End Select

					Case ValidationDataType.String

						Dim result As Integer = String.Compare(propValue1, propValue2, StringComparison.CurrentCulture)

                        Select Case TheOperator
                            Case ValidationOperator.Equal
                                Return result = 0
                            Case ValidationOperator.NotEqual
                                Return result <> 0
                            Case ValidationOperator.GreaterThan
                                Return result > 0
                            Case ValidationOperator.GreaterThanEqual
                                Return result >= 0
                            Case ValidationOperator.LessThan
                                Return result < 0
                            Case ValidationOperator.LessThanEqual
                                Return result <= 0
                        End Select

				End Select
				Return False
			Catch
				Return False
			End Try
		End Function
	End Class
End Namespace
