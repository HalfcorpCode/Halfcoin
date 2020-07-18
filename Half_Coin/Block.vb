'© 2016-2017 Gianluca Cantone (Raw code)

'Allows instances of this class to be serialized.
<Serializable()>
Public Class Block                                                              'Class for storing transaction and metadata in a structure called a block.

    Public Version As String = ""                                               'Variable to store the version of the software used to create this block.
    Public Previous_Hash As String = ""                                         'Variable to store the hash of the previous block.
    Public Merkle_Root As String = ""                                           'Variable to store the merkle root of all the transactions contained in the block.
    Public Time As String = ""                                                  'Variable to store the Unix timestamp when this block was created.
    Public Difficulty As Integer = 0                                            'Variable to store the difficulty of the mining process when mining this block.
    Public Nonce As Integer = 0                                                 'Variable to store the nonce value used to create a valid block header hash of this block.
    Public Transactions As New List(Of Transaction)                             'Object variable list to store all of the transactions in this block.

    Public Function Func_Header_Hash()                                          'Subroutine for calculating the block header hash of this block.

        Return sha256(Version & Previous_Hash & Merkle_Root & Time & Difficulty & Nonce) 'Concatenates the properties of this block and hashes the result then returns it.

    End Function

End Class