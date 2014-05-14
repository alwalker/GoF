Imports Microsoft.VisualBasic
Imports System
Imports System.Security
Imports System.Security.Cryptography
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Encryption
	''' <summary>
	''' Cryptographic class for encryption and decryption of string values.
	''' </summary>
	Public NotInheritable Class Crypto
		' Arbitrary key and iv vector. 
		' You will want to generate (and protect) your own when using encryption.
		Private Shared actionKey As String = "EA81AA1D5FC1EC53E84F30AA746139EEBAFF8A9B76638895"
		Private Shared actionIv As String = "87AF7EA221F3FFF5"

		Private Shared des3 As TripleDESCryptoServiceProvider

		''' <summary>
		''' Default constructor. Initializes the DES3 encryption provider. 
		''' </summary>
		Private Sub New()
		End Sub
		Shared Sub New()
			des3 = New TripleDESCryptoServiceProvider()
			des3.Mode = CipherMode.CBC
		End Sub

		''' <summary>
		''' Generates a 24 byte Hex key.
		''' </summary>
		''' <returns>Generated Hex key.</returns>
		Public Shared Function GenerateKey() As String
			' Length is 24
			des3.GenerateKey()
			Return BytesToHex(des3.Key)
		End Function

		''' <summary>
		''' Generates an 8 byte Hex IV (Initialization Vector).
		''' </summary>
		''' <returns>Initialization vector.</returns>
		Public Shared Function GenerateIV() As String
			' Length = 8
			des3.GenerateIV()
			Return BytesToHex(des3.IV)
		End Function

		''' <summary>
		''' Converts a hex string to a byte array.
		''' </summary>
		''' <param name="hex">Hex string.</param>
		''' <returns>Byte array.</returns>
		Private Shared Function HexToBytes(ByVal hex As String) As Byte()
            Dim bytes(CInt(hex.Length / 2 - 1)) As Byte
            For i As Integer = 0 To CInt(hex.Length / 2 - 1)
                Dim code As String = hex.Substring(i * 2, 2)
                bytes(i) = Byte.Parse(code, System.Globalization.NumberStyles.HexNumber)
            Next i
			Return bytes
		End Function

		''' <summary>
		''' Converts a byte array to a hex string.
		''' </summary>
		''' <param name="bytes">Byte array.</param>
		''' <returns>Converted hex string</returns>
		Private Shared Function BytesToHex(ByVal bytes() As Byte) As String
			Dim hex As New StringBuilder()
			For i As Integer = 0 To bytes.Length - 1
				hex.AppendFormat("{0:X2}", bytes(i))
			Next i
			Return hex.ToString()
		End Function

		''' <summary>
		''' Encrypts a memory string (i.e. variable).
		''' </summary>
		''' <param name="data">String to be encrypted.</param>
		''' <param name="key">Encryption key.</param>
		''' <param name="iv">Encryption initialization vector.</param>
		''' <returns>Encrypted string.</returns>
		Public Shared Function Encrypt(ByVal data As String, ByVal key As String, ByVal iv As String) As String
			Dim bdata() As Byte = Encoding.ASCII.GetBytes(data)
			Dim bkey() As Byte = HexToBytes(key)
			Dim biv() As Byte = HexToBytes(iv)

			Dim stream As New MemoryStream()
			Dim encStream As New CryptoStream(stream, des3.CreateEncryptor(bkey,biv), CryptoStreamMode.Write)

			encStream.Write(bdata, 0, bdata.Length)
			encStream.FlushFinalBlock()
			encStream.Close()

			Return BytesToHex(stream.ToArray())
		End Function

		''' <summary>
		''' Decrypts a memory string (i.e. variable).
		''' </summary>
		''' <param name="data">String to be decrypted.</param>
		''' <param name="key">Original encryption key.</param>
		''' <param name="iv">Original initialization vector.</param>
		''' <returns>Decrypted string.</returns>
		Public Shared Function Decrypt(ByVal data As String, ByVal key As String, ByVal iv As String) As String
			Dim bdata() As Byte = HexToBytes(data)
			Dim bkey() As Byte = HexToBytes(key)
			Dim biv() As Byte = HexToBytes(iv)

			Dim stream As New MemoryStream()
			Dim encStream As New CryptoStream(stream, des3.CreateDecryptor(bkey, biv), CryptoStreamMode.Write)

			encStream.Write(bdata, 0, bdata.Length)
			encStream.FlushFinalBlock()
			encStream.Close()

			Return Encoding.ASCII.GetString(stream.ToArray())
		End Function

		''' <summary>
		''' Standard encrypt method for Patterns in Action.
		''' Uses the predefined key and iv.
		''' </summary>
		''' <param name="data">String to be encrypted.</param>
		''' <returns>Encrypted string</returns>
		Public Shared Function ActionEncrypt(ByVal data As String) As String
			Return Encrypt(data, actionKey, actionIv)
		End Function

		''' <summary>
		''' Standard decrypt method for Patterns in Action.
		''' Uses the predefined key and iv.
		''' </summary>
		''' <param name="data">String to be decrypted.</param>
		''' <returns>Decrypted string.</returns>
		Public Shared Function ActionDecrypt(ByVal data As String) As String
			Return Decrypt(data, actionKey, actionIv)
		End Function
	End Class
End Namespace



