'© 2016-2017 Gianluca Cantone (Raw code)

'Allows instances of this class to be serialized.
<Serializable()>
Public Class TXT_Input                                                          'Class for transaction inputs.

    Public TXT_Hash As String = ""                                              'Variable for storing the hash of the transaction that this input references.
    Public Index As Integer = 0                                                 'Variable to store the index of the output this input references.
    Public Unlocking_Script As String = ""                                      'Variable to store the unlocking script required to unlock the output.

    Public Function Func_Input_Hash()                                           'Function for calculating the hash of this input.

        Return sha256(TXT_Hash & Index & Unlocking_Script)                      'Returns the hash of the variables TXT_Hash, Index and Unlocking_Script concatenated together.

    End Function

End Class