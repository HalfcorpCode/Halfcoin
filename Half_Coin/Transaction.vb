'© 2016-2017 Gianluca Cantone (Raw code)

'Allows instances of this class to be serialized.
<Serializable()>
Public Class Transaction                                                        'Class for transactions.

    Public Version As String = ""                                               'Variable to store the version of the software used to create this transaction.
    Public Inputs As New List(Of TXT_Input)                                     'Object variable list to store all of the inputs of this transaction.
    Public Outputs As New List(Of TXT_Output)                                   'Object variable list to store all of the outputs of this transaction.

    Public Function Func_Transaction_Hash()                                     'Function for calculating the hash of this transaction.

        Dim Input_String As String = ""                                         'Variable to store the string of input hashes.

        For i = 0 To Inputs.Count - 1                                           'For loop from i equals 0 to the number of items in the Inputs list minus 1.

            Input_String = Input_String & Inputs(i).Func_Input_Hash()           'The variable Input_String equals itself concatenated with the result of the Func_Input_Hash method of the item at index i of the Inputs list.

        Next i

        Dim Output_String As String = ""                                        'Variable to store the string of output hashes.

        For i = 0 To Outputs.Count - 1                                          'For loop from i equals 0 to the number of items in the Outputs list minus 1.

            Output_String = Output_String & Outputs(i).Func_Output_Hash()       'The variable Output_String equals itself concatenated with the result of the Func_Output_Hash method of the item at index i of the Outputs list.

        Next i

        Return sha256(Version & Input_String & Output_String)                   'Returns the hash of the Version, Input_String and Output_String variable concatenated together.

    End Function

End Class