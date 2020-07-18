'© 2016-2017 Gianluca Cantone (Raw code)

'Allows instances of this class to be serialized.
<Serializable()>
Public Class TXT_Output                                                         'Class for transaction outputs.

    Public Value As Integer = 0                                                 'Variable to store the value of this output.
    Public Locking_Script As String = ""                                        'Variable to store the locking script of this output.

    Public Function Func_Output_Hash()                                          'Function for calculating the hash of this output.

        Return sha256(Value & Locking_Script)                                   'Returns the hash of the variables Value and Locking_Script concatenated together.

    End Function

End Class