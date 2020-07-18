'© 2016-2017 Gianluca Cantone (Raw code)

Module Hash                                                                           'Module to contain hash algorithm used across all forms and classes.

    Public Function sha256(ByVal content As String) As String                         'Function for hashing a string using the SHA256 hash algorithm.

        Dim Hash_Object As New Security.Cryptography.SHA256CryptoServiceProvider      'Object variable for the SHA256 hash algorithm to work with.
        Dim Hash_Byte_String() As Byte = System.Text.Encoding.ASCII.GetBytes(content) 'Converts the Content variable into unicode bytes and are stored in an array.
        Hash_Byte_String = Hash_Object.ComputeHash(Hash_Byte_String)                  'Finds the SHA256 hash of the array of bytes.
        Dim SHA_Result As String = ""                                                 'Variable to store result of function.

        For Each Bit As Byte In Hash_Byte_String                                      'For each of the byte values stored in the variable Hash_Byte_String.

            SHA_Result &= Bit.ToString("x2")                                          'Appends each of the bytes to the variable result and converts them into a numerical string .

        Next

        Return SHA_Result                                                             'Returns the SHA_Result variable.

    End Function

End Module