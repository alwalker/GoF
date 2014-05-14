Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.Text
Imports System.IO
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Reflection

Imports ImageService.ServiceContracts

Namespace ImageService.ServiceImplementations
	''' <summary>
	''' Image server service implementation. Returns requested images.
	''' Currently, the service provides no add, edit, delete functionality.
	''' </summary>
	Public Class ImageService
		Implements IImageService
		''' <summary>
		''' Gets large customer image
		''' </summary>
		''' <param name="customerId">Customer Identifier.</param>
		''' <returns>Image stream.</returns>
		Public Function GetCustomerImageLarge(ByVal customerId As String) As Stream Implements IImageService.GetCustomerImageLarge
			Return GetCustomerImage("Large", customerId)
		End Function

		''' <summary>
		''' Gets small customer image
		''' </summary>
		''' <param name="customerId">Customer Identifier.</param>
		''' <returns>Image stream.</returns>
		Public Function GetCustomerImageSmall(ByVal customerId As String) As Stream Implements IImageService.GetCustomerImageSmall
			Return GetCustomerImage("Small", customerId)
		End Function

		''' <summary>
		''' Helper methods. Gets large or small customer image.
		''' </summary>
		''' <param name="size">Image size. Small or Large.</param>
		''' <param name="customerId">Customer Identifier.</param>
		''' <returns>Image stream.</returns>
		Private Function GetCustomerImage(ByVal size As String, ByVal customerId As String) As Stream
			' Get host folder
			Dim path As String = AppDomain.CurrentDomain.BaseDirectory

			' Application has up to 91 images. Note: image upload is not implemented.
			Dim id As Integer = Integer.Parse(customerId)
			Dim name As String = If((id = 0 OrElse id > 91), "anon", customerId)
            Dim file As String = System.IO.Path.Combine(path, "Images\Customers\" & size & "\" & name & ".jpg")

			Dim stream As New FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read)
			WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg"
			Return stream
		End Function

		''' <summary>
		''' Gets product image. For demo purposed, this returns always same image.
		''' </summary>
		''' <param name="productId">Product Identifier.</param>
		''' <returns>Image stream.</returns>
		Public Function GetProductImage(ByVal productId As String) As Stream Implements IImageService.GetProductImage
			' Get host folder
            Dim path As String = AppDomain.CurrentDomain.BaseDirectory

			' Always the same. Product images are required.
            Dim file As String = System.IO.Path.Combine(path, "Images\Products\computerimage.gif")

			Dim stream As New FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read)
			WebOperationContext.Current.OutgoingResponse.ContentType = "image/gif"
			Return stream
		End Function
	End Class
End Namespace