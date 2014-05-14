Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Security.Cryptography
Imports System.Reflection

Namespace DoFactory.HeadFirst.Decorator.IO
	' InputTest test application

	Friend Class InputTest
        Shared Sub Main()

            ' Get fully qualified file names
            Dim thePath As String = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()(0).FullyQualifiedName)
            thePath = thePath.Substring(0, thePath.IndexOf("\bin") + 1)

            Dim inFile As String = thePath & "MyInFile.txt"
            Dim outFile As String = thePath & "MyOutFile.txt"
            Dim encFile As String = thePath & "MyEncFile.txt"

            Dim fin As New FileStream(inFile, FileMode.Open, FileAccess.Read)
            Dim fout As New FileStream(outFile, FileMode.OpenOrCreate, FileAccess.Write)

            ' Clear output file
            fout.SetLength(0)

            'Create variables to help with read and write.
            Dim rdlen As Integer = 0
            Dim totlen As Long = fin.Length
            Dim bin(99) As Byte
            Dim len As Integer

            'Read from the input file, then write directly output file.
            Do While rdlen < totlen
                len = fin.Read(bin, 0, 100)
                fout.Write(bin, 0, len)
                rdlen += len
            Loop
            fout.Close()
            fin.Close()

            Console.WriteLine("Created unencrypted MyOutFile.txt")

            ' -- Now use CryptoStream as Decorator --

            fin = New FileStream(inFile, FileMode.Open, FileAccess.Read)
            fout = New FileStream(encFile, FileMode.OpenOrCreate, FileAccess.Write)

            ' Clear output file
            fout.SetLength(0)

            ' Setup Triple DES encryption
            Dim des3 As New TripleDESCryptoServiceProvider()
            Dim key() As Byte = HexToBytes("EA81AA1D5FC1EC53E84F30AA746139EEBAFF8A9B76638895")
            Dim IV() As Byte = HexToBytes("87AF7EA221F3FFF5")

            ' CryptoStream 'decorates' output stream
            Console.WriteLine(Constants.vbLf & "Decorate output stream with CryptoStream...")
            Dim fenc As New CryptoStream(fout, des3.CreateEncryptor(key, IV), CryptoStreamMode.Write)

            ' Read from the input file, then write encrypted to the output file
            rdlen = 0
            Do While rdlen < totlen
                len = fin.Read(bin, 0, 100)
                fenc.Write(bin, 0, len)
                rdlen += len
            Loop

            fin.Close()
            fout.Close()

            Console.WriteLine("Created encrypted MyEncFile.txt")

            ' Wait for user
            Console.ReadKey()
        End Sub

		' Utility method: convert hex string to bytes

		Public Shared Function HexToBytes(ByVal hex As String) As Byte()
            Dim bytes(CInt(hex.Length / 2) - 1) As Byte

            For i As Integer = 0 To CInt(hex.Length / 2) - 1
                Dim code As String = hex.Substring(i * 2, 2)
                bytes(i) = Byte.Parse(code, System.Globalization.NumberStyles.HexNumber)
            Next i
			Return bytes
		End Function
	End Class
End Namespace