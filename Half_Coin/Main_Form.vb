'© 2016-2017 Gianluca Cantone (Raw code)

Imports System.IO                                                                                       'Namespace for reading and writing to files and data streams (Input/output).
Imports System.Net.Sockets                                                                              'Namespace for interfacing with ports, sockets and the use of TCP communication.
Imports System.Runtime.Serialization.Formatters.Binary                                                  'Namespace for creating binary formatters for data serialization.
Imports System.Text                                                                                     'Namespace for manipulating text.
Imports System.Text.RegularExpressions                                                                  'Namespace for creating and using regular expressions.
Imports System.Threading                                                                                'Namespace for creating new threads.
Imports NATUPNPLib                                                                                      'Namespace for interfacing with the UPNP section of the NAT router.
Imports upnp                                                                                            'Custom extension for creating and removing UPNP mappings.

Public Class Halfcoin                                                                                   'Main program code.

#Region "Variables"                                                                                     'Region containing code for variable declaration.

    'General Variables

    Dim Version As String = 1                                                                           'Variable to store the program version number.
    Dim Node_Directory As String = AppDomain.CurrentDomain.BaseDirectory & "\data\Node_List.dat"        'Variable to store the directory of the list of the node list.
    Dim Address_Directory As String = AppDomain.CurrentDomain.BaseDirectory & "\data\Address_Book.dat"  'Variable to store the directory of the address book.
    Dim Blockchain_Directory As String = AppDomain.CurrentDomain.BaseDirectory & "\data\Blockchain.dat" 'Variable to store the directory of the blockchain database.
    Dim UTXO_Directory As String = AppDomain.CurrentDomain.BaseDirectory & "\data\UTXO.dat"             'Variable to store the directory of the UTXO database.
    Dim Hash_Database_Directory As String = AppDomain.CurrentDomain.BaseDirectory & "\data\Hash_Database.dat" 'Variable to store the directory of the hash database.
    Dim Logged_In As Boolean = False                                                                    'Variable to indicate if the user has logged in.
    Dim Exception_Message As String = ""                                                                'Variable to store error exception messages.

    'Netcode Variables

    Dim WAN_Enabled As Boolean = False                                                                  'Variable to store if the program is working over WAN or LAN.
    Dim Parent_Node As Boolean = True                                                                   'Variable to store if the program is operating in parent or child mode.
    Dim Prefered_Connection As Integer = 3                                                              'Variable to store the prefered number of connections this node should try to create.
    Dim External_IPv4 As String = ""                                                                    'Variable to store the external IPv4 address of this node.
    Dim Max_Connections As Integer = 8                                                                  'Variable to store the maximum number of connection allowed to be made.
    Dim upnpnat As New NATUPNPLib.UPnPNAT                                                               'Object variable to store a UPNP interface to the router.
    Dim mappings As NATUPNPLib.IStaticPortMappingCollection = upnpnat.StaticPortMappingCollection       'Object variable to store a UPNP mapping.
    Dim Local_IPV4 As String = ""                                                                       'Variable to store this nodes IPv4 address.
    Dim Port As Integer = 39000                                                                         'Variable to store the port all inbound and outbound connections should use.
    Dim Local_PC_Name As String = ""                                                                    'Variable to store the local PC host name.
    Dim TCP_Client As TcpClient                                                                         'Object variable to store a tcp network client.
    Dim TCP_Listener As New TcpListener(Port)                                                           'Object variable that stores a TCP listener to listen for tcp information on a port Port.
    Dim Incoming_IPV4 As String = ""                                                                    'Variable to store the IPv4 address of incoming connection.
    Dim Network_Thread As Thread                                                                        'Object variable to store the thread for sending network messages.
    Dim Network_Queue As New Queue(Of String)                                                           'Object variable queue for storing and queueing up messages to be sent across the network.
    Dim IP_Queue As New Queue(Of String)                                                                'Object variable queue for storing and queueing up the IPv4 destination address for messages to be sent across the network.
    Dim Error_Pending As Boolean = False                                                                'Variable to store if an error message is waiting to be printed to the console.
    Dim Known_Node_List As New List(Of String)                                                          'Object variable list to store a list of node IP's known by this node.
    Dim Active_Node_List As New List(Of String)                                                         'Object variable list to store a list of active nodes IP's.
    Dim Child_Node_List As New List(Of String)                                                          'Object variable list to store a list of child nodes IP's.
    Dim Parent_IP As String = ""                                                                        'Variable to store the IP of the parent node.
    Dim Check_Count As Integer = 0                                                                      'Variable to store the number of nodes checked for connections.
    Dim Success_Node_Count As Integer = 0                                                               'Variable to store the number of active node connections.
    Dim Node_State As Integer = 0                                                                       'Variable to store the index of the most recent node used to seed IP's.
    Dim Height_State As Integer = 0                                                                     'Variable to store how many IP's have been asked for their height.
    Dim Heights_List As New List(Of Integer)                                                            'Object variable list to store the blockchain heights of active nodes.
    Dim Heights_Addresses As New List(Of String)                                                        'Object variable list to store the IP addresses associated with each item in the Heights_List.
    Dim Height_Target_IPV4 As String = ""                                                               'Variable to store the index of the item in the Heights_Addresses list that coresponds to the highest height.
    Dim Blocks_Remaining As Integer = 0                                                                 'Variable to store the number of blocks that this node needs to download before its blockchain is up to date.
    Dim Block_Target As Integer = 0                                                                     'Variable to store the height of the next block that needs to be downloaded from the network.
    Dim Node_Online As Boolean = False                                                                  'Variable to store the state of the program, either false( standby/syncing) or true (online and ready).
    
    'Key Generation Variables

    Dim Private_Key_Hide As Boolean = False                                                             'Variable to store if the setting to hide the private key is on or off.
    Dim Login_Word_Hide As Boolean = False                                                              'Variable to store if the setting to hide the login word is on or off.
    Dim Private_Key As String = ""                                                                      'Variable to store the users private key.
    Dim Public_Key As String = ""                                                                       'Variable to store the users public key.
    Dim Login_Word As String = ""                                                                       'Variable to store the users login word.
    Dim Address As String = ""                                                                          'Variable to store newly generated send to addresses for the users account.
    Dim New_Address As String = ""                                                                      'Variable to store a new contact to put into the address book.
    Dim Nonce As String = ""                                                                            'Variable to store the nonce value used for address generation.
    Dim Address_Salt As String                                                                          'Variable to store the salt that is used to generate unique addresses from the public key.

    'Transaction Variables

    Dim Recipient_Public_Key As String = ""                                                             'Variable to store the recipient public key entered by the user.
    Dim Recipient_Address As String = ""                                                                'Variable to store the recipient address entered by the user.
    Dim Transaction_Value As Integer = 0                                                                'Variable to store transaction value entered by the user.
    Dim Transaction_Fee As Integer = 0                                                                  'Variable to store transaction fee entered by the user.
    Dim New_Transaction As New Transaction                                                              'Object variable transaction to store a new transaction.
    Dim Memory_Pool As New List(Of Transaction)                                                         'Object variable list to store transactions coming in from the network.
    Dim Validation_Error As String = ""                                                                 'Variable to store information regarding errors in validation.
    Dim Validation_Message As String = ""                                                               'Variable to store extra information regarding errors in validation.
    Dim Inbound_Block As New Block                                                                      'Object variable block to store the block downloaded from the network.

    'Blockchain Variables

    Dim Bootstrap_Height As Integer = 0                                                                 'Variable to store the height of this nodes blockchain at bootstrap. 
    Dim Balance As Integer = 0                                                                          'Variable to store the users balance.
    Dim Unconfirmed_Balance As Integer = 0                                                              'Variable to store the value of the users pending transactions.
    Dim UTXO_Hash_List As New List(Of String)                                                           'Object variable list to store the hash of UTXO transactions for the UTXO database.
    Dim UTXO_Index_List As New List(Of Integer)                                                         'Object variable list to store the index of UTXO transactions for the UTXO database.
    Dim UTXO_Value_List As New List(Of Integer)                                                         'Object variable list to store the value of UTXO transactions for the UTXO database.
    Dim UTXO_Script_List As New List(Of String)                                                         'Object variable list to store the locking script of UTXO transactions for the UTXO database.
    Dim Hash_Database As New List(Of String)                                                            'Object variable list to store the block hash database.

    'Mining Variables

    Dim Mine_Difficulty As Integer = 5                                                                  'Variable to store the difficulty of the mining process. (Initilized as 5 to prevent instability from mining at too easy difficulty)
    Dim Block_Reward As Integer = 0                                                                     'Variable to store the reward given when a block is successfuly mined.
    Dim Fee_Reward As Integer = 0                                                                       'Variable to store the reward given by transaction fees when a block is successfuly mined.
    Dim Total_Reward As Integer = 0                                                                     'Variable to store the total reward given when a block is successfuly mined.
    Dim Mine_Nonce(8) As ULong                                                                          'Array variable to store the incremented nonce values used to generate new hashes for the mining process.
    Dim Winning_Nonce As Integer = 0                                                                    'Variable to store the index of the successful hash in the Mine_Nonce array
    Dim Mine_Zeros As String = ""                                                                       'Variable to store the leading zeros that will be compared with a block header hash to check to see if it is a successful hash.
    Dim Header_Data As String = ""                                                                      'Variable to store the block header data that will be hashed with the nonce variable to generate block header hashes.
    Dim Hash_Attempt(8) As String                                                                       'Array variable to store the calculated block header hashes ready for comparrison with the mine zeros variable.
    Dim Tick As Integer = 0                                                                             'Variable to store the elapsed mnining time in seconds.
    Dim Candidate_Block As New Block                                                                    'Object variable block that will be mined by the mining process.
    Dim Core_Count As Integer = System.Environment.ProcessorCount                                       'Variable to stor the number of logical processor cores avaliable on the local PC.
    Dim Core_Select As Integer = 0                                                                      'Variable to store the number of logical processor cores selected for mining.
    Dim Abort As Boolean = False                                                                        'Variable to store the state of the mining process.
    Dim Thread_Worker01 As New System.ComponentModel.BackgroundWorker                                   'Object variable background worker for running the mining subroutine on a seperate thread.
    Dim Thread_Worker02 As New System.ComponentModel.BackgroundWorker                                   'Object variable background worker for running the mining subroutine on a seperate thread.
    Dim Thread_Worker03 As New System.ComponentModel.BackgroundWorker                                   'Object variable background worker for running the mining subroutine on a seperate thread.
    Dim Thread_Worker04 As New System.ComponentModel.BackgroundWorker                                   'Object variable background worker for running the mining subroutine on a seperate thread.
    Dim Thread_Worker05 As New System.ComponentModel.BackgroundWorker                                   'Object variable background worker for running the mining subroutine on a seperate thread.
    Dim Thread_Worker06 As New System.ComponentModel.BackgroundWorker                                   'Object variable background worker for running the mining subroutine on a seperate thread.
    Dim Thread_Worker07 As New System.ComponentModel.BackgroundWorker                                   'Object variable background worker for running the mining subroutine on a seperate thread.
    Dim Thread_Worker08 As New System.ComponentModel.BackgroundWorker                                   'Object variable background worker for running the mining subroutine on a seperate thread.
    Dim Nonce_Step As Integer = 0                                                                       'Variable to store the amount the nonce value should increase by.
    Dim Thread_Shutdown_Count As Integer = 0                                                            'Variable to keep track of how many threads have shutdown once mining is complete.
    Dim Block_Missed As Boolean = False                                                                 'Variable to indicate if another node found a block before you.
    Dim Single_Mine As Boolean = False                                                                  'Variable to indicate weather the mining process should stop after 1 successful mine.
    Dim Hash_Rate As Integer = 0                                                                        'Variable to store the users hash rate.

    'Explorer Variables

    Dim Explore_Block As Block                                                                          'Object variable block to store the block you are exploring
    Dim Transaction_Index As Integer = 0                                                                'Variable to store the index of the transaction you are exploring
    Dim Input_Index As Integer = 0                                                                      'Variable to store the index of the input you are exploring.
    Dim Output_Index As Integer = 0                                                                     'Variable to store the index of the output you are exploring.

    'Large Variables

    Dim Genesis_Hash As String = "000002d43a81d9576f6d69ffc9612ab3cf7493e50c20431cec301a8b91c59cf0"     'Variable to store the header hash of the genesis block.
    Dim Genesis_Block As String = "AAEAAAD/////AQAAAAAAAAAMAgAAAEBIYWxmX0NvaW4sIFZlcnNpb249MS4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1udWxsBQEAAAAPSGFsZl9Db2luLkJsb2NrBwAAAAdWZXJzaW9uDV" _
                                  & "ByZXZpb3VzX0hhc2gLTWVya2xlX1Jvb3QEVGltZQpEaWZmaWN1bHR5BU5vbmNlDFRyYW5zYWN0aW9ucwEBAQEAAAMICHxTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW0hhbGZfQ29pbi5UcmFuc2" _
                                  & "FjdGlvbiwgSGFsZl9Db2luLCBWZXJzaW9uPTEuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAYDAAAAA1ZfMQYEAAAAQDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD" _
                                  & "AwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAGBQAAAEBmZDA5NDVhNzQxNmNlZTE2NzI3YTc0NWIwMjkxMDcwN2M1MmQxZDRkMzhhY2I3YjdhMTgxM2NmM2E5YzU3YjYyBgYAAAAKMTQ4MjU5OD" _
                                  & "U0NgUAAAAM3RAACQcAAAAEBwAAAHxTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW0hhbGZfQ29pbi5UcmFuc2FjdGlvbiwgSGFsZl9Db2luLCBWZXJzaW9uPTEuMC4wLjAsIEN1bHR1cmU9bmV1dH" _
                                  & "JhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAwAAAAZfaXRlbXMFX3NpemUIX3ZlcnNpb24EAAAXSGFsZl9Db2luLlRyYW5zYWN0aW9uW10CAAAACAgJCAAAAAEAAAABAAAABwgAAAAAAQAAAAQAAAAEFUhhbGZfQ2" _
                                  & "9pbi5UcmFuc2FjdGlvbgIAAAAJCQAAAA0DBQkAAAAVSGFsZl9Db2luLlRyYW5zYWN0aW9uAwAAAAdWZXJzaW9uBklucHV0cwdPdXRwdXRzAQMDelN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbSG" _
                                  & "FsZl9Db2luLlRYVF9JbnB1dCwgSGFsZl9Db2luLCBWZXJzaW9uPTEuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1de1N5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMV" _
                                  & "tbSGFsZl9Db2luLlRYVF9PdXRwdXQsIEhhbGZfQ29pbiwgVmVyc2lvbj0xLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGxdXQIAAAAJAwAAAAkLAAAACQwAAAAECwAAAHpTeXN0ZW" _
                                  & "0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW0hhbGZfQ29pbi5UWFRfSW5wdXQsIEhhbGZfQ29pbiwgVmVyc2lvbj0xLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGxdXQMAAA" _
                                  & "AGX2l0ZW1zBV9zaXplCF92ZXJzaW9uBAAAFUhhbGZfQ29pbi5UWFRfSW5wdXRbXQIAAAAICAkNAAAAAQAAAAEAAAAEDAAAAHtTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW0hhbGZfQ29pbi5UWF" _
                                  & "RfT3V0cHV0LCBIYWxmX0NvaW4sIFZlcnNpb249MS4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1udWxsXV0DAAAABl9pdGVtcwVfc2l6ZQhfdmVyc2lvbgQAABZIYWxmX0NvaW4uVFhUX0" _
                                  & "91dHB1dFtdAgAAAAgICQ4AAAABAAAAAQAAAAcNAAAAAAEAAAAEAAAABBNIYWxmX0NvaW4uVFhUX0lucHV0AgAAAAkPAAAADQMHDgAAAAABAAAABAAAAAQUSGFsZl9Db2luLlRYVF9PdXRwdXQCAAAACRAAAAANAw" _
                                  & "UPAAAAE0hhbGZfQ29pbi5UWFRfSW5wdXQDAAAACFRYVF9IYXNoBUluZGV4EFVubG9ja2luZ19TY3JpcHQBAAEIAgAAAAYRAAAACENvaW5iYXNlAAAAAAYSAAAAQiJUaGUgdHJ1ZSBzaWduIG9mIGludGVsbGlnZW" _
                                  & "5jZSBpcyBub3Qga25vd2xlZGdlLCBidXQgaW1hZ2luYXRpb24uIgUQAAAAFEhhbGZfQ29pbi5UWFRfT3V0cHV0AgAAAAVWYWx1ZQ5Mb2NraW5nX1NjcmlwdAABCAIAAAAAAAEABhMAAACaAWE1NmQxMzM5NjkzM2" _
                                  & "E3N2YzZTYzNmMyNmQ5MzM2NDMyOGYzNzdiYmE1ZmMyNmQyODg0OGY2NjI4OTViYjA3M2IsODZlMzg4NzEzY2YyODc4ZDJjNWJkYWM3MDMxYTc4MTQwODVhM2RhMDgzNzNlYTczZGU4ZDZhMzZhMTY5MmZkMzE0OD" _
                                  & "I1OTg1NDYsT3BfQ2hlY2tfU2FsdCwL " & vbNewLine                       'Variable to store the genesis block data.

#End Region

#Region "Bootstrap"                                                                         'Region containing code that executes when the program bootstraps or shuts down.

    Private Sub Halfcoin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load 'Subroutine that executes first to initilize the program and load local data from files.

        Sub_Console_Print("Starting Halfcoin client version " & Version, "Main", "Black")   'Sends a message to the console subroutine saying the program is starting.

        lblVersion.Text = lblVersion.Text & Version                                         'Sets the lblVersion label to itself with the Version variable appened to it.

        Dim Current_Line As String = ""                                                     'Variable to store the current line being read from the selected file.

        Sub_Console_Print("Checking for address book file...", "Main", "Black")             'Sends a message to the console subroutine saying the program is checking for the address book file.

        If Not System.IO.File.Exists(Address_Directory) Then                                'Checks to see if the address book file does not exist.

            Sub_Console_Print("File not found, creating new one", "Main", "Red")            'Sends a message to the console subroutine saying the file has not been found.

            My.Computer.FileSystem.WriteAllText(Address_Directory, "", False)               'If it doesnt, one is created with the nothing written into it.

        Else

            Sub_Console_Print("File found, loading contents", "Main", "Green")              'Sends a message to the console subroutine saying the file has been found.

        End If

        Dim Object_Address_Reader As New System.IO.StreamReader(Address_Directory)          'Opens a stream to read data from the directory stored in the variable Address_Directory.

        Do While Object_Address_Reader.Peek() <> -1                                         'Do while loop whilst there is still data in the file to be read.

            Current_Line = Object_Address_Reader.ReadLine()                                 'Sets the Current_Line variable equal to the line being read from the file.
            lstAdr.Items.Add(Current_Line)                                                  'Adds the variable Current_Line to the lstAdr list box.

        Loop

        Object_Address_Reader.Close()                                                       'Closes the data stream to the address book file.

        Sub_Console_Print("Checking for node list...", "Main", "Black")                     'Sends a message to the console subroutine saying the program is checking for the node list file.

        If Not System.IO.File.Exists(Node_Directory) Then                                   'Checks to see if the node file does not exist.

            Sub_Console_Print("File not found, creating new one", "Main", "Red")            'Sends a message to the console subroutine saying the file has not been found.

            My.Computer.FileSystem.WriteAllText(Node_Directory, "", False)                  'If it doesnt, one is created with nothing written into it.

        Else

            Sub_Console_Print("File found, loading contents", "Main", "Green")              'Sends a message to the console subroutine saying the file has been found.

        End If

        Dim Node_Reader As New System.IO.StreamReader(Node_Directory)                       'Opens a stream to read data from the directory stored in the variable Node_Directory.

        Do While Node_Reader.Peek() <> -1                                                   'Do while loop whilst there is still data in the file to be read.

            Current_Line = Node_Reader.ReadLine()                                           'Sets the Current_Line variable equal to the line being read from the file.
            Known_Node_List.Add(Current_Line)                                               'Adds the Current_Line variable to the Known_Nodes_List.

        Loop

        Node_Reader.Close()                                                                 'Closes the data stream to the node list file.

        Sub_Refresh_Node_List()                                                             'Executes the Sub_Refresh_Node_List subroutine.

        Sub_Console_Print("Checking for blockchain database file...", "Main", "Black")      'Sends a message to the console subroutine saying the program is checking for the blockchain file.

        If Not System.IO.File.Exists(Blockchain_Directory) Then                             'Checks to see if the blockchain file does not exist.

            Sub_Console_Print("File not found, creating new one", "Main", "Red")            'Sends a message to the console subroutine saying the file has not been found.

            My.Computer.FileSystem.WriteAllText(Blockchain_Directory, Genesis_Block, False) 'If it doesnt, one is created and the Genesis_Block variable is written to it.

        Else

            Sub_Console_Print("File found, loading contents", "Main", "Green")              'Sends a message to the console subroutine saying the file has been found.

        End If

        Sub_Console_Print("Checking for UTXO database file...", "Main", "Black")            'Sends a message to the console subroutine saying the program is checking for the UTXO database file.

        If Not System.IO.File.Exists(UTXO_Directory) Then                                   'Checks to see if the UTXO database file does not exist.

            Sub_Console_Print("File not found, creating new one", "Main", "Red")            'Sends a message to the console subroutine saying the file has not been found.

            My.Computer.FileSystem.WriteAllText(UTXO_Directory, "", False)                  'If it doesnt, one is created containing nothing.
            Sub_UTXO(Func_Calculate_Height)                                                 'Executes the Sub_UTXO subroutine passing the local blockchain height as the parameter.

        Else

            Sub_Console_Print("File found, loading contents", "Main", "Green")              'Sends a message to the console subroutine saying the file has been found.

        End If

        Sub_Console_Print("Checking for hash database file...", "Main", "Black")            'Sends a message to the console subroutine saying the program is checking for the hash database file.

        If Not System.IO.File.Exists(Hash_Database_Directory) Then                          'Checks to see if the hash database file does not exist.

            Sub_Console_Print("File not found, creating new one", "Main", "Red")            'Sends a message to the console subroutine saying the file has not been found.

            My.Computer.FileSystem.WriteAllText(Hash_Database_Directory, Genesis_Hash, False) 'If it doesnt, one is created and the Genesis_Hash variable is written to it.

        Else

            Sub_Console_Print("File found, loading contents", "Main", "Green")              'Sends a message to the console subroutine saying the file has been found.

        End If

        Dim Hash_Database_Reader As New System.IO.StreamReader(Hash_Database_Directory)     'Opens a stream to read data from the directory stored in the variable Address_Directory.

        Do While Hash_Database_Reader.Peek() <> -1                                          'Do while loop whilst there is still data in the file to be read.

            Current_Line = Hash_Database_Reader.ReadLine()                                  'Sets the Current_Line variable equal to the line being read from the file.
            Hash_Database.Add(Current_Line)                                                 'Adds the variable Current_Line to the Hash_Database list.

        Loop

        Hash_Database_Reader.Close()                                                        'Closes the data stream to the hash database file.

        Sub_UTXO_Load()                                                                     'Executes the Sub_UTXO_Load subroutine.

        Local_IPV4 = Func_Local_IP()                                                        'Sets the Local_IPv4 variable equal to the result of the Func_Local_IP() function.
        External_IPv4 = Func_External_IP()                                                  'Sets the External_IPv4 variable equal to the result of the Func_External_IP() function.

        TCPTimer.Start()                                                                    'Starts the TCP timer.
        TCP_Listener.Start()                                                                'Starts the TCP_Listener object.

        Sub_Console_Print("The local IPV4 address of this node is: " & Local_IPV4, "Main", "Black") 'Sends a message to the console subroutine displaying the local IP address of this node.
        Sub_Console_Print("The external IPV4 address of this node is: " & External_IPv4, "Main", "Black") 'Sends a message to the console subroutine displaying the external IP address of this node.

        Bootstrap_Height = Func_Calculate_Height()                                          'Sets the Bootstrap_Height variable equal to the result of the Func_Calculate_Height() function.

        txtLocalHeight.Text = Func_Calculate_Height()                                       'Sets the txtLocalHeight text box to the local blockchain height.
        txtBlockSize.Text = FileLen(Blockchain_Directory)                                   'Sets the txtBlockSize text box to the file size in bytes of the blockchain.
        txtUTXONum.Text = System.IO.File.ReadAllLines(UTXO_Directory).Length                'Sets the txtUTXONum text box to the number of lines in the UTXO database.
        txtUTXOSize.Text = FileLen(UTXO_Directory)                                          'Sets the txtUTXOSize text box to the file size in bytes of the UTXO database.

        For i = 1 To Core_Count                                                             'For loop from i equals 1 to the Core_Count variable.

            cmbCores.Items.Add(i)                                                           'Adds i to the cmbCores combo box.

        Next i

        AddHandler Thread_Worker01.DoWork, AddressOf Sub_Mine01                             'Adds a handler to tell the Thread_Worker01 background worker what subroutine to execute.
        AddHandler Thread_Worker01.RunWorkerCompleted, AddressOf Sub_Mine_Complete          'Adds a handler to tell the Thread_Worker01 background worker what subroutine to run when its complete.
        Thread_Worker01.WorkerSupportsCancellation = True                                   'Sets the WorkerSupportsCancellation property of the Thread_Worker01 background worker to true.

        AddHandler Thread_Worker02.DoWork, AddressOf Sub_Mine02                             'Adds a handler to tell the Thread_Worker02 background worker what subroutine to execute.
        AddHandler Thread_Worker02.RunWorkerCompleted, AddressOf Sub_Mine_Complete          'Adds a handler to tell the Thread_Worker02 background worker what subroutine to run when its complete.
        Thread_Worker02.WorkerSupportsCancellation = True                                   'Sets the WorkerSupportsCancellation property of the Thread_Worker02 background worker to true.

        AddHandler Thread_Worker03.DoWork, AddressOf Sub_Mine03                             'Adds a handler to tell the Thread_Worker03 background worker what subroutine to execute.
        AddHandler Thread_Worker03.RunWorkerCompleted, AddressOf Sub_Mine_Complete          'Adds a handler to tell the Thread_Worker03 background worker what subroutine to run when its complete.
        Thread_Worker03.WorkerSupportsCancellation = True                                   'Sets the WorkerSupportsCancellation property of the Thread_Worker03 background worker to true.

        AddHandler Thread_Worker04.DoWork, AddressOf Sub_Mine04                             'Adds a handler to tell the Thread_Worker04 background worker what subroutine to execute.
        AddHandler Thread_Worker04.RunWorkerCompleted, AddressOf Sub_Mine_Complete          'Adds a handler to tell the Thread_Worker04 background worker what subroutine to run when its complete.
        Thread_Worker04.WorkerSupportsCancellation = True                                   'Sets the WorkerSupportsCancellation property of the Thread_Worker04 background worker to true.

        AddHandler Thread_Worker05.DoWork, AddressOf Sub_Mine05                             'Adds a handler to tell the Thread_Worker05 background worker what subroutine to execute.
        AddHandler Thread_Worker05.RunWorkerCompleted, AddressOf Sub_Mine_Complete          'Adds a handler to tell the Thread_Worker05 background worker what subroutine to run when its complete.
        Thread_Worker05.WorkerSupportsCancellation = True                                   'Sets the WorkerSupportsCancellation property of the Thread_Worker05 background worker to true.

        AddHandler Thread_Worker06.DoWork, AddressOf Sub_Mine06                             'Adds a handler to tell the Thread_Worker06 background worker what subroutine to execute.
        AddHandler Thread_Worker06.RunWorkerCompleted, AddressOf Sub_Mine_Complete          'Adds a handler to tell the Thread_Worker06 background worker what subroutine to run when its complete.
        Thread_Worker06.WorkerSupportsCancellation = True                                   'Sets the WorkerSupportsCancellation property of the Thread_Worker06 background worker to true.

        AddHandler Thread_Worker07.DoWork, AddressOf Sub_Mine07                             'Adds a handler to tell the Thread_Worker07 background worker what subroutine to execute.
        AddHandler Thread_Worker07.RunWorkerCompleted, AddressOf Sub_Mine_Complete          'Adds a handler to tell the Thread_Worker07 background worker what subroutine to run when its complete.
        Thread_Worker07.WorkerSupportsCancellation = True                                   'Sets the WorkerSupportsCancellation property of the Thread_Worker07 background worker to true.

        AddHandler Thread_Worker08.DoWork, AddressOf Sub_Mine08                             'Adds a handler to tell the Thread_Worker08 background worker what subroutine to execute.
        AddHandler Thread_Worker08.RunWorkerCompleted, AddressOf Sub_Mine_Complete          'Adds a handler to tell the Thread_Worker08 background worker what subroutine to run when its complete.
        Thread_Worker08.WorkerSupportsCancellation = True                                   'Sets the WorkerSupportsCancellation property of the Thread_Worker08 background worker to true.

        Sub_Console_Print("Starting network thread...", "Main", "Black")                    'Sends a message to the console subroutine saying the network thread is starting.

        Network_Thread = New Thread(AddressOf Network_Thread_Send)                          'Sets up the Network_Thread as a new thread placing the Network_Thread_Send subroutine on it.
        Network_Thread.IsBackground = True                                                  'Makes the Network_Thread a background thread.
        Network_Thread.Start()                                                              'Starts the Network_Thread.

        Sub_Console_Print("Network thread online", "Main", "Green")                         'Sends a message to the console subroutine saying the network thread is online.
        Sub_Console_Print("Bootstrap complete", "Main", "Green")                            'Sends a message to the console subroutine saying the bootstrap process is complete.

    End Sub

    Private Sub _FormClosing() Handles Me.FormClosing                                       'Subroutine that executes when the program closes.

        For i = 0 To Active_Node_List.Count - 1                                             'For loop from i equals 0 to the number of items in the Active_Node_List minus 1.

            Sub_P2P_Shutdown(Active_Node_List(i))                                           'Executes the Sub_P2P_Shutdown subroutine passing the item at index i of the Active_Node_List as the parameter.

        Next i

        TCP_Listener.Stop()                                                                 'Stops TCP_Listener listening.
        Splash.Close()                                                                      'Closes the splash screen form.

    End Sub

#End Region

#Region "Synchronization"                                                                                   'Region containing code for starting up the program, connecting to the network and syncing.

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click 'Subroutine for starting the connection and synchronization process.

        Sub_Console_Print("Starting connection process", "Main", "Black")                                   'Sends a message to the console subroutine saying the connection process has started.

        If cmbNetShip.SelectedItem = "Parent" Then                                                          'If the currently selected item in the cmbNetShip combo box is "Parent".

            Parent_Node = True                                                                              'Sets the Parent_Node variable to true.

        ElseIf cmbNetShip.SelectedItem = "Child" Then                                                       'Else if the currently selected item in the cmbNetShip combo box is "Child".

            Parent_Node = False                                                                             'Sets the Parent_Node variable to false.
            Parent_IP = txtParentIP.Text                                                                    'Sets the Parent_IP variable equal to the txtParentIP text box.

            If Func_Validate(Parent_IP, "IP") <> True Then                                                  'If the result of the Func_Validate subroutine when passing the Parent_IP variable and "IP" as parameters does not equal true.

                Exit Sub                                                                                    'Exits the subroutine.

            End If

            Prefered_Connection = 1                                                                         'Sets the Prefered_Connection variable equal to 1.
            cmbNetType.Text = "LAN"                                                                         'Sets the cmbNetType combo box to the string "LAN".

            My.Computer.FileSystem.WriteAllText(Node_Directory, "", False)                                  'Writes nothing to the node list file.

            Known_Node_List.Clear()                                                                         'Clears the Known_Node_List.
            Known_Node_List.Add(Parent_IP)                                                                  'Adds the Parent_IP variable to the Known_Node_List.

        Else

            MsgBox("Please select either Parent or Child from the drop down box in the settings menu.")     'Displays a message box asking the user to make a selection from the parent or child box.
            Exit Sub                                                                                        'Exits the subroutine.

        End If

        Sub_Console_Print("Parent mode: " & Parent_Node, "Main", "Black")                                   'Sends a message to the console subroutine displaying the Parent node.

        Check_Count = 0                                                                                     'Sets the Check_Count variable to 0.
        Success_Node_Count = 0                                                                              'Sets the Success_Node_Count variable to 0.
        Node_State = 0                                                                                      'Sets the Node_State variable to 0.
        Height_State = 0                                                                                    'Sets the Height_State variable equal to 0.
        Active_Node_List.Clear()                                                                            'Clears the Active_Node_List.
        Heights_List.Clear()                                                                                'Cleras the Heights_List.
        Heights_Addresses.Clear()                                                                           'Clears the Heights_Addresses list.
        Height_Target_IPV4 = ""                                                                             'Sets the Height_Target_IPV4 variable equal to nothing.

        Sub_Console_Print("Connecting to " & Prefered_Connection & " nodes", "Main", "Black")               'Sends a message to the console subroutine displaying the number of prefered connections.


        If cmbNetType.SelectedItem = "LAN" Then                                                             'If the currently selected item in the cmbNetType combo box is "LAN".

            WAN_Enabled = False                                                                             'Sets the WAN_Enabled variable to false.

        ElseIf cmbNetType.SelectedItem = "WAN" Then                                                         'Else if the currently selected item in the cmbNetType combo box is "WAN".

            WAN_Enabled = True                                                                              'Sets the WAN_Enabled variable to true.

            If External_IPv4 = "ERROR" Then                                                                 'If the External_IPv4 variable equals "ERROR".

                MsgBox("This node cannot connect to the network as its external IP could not be found or is invalid, please check your internet connection or add your external IP manually.") 'Displays a message box saying the connection could not be made as there is an error with the external IP.
                Sub_Console_Print("Error while connecting to connect to the network: Invalid external IPv4", "Main", "red") 'Sends a message to the console subroutine saying the connection could not be made as there is an error with the external IP.
                Exit Sub                                                                                    'Exits the subroutine.

            End If

            If cmbUPNP.SelectedItem = "Enabled" Then                                                        'If the currently selected item in the cmbUPNP combo box is "Enabled".

                Sub_Console_Print("Establishing UPNP port mapping...", "Main", "black")                     'Sends a message to the console subroutine saying a UPNP mapping is being made.

                Try                                                                                         'Starts a try catch block.

                    mappings.Add(Port, "TCP", Port, Local_IPV4, True, "Halfcoin default mapping")           'Executes the Add method of the mappings object passing many parameters.
                    Sub_Console_Print("UPNP port mapping successful", "Main", "Green")                      'Sends a message to the console subroutine saying the UPNP mapping was added successfully.

                Catch EX As Exception                                                                       'Catches EX as an exception.

                    MsgBox("This node could not connect to the network as a UPNP mapping could not be made or there is an existing mapping conflict. If your router does not support UPNP, please port forward port " & Port & " or find an alternative means of NAT transversal.") 'Displays a message box saying the UPNP mapping could not be added.
                    Sub_Console_Print("Error while creating UPNP mapping: " & EX.ToString, "Main", "red")   'Sends a message to the console subroutine saying there was an error while creating the UPNP mapping.
                    Exit Sub                                                                                'Exits the subroutine.

                End Try

            ElseIf cmbUPNP.SelectedItem = "Disabled" Then                                                   'Else if the currently selected item in the cmbUPNP combo box is "Disabled".

                Sub_Console_Print("UPNP is disabled, attempting to use port " & Port, "Main", "black")      'Sends a message to the console subroutine saying a the program is using a port without using UPNP.

            Else

                MsgBox("Please select either Enabled or Disabled for the UPNP setting from the drop down box in the settings menu.") 'Displays a message box asking the user to select an item from the cmbUPNP combo box.
                Exit Sub                                                                                    'Exits the subroutine.

            End If

        Else

            MsgBox("Please select either LAN or WAN from the drop down box in the settings menu.")          'Displays a message box asking the user to select an item from the cmbNetType combo box.
            Exit Sub                                                                                        'Exits the subroutine.

        End If

        Sub_Console_Print("WAN mode: " & WAN_Enabled, "Main", "Black")                                      'Sends a message to the console subroutine displaying the WAN mode.

        If Known_Node_List.Count = 0 Then                                                                   'If the number of items in the Known_Node_List is equal to 0.

            Sub_Console_Print("No nodes known. Please add at least one IP's using the console", "Main", "red") 'Sends a message to the console subroutine asking the user to add at least 1 IP.
            Exit Sub                                                                                        'Exits the subroutine.

        Else

            If Parent_Node = True Then                                                                      'If the Parent_Node variable equals true.

                Sub_P2P_Version_Send(Known_Node_List(Known_Node_List.Count - 1))                            'Executes the Sub_P2P_Version_Send subroutine passing the item at index equal to the number of items in the Known_Node_List minus 1 of the Known_Node_List as the parameter.

            Else

                Sub_P2P_Child_Version_Send(Known_Node_List(Known_Node_List.Count - 1))                      'Executes the Sub_P2P_Child_Version_Send subroutine passing the item at index equal to the number of items in the Known_Node_List minus 1 of the Known_Node_List as the parameter.

            End If

        End If

    End Sub

    Public Sub Sub_Sync00()                                                                                 'Subroutine for checking the number of IP's the node knows and asking for more if needed.

        If Success_Node_Count < Prefered_Connection Then                                                    'If the Success_Node_Count variable is smaller then the Prefered_Connection variable.

            If Check_Count < Known_Node_List.Count Then                                                     'If the Check_Count variable is smaller then the number of items in the Known_Node_List.

                Sub_P2P_Version_Send(Known_Node_List(Known_Node_List.Count - 1 - Check_Count))              'Executes the Sub_P2P_Version_Send subroutine passing item at the index equal the number of items in the Known_Node_List minus 1 minus the Check_Count variable of the Known_Node_List as the parameter.

            Else

                If Active_Node_List.Count = 0 Then                                                          'If the number of items in the Active_Node_List is equal to 0.

                    Sub_Console_Print("No nodes online. Please add more IP's using the console", "Main", "red") 'Sends a message to the console subroutine asking the user to add more IP's.
                    Exit Sub                                                                                'Exits the subroutine.

                Else

                    Sub_Console_Print("Not enough active IP's, requesting more", "Main", "Black")           'Sends a message to the console subroutine saying theres not enough IP's so more will be requested.

                End If

                Sub_P2P_Get_Address(Active_Node_List(Active_Node_List.Count - 1), Prefered_Connection - Success_Node_Count) 'Executes the Sub_P2P_Get_Address subroutine passing the item at the index equal to the number of items in the Active_Node_List minus 1 of the Active_Node list and the Prefered_Connection variable minus the Success_Node_Count variable as parameters.

            End If

        ElseIf Success_Node_Count = Prefered_Connection Then                                                'Else if the Success_Node_Count variable equals the Prefered_Connection variable.

            Sub_Console_Print("Enough IP's online", "Main", "green")                                        'Sends a message to the console subroutine saying enough IP's are online.
            Sub_Sync02()                                                                                    'Executes the Sub_Sync02 subroutine.

        End If

    End Sub

    Public Sub Sub_Sync01(ByVal Message As String)                                                          'Subroutine for adding inbound IP's to the correct lists. Takes the message by value as string as the parameter.

        Dim Active_Empty As Boolean = False                                                                 'Variable to store weather the message has active IP's in it.
        Dim Known_Empty As Boolean = False                                                                  'Variable to store weather the message has known IP's in it.
        Dim Temp_Active As New List(Of String)                                                              'Object variable list to store active IP's from the message temporarily.
        Dim Temp_Known As New List(Of String)                                                               'Object variable list to store known IP's from the message temporarily.
        Dim Enough_Nodes As Boolean = False                                                                 'Variable to store if enough nodes are known yet.

        If Message.Length = 5 Then                                                                          'If the length of the Message variable is equal to 5. 

            Enough_Nodes = False                                                                            'Sets the Enough_Nodes variable to false.
            Active_Empty = True                                                                             'Sets the Active_Empty variable to true.
            Known_Empty = True                                                                              'Sets the Known_Empty variable to true.
            Sub_Console_Print("Address message contained no IP's", "Main", "red")                           'Sends a message to the console subroutine saying the message contained no IP's.

        End If

        Message = Message.Substring(Message.IndexOf(" ") + 1, Message.Length - Message.IndexOf(" ") - 1)    'Sets the Message variable equal to a substring of itself starting at the index equal to the first index of " " plus 1 with length equal to itself minus the substring start index minus 1.

        While Active_Empty = False                                                                          'While loop whilst the Active_Empty variable equals false.

            If Message.Substring(0, 1) = ":" Then                                                           'If a substring of the Message variable starting at index 0 with length 1 equals ":".

                Active_Empty = True                                                                         'Sets the Active_Empty variable to true. 
                Message = Message.Substring(1, Message.Length - 1)                                          'Sets the Message variable equal to a substring of itself starting at index 1 with length equal to itself minus 1.
                Exit While                                                                                  'Exits the while loop.

            End If

            Dim Next_IP As String = Message.Substring(0, Message.IndexOf(","))                              'Variable to hold the IP's extracted from the message, initialized as a substring of the Message variable starting at index 0 with length equal to the index of "," in itself.
            Message = Message.Substring(Next_IP.Length + 1, Message.Length - Next_IP.Length - 1)            'Sets the Message variable equal to a substring of itself starting at index equal to the length of the Next_IP variable plus 1 with length equal to itself minus the length of the Next_IP variable minus 1.
            Temp_Active.Add(Next_IP)                                                                        'Adds the Next_IP variable to the Temp_Active list.

        End While

        While Known_Empty = False                                                                           'While loop whilst the Known_Empty variable equals false.

            If Message.Substring(0, 1) = "|" Then                                                           'If a substring of the Message variable starting at index 0 with length 1 equals "|".

                Known_Empty = True                                                                          'Sets the Known_Empty variable to true.
                Exit While                                                                                  'Exits the while loop.

            End If

            Dim Next_IP As String = Message.Substring(0, Message.IndexOf(","))                              'Variable to hold the IP's extracted from the message, initialized as a substring of the Message variable starting
            Message = Message.Substring(Next_IP.Length + 1, Message.Length - Next_IP.Length - 1)            'Sets the Message variable equal to a substring of itself starting at index equal to the length of the Next_IP variable plus 1 with length equal to itself minus the length of the Next_IP variable minus 1.
            Temp_Known.Add(Next_IP)                                                                         'Adds the Next_IP variable to the Temp_Known list.

        End While

        Sub_Console_Print("Address message contained " & Temp_Active.Count & " active IP's", "Main", "Black") 'Sends a message to the console subroutine displaying the number of active IP's given in the message.
        Sub_Console_Print("Address message contained " & Temp_Known.Count & " extra IP's", "Main", "Black") 'Sends a message to the console subroutine displaying the number of known IP's given in the message.

        Temp_Active = Func_Repeat_Check(Temp_Active)                                                        'Sets the Temp_Active list equal to the result of the Func_Repeat_Check function passing the Temp_Active list as the parameter.
        Temp_Known = Func_Repeat_Check(Temp_Known)                                                          'Sets the Temp_Known list equal to the result of the Func_Repeat_Check function passing the Temp_Known list as the parameter.

        Temp_Active = Func_Active_List_Check(Temp_Active)                                                   'Sets the Temp_Active list equal to the result of the Func_Active_List_Check function passing the Temp_Active list as the parameter.
        Temp_Active = Func_Know_List_Check(Temp_Active)                                                     'Sets the Temp_Active list equal to the result of the Func_Known_List_Check function passing the Temp_Active list as the parameter.
        Temp_Known = Func_Active_List_Check(Temp_Known)                                                     'Sets the Temp_Known list equal to the result of the Func_Active_List_Check function passing the Temp_Known list as the parameter.
        Temp_Known = Func_Know_List_Check(Temp_Known)                                                       'Sets the Temp_Known list equal to the result of the Func_Known_List_Check function passing the Temp_Known list as the parameter.

        Sub_Console_Print("Address message contained " & Temp_Active.Count & " new active IP's", "Main", "Black") 'Sends a message to the console subroutine displaying the number of new active nodes.
        Sub_Console_Print("Address message contained " & Temp_Known.Count & " new extra IP's", "Main", "Black") 'Sends a message to the console subroutine displaying the number of new known nodes.

        For i = 0 To Temp_Active.Count - 1                                                                  'For loop from i equals 0 to the number of items in the Temp_Active list minus 1.

            Active_Node_List.Add(Temp_Active(i))                                                            'Adds the item at index i of the Temp_Active list to the Active_Node_List.
            Success_Node_Count = Success_Node_Count + 1                                                     'Sets the Success_Node_Count equal to itself plus 1.

        Next i

        If Success_Node_Count = Prefered_Connection Then                                                    'If the Success_Node_Count variable equals the Prefered_Connection variable.

            Enough_Nodes = True                                                                             'Sets the Enough_Nodes variable to true.

        End If

        For i = 0 To Temp_Known.Count - 1                                                                   'For loop from i equals 0 to the number of items in the Temp_Known list minus 1.

            Known_Node_List.Add(Temp_Known(i))                                                              'Adds the item at index i of the Temp_Known list to the Known_Node_List.

        Next i

        Sub_Reconfig_Node_Lists()                                                                           'Executes the Sub_Reconfig_Node_Lists subroutine.
        Sub_Refresh_Node_List()                                                                             'Executes the Sub_Refresh_Node_List subroutine.
        Sub_Refresh_Node_File()                                                                             'Executes the Sub_Refresh_Node_File subroutine.

        If Enough_Nodes = True Then                                                                         'If the Enough_Nodes variable equals true.

            Sub_Console_Print("Enough IP's online", "Main", "green")                                        'Sends a message to the console subroutine saying enough IP's are online.
            Sub_Sync02()                                                                                    'Executes the Sub_Sync02 subroutine.
            Exit Sub                                                                                        'Exits the subroutine.

        Else

            Sub_Console_Print("Not enough active IP's, requesting more", "Main", "Black")                   'Sends a message to the console subroutine saying not enough active IP's so more are being requested.
            Node_State = Node_State + 1                                                                     'Sets the Node_State variable equal to itself plus 1.

            If Active_Node_List.Count - 1 - Node_State < 0 Then                                             'If the number of items in the Active_Node_List minus 1 minus the Node_State variable is smaller then 0.

                Sub_Console_Print("WARNING: Active IP's could not provide enough additional active IP's. If restarting your client does not fix the error, add more IP's or reduce the prefered number of connections. This node will now operate with a reduced connection list until more active IP's can be provided", "Main", "red") 'Sends a message to the console subroutine saying not enough IP's could be requested from other nodes.
                picboxWarn.Visible = True                                                                   'Sets the visible property of the picboxWarn picture box to true.
                Prefered_Connection = Active_Node_List.Count                                                'Sets the Prefered_Connections variable equal to the number of items in the Active_Node_List.
                Sub_Sync02()                                                                                'Executes the Sub_Sync02 subroutine.
                Exit Sub                                                                                    'Exits the subroutine.

            End If

            Sub_P2P_Get_Address(Active_Node_List(Active_Node_List.Count - 1 - Node_State), Prefered_Connection - Success_Node_Count) 'Executes the Sub_P2P_Get_Address subroutine passing the item at index equal to the number of items in the Active_Node_List minus 1 minus the Node_State variable of the Active_Node_List and the Prefered_Connection variable minus the Success_Node_Count variable as parameters.

        End If

    End Sub

    Public Sub Sub_Sync02()                                                                                 'Subroutine for updating label colours and starting the next step of the synchronization process.

        Sub_Reconfig_Node_Lists()                                                                           'Executes the Sub_Reconfig_Node_Lists subroutine.
        Sub_Refresh_Node_List()                                                                             'Executes the Sub_Refresh_Node_List subroutine.
        Sub_Refresh_Node_File()                                                                             'Executes the Sub_Refresh_Node_File subroutine.
        Sub_Console_Print("IPs loaded", "Main", "Green")                                                    'Sends a message to the console subroutine saying the received IPs have been loaded.

        lblStat1.Text = "Online"                                                                            'Sets the label lblStat1 to "Online".
        lblStat1.ForeColor = Color.Green                                                                    'Sets the colour of the label lblStat1 to green.
        lblStat2.Text = "Online"                                                                            'Sets the label lblStat2 to "Online".
        lblStat2.ForeColor = Color.Green                                                                    'Sets the colour of the label lblStat2 to green.
        lblNodes.Text = Active_Node_List.Count                                                              'Sets the label lblNodes equal to the number of items in the Active_Node_List.
        lblNodes.ForeColor = Color.Green                                                                    'Sets the colour of the label lblNodes to green.

        Sub_Console_Print("Connection complete, updating blockchain", "Main", "Green")                      'Sends a message to the console subroutine saying the connection process has completed and the blockchain will now be updated.
        Sub_P2P_Get_Height(Active_Node_List(Active_Node_List.Count - 1))                                    'Executes the Sub_P2P_Get_Height subroutine passing the item at the index equal to the number of items in the Active_Node_List minus 1 of the Active_Node_List as the parameter.

    End Sub

    Public Sub Sub_Sync03(ByVal Message As String)                                                          'Subroutine for calculating how out of sync this node is from the rest of the network. Takes the message by value as string as the parameter.

        Height_State = Height_State + 1                                                                     'Sets the Height_State variable equal to itself plus 1.
        Heights_List.Add(Message.Substring(4, Message.Length - 4))                                          'Adds a substring of the Message variable starting at index 4 with length equal to itself minus 4 to the Heights_List.
        Heights_Addresses.Add(Incoming_IPV4)                                                                'Adds the Incoming_IPV4 variable to the Heights_Addresses list.

        Dim Height_Index As Integer = 0                                                                     'Variable to store the index of the heighest height in the Heights_List.

        If Height_State < Prefered_Connection Then                                                          'If the Height_State variable is smaller then the Prefered_Connection variable.

            Sub_P2P_Get_Height(Active_Node_List(Active_Node_List.Count - 1 - Height_State))                 'Executes the Sub_P2P_Get_Height subroutine passing the item at the index equal to the number of items in the Active_Node_List minus 1 minus the Height_State variable of the Active_Node_List as the parameter.
            Exit Sub                                                                                        'Exits the subroutine.

        End If

        Sub_Console_Print("Calculating local blockchain data...", "Main", "Black")                          'Sends a message to the console subroutine saying local blockchain data is being calculated.

        Dim Copy_Heights_List As New List(Of Integer)                                                       'Object variable list to store an unsorted copy of the heights.
        Copy_Heights_List = Heights_List                                                                    'Sets the Copy_Heights_List equal to the Heights_List.

        Heights_List.Sort()                                                                                 'The Heights_List is sorted by size.

        Height_Index = Copy_Heights_List.IndexOf(Heights_List(Heights_List.Count - 1))                      'Sets the Height_Index variable equal to the index of the item at the index equal to the number of items in the Heights_List minus 1 of the Heights_List in the Copy_Heights_List.
        Height_Target_IPV4 = Heights_Addresses(Height_Index)                                                'Sets the Height_Target_IPV4 equal to the item at index Height_Index of the Heights_Addresses list.
        Blocks_Remaining = Heights_List(Heights_List.Count - 1) - Func_Calculate_Height()                   'Sets the Blocks_Remaining variable equal to the item at the index equal to the number of items in the Heights_List minus 1 of the Heights_List minus the local blockchain height.

        If Blocks_Remaining < 0 Then                                                                        'If the Blocks_Remaining variable is smaller then 0.

            Blocks_Remaining = 0                                                                            'Sets the Blocks_Remaining variable to 0.

        End If

        Sub_Console_Print("Network height : " & Heights_List(Heights_List.Count - 1), "Main", "Black")      'Sends a message to the console subroutine displaying the height of the network blockchain.
        Sub_Console_Print("Local height : " & Func_Calculate_Height(), "Main", "Black")                     'Sends a message to the console subroutine displaying the height of this nodes blockchain.
        Sub_Console_Print("Blocks remaining : " & Blocks_Remaining, "Main", "Black")                        'Sends a message to the console subroutine displaying the number of blocks that need to be downloaded.

        If Blocks_Remaining <= 0 Then                                                                       'If the Block_Remaining variable is less than or equal to 0.

            Sub_Sync05()                                                                                    'Executes the Sub_Sync05 subroutine.
            prbBlocks.Minimum = 0                                                                           'Sets the prbBlocks progress bar minimum value to 0.
            prbBlocks.Value = 0                                                                             'Sets the prbBlocks progress bar value to 0.
            prbBlocks.Maximum = 0                                                                           'Sets the prbBlocks progress bar maximum value to 0.
            Exit Sub                                                                                        'Exits the subroutine.

        Else

            prbBlocks.Minimum = 0                                                                           'Sets the prbBlocks progress bar minimum value to 0.
            prbBlocks.Value = 0                                                                             'Sets the prbBlocks progress bar value to 0.
            prbBlocks.Maximum = 0                                                                           'Sets the prbBlocks progress bar maximum value to 0.

            prbBlocks.Minimum = Func_Calculate_Height()                                                     'Sets the prbBlocks progress bar minimum value to the local blockchain height.
            prbBlocks.Maximum = Heights_List(Heights_List.Count - 1)                                        'Sets the prbBlocks progress bar maximum value to the last item in the Heights_List.
            lblBlocksRem.Text = "Blocks Remaining: " & Blocks_Remaining                                     'Sets the lblBlocksRem label to the string "Blocks Remaining: " and appends the variable Blocks_Remaining to the end.

            Block_Target = Func_Calculate_Height() + 1                                                      'Sets the Block_Target variable equal to the local blockchain height plus 1.
            Sub_P2P_Get_Block(Height_Target_IPV4, Block_Target)                                             'Executes the Sub_P2P_Get_Block subroutine passing the Height_Target_IPV4 and the Block_Target variables as parameters.

        End If

    End Sub

    Public Sub Sub_Sync04()                                                                                 'Subroutine that executes when a new block arrives due to the syncing process.

        prbBlocks.Increment(1)                                                                              'Increments the prbBlocks progress bar by 1.
        Blocks_Remaining = Blocks_Remaining - 1                                                             'Sets the Blocks_Remaining variable equal to itself minus 1.
        lblBlocksRem.Text = "Blocks Remaining: " & Blocks_Remaining                                         'Sets the label lblBlocksRem to the string "Blocks Remaining: " and appends the Blocks_Remaining variable to the end.

        If Blocks_Remaining <= 0 Then                                                                       'If the Block_Remaining variable is less than or equal to 0.

            Height_State = 0                                                                                'Sets the Height_State variable to 0.
            Sub_P2P_Get_Height(Active_Node_List(Active_Node_List.Count - 1))                                'Executes the Sub_P2P_Get_Height subroutine passing the item at the index equal to the number of items in the Active_Node_List minus 1 of the Active_Node_List as the parameter.

        Else

            Block_Target = Block_Target + 1                                                                 'Sets the Block_Target variable equal to itself plus 1.
            Sub_P2P_Get_Block(Height_Target_IPV4, Block_Target)                                             'Executes the Sub_P2P_Get_Block subroutine passing the Height_Target_IPV4 and the Block_Target variables as parameters.

        End If

    End Sub

    Public Sub Sub_Sync05()                                                                                 'Subroutine that finishes up the synchronization process.

        lblSync1.Text = "Synchronized"                                                                      'Sets the label lblSync1 to the string "Synchronized".
        lblSync1.ForeColor = Color.Green                                                                    'Sets the font colour of the label lblSync1 to green.
        lblSync2.Text = "Synchronized"                                                                      'Sets the label lblSync2 to the string "Synchronized".
        lblSync2.ForeColor = Color.Green                                                                    'Sets the font colour of the label lblSync1 to green.
        lblMemPool1.ForeColor = Color.Green                                                                 'Sets the font colour of the label lblMemPool1 to green.

        Node_Online = True                                                                                  'Sets the Node_Online variable to true.

        btnConnect.Enabled = False                                                                          'Disables the connect button.

        Sub_Console_Print("Blockchain updated, network synchronization complete", "Main", "Green")          'Sends a message to the console subroutine saying the node is online.
        Sub_Console_Print("Program ready", "Main", "Green")                                                 'Sends a message to the console subroutine saying the program is ready to use.

    End Sub

    Public Function Func_Insert_Check(ByVal IP As String)                                                   'Function for checking if this node already has incoming IP's. Takes the IP by value as string as the parameter.

        For i = 0 To Active_Node_List.Count - 1                                                             'For loop from i equals 0 to the number of items in the Active_Node_List minus 1.

            If Active_Node_List(i) = IP Then                                                                'If the item at index i of the Active_Node_List is equal to the IP variable.

                Return False                                                                                'Returns false.

            End If

        Next i

        If IP = Local_IPV4 Then                                                                             'If the IP variable equals the Local_IPV4 variable.

            Return False                                                                                    'Returns false.

        End If

        Return True                                                                                         'Returns true.

    End Function

    Public Sub Sub_Reconfig_Node_Lists()                                                                    'Subroutine for sorting the IP lists so they are in most active order.

        Dim Delete_List As New List(Of String)                                                              'Object variable list for storing IP's to be removed.

        For i = 0 To Active_Node_List.Count - 1                                                             'For loop from i equals 0 to the number of items in the Active_Node_List minus 1.

            For i2 = 0 To Known_Node_List.Count - 1                                                         'For loop from i2 equals 0 to the number of items in the Known_Node_List minus 1.

                If Active_Node_List(i) = Known_Node_List(i2) Then                                           'If the item at index i of the Active_Node_List is equal to the item at index i2 of the Known_Node_List.

                    Delete_List.Add(Active_Node_List(i))                                                    'The item at index i of the Active_Node_List is added to the Delete_List.

                End If

            Next i2

        Next i

        For i = 0 To Delete_List.Count - 1                                                                  'For loop from i equals 0 to the number of items in the Delete_List minus 1.

            Known_Node_List.Remove(Delete_List(i))                                                          'The item at index i of the Delete_List is removed from the Known_Node_List.

        Next i

        For i = Active_Node_List.Count - 1 To 0 Step -1                                                     'For loop from i equals the number of items in the Active_Node_List minus 1 to 0 with a step of -1.

            Known_Node_List.Add(Active_Node_List(i))                                                        'Adds the item at index i of the Active_Node_List to the Known_Node_List.

        Next i

    End Sub

    Public Function Func_Repeat_Check(ByVal Temp_List As List(Of String))                                   'Function for checking for repeate IP's from inbound messages. Takes a list of IP's by value as the parameter.

        Dim Delete_List As New List(Of String)                                                              'Object variable list for storing IP's to be removed.

        For i = 0 To Temp_List.Count - 1                                                                    'For loop from i equals 0 to the number of items in the Temp_List minus 1.

            If Temp_List(i) = Local_IPV4 Then                                                               'If the item at index i of the Temp_List is equal to the Local_IPV4 variable.

                Delete_List.Add(Temp_List(i))                                                               'The item at index i of the Temp_List is added to the Delete_List.

            End If

        Next i

        For i = 0 To Delete_List.Count - 1                                                                  'For loop from i equals 0 to the number of items in the Delete_List minus 1.

            Temp_List.Remove(Delete_List(i))                                                                'Removes the item at index i of the Delete_List from the Temp_List.

        Next i

        Return Temp_List                                                                                    'Returns the Temp_List.

    End Function

    Public Function Func_Know_List_Check(ByVal Temp_Know_List As List(Of String))                           'Function for removing duplicate known IP's.

        Dim Delete_List As New List(Of String)                                                              'Object variable list for storing IP's to be removed.

        For i = 0 To Temp_Know_List.Count - 1                                                               'For loop from i equals 0 to the number of items in the Temp_Know_List minus 1.

            For i2 = 0 To Known_Node_List.Count - 1                                                         'For loop from i equals 0 to the number of items in the Know_Node_List minus 1.

                If Temp_Know_List(i) = Known_Node_List(i2) Then                                             'If the item at index i of the Temp_Known_List is equal to the item at index i2 of the Known_Node_List.

                    Delete_List.Add(Temp_Know_List(i))                                                      'The item at index i of the Temp_Known_List is added to the Delete_List.

                End If

            Next i2

        Next i

        For i = 0 To Delete_List.Count - 1                                                                  'For loop from i equals 0 to the number of items in the Delete_List minus 1.

            Temp_Know_List.Remove(Delete_List(i))                                                           'The item at index i of the Delete_List is removed from the Temp_Know_List.

        Next i

        Return Temp_Know_List                                                                               'Returns the Temp_Known_List.

    End Function

    Public Function Func_Active_List_Check(ByVal Temp_Active_List As List(Of String))                       'Function for removing duplicate active IP's

        Dim Delete_List As New List(Of String)                                                              'Object variable list for storing IP's to be removed.

        For i = 0 To Temp_Active_List.Count - 1                                                             'For loop from i equals 0 to the number of items in the Temp_Active_List minus 1.

            For i2 = 0 To Active_Node_List.Count - 1                                                        'For loop from i equals 0 to the number of items in the Active_Node_List minus 1.

                If Temp_Active_List(i) = Active_Node_List(i2) Then                                          'If the item at index i of the Temp_Active_List is equal to the item at index i2 of the Active_Node_List.

                    Delete_List.Add(Temp_Active_List(i))                                                    'The item at index i of the Temp_Active_List is added to the Delete_List.

                End If

            Next i2

        Next i

        For i = 0 To Delete_List.Count - 1                                                                  'For loop from i equals 0 to the number of items in the Delete_List minus 1.

            Temp_Active_List.Remove(Delete_List(i))                                                         'The item at index i of the Delete_List is removed from the Temp_Active_List.

        Next i

        Return Temp_Active_List                                                                             'Returns the Temp_Active_List.

    End Function


    Public Sub Sub_Send_Ips(ByVal Message As String)                                                        'Subroutine that sends IP's to other nodes. Takes the message by value as string as as a parameter.

        Dim Payload As String = ""                                                                          'Variable to store the concatonated IP's.
        Dim IP_Count As Integer = 0                                                                         'Variable to store the number of IP's being sent.
        Dim Amount As Integer = 0                                                                           'Variable to store the number of IP's asked for.

        Amount = Message.Substring(4, Message.Length - 4)                                                   'Sets the Amount variable equal to a substring of the Message variable starting at index 4 with length equal to itself minus 4.

        If Amount < 5 Then                                                                                  'If the amount variable is smaller then 5.

            Amount = 5                                                                                      'Sets the Amount variable to 5.

        End If

        For i = Active_Node_List.Count - 1 To 0 Step -1                                                     'For loop from i equals the number of items in the Active_Node_List minuis 1 to 0 with a step of -1.

            If IP_Count = Amount Then                                                                       'If the IP_Count variable equals the Amount variable.

                Exit For                                                                                    'Exits the for loop.

            End If

            Payload = Payload & Active_Node_List(i) & ","                                                   'Sets the Payload variable equal to itself with the item at index i of the Active_Node_List and "," appened to it.
            IP_Count = IP_Count + 1                                                                         'Sets the IP_Count variable equal to itself plus 1.

        Next i

        Payload = Payload & ":"                                                                             'Sets the Payload variable equal to itself with ":" appened to it.

        For i = Known_Node_List.Count - 1 To 0 Step -1                                                      'For loop from i equals the number of items in the Known_Node_List minus 1 to 0 with a step of -1.

            If IP_Count = Amount Then                                                                       'If the IP_Count variable equals the Amount variable.

                Exit For                                                                                    'Exits the for loop.

            End If

            Payload = Payload & Known_Node_List(i) & ","                                                    'Sets the Payload variable equals itself with the item at index i of the Known_Node_List and "," appened to it.
            IP_Count = IP_Count + 1                                                                         'Sets the IP_Count variable equal to itself plus 1.

        Next i

        Payload = Payload & "|"                                                                             'Sets the Payload variable equal to itself with "|" appened to it.

        Sub_P2P_Address(Incoming_IPV4, Payload)                                                             'Executes the Sub_P2P_Address subroutine passing the Incoming_IPV4 and Payload variables as parameters.

    End Sub

    Public Sub Sub_Send_Heights()                                                                           'Subroutine that sends the height of this nodes blockchain.

        Sub_P2P_Height(Incoming_IPV4, Func_Calculate_Height)                                                'Executes the Sub_P2P_Height subroutine passing the Incoming_IPV4 and Payload variables as parameters.

    End Sub

    Public Sub Sub_Send_Block(ByVal message As String)                                                      'Subroutine that sends requested blocks while syncing. Takes the message by value as string as a parameter.

        Dim Block_Index As Integer = 0                                                                      'Variable to store the index of the block requested.

        Block_Index = message.Substring(4, message.Length - 4)                                              'Sets the Block_Index variable equal to a substring of the Message variable starting at index 4 with length equal to itself minus 4.

        Sub_P2P_Block(Incoming_IPV4, Func_Block_Read(Block_Index))                                          'Executes the Sub_P2P_Block subroutine passing the Incoming_IPV4 variable and the block at index Block_Index as parameters.

    End Sub

#End Region

#Region "Main Form"                                                                     'Region containing code used across the form and regually accessed by other subroutines.

    Public Sub Sub_Console_Print(ByVal Text As String, ByVal Console As String, ByVal Colour As String) 'Subroutine to print strings to one of the programs information consoles. Takes the desired string, the destination console and the font colourby value as strings as parameters.

        Select Case Console                                                             'Selects which console based on the Console variable.

            Case "Main"                                                                 'If the Console variable equals "Main".

                txtConsoleMain.SelectionColor = Color.Blue                              'Changes the txtConsoleMain text box font colour to blue.
                txtConsoleMain.AppendText(My.Computer.Clock.LocalTime)                  'Appends the current date and time to the text box.
                txtConsoleMain.SelectionColor = Color.Black                             'Changes the txtConsoleMain text box font colour to black.
                txtConsoleMain.AppendText(": ")                                         'Appends a colon and space to the text box.
                txtConsoleMain.SelectionColor = Color.FromName(Colour)                  'Changes the txtConsoleMain text box font colour to what is specified in the parameter Colour.
                txtConsoleMain.AppendText(Text & vbNewLine)                             'Appends the text parameter followed by a line break.

            Case "Mine"                                                                 'If the console variable equals "Mine".

                txtConsoleMine.SelectionColor = Color.Blue                              'Changes the txtConsoleMine text box font colour to blue.
                txtConsoleMine.AppendText(My.Computer.Clock.LocalTime)                  'Appends the current date and time to the text box.
                txtConsoleMine.SelectionColor = Color.Black                             'Changes the txtConsoleMine text box font colour to black.
                txtConsoleMine.AppendText(": ")                                         'Appends a colon and space to the text box.
                txtConsoleMine.SelectionColor = Color.FromName(Colour)                  'Changes the txtConsoleMine text box font colour to what is specified in the parameter Colour.
                txtConsoleMine.AppendText(Text & vbNewLine)                             'Appends the text parameter followed by a line break.

        End Select

    End Sub

    Public Sub Sub_Refresh_Node_List()                                                  'Subroutine to refresh the node list box.

        lstActiveNodes.Items.Clear()                                                    'Clears the lstActiveNodes list box.

        For i = 0 To Active_Node_List.Count - 1                                         'For loop from i equals 0 to the number of items in the Active_Node_List minus 1.

            lstActiveNodes.Items.Add(Active_Node_List(i))                               'Adds the item at index i of the Active_Node_List to the lstNodes list box.

        Next i

        lstKnownNodes.Items.Clear()                                                     'Clears the lstKnownNodes list box.

        For i = 0 To Known_Node_List.Count - 1                                          'For loop from i equals 0 to the number of items in the Known_Node_List list box minus 1.

            lstKnownNodes.Items.Add(Known_Node_List(i))                                 'Adds the item at index i of the Known_Node_List to the lstKnownNodes list box.

        Next i

        Sub_Console_Print("The node lists have been refreshed", "Main", "Green")        'Sends a message to the console subroutine saying the node list has been refreshed.

    End Sub

    Public Sub Sub_Refresh_Node_File()                                                  'Subroutine that updates and refreshes the node list file.

        My.Computer.FileSystem.WriteAllText(Node_Directory, "", False)                  'Clears the node list file.

        For i = 0 To Known_Node_List.Count - 1                                          'For loop from i equals 0 to the number of items in the Known_Node_List minus 1.

            My.Computer.FileSystem.WriteAllText(Node_Directory, Known_Node_List(i) & vbNewLine, True) 'Writes the item at index i of the Known_Node_List to the node list file and inserts a line break.

        Next i

    End Sub

    Public Function Func_Unix_Time()                                                    'Function for calculating the current Unix time value.

        Return CInt((DateTime.Now - New DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds) 'Minuses the date now from the Unix time start date and converts to seconds then converts to an integer and returns it.

    End Function

    Public Function Func_Validate(ByVal Data As String, ByVal Type As String)           'Function for validating data entered by the user.

        Select Case Type                                                                'Starts a select case block using the Type variable.

            Case Is = "Login"                                                           'Case where Type equals "Login".

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Login word must contain atleast one character.")            'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "Public Key"                                                      'Case where Type equals "Public Key".

                If Data.Length < 64 Then                                                'If the length of the Data variable is smaller then 64.

                    MsgBox("The public key you have entered is too short. All public keys must be 64 characters long.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data.Length > 64 Then                                            'Else if the length of the Data variable is bigger then 64.

                    MsgBox("The public key you have entered is too long. All public keys must be 64 characters long.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

                Dim Base16 As New Regex("^[a-f0-9]+$")                                  'Object variable Regex to store a regular expression for base 16 strings.

                If Base16.IsMatch(Data) <> True Then                                    'If the Data variable does not match the Base16 regular expression variable.

                    MsgBox("The public key you have entered is in an incorrect format. All characters must be hexadecimal (0-9 a-f).") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "Label"                                                           'Case where Type equals "Label".

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Label must contain atleast one character.")                 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

                If Data.Length > 50 Then                                                'If the length of the Data variable is bigger then 50.

                    MsgBox("Label cannot be greater then 50 characters.")               'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

                If Data.Contains("|") Then                                              'If the Data variable contains "|".

                    MsgBox("Label cannot contain the | character.")                     'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "Address"                                                         'Case where Type equals "Address".

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Label must contain atleast one character.")                 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

                If Data.Length < 74 Then                                                'If the length of the Data variable is less then 74.

                    MsgBox("The address you have entered is too short. All addresses must be 74 characters long.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data.Length > 74 Then                                            'Else if the length of the Data variable is bigger then 74.

                    MsgBox("The address you have entered is too long. All addresses must be 74 characters long.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

                Dim Base16 As New Regex("^[a-f0-9]+$")                                  'Object variable Regex to store a regular expression for base 16 strings.
                Dim Base10 As New Regex("^[0-9]+$")                                     'Object variable Regex to store a regular expression for base 10 strings.

                If Base16.IsMatch(Data) <> True Then                                    'If the Data variable does not match the Base16 regular expression variable.

                    MsgBox("The address you have entered is in an incorrect format. All characters must be hexadecimal (0-9 a-f).") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

                If Base10.IsMatch(Data.Substring(64, 10)) <> True Then                  'If a substring of the Data variable starting at index 64 with length 10 does not match the Base10 regular expression variable.

                    MsgBox("The address you have entered is in an incorrect format. The last 10 characters should all be values between 0 and 9.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "Amount"                                                          'Case where Type equals "Amount".

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Please enter a value to send.")                             'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf IsNumeric(Data) = False Then                                     'Else if the Data variable is not a number

                    MsgBox("You must enter a valid value.")                             'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data <= 0 Then                                                   'Else if the Data variable is smaller then or equal to 0.

                    MsgBox("You must enter a value greater then 0.")                    'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data.Contains(".") Then                                          'Else if the Data variable contains ".".

                    MsgBox("You must enter an integer, Halfcoins cannot be split up into smaller denominations.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data > Balance Then                                              'Else if the Data variable is greater then the Balance variable.

                    MsgBox("You must enter a value smaller then or equal to your balance.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "Fee Amount"                                                      'Case where Type equals "Fee Amount".

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Please enter a transaction fee (Minimum of 10).")           'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf IsNumeric(Data) = False Then                                     'Else if the Data variable is not a number

                    MsgBox("You must enter a valid value for the transaction fee.")     'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data.Contains(".") Then                                          'Else if the Data variable contains ".".

                    MsgBox("You must enter an integer, Halfcoins cannot be split up into smaller denominations.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data < 10 Then                                                   'Else if the Data variable is greater then 10.

                    MsgBox("You must enter a value greater then or equal to 10.")       'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data > Balance - txtSendAmount.Text Then                         'Else if the Data variable is greater then the Balance variable minus the send amount.

                    MsgBox("You must enter a value smaller then or equal to your balance when the value you are sending has been deducted from it.") 'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "Height"                                                          'Case where Type equals "Height".

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Please enter a height.")                                    'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf IsNumeric(Data) = False Then                                     'Else if the Data variable is not a number

                    MsgBox("You must enter a valid height value.")                      'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data <= 0 Then                                                   'Else if the Data variable is smaller then or equal to 0.

                    MsgBox("You must enter a height value greater then 0.")             'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data.Contains(".") Then                                          'Else if the Data variable contains ".".

                    MsgBox("You must enter an integer.")                                'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data > Func_Calculate_Height() Then                              'Else if the Data variable is greater then the local blockchain height.

                    MsgBox("No block found at entered height.")                         'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "IP"                                                              'Case where Type equals "IP".

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Please enter an IPv4.")                                     'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data.Length < 7 Then                                             'Else if the length of the Data variable is smaller then 7. 

                    MsgBox("IP length too short, you must enter a valid IPv4 value.")   'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data.Contains(".") = False Then                                  'Else if the Data variable does not contain "."

                    MsgBox("You must enter a valid IPv4 value.")                        'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf IsNumeric(Data.Substring(0, Data.IndexOf("."))) = False Then     'Else if a substring of the Data variable starting at index 0 with length equal to the index of ".".

                    MsgBox("You must enter a valid IPv4 value.")                        'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "Connections"                                                     'Case where Type equals "Connections".

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Please enter a value.")                                     'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf IsNumeric(Data) = False Then                                     'Else if the Data variable is not a number

                    MsgBox("You must enter a valid value.")                             'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf Data <= 0 Then                                                   'Else if the Data variable is smaller then or equal to 0.

                    MsgBox("You must enter a value greater then 0.")                    'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false.

                ElseIf Data > 8 Then                                                    'Else if the Data variable is greater then 8.

                    MsgBox("You cant have more then 8 concurrent connections.")         'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false.

                ElseIf Data.Contains(".") Then                                          'Else if the Data variable contains ".".

                    MsgBox("You must enter an integer.")                                'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                End If

            Case Is = "Core"                                                            'Case where Type equals "Core".

                Dim Valid_Cores As New Regex("^[0-8]+$")                                'Object variable Regex to store a regular expression for strings containing the digits 0-8.

                If Data = "" Then                                                       'If the Data variable equals nothing.

                    MsgBox("Please enter the number of cores to use when mining.")      'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false. 

                ElseIf IsNumeric(Data) = False Then                                     'Else if the Data variable is not a number

                    MsgBox("You must enter a valid number of cores.")                   'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false.

                ElseIf Data > Core_Count Then                                           'Else if the Data variable is greater then the Core_Count variable.

                    MsgBox("The number of cores you have selected is too high.")        'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false.

                ElseIf Data > 8 Then                                                    'Else if the Data variable is greater then 8.

                    MsgBox("The maximum number of cores you can use for mining is 8.")  'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false.

                ElseIf Data < 1 Then                                                    'Else if the Data variable is smaller then 1.

                    MsgBox("Please select a valid number of cores.")                    'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false.

                ElseIf Valid_Cores.IsMatch(Data) <> True Then                           'Else if the Data variable does not match the Valid_Cores regular expression variable.

                    MsgBox("Please select a valid number of cores.")                    'Displays a message box to the user saying why the validation check failed.
                    Return False                                                        'Returns the boolean result false.

                ElseIf Data = 1 Then                                                    'Else if the Data variable equals 1.

                    Nonce_Step = 1                                                      'Sets the Nonce_Step variable equal 1.
                    Return True                                                         'Returns the boolean result true.

                ElseIf Data = 2 Then                                                    'Else if the Data variable equals 2.

                    Nonce_Step = 2                                                      'Sets the Nonce_Step variable equal 2.
                    Return True                                                         'Returns the boolean result true.

                ElseIf Data = 3 Then                                                    'Else if the Data variable equals 3.

                    Nonce_Step = 3                                                      'Sets the Nonce_Step variable equal 3.
                    Return True                                                         'Returns the boolean result true.

                ElseIf Data = 4 Then                                                    'Else if the Data variable equals 4.

                    Nonce_Step = 4                                                      'Sets the Nonce_Step variable equal 4.
                    Return True                                                         'Returns the boolean result true.

                ElseIf Data = 5 Then                                                    'Else if the Data variable equals 5.

                    Nonce_Step = 5                                                      'Sets the Nonce_Step variable equal 5.
                    Return True                                                         'Returns the boolean result true.

                ElseIf Data = 6 Then                                                    'Else if the Data variable equals 6.

                    Nonce_Step = 6                                                      'Sets the Nonce_Step variable equal 6.
                    Return True                                                         'Returns the boolean result true.

                ElseIf Data = 7 Then                                                    'Else if the Data variable equals 7.

                    Nonce_Step = 7                                                      'Sets the Nonce_Step variable equal 7.
                    Return True                                                         'Returns the boolean result true.

                ElseIf Data = 8 Then                                                    'Else if the Data variable equals 8.

                    Nonce_Step = 8                                                      'Sets the Nonce_Step variable equal 8.
                    Return True                                                         'Returns the boolean result true.

                End If

                Return False                                                            'Returns the boolean result false.

            Case Else                                                                   'Case selected if Type equals something else.

                MsgBox("Error in validation.")                                          'Displays a message box to the user saying there was a error in validation.
                Return False                                                            'Returns the boolean result false.

        End Select

        Return True                                                                     'Returns the boolean result true. 

    End Function

    Public Function Func_New_Address()                                                  'Function for creating new addresses for the users account.

        Nonce = Func_Unix_Time()                                                        'Nonce variable equals the current unix time. 
        Address_Salt = sha256(Private_Key & Nonce)                                      'Appends the Nonce variable to the Private_Key variable and hashes it. This then becomes the variable Address_Salt.
        Address = sha256(Public_Key & Address_Salt) & Nonce                             'The Address_Salt variableis appened to the Public_Key variable and hashed and the Nonce variable is appened to the end. It is then stored in the Address variable.

        Return Address                                                                  'Returns the Address variable.

    End Function

    Public Sub Sub_Command(ByVal Command As String)                                     'Subroutine that handles executing commands from the console. Takes the entered message by value as string as the parameter.

        Select Case True                                                                'Selects the case that is true.

            Case Command.Length <= 3                                                    'Case where the length of the Command variable is smaller then or equal to 3.

                Sub_Console_Print("Command Not Recognised", "Main", "Red")              'Sends a message to the console subroutine saying the command is not recognised.

            Case Command = "help"                                                       'Case where the Command variable is equal to "help".

                Sub_Console_Print("Avaliable Commands:", "Main", "Black")               'Sends a message to the console subroutine displaying avaliable commands.
                Sub_Console_Print("help - Displays a list of avaliable commands", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the help command.
                Sub_Console_Print("add ip (IP) - Adds an IPv4 to the list of known nodes", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the add ip command.
                Sub_Console_Print("clear ip - Clears all IPv4 addresses", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the clear ip command.
                Sub_Console_Print("ping (IP) - Sends a ping message to the entered IP", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the ping command.
                Sub_Console_Print("set ip (IP) - Manually sets this nodes external IPv4", "Main", "Black") 'Sends a message to the console subroutine displayinginstructions for the set ip command.
                Sub_Console_Print("prefered connections (Value) - Sets the number of connections this node will try to make, the node will always work with at least one regaradless of this value", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the prefered connections command.
                Sub_Console_Print("time - Displays the current Unix timestamp", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the time command.
                Sub_Console_Print("height - Displays the height of the local blockchain", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the height command.
                Sub_Console_Print("network status - Displays if this node is online or offline", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the network status command.
                Sub_Console_Print("absolute balance - Calculates the balance of the account logged in by parsing the entire blockchain without accessing the UTXO databse. (This process can take a long time)", "Main", "Black") 'Sends a message to the console subroutine displaying instructions for the absolute balance command.

            Case Command = "time"                                                       'Case where the Command variable is equal to "time".

                Sub_Console_Print("Unix time: " & Func_Unix_Time(), "Main", "Black")    'Sends a message to the console subroutine displaying the current unix time.

            Case Command = "height"                                                     'Case where the Command variable is equal to "height".

                Sub_Console_Print("Local blockchain height: " & Func_Calculate_Height(), "Main", "Black") 'Sends a message to the console subroutine displaying the blockchain height.

            Case Command.Contains("add ip")                                             'Case where the Command variable contains "add ip".

                If Command.Substring(0, 6) <> "add ip" Then                             'If a substring of the Command variable starting at index 0 with length 6 does not equal the string "add ip".

                    Sub_Console_Print("Command Not Recognised", "Main", "Red")          'Sends a message to the console subroutine saying the command is not recognised.

                Else

                    If Command.Length < 7 Then                                          'If the length of the Command variable is smaller then 7.

                        Sub_Console_Print("No IP entered", "Main", "Red")               'Sends a message to the console subroutine saying no ip was entered.
                        Exit Sub                                                        'Exits the subroutine.

                    End If

                    Dim IP_Add As String = ""                                           'Clears the IP_Add variable.

                    IP_Add = Command.Substring(7, Command.Length - 7)                   'Sets the IP_Add variable equal to a substring of the Command variable starting at index 0 with length equal to itself minus 7.

                    If Func_Validate(IP_Add, "IP") = False Then                         'If the result of the Func_Validate subroutine passing the IP_Add variable and "IP" as parameters equals false.

                        Exit Sub                                                        'Exits the subroutine.

                    Else

                        Known_Node_List.Add(IP_Add)                                     'The Add_IP variable is added to the Known_Node_List
                        Sub_Refresh_Node_List()                                         'Executes the Sub_Refresh_Node_List subroutine.
                        Sub_Refresh_Node_File()                                         'Exexcuts the Sub_Refresh_Node_File subroutine.

                        Sub_Console_Print("IP Added successfully", "Main", "Green")     'Sends a message to the console subroutine saying the IP has been added.

                    End If

                End If

            Case Command.Contains("set ip")                                             'Case where the Command variable contains "set ip".

                If Command.Substring(0, 6) <> "set ip" Then                             'If a substring of the Command variable starting at index 0 with length 6 does not equal the string "set ip".

                    Sub_Console_Print("Command Not Recognised", "Main", "Red")          'Sends a message to the console subroutine saying the command is not recognised.

                ElseIf Command.Length < 8 Then                                          'Else if the length of the Command variable is smaller then 8.

                    Sub_Console_Print("You must enter an IP to set", "Main", "Red")     'Sends a message to the console subroutine saying you must enter an IP.

                ElseIf Func_Validate(Command.Substring(7, Command.Length - 7), "IP") = False Then 'Else if the Func_Validate subroutine passing a substring of the Command variable starting at index 7 with length equal to itself minus 7 and "IP" as parameters equals false.

                Else

                    External_IPv4 = Command.Substring(7, Command.Length - 7)            'Sets the External_IPv4 variable equal to a substring of the Command variable starting at index 7 with length equal to itself minus 7.
                    Sub_Console_Print("New external IPV4 address set successfuly", "Main", "green") 'Sends a message to the console subroutine saying the external IPv4 was set successfully.
                    Sub_Console_Print("The external IPV4 address of this node is: " & External_IPv4, "Main", "Black") 'Sends a message to the console subroutine displaying the external IP address.

                End If

            Case Command.Contains("prefered connections")                               'Case where the Command variable contains "prefered connections".

                If Command.Substring(0, 20) <> "prefered connections" Then              'If a substring of the Command variable starting at index 0 with length 20 does not equal the string "prefered connections".

                    Sub_Console_Print("Command Not Recognised", "Main", "Red")          'Sends a message to the console subroutine saying the command is not recognised.

                ElseIf Command.Length < 22 Then                                         'Else if the length of the Command variable is less then 22.

                    Sub_Console_Print("You must enter a value to set", "Main", "Red")   'Sends a message to the console subroutine saying you must enter a value.

                ElseIf Func_Validate(Command.Substring(21, Command.Length - 21), "Connections") = False Then 'Else if the Func_Validate subroutine passing a substring of the Command variable starting at index 21 with length equal to itself minus 21 and "Connections" as parameters equals false.

                Else

                    Prefered_Connection = Command.Substring(21, Command.Length - 21)    'Sets the Prefered_Connections variable equal to a substring of the Command variable starting at index 21 with length equal to itself minus 21.
                    Sub_Console_Print("New number of prefered connections set successfuly", "Main", "green") 'Sends a message to the console subroutine the new prefered connections was set successfully.
                    Sub_Console_Print("The number of prefered connections for this node is: " & Prefered_Connection, "Main", "Black") 'Sends a message to the console subroutine displaying the new number of prefered connections.

                End If

            Case Command = "clear ip"                                                   'Case where the Command variable is equal to "clear ip".

                Known_Node_List.Clear()                                                 'Clears the Known_Node_List.
                Active_Node_List.Clear()                                                'Clears the Active_Node_List.

                Sub_Refresh_Node_List()                                                 'Executes the Sub_Refresh_Node_List.
                Sub_Refresh_Node_File()                                                 'Executes the Sub_Refresh_Node_File.

                Sub_Console_Print("IPs cleared", "Main", "Green")                       'Sends a message to the console subroutine saying the IP's have been cleared.

            Case Command.Contains("ping")                                               'Case where the Command variable contains "ping".

                If Command.Substring(0, 4) <> "ping" Then                               'If a substring of the Command variable starting at 0 with length 4 does not equal "ping".

                    Sub_Console_Print("Command Not Recognised", "Main", "Red")          'Sends a message to the console subroutine saying the command is not recognised.

                ElseIf Command.Length < 5 Then                                          'Else if the length of the Command variable is smaller then 5.

                    Sub_Console_Print("You must enter an IP to ping", "Main", "Red")    'Sends a message to the console subroutine saying an IP must be entered.

                ElseIf Func_Validate(Command.Substring(5, Command.Length - 5), "IP") = False Then 'Else if the Func_Validate subroutine passing a substring of the Command variable starting at index 5 with length equal to itself minus 5 and "IP" as parameters equals false.

                Else

                    Sub_P2P_Ping(Command.Substring(5, Command.Length - 5))              'Executes the Sub_P2P_Ping subroutine passing a substring of the Command variable starting at index 5 with length equal to itself minus 5 as the parameter.

                End If

            Case Command = "network status"                                             'Case where the Command variable is equal to "network status".

                If Node_Online = False Then                                             'If the Node_Online variable equals false.

                    Sub_Console_Print("Node offline", "Main", "red")                    'Sends a message to the console subroutine saying the node is offline.

                Else

                    Sub_Console_Print("Node online", "Main", "green")                   'Sends a message to the console subroutine saying the node is online.

                End If

            Case Command = "force online"                                               'Case where the Command variable is equal to "force online".

                Node_Online = True                                                      'Sets the Node_Online variable to true.
                Sub_Console_Print("Node online", "Main", "black")                       'Sends a message to the console subroutine saying the node is online.

            Case Command = "force offline"                                              'Case where the Command variable is equal to "force offline".

                Node_Online = False                                                     'Sets the Node_Online variable to false.
                Sub_Console_Print("Node offline", "Main", "black")                      'Sends a message to the console subroutine saying the node is offline.

            Case Command = "absolute balance"                                           'Case where the Command variable is equal to "absolute balance".

                Sub_Console_Print("Calculating balance from blockchain...", "Main", "black") 'Sends a message to the console subroutine saying the absolute balance is being calculated.
                Sub_Console_Print("WARNING: This process can take a long time", "Main", "red") 'Sends a message to the console subroutine warning the use that this will take time.
                Sub_Console_Print("Absolute balance: " & Func_Calculate_Balance(), "Main", "black") 'Sends a message to the console subroutine displaying the absolute balance.

            Case Else                                                                   'Case where the Command variable does not fit any other case.

                Sub_Console_Print("Command Not Recognised", "Main", "Red")              'Sends a message to the console subroutine saying the command is not recognised.

        End Select

        txtConsoleMain.Focus()                                                          'Puts the cursor at the bottom of the console.
        txtCom.Focus()                                                                  'Returns the cursor to the txtCom text box.

    End Sub

#End Region

#Region "User Interface"                                                                'Region containing code for the user interface.

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click 'Subroutine that submits the users login word to the program and generates  public and private key pairs.

        If Func_Validate(txtLogin.Text, "Login") = False Then                           'If the result of the Func_Validate subroutine passing the text within the txtLogin text box and the string "Login" as parameters is false.

            Exit Sub                                                                    'Exits the subroutine.

        End If

        Logged_In = True                                                                'Sets the Logged_In variable to true.

        Sub_Console_Print("Generating key pairs from login word", "Main", "Black")      'Sends a message to the console subroutine saying the program is generating a key pair.

        Login_Word = txtLogin.Text                                                      'Stores whats entered in the login text box in the variale Login_Word.
        Private_Key = sha256(Login_Word)                                                'Sets the Private_Key variable equal to the hash of the Login_Word variable..
        Public_Key = sha256(Private_Key)                                                'Sets the Public_Key variable equal to the hash of the Private_Key variable.

        If Private_Key_Hide = False Then                                                'If the Private_Key_Hide variable equals false.

            txtPriKey.Text = Private_Key                                                'Displays the Private_Key variable in the txtPriKey text box.

        End If

        txtPubKey.Text = Public_Key                                                     'Displays the Public_Key variable in the txtPubKey text box.
        Balance = Func_Balance_UTXO()                                                   'Sets the Balance variable equal to the result of the Func_Balance_UTXO function.
        txtBal.Text = Balance                                                           'Displays the Balance variable in the txtBal text box.
        txtAdr.Text = ""                                                                'Clears the txtAdr text box.
        Unconfirmed_Balance = Func_Unconfirmed_Balance()                                'Sets the Unconfirmed_Balance variable equal to the result of the Func_Unconfirmed_Balance
        txtUnBal.Text = Unconfirmed_Balance                                             'Displays the Unconfirmed_Balance variable in the txtUnBal text box.


    End Sub

    Private Sub btnHidLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHidLog.Click 'Subroutine that will hide the login word from the user.

        If Login_Word_Hide = False Then                                                 'If the Login_Word_Hide variable is false.

            Login_Word_Hide = True                                                      'Sets the Login_Word_Hide variable to true.
            txtLogin.Text = ""                                                          'Clears the txtLogin text box.

        ElseIf Login_Word_Hide = True Then                                              'Else if the Login_Word_Hide variable is true.

            Login_Word_Hide = False                                                     'Sets the Login_Word_Hide to false.
            txtLogin.Text = Login_Word                                                  'Displays the Login_Word variable in the txtLogin text box.

        End If

    End Sub

    Private Sub btnHidePri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHidePri.Click 'Subroutine that will hide the private key from the user.

        If Private_Key_Hide = False Then                                                'If the Private_Key_Hide variable is false.

            Private_Key_Hide = True                                                     'Sets the Private_Key_Hide variable to true.
            txtPriKey.Text = ""                                                         'Clears the txtPriKey text box.

        ElseIf Private_Key_Hide = True Then                                             'Else if the Private_Key_Hide variable is true.

            Private_Key_Hide = False                                                    'Sets the Private_Key_Hide variable to false.
            txtPriKey.Text = Private_Key                                                'Displays the Private_Key variable in the txtPriKey text box.

        End If

    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click 'Subroutine for displaying help information to the user.

        MsgBox("To use Halfcoin, login to your account or create a new one by typing in your login word into the box above. Use the Send Money tab to create addresses to your account and send funds to other accounts. Regular contacts can be saved in the address book. To mine for coins, see the Mine tab. Settings can be monitored and changed in the settings tab. An internet connection is required to use this software.", MsgBoxStyle.Question) 'Displays a message box containing information on how to use the program.

    End Sub

    Private Sub btnCopyRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyRight.Click 'Subroutine for presenting copyright information.

        MsgBox("Copyright information:" & vbNewLine & vbNewLine & "The raw code for this program © 2016-2017 Gianluca Cantone" & vbNewLine & "Original concept for this program © Bitcoin Project 2009-2017", MsgBoxStyle.Information) 'Displays a message box to the user containing copyright information.

    End Sub


    Private Sub btnNewAdr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAdr.Click 'Subroutine that will generate a new address for then user account and display it.

        If Logged_In = False Then                                                       'If the Logged_In variable equals false.

            MsgBox("You must login with an account before you can create addresses.")   'Displays a message box telling the user to login before using this feature.
            Exit Sub                                                                    'Exits the subroutine.

        End If

        Sub_Console_Print("Generating new address, please wait 1 second before generating another", "Main", "black") 'Sends a message to the console subroutine saying a new address is being generated.

        txtAdr.Text = Func_New_Address()                                                'The result of the Func_New_Address is displayed in the txtAdr text box.

    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click 'Subroutine for submiting transactions.

        If Logged_In = False Then                                                       'If the Logged_In variable equals false.

            MsgBox("You must login with an account before you can send transactions.")  'Displays a message box telling the user to login before using this feature.
            Exit Sub                                                                    'Exits the subroutine.

        End If

        If Func_Validate(txtRepPubKey.Text, "Public Key") = False Then                  'If the result of the Sub_Validation subroutine passing the text within the txtRepPubKey text box and the string "Public Key" as parameters equals fasle.

            Exit Sub                                                                    'Exits the subroutine.

        End If

        If Func_Validate(txtRepAdr.Text, "Address") = False Then                        'If the result of the Sub_Validation subroutine passing the text within the txtRepAdr text box and the string "Address" as parameters equals false.

            Exit Sub                                                                    'Exits the subroutine.

        End If


        If Func_Validate(txtSendAmount.Text, "Amount") = False Then                     'If the result of the Sub_Validation subroutine passing the text within the txtSendAmount text box and the string "Amount" as parameters equals false.

            Exit Sub                                                                    'Exits the subroutine.

        End If

        If Func_Validate(txtTransFee.Text, "Fee Amount") = False Then                   'If the result of the Sub_Validation subroutine passing the text within the txtTransFee text box and the string "Fee Amount" as parameters equals false.

            Exit Sub                                                                    'Exits the subroutine.

        End If

        Recipient_Public_Key = txtRepPubKey.Text                                        'Stores the text entered in the txtRepPubKey text box in the Recipient_Public_Key variable.
        Recipient_Address = txtRepAdr.Text                                              'Stores the text entered in the txtRepAdr text box in the Recipient_Address variable.
        Transaction_Value = txtSendAmount.Text                                          'Stores the text entered in the txtSendAmount text box in the Transaction_Value variable.
        Transaction_Fee = txtTransFee.Text                                              'Stores the text entered in the txtTransFee text box in the Transaction_Fee variable.

        If MsgBox("Please confirm:" & vbNewLine & vbNewLine & "Recipient Public Key: " & vbNewLine & vbNewLine & Recipient_Public_Key & vbNewLine & vbNewLine & "Recipient Address: " & vbNewLine & vbNewLine & Recipient_Address & vbNewLine & vbNewLine & "Transaction Value: " & Transaction_Value & vbNewLine & "Transaction Fee: " & Transaction_Fee & vbNewLine & vbNewLine & "Are you sure you wish confirm this transaction?", MessageBoxButtons.YesNo) = MsgBoxResult.No Then  'Creates a yes or no box containing the details of the entered transaction and asks the user if they are sure.

            MsgBox("The transaction has been terminated.")                              'Displays a message box saying the transaction has been terminated.
            Exit Sub                                                                    'Exits the subroutine.

        End If

        Sub_Console_Print("Processing local transaction", "Main", "black")              'Sends a message to the console subroutine saying a new transaction is being processed.

        Sub_Create_Transaction()                                                        'Executes the Sub_Create_Transaction subroutine.

    End Sub


    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click 'Subroutine for adding a new contact to the address book.

        If Func_Validate(txtEnterPubKey.Text, "Public Key") = False Then                'If the result of the Sub_Validation subroutine passing the text within the txtEnterPubKey text box and the string "Public Key" as parameters equals false.

            Exit Sub                                                                    'Exits the subroutine.

        End If

        If Func_Validate(txtLabel.Text, "Label") = False Then                           'If the result of the Sub_Validation subroutine passing the text within the txtLabel text box and the string "Label" as parameters equals false.

            Exit Sub                                                                    'Exits the subroutine.

        End If

        New_Address = txtLabel.Text & " | " & txtEnterPubKey.Text                       'The contents of the label and public key text boxes are appended togewther with " | " between them. This is then stored in the variable New_Address.
        lstAdr.Items.Add(New_Address)                                                   'The New_Address variable is added to the lstAdr list box.

        Dim Current_Line As String = ""                                                 'Variable to store current line being written to the address data file.

        My.Computer.FileSystem.WriteAllText(Address_Directory, "", False)               'Clears the address data file by writting nothing to it.

        For i As Integer = 0 To lstAdr.Items.Count - 1                                  'For loop from i equals 0 to the number of items in the lstAdr list box minus 1.

            Current_Line = lstAdr.Items.Item(i) & vbNewLine                             'Sets the Current_Line variable equal to the item at index i of the lstAdr list box with a new line appened to it.
            My.Computer.FileSystem.WriteAllText(Address_Directory, Current_Line, True)  'Writes the Current_Line variable to the address book data file.

        Next i

        txtEnterPubKey.Text = ""                                                        'Clears the txtEnterPubKey text box.
        txtLabel.Text = ""                                                              'Clears the txtLabel text box.

    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click 'Subroutine for removeing a contact from the address book.

        lstAdr.Items.Remove(lstAdr.SelectedItem)                                        'Removes the currently selected item in the lstAdr list box.

        Dim Current_Line As String = ""                                                 'Variable to store current line being written to the address data file.

        My.Computer.FileSystem.WriteAllText(Address_Directory, "", False)               'Clears the address data file by writting nothing to it.

        For i As Integer = 0 To lstAdr.Items.Count - 1                                  'For loop from i equals 0 to the number of items in the lstAdr list box minus 1.

            Current_Line = lstAdr.Items.Item(i) & vbNewLine                             'Sets the Current_Line variable equal to the item at index i of the lstAdr list box with a new line appened to it.
            My.Computer.FileSystem.WriteAllText(Address_Directory, Current_Line, True)  'Writes the Current_Line variable to the address book data file.

        Next i

    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click 'Subroutine that will load data from the address book into the send money tab.

        Dim Selected_Address As String = lstAdr.SelectedItem                            'Variable to store the currently selected item in the address list box.

        txtRepPubKey.Text = Selected_Address.Substring(Selected_Address.IndexOf("|") + 2, Selected_Address.Length - Selected_Address.IndexOf("|") - 2) 'Displays the variable Selected_Addres in the text box txtRepPubKey but truncates the label field leaving only the public key. 

        TabControl.SelectTab(1)                                                         'Switches over to the send money tab.

    End Sub


    Private Sub btnEXE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEXE.Click 'Subroutine that submits commands to the console.

        Sub_Console_Print(txtCom.Text, "Main", "black")                                 'Sends a message to the console subroutine saying displaying what was entered into the console by ther user.
        Sub_Command(txtCom.Text)                                                        'Executes the Sub_Command subroutine passing the text in the txtCom as the parameter.
        txtCom.Text = ""                                                                'Clears the txtCom text box.

    End Sub

    Private Sub txtCom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCom.KeyDown 'Subroutine for allowing the user to press enter to submit commands to the console.

        If e.KeyCode = Keys.Return Then                                                 'If the key pressed is return.

            Me.btnEXE.PerformClick()                                                    'Clicks the btnEXE button.

        End If

    End Sub

    Private Sub TabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.SelectedIndexChanged 'Subroutine that executes when the console tab is selected.

        If TabControl.SelectedIndex = 5 Then                                            'If the index of the selected tab is equal to 5.

            txtCom.Focus()                                                              'Moves the cursor to the txtCom text box.

        End If

    End Sub


    Private Sub btnStartMine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartMine.Click 'Subroutine to start the mining process.

        If Logged_In = False Then                                                       'If the Logged_In variable is false.

            MsgBox("You must login with an account before you can mine.")               'Displays a message box telling the user to login before using this feature.
            Exit Sub                                                                    'Exits the subroutine.

        End If

        txtTotalTime.Text = 0                                                           'Sets the value of the txtTotalTime text box to 0.
        txtBlocksMined.Text = 0                                                         'Sets the value of the txtBlocksMined text box to 0.
        txtBlocksMissed.Text = 0                                                        'Sets the value of the txtBlocksMissed text box to 0.

        Sub_Start_Mining()                                                              'Executes the Sub_Start_Mining subroutine.

    End Sub

    Private Sub btnStopMine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopMine.Click 'Subroutine for stoping the mining process.

        Abort = True                                                                    'Sets the Abort variable to true.
        Thread_Worker01.CancelAsync()                                                   'Cancels the Thread_Worker01 background worker.
        btnStartMine.Enabled = True                                                     'Enables the btnStartMine button.

    End Sub

    Private Sub ckbStop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbStop.CheckedChanged 'Subroutine for making the mining process only mine 1 block.

        If ckbStop.Checked = True Then                                                  'If the ckbStop check box is checked.

            Single_Mine = True                                                          'Set the Single_Mine variable equal to true.

        Else

            Single_Mine = False                                                         'Set the Single_Mine variable equal to false.

        End If

    End Sub


    Private Sub cmbNetShip_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNetShip.SelectedIndexChanged 'Subroutine for enabling the user to enter a parent IP if child mode is selected.

        If cmbNetShip.Text = "Child" Then                                               'If the selected item in the cmbNetShip combo box is equal to "Child".

            Parent_Node = False                                                         'Sets the Parent_Node variable to false.
            txtParentIP.Enabled = True                                                  'Enables the txtParentIP text box.

        Else

            Parent_Node = True                                                          'Sets the Parent_Node variable to true.
            txtParentIP.Enabled = False                                                 'Disables the txtParentIP text box.

        End If

    End Sub

    Private Sub btnUTXORebuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUTXORebuild.Click   'Subroutine for rebuilding the UTXO database.

        If MsgBox("Are you sure you wish to rebuild the entire UTXO database? WARNING: This process can take a large amount of time depending on the current size of the blockchain and computer power avaliable. This action cannot be reversed and must be completed before the program can be used. Network connection is NOT required.", MessageBoxButtons.YesNo) = MsgBoxResult.No Then  'Creates a yes or no box with a message asking the user if they are sure they want to perform this action.

            MsgBox("Process aborted")                                                   'Creates a message box saying the process was aborted.
            Exit Sub                                                                    'Exits the subroutine.

        End If

        MsgBox("Process starting.")                                                     'Displays a message box saying the process is starting.

        Sub_Console_Print("Rebuilding UTXO database...", "Main", "Black")               'Sends a message to the console subroutine saying the program is rebuilding the UTXO database.

        Try                                                                             'Starts a try and catch block.

            My.Computer.FileSystem.WriteAllText(UTXO_Directory, "", False)              'Writes nothing to the UTXO database file.
            Sub_UTXO_Load()                                                             'Executes the Sub_UTXO_Load subroutine.
            Sub_UTXO(1)                                                                 'Executes the Sub_UTXO subroutine passing 1 as the parameter.

            txtUTXONum.Text = System.IO.File.ReadAllLines(UTXO_Directory).Length        'Sets the txtUTXONum text box to the number of lines in the UTXO database.
            txtUTXOSize.Text = FileLen(UTXO_Directory)                                  'Sets the txtUTXOSize text box to the file size in bytes of the UTXO database.

        Catch EX As Exception                                                           'Catches EX as an exception.

            Sub_Console_Print("Error with rebuilding the UTXO database. Error: " & EX.ToString, "Main", "red") 'Sends a message to the console subroutine saying there was an error when trying to rebuild the UTXO database and displays the exception.
            MsgBox("Process incomplete. The process encountered an error while trying to rebuild the UTXO database. Refer to the console for more information.") 'Displays a message box saying there was an error when trying to rebuild the UTXO database.

        End Try

        Sub_Console_Print("The UTXO database has successfully been rebuilt", "Main", "Green") 'Sends a message to the console subroutine saying the UTXO database has been successfully rebuilt.
        MsgBox("Process complete. The UTXO database has successfully been rebuilt.")    'Creates a message box saying the UTXO database has been successfully rebuilt.

    End Sub

    Private Sub btnHashRebuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHashRebuild.Click

        If MsgBox("Are you sure you wish to rebuild the entire hash database?", MessageBoxButtons.YesNo) = MsgBoxResult.No Then  'Creates a yes or no box with a message asking the user if they are sure they want to perform this action.

            MsgBox("Process aborted")                                                   'Creates a message box saying the process was aborted.
            Exit Sub                                                                    'Exits the subroutine.

        End If

        MsgBox("Process starting.")                                                     'Creates a message box saying the process is starting.

        Sub_Console_Print("Rebuilding hash database...", "Main", "Black")               'Sends a message to the console subroutine saying the program is rebuilding the hash database.

        Try                                                                             'Starts a try and catch block.

            My.Computer.FileSystem.WriteAllText(Hash_Database_Directory, "", False)     'Writes nothing to the hash database file.
            Hash_Database.Clear()                                                       'Clears the Hash_Database list.
            Sub_Hash_Database_Add(1)                                                    'Executes the Sub_Hash_Database_Add subroutine passing 1 as the parameter.

        Catch EX As Exception                                                           'Catches EX as an exception.

            Sub_Console_Print("Error with rebuilding the hash database. Error: " & EX.ToString, "Main", "red") 'Sends a message to the console subroutine saying there was an error when trying to rebuild the hash database and displays the exception.
            MsgBox("Process incomplete. The process encountered an error while trying to rebuild the hash database. Refer to the console for more information.") 'Creates a message box saying there was an error when trying to rebuild the hash database.

        End Try

        Sub_Console_Print("The hash database has successfully been rebuilt", "Main", "Green") 'Sends a message to the console subroutine saying the hash database has been successfully rebuilt.
        MsgBox("Process complete. The hash database has successfully been rebuilt.")    'Creates a message box saying the hash database has been successfully rebuilt.


    End Sub

    Private Sub btnBlockReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlockReset.Click 'Subroutine for recreating the blockchain.

        If MsgBox("Are you sure you wish to reset the entire local blochchain? WARNING: The file will be deleted and all blocks will be redownloaded upon restart. The synchronization process can take a long time and use a lot of band width depending on the current size of the blockchain. As a result, the UTXO database will also be fully rebuilt.", MessageBoxButtons.YesNo) = MsgBoxResult.No Then  'Creates a yes or no box with a message asking the user if they are sure they want to perform this action.

            MsgBox("Process aborted.")                                                  'Creates a message box saying the process was aborted.
            Exit Sub                                                                    'Exits the subroutine.

        End If

        MsgBox("Process starting.")                                                     'Creates a message box saying the process is starting.

        Sub_Console_Print("Resetting the blockchain...", "Main", "Black")               'Sends a message to the console subroutine saying the blockchain is being reset.

        My.Computer.FileSystem.DeleteFile(Blockchain_Directory)                         'Deletes the blockchain file.
        My.Computer.FileSystem.DeleteFile(UTXO_Directory)                               'Deletes the UTXO database file.
        My.Computer.FileSystem.DeleteFile(Hash_Database_Directory)                      'Deletes the hash database file.

        txtLocalHeight.Text = 0                                                         'Sets the txtLocalHeight text box to 0
        txtBlockSize.Text = 0                                                           'Sets the txtBlockSize text box to 0.
        txtUTXONum.Text = 0                                                             'Sets the txtUTXONum text box to 0.
        txtUTXOSize.Text = 0                                                            'Sets the txtUTXOSize text box to 0.

        btnUTXORebuild.Enabled = False                                                  'Disables the btnUTXORebuild button.
        btnBlockReset.Enabled = False                                                   'Disables the btnBlockReset button.
        btnHashRebuild.Enabled = False                                                  'Disables the btnHashRebuild button.

        Sub_Console_Print("The blockchain file has successfully been deleted.", "Main", "Green") 'Sends a message to the console subroutine saying the blockchain has been successfully deleted.
        MsgBox("The blockchain has successfully been deleted. Please restart the program to commence synchronization.") 'Creates a message box telling the user to restart the program to redownload the blockchain.

    End Sub

#End Region

#Region "Networking"                                                                            'Region containing code for network access.

    Public Function Func_Remove_Port(ByVal IP As String)                                        'Function that removes the port suffix from a given IP. Takes an IP by value as string as the parameter.

        Dim Index As Integer = IP.IndexOf(":")                                                  'Variable to store the location within the IP variable of ":".
        Dim New_IP As String = IP.Substring(0, Index)                                           'Variable to store a substring of the IP variable starting at index 0 with length equal to the Index variable.

        Return New_IP                                                                           'Returns the New_IP variable.

    End Function

    Public Sub Sub_P2P_Inbound(ByVal Message As String)                                         'Subroutine for dealing with inbound P2P messages from the network. Takes the inbound message by value as string as the parameter.

        If Message.Length < 3 Then                                                              'If the length of the Message variable is less then 3.

            Sub_Console_Print("ERROR with inbound message: Message from IP: " & Incoming_IPV4 & " does not conform to Halfcoin protocol", "Main", "red") 'Sends a message to the console subroutine saying the received message is not a part of the Halfcoin protocol.
            Exit Sub                                                                            'Exits the subroutine.

        End If

        Select Case Message.Substring(0, 3)                                                     'Select what code to execute depending on a substring of the Message variable starting at index 0 with length 3.

            Case Is = "000"                                                                     'Case where the Message substring equals "000".

                Sub_Console_Print("Version Send message received from: " & Incoming_IPV4 & ", Version of node is: " & Message.Substring(4, Message.Length - 4), "Main", "green") 'Sends a message to the console subroutine saying a version send message has been received and displys the version.
                Sub_Console_Print("Sending Version Reply message", "Main", "black")             'Sends a message to the console subroutine saying a reply message will now be sent.

                If Node_Online = True Then                                                      'If the Node_Online variable is true.

                    If Active_Node_List.Count >= Max_Connections Then                           'If the number of items in the Active_Node_List is bigger or equal to the Max_Connections variable.
                    Else
                        If Func_Insert_Check(Incoming_IPV4) = True Then                         'If the result of the Func_Insert_Check when passing the Incoming_IPV4 variable as the parameter equals true.

                            Active_Node_List.Add(Incoming_IPV4)                                 'Adds the Incoming_IPV4 variable to the Active_Node_List.
                            Sub_Reconfig_Node_Lists()                                           'Executes the Sub_Reconfig_Node_Lists subroutine.
                            Sub_Refresh_Node_List()                                             'Executes the Sub_Refresh_Node_List subroutine.
                            Sub_Refresh_Node_File()                                             'Executes the Sub_Refresh_Node_File subroutine.

                            If Active_Node_List.Count >= Prefered_Connection Then               'If the number of items in the Active_Node_List is bigger then or equal to the Prefered_Connection variable.

                                picboxWarn.Visible = False                                      'Makes the picboxWarn picture box invisible.

                            End If

                            lblNodes.Text = Active_Node_List.Count                              'Sets the lblNodes text box equal to the number of items in the Active_Node_List.

                        End If

                    End If

                End If

                Sub_P2P_Version_Reply(Incoming_IPV4)                                            'Executes the Sub_P2P_Version_Reply subroutine passing the Incoming_IPV4 as the parameter.

            Case Is = "001"                                                                     'Case where the Message substring equals "001".

                Sub_Console_Print("Version Reply message received from: " & Incoming_IPV4, "Main", "green") 'Sends a message to the console subroutine saying a Version Reply message has been received.

                If Node_Online = False Then                                                     'If the Node_Online variable equals false.

                    Check_Count = Check_Count + 1                                               'Sets the Check_Count variable equal to itself plus 1.
                    Success_Node_Count = Success_Node_Count + 1                                 'Sets the Success_Node_Count variable equal to itself plus 1.
                    Active_Node_List.Add(Incoming_IPV4)                                         'Adds the Incoming_IPV4 variable to the Active_Node_List.
                    Sub_Sync00()                                                                'Executes the Sub_Sync00 subroutine.

                End If

            Case Is = "002"                                                                     'Case where the Message substring equals "002".

                Sub_Console_Print("Get Address message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Get Address message has been received.
                Sub_Send_Ips(Message)                                                           'Executes the Sub_Send_Ips subroutine passing the Message variable as the parameter.

            Case Is = "003"                                                                     'Case where the Message substring equals "003".

                Sub_Console_Print("Addresses message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying an Address message has been received.
                Sub_Sync01(Message)                                                             'Executes the Sub_Sync01 subroutine passing the Message variable as the parameter.

            Case Is = "004"                                                                     'Case where the Message substring equals "004".

                Sub_Console_Print("Get Height message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Get Height message has been received.
                Sub_Send_Heights()                                                              'Executes the Sub_Send_Heights subroutine.

            Case Is = "005"                                                                     'Case where the Message substring equals "005".

                Sub_Console_Print("Height message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Height message has been received.
                Sub_Sync03(Message)                                                             'Executes the Sub_Sync03 subroutine passing the Message variable as the parameter.

            Case Is = "006"                                                                     'Case where the Message substring equals "006".

                Sub_Console_Print("Get Block message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Get Block message has been received.
                Sub_Send_Block(Message)                                                         'Executes the Sub_Send_Block subroutine passing the Message variable as the parameter.

            Case Is = "007"                                                                     'Case where the Message substring equals "007".

                Sub_Console_Print("Block message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Block message has been received.
                Sub_Block(Message)                                                              'Executes the Sub_Block subroutine passing the Message variable as the parameter.

            Case Is = "008"                                                                     'Case where the Message substring equals "008".

                Sub_Console_Print("Transaction message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Transaction message has been received.

                If Node_Online = True Then                                                      'If the Node_Online variable equals true.

                    Sub_Transaction(Message)                                                    'Executes the Sub_Transaction subroutine passing the Message variable as the parameter.

                Else

                    Sub_Console_Print("Transaction message rejected, local node is not online", "Main", "red") 'Sends a message to the console subroutine saying a Transaction message has been rejected.

                End If

            Case Is = "009"                                                                     'Case where the Message substring equals "009".

                Sub_Console_Print("Ping message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Ping message has been received.
                Sub_Console_Print("Sending a Pong message reply", "Main", "Green")              'Sends a message to the console subroutine saying a Pong message is being sent.
                Sub_P2P_Pong(Incoming_IPV4)                                                     'Executes the Sub_P2P_Pong subroutine passing the Incoming_IPV4 variable as the parameter.

            Case Is = "010"                                                                     'Case where the Message substring equals "010".

                Sub_Console_Print("Pong message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Pong message has been received.
                Sub_Console_Print("Ping process complete", "Main", "Green")                     'Sends a message to the console subroutine saying the ping process is complete.

            Case Is = "011"                                                                     'Case where the Message substring equals "011".

                Sub_Console_Print("Block Hash message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Block Hash message has been received.
                Sub_Block_Hash(Message)                                                         'Executes the Sub_Block_Hash subroutine passing the Message variable as the parameter.

            Case Is = "012"                                                                     'Case where the Message substring equals "012".

                Sub_Console_Print("Block Data message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Block Data message has been received.
                Sub_Block_Data(Message)                                                         'Executes the Sub_Block_Data subroutine passing the Message variable as the parameter.

            Case Is = "013"                                                                     'Case where the Message substring equals "013".

                Sub_Console_Print("Transaction Hash message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Transaction Hash message has been received.
                Sub_Transaction_Hash(Message)                                                   'Executes the Sub_Transaction_Hash subroutine passing the Message variable as the parameter.

            Case Is = "014"                                                                     'Case where the Message substring equals "014".

                Sub_Console_Print("Transaction Data message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Transaction Data message has been received.
                Sub_Transaction_Data(Message)                                                   'Executes the Sub_Transaction_Data subroutine passing the Message variable as the parameter.

            Case Is = "015"                                                                     'Case where the Message substring equals "015".

                Sub_Console_Print("Shutdown message received from: " & Incoming_IPV4, "Main", "Green") 'Sends a message to the console subroutine saying a Shutdown message has been received.

                If Active_Node_List.Contains(Incoming_IPV4) Then                                'If the Active_Node_List contains the Incoming_IPV4 variable.

                    Active_Node_List.Remove(Incoming_IPV4)                                      'Removes the Incoming_IPV4 variable from the Active_Node_List.

                    Sub_Reconfig_Node_Lists()                                                   'Executes the Sub_Reconfig_Node_Lists subroutine.
                    Sub_Refresh_Node_List()                                                     'Executes the Sub_Refresh_Node_List subroutine.
                    Sub_Refresh_Node_File()                                                     'Executes the Sub_Refresh_Node_File subroutine.

                    If Active_Node_List.Count < Prefered_Connection Then                        'If the number of items in the Active_Node_List is less then the Prefered_Connection variable.

                        picboxWarn.Visible = True                                               'Makes the picboxWarn picture box visible.

                    End If

                    lblNodes.Text = Active_Node_List.Count                                      'Sets the lblNodes text box equal to the number of items in the Active_Node_List.

                    Sub_Console_Print("Node: " & Incoming_IPV4 & " successfully removed from active node list", "Main", "Green") 'Sends a message to the console subroutine saying the IP has been successfully removed.

                End If

                If Child_Node_List.Contains(Incoming_IPV4) Then                                 'If the Child_Node_List contains the Incoming_IPV4 variable.

                    Child_Node_List.Remove(Incoming_IPV4)                                       'Removes the Incoming_IPV4 variable from the Child_Node_List.

                    Sub_Console_Print("Node: " & Incoming_IPV4 & " successfully removed from child node list", "Main", "Green") 'Sends a message to the console subroutine saying the IP has been successfully removed.

                End If

            Case Is = "016"                                                                     'Case where the Message substring equals "016".

                Sub_Console_Print("Child Version Send message received from: " & Incoming_IPV4 & ", Version of node is: " & Message.Substring(4, Message.Length - 4), "Main", "green") 'Sends a message to the console subroutine saying a Child Version Send message has been received and displays the version.

                Child_Node_List.Add(Incoming_IPV4)                                              'Adds the Incoming_IPV4 variable to the Child_Node_List.

                Sub_Console_Print("Sending Version Reply message", "Main", "black")             'Sends a message to the console subroutine saying a reply message will now be sent.
                Sub_P2P_Version_Reply(Incoming_IPV4)                                            'Executes the Sub_P2P_Version_Reply subroutine passing the Incoming_IPV4 variable as the parameter.

            Case Else

                Sub_Console_Print("WARNING: Unexpected error with inbound transmission", "Main", "Red") 'Sends a message to the console subroutine saying no case was selected so an error has occured.

        End Select

    End Sub

    Public Sub Sub_P2P_Version_Send(ByVal Target_IP As String)                                  'Function for sending a P2P version message to the network. Takes the destination IP by value as string as the parameter.

        Sub_Console_Print("Sending Version Send message to " & Target_IP, "Main", "Black")      'Sends a message to the console subroutine saying a version message is being sent.

        If Func_TCP_Ping("000 " & Version, Target_IP) = False Then                              'If the result of the Func_TCP_Ping subroutine passing a string, the Version variable and the Target_IP variable as parameters is false.

            Check_Count = Check_Count + 1                                                       'Sets the Check_Count variable equal to itself plus 1.
            Sub_Sync00()                                                                        'Executes the Sub_Sync00 subroutine.

        End If

    End Sub

    Public Sub Sub_P2P_Version_Reply(ByVal Target_IP As String)                                 'Function for sending a P2P version reply message to the network. Takes the destination IP by value as string as the parameter.

        Sub_Console_Print("Sending Version Reply message to " & Target_IP, "Main", "Black")     'Sends a message to the console subroutine saying a version reply message is being sent.

        Func_TCP_Send("001 ", Target_IP)                                                        'Executes the Sub_TCP_Send subroutine passing a string and the Target_IP variable as parameters.

    End Sub

    Public Sub Sub_P2P_Get_Address(ByVal IP As String, ByVal Amount As Integer)                 'Subroutine for sending a P2P get address message to the network. Takes the destination IP and the amount by value as a string as the parameters.

        Sub_Console_Print("Sending Get Address message to: " & IP, "Main", "Black")             'Sends a message to the console subroutine saying a get address message is being sent.
        Func_TCP_Send("002 " & Amount, IP)                                                      'Executes the Func_TCP_Send subroutine passing a string, the Amount and the IP variable as parameters.

    End Sub

    Public Sub Sub_P2P_Address(ByVal IP As String, ByVal Message As String)                     'Subroutine for sending a P2P get address message to the network. Takes the destination IP and message by value as parameters.

        Sub_Console_Print("Sending an Address message to: " & IP, "Main", "Green")              'Sends a message to the console subroutine saying an address message is being sent.
        Func_TCP_Send("003 " & Message, IP)                                                     'Executes the Func_TCP_Send subroutine passing a string, the Message and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Get_Height(ByVal IP As String)                                           'Subroutine for sending a P2P get height message to the network. Takes the destination IP by value as a parameter.

        Sub_Console_Print("Sending Get Height message to: " & IP, "Main", "Black")              'Sends a message to the console subroutine saying a get height message is being sent.
        Func_TCP_Send("004 ", IP)                                                               'Executes the Func_TCP_Send subroutine passing a string and IP variable as parameters.

    End Sub

    Public Sub Sub_P2P_Height(ByVal IP As String, ByVal Height As String)                       'Subroutine for sending a P2P height message to the network. Takes the destination IP and height by value as parameters.

        Sub_Console_Print("Sending a Height message to: " & IP, "Main", "Green")                'Sends a message to the console subroutine saying a height message is being sent.
        Func_TCP_Send("005 " & Height, IP)                                                      'Executes the Func_TCP_Send subroutine passing the height variable and IP as parameters.

    End Sub

    Public Sub Sub_P2P_Get_Block(ByVal IP As String, ByVal Block_Height As Integer)             'Subroutine for sending a P2P get block message to the network. Takes the destination IP and block height by value as string as the parameter.

        Sub_Console_Print("Sending a Get Block message to: " & IP, "Main", "Green")             'Sends a message to the console subroutine saying a get block message is being sent.
        Func_TCP_Send("006 " & Block_Height, IP)                                                'Executes the Func_TCP_Send subroutine passing the block height and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Transaction(ByVal IP As String, ByVal Transaction As Transaction)    'Subroutine for sending a P2P transaction message to the network. Takes the destination IP by value as string and the transaction by value as transaction as the parameters.

        Sub_Console_Print("Sending a Transaction message", "Main", "Green")                 'Sends a message to the console subroutine saying a transaction message is being sent.

        Dim Serial_Stream As New MemoryStream                                               'Object variable to store a memory stream.
        Dim Byte_Formatter As New BinaryFormatter                                           'Object variable to store a binary formatter.

        Byte_Formatter.Serialize(Serial_Stream, Transaction)                                'Serializes the Transaction object to the Serial_Stream memory stream.

        Dim Serial_String As String = System.Convert.ToBase64String(Serial_Stream.ToArray)  'Converts the Serial_Stream memory stream to base 64 and stores in the variable Serial_String.
        Serial_Stream.Close()                                                               'Closes the Serial_Stream memory stream.
        Func_TCP_Send("008 " & Serial_String, IP)                                           'Executes the Func_TCP_Send subroutine passing the Serial_String and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Block(ByVal IP As String, ByVal Block As Block)                          'Subroutine for sending a P2P block message to the network. Takes the destination IP by value as string and block by value as block as the parameters.

        Sub_Console_Print("Sending a Block message to: " & IP, "Main", "Green")                 'Sends a message to the console subroutine saying a block message is being sent.

        Dim Serial_Stream As New MemoryStream                                                   'Object variable to store a memory stream.
        Dim Byte_Formatter As New BinaryFormatter                                               'Object variable to store a binary formatter.

        Byte_Formatter.Serialize(Serial_Stream, Block)                                          'Serializes the Block object to the Serial_Stream memory stream.

        Dim Serial_String As String = System.Convert.ToBase64String(Serial_Stream.ToArray)      'Converts the Serial_Stream memory stream to base 64 and stores in the string variable Serial_String.
        Serial_Stream.Close()                                                                   'Closes the Serial_Stream memory stream.

        Func_TCP_Send("007 " & Serial_String, IP)                                               'Executes the Func_TCP_Send subroutine passing the Serial_String and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Ping(ByVal IP As String)                                                 'Subroutine for sending a P2P ping message to the network. Takes the destination IP by value as string as the parameter.

        Sub_Console_Print("Sending a Ping message to: " & IP, "Main", "Green")                  'Sends a message to the console subroutine saying a ping message is being sent.
        Func_TCP_Ping("009 ", IP)                                                               'Executes the Func_TCP_Ping subroutine passing the block height and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Pong(ByVal IP As String)                                                 'Subroutine for sending a P2P pong message to the network. Takes the destination IP by value as string as the parameter.

        Sub_Console_Print("Sending a Pong message to: " & IP, "Main", "Green")                  'Sends a message to the console subroutine saying a pong message is being sent.
        Func_TCP_Send("010 ", IP)                                                               'Executes the Func_TCP_Send subroutine passing the block height and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Block_Hash(ByVal IP As String)                                           'Subroutine for sending a P2P block hash message to the network. Takes the destination IP by value as string as the parameter.

        Sub_Console_Print("Sending a Block Hash message to: " & IP, "Main", "Green")            'Sends a message to the console subroutine saying a block hash message is being sent.

        Dim Hash As String = ""                                                                 'Variable to store the hash to be sent.
        Dim Scan_Block As Block = Func_Block_Read(Func_Calculate_Height)                        'Object variable block to store the highest block on the local blockchain.

        Hash = Scan_Block.Func_Header_Hash                                                      'Sets the Hash variable equal to the result of the Func_Header_Hash method of the Scan_Block object.
        Func_TCP_Send("011 " & Hash, IP)                                                        'Executes the Func_TCP_Send subroutine passing the block height and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Block_Data(ByVal IP As String, ByVal Hash As String)                     'Subroutine for sending a P2P block data message to the network. Takes the destination IP and the hash by value as string as the parameters.

        Sub_Console_Print("Sending a Block Data message to: " & IP, "Main", "Green")            'Sends a message to the console subroutine saying a block data message is being sent.
        Func_TCP_Send("012 " & Hash, IP)                                                        'Executes the Func_TCP_Send subroutine passing the Hash and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Transaction_Hash(ByVal IP As String)                                     'Subroutine for sending a P2P transaction hash message to the network. Takes the destination IP by value as string as the parameter.

        Sub_Console_Print("Sending a Transaction Hash message to: " & IP, "Main", "Green")      'Sends a message to the console subroutine saying a transaction hash message is being sent.

        Dim Hash As String = ""                                                                 'Clears the Hash variable.

        Hash = Memory_Pool(Memory_Pool.Count - 1).Func_Transaction_Hash                         'Sets the Hash variable equal to the result of the Func_Transaction_Hash method of the item with index equal to the number of items in the Memory_Pool minus 1 of the Memory_Pool. 
        Func_TCP_Send("013 " & Hash, IP)                                                        'Executes the Func_TCP_Send subroutine passing the Hash and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Transaction_Data(ByVal IP As String, ByVal Hash As String)               'Subroutine for sending a P2P transaction data message to the network. Takes the destination IP and hash by value as string as the parameters.

        Sub_Console_Print("Sending a Transaction Data message to: " & IP, "Main", "Green")      'Sends a message to the console subroutine saying a transaction data message is being sent.
        Func_TCP_Send("014 " & Hash, IP)                                                        'Executes the Func_TCP_Send subroutine passing the Hash and IP variables as parameters.

    End Sub

    Public Sub Sub_P2P_Shutdown(ByVal IP As String)                                             'Subroutine for sending a P2P shutdown message to the network. Takes the destination IP by value as string as the parameter.

        Sub_Console_Print("Sending a Shutdown message to: " & IP, "Main", "Green")              'Sends a message to the console subroutine saying a shutdown message is being sent.
        Func_TCP_Ping("015 ", IP)                                                               'Executes the Func_TCP_Send subroutine passing the IP variable as the parameter.

    End Sub

    Public Sub Sub_P2P_Child_Version_Send(ByVal Target_IP As String)                            'Subroutine for sending a P2P child version send message to the network. Takes the destination IP by value as string as the parameter.

        Sub_Console_Print("Sending Child Version Send message to " & Target_IP, "Main", "Black") 'Sends a message to the console subroutine saying a child version send message is being sent.

        If Func_TCP_Ping("016 " & Version, Target_IP) = False Then                              'Executes the Sub_TCP_Send subroutine passing the Version and the Target_IP variables as parameters.

            Check_Count = Check_Count + 1                                                       'Sets the Check_Count variable equal to itself plus 1.
            Sub_Sync00()                                                                        'Executes the Sub_Sync00 subroutine.

        End If

    End Sub


    Public Sub Sub_Block_Hash(ByVal Message As String)                                          'Subroutine for processing block hash messages. Takes the message by value as string as a parameter.

        Dim Match As Boolean = False                                                            'Variable to store if a match between the hash and the contents of the hash database.
        Dim Hash As String = Message.Substring(4, Message.Length - 4)                           'Variable to store the hash from the message initialized as a substring of the message variable starting at index 4 with length equal to itself minus 4.

        For i = 0 To Hash_Database.Count - 1                                                    'For loop from i equals 0 to the number of items in the Hash_Database minus 1.

            If Hash = Hash_Database(i) Then                                                     'If the item at index i of the Hash_Database list is equal to the Hash variable.

                Match = True                                                                    'Sets the Match variable to true.
                Exit For                                                                        'Exits the for loop.

            End If

        Next i

        If Match = False Then                                                                   'If the Match variable equals false.

            Sub_P2P_Block_Data(Incoming_IPV4, Hash)                                             'Executes the Sub_P2P_Block_Data subroutine passing the Incoming_IPV4 and Hash variable as parameters.

        Else

            Sub_Console_Print("Block ignored, already in blockchain", "Main", "black")          'Sends a message to the console subroutine saying the block was ignored.

        End If

    End Sub

    Public Sub Sub_Block_Data(ByVal Message As String)                                          'Subroutine for processing block data messages. Takes the message by value as string as a parameter.

        Dim Index As Integer = 0                                                                'Variable to store the index of the hash in the hash database.
        Dim Hash As String = Message.Substring(4, Message.Length - 4)                           'Variable to store the hash from the message initialized as a substring of the message variable starting at index 4 with length equal to itself minus 4.

        For i = 0 To Hash_Database.Count - 1                                                    'For loop from i equals 0 to the number of items in the Hash_Database minus 1.

            If Hash = Hash_Database(i) Then                                                     'If the item at index i of the Hash_Database list is equal to the Hash variable.

                Index = i + 1                                                                   'Sets the Index variable equal to i plus 1.
                Exit For                                                                        'Exits the for loop.

            End If

        Next i

        Sub_P2P_Block(Incoming_IPV4, Func_Block_Read(Index))                                    'Executes the Sub_P2P_Block subroutine passing the Incoming_IPV4 variable and the block at height index as parameters.

    End Sub

    Public Sub Sub_Transaction_Hash(ByVal Message As String)                                    'Subroutine for processing transaction hash messages. Takes the message by value as string as a parameter.

        Dim Match As Boolean = False                                                            'Variable to store if a match between the hash and the contents of the memory pool has been found.
        Dim Hash As String = Message.Substring(4, Message.Length - 4)                           'Variable to store the hash from the message initialized as a substring of the message variable starting at index 4 with length equal to itself minus 4.

        For i = 0 To Memory_Pool.Count - 1                                                      'For loop from i equals 0 to the number of items in the Memory_Pool minus 1.

            If Hash = Memory_Pool(i).Func_Transaction_Hash Then                                 'If the result of the Func_Transaction_Hash method of the item at index i of the Memory_Pool is equal to thje Hash variable.

                Match = True                                                                    'Sets the Match variable to true.
                Exit For                                                                        'Exits the for loop.

            End If

        Next i

        If Match = False Then                                                                   'If the Match variable equals false.

            Sub_P2P_Transaction_Data(Incoming_IPV4, Hash)                                       'Executes the Sub_P2P_Transaction_Data subroutine passing the Incoming_IPV4 and Hash variable as parameters.

        Else

            Sub_Console_Print("Transaction ignored, already in memory pool", "Main", "black")   'Sends a message to the console subroutine saying the transaction was ignored.

        End If

    End Sub

    Public Sub Sub_Transaction_Data(ByVal Message As String)                                    'Subroutine for processing transaction data messages. Takes the message by value as string as a parameter.

        Dim Index As Integer = 0                                                                'Variable to store the index of the hash in the Memory_Pool.
        Dim Hash As String = Message.Substring(4, Message.Length - 4)                           'Variable to store the hash from the message initialized as a substring of the message variable starting at index 4 with length equal to itself minus 4.

        For i = 0 To Memory_Pool.Count - 1                                                      'For loop from i equals 0 to the number of items in the Memory_Pool minus 1.

            If Hash = Memory_Pool(i).Func_Transaction_Hash Then                                 'If the result of the Func_Transaction_Hash method of the item at index i of the Memory_Pool is equal to the Hash variable.

                Index = i                                                                       'Sets the Index variable equal to i.
                Exit For                                                                        'Exits the for loop.

            End If

        Next i

        Sub_P2P_Transaction(Incoming_IPV4, Memory_Pool(Index))                                  'Executes the Sub_P2P_Transaction subroutine passing the Incoming_IPV4 variable and the item at index Index of the Memory_Pool as parameters.

    End Sub

    Public Sub Sub_Transaction(ByVal Message As String)                                         'Subroutine for processing inbound transactions messages. Takes the message by value as string as a parameter.

        Dim Transaction_Data As String = Message.Substring(4, Message.Length - 4)               'Variable to store the transaction data. Initilized as the Message variable with the first 4 characters removed from it.
        Dim Inbound_Transaction As Object                                                       'Object variable to store the inbound transaction.

        Try                                                                                     'Starts a try catch block.

            Dim Byte_Formatter As New BinaryFormatter                                           'Object variable to store a binary formatter.
            Dim Byte_Data() As Byte = System.Convert.FromBase64String(Transaction_Data).ToArray 'Converts the Serial_String variable from base 64 and saves it into the Byte_Data variable.
            Dim Serial_Stream As New MemoryStream(Byte_Data)                                    'Object variable to store a memory stream, loads the Byte_Data into it.
            Inbound_Transaction = Byte_Formatter.Deserialize(Serial_Stream)                     'Transaction object is loaded with data deserialized from the memory stream.
            Serial_Stream.Close()                                                               'Closes the Serial_Stream.

        Catch EX As Exception                                                                   'Catches EX as an exception.

            Sub_Console_Print("ERROR: Something went wrong while deserializeing inbound transaction: " & EX.ToString, "Main", "red") 'Sends a message to the console subroutine saying something went wrong when deserializeing and displays the exception.
            Exit Sub                                                                            'Exits the subroutine.

        End Try

        Try                                                                                     'Starts a try catch block.

            Dim Validation_Result As Boolean = False                                            'Variable to store the result of the validation process.

            Validation_Result = Func_Validate_Transaction(Inbound_Transaction)                  'The Validation_Result variable is set equal to the result of the Func_Validate_Transaction function when the Inbound_Transaction object is passed as the parameter.

            If Validation_Result <> True Then                                                   'If the Validation_Result variable is not equal to true.

                Sub_Console_Print("ERROR: Transaction validation failed, transaction rejected", "Main", "red") 'Sends a message to the console subroutine saying the transaction failed validation.
                Sub_Console_Print(Validation_Error, "Main", "red")                              'Sends a message to the console subroutine displaying the Validation_Error variable.
                Exit Sub                                                                        'Exits the subroutine.

            End If

            Sub_Console_Print("Transaction valid", "Main", "green")                             'Sends a message to the console subroutine saying the transaction was valid.

        Catch EX As Exception                                                                   'Catches EX as an exception.

            Sub_Console_Print("ERROR: Something went wrong while validating inbound transaction: " & EX.ToString, "Main", "red") 'Sends a message to the console subroutine saying something went wrong when validating the transaction and displays the exception.
            Exit Sub                                                                            'Exits the subroutine.

        End Try

        Memory_Pool.Add(Inbound_Transaction)                                                    'The Inbound_Transaction object is added to the Memory_Pool.
        lblMemPool1.Text = Memory_Pool.Count                                                    'Sets the lblMemPool1 label to the number of items in the Memory_Pool.

        Unconfirmed_Balance = Func_Unconfirmed_Balance()                                        'Sets the Unconfirmed_Balance variable equal to the result of the Func_Unconfirmed_Balance function.
        txtUnBal.Text = Unconfirmed_Balance                                                     'Sets the txtUnBal text box equal to the Unconfirmed_Balance variable.

        For i = 0 To Active_Node_List.Count - 1                                                 'For loop from i equals 0 to the number of items in the Active_Node_List minus 1.

            If Active_Node_List(i) <> Incoming_IPV4 Then                                        'If the item at index i of the Active_Node_List is not equal to the Incoming_IPV4 variable.

                Sub_P2P_Transaction_Hash(Active_Node_List(i))                                   'Executes the Sub_P2P_Transaction_Hash subroutine passing the item at index i of the Active_Node_List as the parameter.

            End If

        Next i

        For i = 0 To Child_Node_List.Count - 1                                                  'For loop from i equals 0 to the number of items in the Child_Node_List minus 1.

            If Child_Node_List(i) <> Incoming_IPV4 Then                                         'If the item at index i of the Child_Node_List is not equal to the Incoming_IPV4 variable.

                Sub_P2P_Transaction_Hash(Child_Node_List(i))                                    'Executes the Sub_P2P_Transaction_Hash subroutine passing the item at index i of the Child_Node_List as the parameter.

            End If

        Next i

    End Sub

    Public Sub Sub_Block(ByVal Message As String)                                               'Subroutine for processing inbound block messages. Takes the message by value as string as a parameter.

        Dim Block_Data As String = Message.Substring(4, Message.Length - 4)                     'Variable to store the block data. Initilized as the Message variable with the first 4 characters removed from it.
        Inbound_Block = New Block                                                               'Object variable to store the inbound block.

        Try                                                                                     'Starts a try catch block.

            Dim Byte_Formatter As New BinaryFormatter                                           'Object variable to store a binary formatter.
            Dim Byte_Data() As Byte = System.Convert.FromBase64String(Block_Data).ToArray       'Converts the Serial_String variable from base 64 and saves it into the Byte_Data variable.
            Dim Serial_Stream As New MemoryStream(Byte_Data)                                    'Object variable to store a memory stream, loads the Byte_Data into it.
            Inbound_Block = Byte_Formatter.Deserialize(Serial_Stream)                           'The Inbound_Block object is loaded with data deserialized from the memory stream.
            Serial_Stream.Close()                                                               'Closes the Serial_Stream.

        Catch EX As Exception                                                                   'Catches EX as an exception.

            Sub_Console_Print("ERROR: Something went wrong while deserializeing inbound block: " & EX.ToString, "Main", "red") 'Sends a message to the console subroutine saying something went wrong when deserializeing and displays the exception.
            Exit Sub                                                                            'Exits the subroutine.

        End Try

        Try                                                                                     'Starts a try catch block.

            Dim Validation_Result As Boolean = False                                            'Variable to store the result of the validation process.

            Validation_Result = Func_Validate_Block(Inbound_Block)                              'The Validation_Result variable is set equal to the result of the Func_Validate_Block function when the Inbound_Block object is passed as the parameter.

            If Validation_Result <> True Then                                                   'If the Validation_Result variable is not equal to true.

                Sub_Console_Print("ERROR: Block validation failed, block rejected", "Main", "red") 'Sends a message to the console subroutine saying the block failed validation.
                Sub_Console_Print(Validation_Message & Validation_Error, "Main", "red")         'Sends a message to the console subroutine displaying the Validation_Message and Validation_Error variables.
                Exit Sub                                                                        'Exits the subroutine.

            End If

            Sub_Console_Print("Block valid", "Main", "green")                                   'Sends a message to the console subroutine saying the block is valid.

        Catch EX As Exception                                                                   'Catches EX as an exception.

            Sub_Console_Print("ERROR: Something went wrong while validating inbound block: " & EX.ToString, "Main", "red") 'Sends a message to the console subroutine saying something went wrong while validating the block and displays the exception.
            Exit Sub                                                                            'Exits the subroutine.

        End Try

        Dim Index As Integer = 0                                                                'Variable to store the index of transactions in the block.
        Dim Remove_List As New List(Of Transaction)                                             'Object variable list to store transactions that need to be removed from the Memory_Pool.

        For i = 1 To Inbound_Block.Transactions.Count - 1                                       'For loop from i equals 1 to the number of items in the Transactions list property of the Inbound_Block object minus 1.

            For i2 = 0 To Memory_Pool.Count - 1                                                 'For loop from i2 equals 0 to the number of items inb the Memory_Pool minus 1.

                If Memory_Pool(i2).Func_Transaction_Hash = Inbound_Block.Transactions(i).Func_Transaction_Hash Then 'If the result of the Func_Transaction_Hash method of the item at index i of the Transactions list property of the Inbound_Block object equals the result of the Func_Transaction_Hash method of the item at index i2 of the Memory_Pool.

                    Remove_List.Add(Memory_Pool(i2))                                            'Adds the item at index i2 of the Memory_Pool to the Remove_List.

                End If

            Next i2

        Next i

        For i = 0 To Remove_List.Count - 1                                                      'For loop from i equals 0 to the number of items in the Remove_List minus 1.

            Memory_Pool.Remove(Remove_List(i))                                                  'Removes the item at index i of the Remove_List from the Memory_Pool.

        Next i

        lblMemPool1.Text = Memory_Pool.Count                                                    'Sets the lblMemPool1 label to the number of items in the Memory_Pool.

        Func_Block_Write(Inbound_Block)                                                         'Executes the Func_Block_Write function passing the Inbound_Block object as the parameter.
        Sub_Hash_Database_Add(Func_Calculate_Height)                                            'Executes the Sub_Hash_Database_Add subroutine passing the local blockchain height as the parameter.
        Sub_UTXO(Func_Calculate_Height)                                                         'Executes the Sub_UTXO subroutine passing the height of the local blockchain as the parameter.

        Balance = Func_Balance_UTXO()                                                           'The Balance variable is set equal to the result of the Func_Balance_UTXO function.
        txtBal.Text = Balance                                                                   'The Balance variable is put into the txtBal text box.

        Abort = True                                                                            'Sets the Abort variable to true.
        Block_Missed = True                                                                     'Sets the Block_Missed variable to true.
        Thread_Worker01.CancelAsync()                                                           'Cancels the Thread_Worker01 background worker.

        Unconfirmed_Balance = Func_Unconfirmed_Balance()                                        'Sets the Unconfirmed_Balance variable equal to the result of the Func_Unconfirmed_Balance function.
        txtUnBal.Text = Unconfirmed_Balance                                                     'Sets the txtUnBal text box equal to the Unconfirmed_Balance variable.

        If Node_Online = False Then                                                             'If the Node_Online variable equals false.

            Sub_Sync04()                                                                        'Executes the Sub_Sync04 subroutine.
            Exit Sub                                                                            'Exits the subroutine.

        Else

            For i = 0 To Active_Node_List.Count - 1                                             'For loop from i equals 0 to the number of items in the Active_Node_List minus 1.

                If Active_Node_List(i) <> Incoming_IPV4 Then                                    'If the item at index i of the Active_Node_List is not equal to the Incoming_IPV4 variable.

                    Sub_P2P_Block_Hash(Active_Node_List(i))                                     'Executes the Sub_P2P_Block_Hash subroutine passing the item at indexi1 of the Active_Node_List as the parameter.

                End If

            Next i

            For i = 0 To Child_Node_List.Count - 1                                              'For loop from i equals 0 to the number of items in the Child_Node_List minus 1.

                If Child_Node_List(i) <> Incoming_IPV4 Then                                     'If the item at index i of the Child_Node_List is not equal to the Incoming_IPV4 variable.

                    Sub_P2P_Block_Hash(Child_Node_List(i))                                      'Executes the Sub_P2P_Block_Hash subroutine passing the item at index i of the Child_Node_List as the parameter.

                End If

            Next i

        End If

    End Sub


    Public Function Func_Local_IP() As String                                                   'Function that will return the local IPv4.

        Func_Local_IP = ""                                                                      'Clears the function variable.
        Local_PC_Name = System.Net.Dns.GetHostName()                                            'Makes the variable Local_PC_Name equal the clients PC's host name.

        Dim Host_IPs As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(Local_PC_Name)     'Stores network connection information from this nodes PC in the Host_IPs variable.

        For Each Addresses As System.Net.IPAddress In Host_IPs.AddressList                      'For loop that seaches through all elements (IP addresses) in Host_IPs variable.

            If Addresses.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then     'If an IP is the IPv4.

                Func_Local_IP = Addresses.ToString()                                            'Sets the fuction variable equal to the Addresses variable converted to a string.

            End If

        Next

    End Function

    Public Function Func_External_IP()                                                          'Function that will return the external IPv4.

        Dim IP As String = ""                                                                   'Variable to store the IP address.                                                                        
        Dim Ready As Boolean = False                                                            'Variable to store if the the IP string is ready to be returned.

        Try                                                                                     'Starts a try catch block.

            Using wc As New Net.WebClient                                                       'Starts wc as a new web client.

                IP = Encoding.ASCII.GetString(wc.DownloadData("http://tools.feron.it/php/ip.php")) 'Sets the IP variable equal to the ASCII string version of the HTTP data downloaded from the web address.

                Do Until Ready = True                                                           'Do loop until the Ready variable equals true.

                    If IP.Contains("?") Then                                                    'If the IP variable contains "?".

                        IP = IP.Substring(1, IP.Length - 1)                                     'Sets the IP variable equal to a substring of itself starting at index 1 with length equal to itself minus 1.

                    Else

                        Ready = True                                                            'Sets the Ready variable to true.

                    End If

                Loop

            End Using

        Catch EX As Exception                                                                   'Catches EX as an exception.

            Sub_Console_Print("Error getting external IPv4. Make sure node is connected to the internet or add external IPv4 manually using the console: " & EX.ToString, "Main", "red") 'Sends a message to the console subroutine saying there was an error getting the external IPv4.
            Return "ERROR"                                                                      'Returns the string "ERROR".

        End Try

        Return IP                                                                               'Returns the IP variable.

    End Function

    Public Function Func_TCP_Send(ByVal Message As String, ByVal Target_IP As String)           'Function for passing TCP messages to the network thread. Takes a message and an IP by value as strings as the parameters.

        Network_Queue.Enqueue(Message)                                                          'Adds the Message variable to the Network_Queue.
        IP_Queue.Enqueue(Target_IP)                                                             'Adds the Target_IP variable to the IP_Queue.

        Return True                                                                             'Returns true.

    End Function

    Public Sub Network_Thread_Send()                                                            'Subroutine that will execute on the network thread to send TCP messages.

        Dim Message As String = ""                                                              'Variable to store the message.
        Dim IP As String = ""                                                                   'Variable to store the destination IP.

        Do                                                                                      'Do loop.

            While Network_Queue.Count > 0                                                       'While loop whilst there are items in the Network_Queue.

                Message = Network_Queue.Dequeue                                                 'Sets the Message variable equal to the next item in the Network_Queue.
                IP = IP_Queue.Dequeue                                                           'Sets the IP variable equal to the next item in the IP_Queue.

                Try                                                                             'Starts a try catch block.

                    TCP_Client = New TcpClient(IP, Port)                                        'Starts a TCP client connection using the Target_IP and port variables.
                    Dim TCP_Writer As New System.IO.StreamWriter(TCP_Client.GetStream())        'Sets up a data stream for characters using System.IO and System.Net.Sockets types using the TCP client.
                    TCP_Writer.Write("</> " & Message & " <\>")                                 'Writes the Message variable to the data stream.
                    TCP_Writer.Flush()                                                          'Strings stored in the buffer are pushed onto the stream and sent; the buffer is then cleared

                Catch EX As Exception                                                           'Catches EX as an exception.

                    Exception_Message = EX.Message                                              'Sets the Exception_Message variable equal to the EX exception message.
                    Error_Pending = True                                                        'Sets the Error_Pending variable to true.

                End Try

            End While

            Thread.Sleep(50)                                                                    'Pauses the thread for 50 miliseconds.

        Loop

    End Sub

    Public Function Func_TCP_Ping(ByVal Message As String, ByVal Target_IP As String)           'Subroutine that will send TCP ping messages. Takes the message and target IP by values as string as the parameters.

        Try                                                                                     'Starts a try catch block.

            TCP_Client = New TcpClient(Target_IP, Port)                                         'Starts a TCP client connection using the Target_IP and port variables.
            Dim TCP_Writer As New System.IO.StreamWriter(TCP_Client.GetStream())                'Sets up a data stream for characters using System.IO and System.Net.Sockets types using the TCP client.
            TCP_Writer.Write("</> " & Message & " <\>")                                         'Writes the Message variable to the data stream.
            TCP_Writer.Flush()                                                                  'Strings stored in the buffer are pushed onto the stream and sent; the buffer is then cleared

            Return True                                                                         'Returns true.

        Catch EX As Exception                                                                   'Catches EX as an exception.

            Sub_Console_Print("ERROR with TCP connection: " & EX.Message, "Main", "red")        'Sends a message to the console subroutine saying there was an error while trying to make a TCP connection and displays the exception.

            Return False                                                                        'Returns false.

        End Try

    End Function

    Private Sub Sub_TCPTick() Handles TCPTimer.Tick                                             'Subroutine executed everytime the TCP timer ticks to manage incoming TCP connections.

        If Error_Pending = True Then                                                            'If the Error_Pending variable is true.

            Sub_Console_Print("ERROR with TCP connection from: " & Incoming_IPV4 & " " & Exception_Message, "Main", "red") 'Sends a message to the console subroutine saying the received message is not a part of the Halfcoin protocol.
            Exception_Message = ""                                                              'Clears the Exception_Message.
            Error_Pending = False                                                               'Sets the Error_Pending variable to false.

        End If

        Dim Message As String = ""                                                              'Variable to store the incoming message.
        Dim nStart As Integer = 0                                                               'Variable to store the index of the first character of the message.
        Dim nLast As Integer = 0                                                                'Variable to store the index of the last character of the message.

        Try                                                                                     'Starts a try and catch block.

            If TCP_Listener.Pending = True Then                                                 'If there is a network request waiting.

                Message = ""                                                                    'Clears the Message variable.
                TCP_Client = TCP_Listener.AcceptTcpClient()                                     'The waiting TCP connection is accepted.
                Dim Reader As New System.IO.StreamReader(TCP_Client.GetStream())                'Reads the data in the data stream from the established connection.
                Incoming_IPV4 = Func_Remove_Port(TCP_Client.Client.RemoteEndPoint.ToString())   'Gets the IP of the sender and saves it into the Incoming_IPV4 variable after removing its port.

                While Reader.Peek > -1                                                          'While there are characters being transmitted across the TCP stream.

                    Message &= Convert.ToChar(Reader.Read()).ToString                           'Converts the data stream to unicode characters then puts it into a string.

                End While

                If Message.Contains("</>") Then                                                 'If the Message variable contains "</>".

                    nStart = InStr(Message, "</>") + 4                                          'Finds the index of "</>" in the Message variable plus 4 and stores it in the variable nStart.
                    nLast = InStr(Message, "<\>") - 1                                           'Finds the index of "<\>" in the Message variable minus 1 and stores it in the variable nLast.
                    Message = Mid(Message, nStart, nLast - nStart)                              'Makes the Message variable equal a substring of itself with the start index equal to the nStart variable and the total length equal to the nLast-Nstart variables..

                Else

                    Sub_Console_Print("ERROR with inbound connection: Message from IP: " & Incoming_IPV4 & " does not conform to Halfcoin protocol", "Main", "red") 'Sends a message to the console subroutine saying the received message is not a part of the Halfcoin protocol.
                    Exit Sub                                                                    'Exits the subroutine.

                End If

                Sub_P2P_Inbound(Message)                                                        'Executes the Sub_P2P_Inbound subroutine passing the Message variable as the parameter.

            End If

        Catch EX As Exception                                                                   'Catches EX as an exception.

            Sub_Console_Print("ERROR with inbound connection: " & EX.ToString, "Main", "red")   'Sends a message to the console subroutine saying there was an error with the connection and displays the exception.

        End Try

    End Sub

#End Region

#Region "Transaction And Block Validation"                                                          'Region containing code for creating transactions and validating transactions and blocks.

    Public Sub Sub_Create_Transaction()                                                             'Subroutine for creating transactions.

        MsgBox("Processing transaction.")                                                           'Displays a message box saying the transaction will now be processed.

        New_Transaction = New Transaction                                                           'Object variable transaction for stroing the new transaction.

        New_Transaction.Version = Version                                                           'Sets the Version property of the New_Transaction object to the Version variable.

        Dim New_Input As TXT_Input                                                                  'Object variable TXT_Input to store the input to the new transaction being created.
        Dim Cost_Met As Boolean = False                                                             'Variable to store weather the value of UTXO's meets the value of the transaction.
        Dim Value_Required As Integer = Transaction_Value + Transaction_Fee                         'Variable to store the total value of the transaction. Initilized by summing the Transaction_Value and Transaction_Fee variables.
        Dim Change As Integer = 0                                                                   'Variable to store the amount of change returned to the user.
        Dim In_Pool As Boolean = False                                                              'Variable to store if UTXO's are used in the memory pool.

        Dim Temp_Total As Integer = 0                                                               'Variable to store the balance temporarly as it is added up.
        Dim Inspect_Script As String = ""                                                           'Variable to store the locking script of UTXO's.
        Dim Inspect_Key As String = ""                                                              'Variable to store the public key extracted from the UTXO's locking scripts.

        Dim Transaction_Hash_List As New List(Of String)                                            'Object variable list to store the hash of UTXO used for this transaction.
        Dim Transaction_Index_List As New List(Of Integer)                                          'Object variable list to store the index of UTXO used for this transaction.
        Dim Transaction_Value_List As New List(Of Integer)                                          'Object variable list to store the value of UTXO used for this transaction.
        Dim Transaction_Script_List As New List(Of String)                                          'Object variable list to store the locking script of UTXO used for this transaction.

        Sub_Console_Print("Assembling UTXO's...", "Main", "Black")                                  'Sends a message to the console subroutine saying UTXO's are being assembled.

        For i = 0 To UTXO_Hash_List.Count - 1                                                       'For loop from i equals 0 to the number of items in the UTXO_Hash_List minus 1.

            Inspect_Script = UTXO_Script_List(i)                                                    'The Inspect_Script variable equals the item at index i of the UTXO_Script_List.
            Inspect_Key = Inspect_Script.Substring(0, Inspect_Script.IndexOf(","))                  'The Inspect_Key variable equals a substring of the Inspect_Script variable starting at index 0 with length equal to the index of "," of the Inspect_Script variable.

            If Inspect_Key = Public_Key Then                                                        'If the Inspect_Key variable equals the Public_Key variable.

                For i2 = 0 To Memory_Pool.Count - 1                                                 'For loop from i2 equals 0 to the number of items in the Memory_Pool minus 1.

                    For i3 = 0 To Memory_Pool(i2).Inputs.Count - 1                                  'For loop from i3 equals 0 to the number of items in the Inputs list property of the item at index i2 of the Memory_Pool.

                        If Memory_Pool(i2).Inputs(i3).TXT_Hash = UTXO_Hash_List(i) Then             'If the item at index i of the UTXO_Hash_List equals the TXT_Hash property of the item at index i3 of the Inputs list property of the of the item at index i2 of the Memory_Pool.

                            If Memory_Pool(i2).Inputs(i3).Index = UTXO_Index_List(i) Then           'If the item at index i of the UTXO_Index_List equals the Index property of the item at index i3 of the Inputs list property of the of the item at index i2 of the Memory_Pool.

                                In_Pool = True                                                      'Sets the In_Pool variable to true.

                            End If

                        End If

                    Next i3

                Next i2

                If In_Pool = False Then                                                             'If the In_Pool variable is false.

                    Transaction_Hash_List.Add(UTXO_Hash_List(i))                                    'The item at index i of the UTXO_Hash_List is added to the Transaction_Hash_List.
                    Transaction_Index_List.Add(UTXO_Index_List(i))                                  'The item at index i of the UTXO_Index_List is added to the Transaction_Index_List.
                    Transaction_Value_List.Add(UTXO_Value_List(i))                                  'The item at index i of the UTXO_Value_List is added to the Transaction_Value_List.
                    Transaction_Script_List.Add(UTXO_Script_List(i))                                'The item at index i of the UTXO_Script_List is added to the Transaction_Script_List.

                    Temp_Total = Temp_Total + UTXO_Value_List(i)                                    'The Temp_Balance variable equals itself plus the item at index i of the UTXO_Value_List.

                End If

                In_Pool = False                                                                     'Sets the In_Pool variable to false.

                If Temp_Total >= Value_Required Then                                                'If the Temp_Total variable is greater then or equal to the Value_Required variable.

                    Cost_Met = True                                                                 'The Cost_Met variable is set to true.

                    Exit For                                                                        'Exits the for loop.

                End If

            End If

        Next i

        If Cost_Met = True Then                                                                     'If the Cost_Met variable equals true.

            Sub_Console_Print("UTXO's found", "Main", "Green")                                      'Sends a message to the console subroutine saying UTXO's have been found.
            Change = Temp_Total - Value_Required                                                    'The Change variable equals the Temp_Total variabe minus the Value_Required variable.

        Else

            If Unconfirmed_Balance <> 0 Then                                                        'If the Unconfirmed_Balance variable is not equal to 0.

                Sub_Console_Print("Transaction could not be completed as UTXO's are still pending", "Main", "Red") 'Sends a message to the console subroutine saying the UTXO's are still pending.
                MsgBox("Please wait until pending funds have been confirmed before attempting this transaction. This can take between 2-10 minutes.") 'Displays a message box saying the user must wait for pending funds to confirm.
                Exit Sub                                                                            'Exits the subroutine.

            Else

                Sub_Console_Print("Error, not enough funds", "Main", "Red")                         'Sends a message to the console subroutine saying the user does not have enough funds.
                MsgBox("Error, you do not posses the fund to submit this transaction.")             'Displays a message box saying the user does not posses the fund to make the transaction.
                Exit Sub                                                                            'Exits the subroutine.

            End If

        End If

        Sub_Console_Print("Generating unlocking scripts...", "Main", "Black")                       'Sends a message to the console subroutine saying unlocking scripts are being generated.

        Dim Inspect_For_Script As String = ""                                                       'Variable to store the locking script of the UTXO being inspected.
        Dim Inspect_For_Address As String = ""                                                      'Variable to store the address inside the locking script of the UTXO being inspected.
        Dim Inspect_Nonce As Integer = 0                                                            'Variable to store the nonce section of the address inside the locking script of the UTXO being inspected.
        Dim Unlocking_Data As String = ""                                                           'Variable to store the unlocking script as it is being generated.

        For i = 0 To Transaction_Hash_List.Count - 1                                                'For loop from i equals 0 to the number of items in the Transaction_Hash_List.Count minus 1.

            New_Input = New TXT_Input                                                               'Object variable TXT_Input to store the input to the new transaction being created.

            New_Input.TXT_Hash = Transaction_Hash_List(i)                                           'The TXT_Hash property of the New_Input object is made equal to the item at index i of the Transaction_Hash_List.
            New_Input.Index = Transaction_Index_List(i)                                             'The Index property of the New_Input object is made equal to the item at index i of the Transaction_Index_List.

            Inspect_For_Script = Transaction_Script_List(i)                                         'The Inspect_For_Script variable is made equal to the item at index i of the Transaction_Script_List.
            Inspect_For_Script = Inspect_For_Script.Substring(Inspect_For_Script.IndexOf(",") + 1, Inspect_For_Script.Length - Inspect_For_Script.IndexOf(",") - 1) 'The Inspect_For_Script variable equals a substring of the Inspect_For_Script variable starting at the first index of "," in the string plus 1 with length equal to its original length minus the first index of "," in the string minus 1.
            Inspect_For_Address = Inspect_For_Script.Substring(0, Inspect_For_Script.IndexOf(","))  'The Inspect_For_Address variable is made equal to the substring of the Inspect_For_Script variable starting at index 0 with length equal to the first index of "," in the string.
            Inspect_Nonce = Inspect_For_Address.Substring(64, Inspect_For_Address.Length - 64)      'The Inspect_Nonce variable is made equal to the substring of the Inspect_For_Address variable starting at index 64 with length equal to itself minus 64.

            Unlocking_Data = sha256(Private_Key & Inspect_Nonce) & ","                              'The Unlocking_Data variable is equal to the hash of the Private_Key and Inspect_Nonce variables with "," appened to the end.

            New_Input.Unlocking_Script = Unlocking_Data                                             'The Unlocking_Script property of the New_Input object is made equal to the Unlocking_Data variable.

            New_Transaction.Inputs.Add(New_Input)                                                   'The New_Input object is added to the Inputs list property of the New_Transaction object.

        Next i

        Sub_Console_Print("Scripts created successfuly", "Main", "Green")                           'Sends a message to the console subroutine saying the scripts have been made successfully.
        Sub_Console_Print("Generating locking scripts...", "Main", "Black")                         'Sends a message to the console subroutine saying locking scripts are being generated.

        Dim New_Output As New TXT_Output                                                            'Object variable TXT_Output to store the outputs of the new transaction being created.
        Dim Locking_Data As String = ""                                                             'Variable to store the locking script as its being generated.

        New_Output.Value = Value_Required - Transaction_Fee                                         'The Value property of the New_Output object is made equal to the Value_Required variable minus the Transaction_Fee variable.

        Locking_Data = Recipient_Public_Key & "," & Recipient_Address & "," & "Op_Check_Salt,"      'The Locking_Data variable is made equal to the Recipient_Public_Key variable, Recipient_Address variable and the string "Op_Check_Salt," all appened together with a "," seperating each item.
        New_Output.Locking_Script = Locking_Data                                                    'The Locking_Script property of the New_Output object is made equal to the Locking_Data variable.

        New_Transaction.Outputs.Add(New_Output)                                                     'The New_Output object is added to the Outputs list property of the New_Transaction object.

        If Change > 0 Then                                                                          'If the Change variable is greater then 0.

            Sub_Console_Print("Creating change...", "Main", "Black")                                'Sends a message to the console subroutine saying change is being created.

            Dim Change_Output As New TXT_Output                                                     'Object variable TXT_Output to store the information of the change being returned to the owner.

            Change_Output.Value = Change                                                            'The value property of the Change_Output object is made equal to the Change variable.

            Func_New_Address()                                                                      'Executes the Func_New_Address function.

            Locking_Data = Public_Key & "," & Address & "," & "Op_Check_Salt,"                      'The Locking_Data variable is made equal to the Public_Key variable, Address Variable and the string "Op_Check_Salt," all appened together with a "," seperating each item.
            Change_Output.Locking_Script = Locking_Data                                             'The Locking_Script property of the Change_Output object is made equal to the Locking_Data variable.

            New_Transaction.Outputs.Add(Change_Output)                                              'The Change_Output object is added to the Outputs property of the New_Transaction object.

            Sub_Console_Print("Change: " & Change, "Main", "Green")                                 'Sends a message to the console subroutine saying displaying the Change variable.

        End If

        Sub_Console_Print("Scripts created successfuly", "Main", "Green")                           'Sends a message to the console subroutine saying the scripts have been created successfully.

        Sub_Console_Print("Transaction complete", "Main", "Green")                                  'Sends a message to the console subroutine saying the transaction is complete.
        Sub_Console_Print("Transmitting transaction", "Main", "Black")                              'Sends a message to the console subroutine saying the transaction is now being transmitted across the network.

        Memory_Pool.Add(New_Transaction)                                                            'The New_Transaction object is added to the Memory_Pool object.
        lblMemPool1.Text = Memory_Pool.Count                                                        'Sets the lblMemPool1 label to the number of items in the Memory_Pool.

        Unconfirmed_Balance = Func_Unconfirmed_Balance()                                            'Sets the Unconfirmed_Balance variable equal to the result of the Func_Unconfirmed_Balance subroutine.
        txtUnBal.Text = Unconfirmed_Balance                                                         'Sets the txtUnBal text box equal to the Unconfirmed_Balance variable.

        For i = 0 To Active_Node_List.Count - 1                                                     'For loop from i equals 0 to the number of items in the Active_Node_List minus 1.

            Sub_P2P_Transaction_Hash(Active_Node_List(i))                                           'Executes the Sub_P2P_Transaction_Hash subroutine passing the item at index i of the Active_Node_List as the parameter.

        Next i

        For i = 0 To Child_Node_List.Count - 1                                                      'For loop from i equals 0 to the number of items in the Child_Node_List minus 1.

            Sub_P2P_Transaction_Hash(Child_Node_List(i))                                            'Executes the Sub_P2P_Transaction_Hash subroutine passing the item at index i of the Child_Node_List as the parameter.

        Next i


    End Sub

    Public Function Func_Validate_Transaction(ByVal TXT As Transaction)                             'Function for validating inbound transactions. Takes a Transaction object as a parameter.

        Sub_Console_Print("Validating transaction...", "Main", "black")                             'Sends a message to the console subroutine saying the inbound transaction is being validated.

        Validation_Error = ""                                                                       'Sets the Validation_Error variable to nothing.

        Dim Outputs_Total As Integer = 0                                                            'Variable to store the total of all of the outputs.
        Dim Check_Counter As Integer = 0                                                            'Variable to store the number of inputs checked against the UTXO database.
        Dim Index As Integer = 0                                                                    'Variable to store indexes of inputs, outputs and transactions.
        Dim Inputs_Total As Integer = 0                                                             'Variable to store the total of all the inputs.
        Dim Script_Result As Boolean = False                                                        'Variable to store the result of the script execution engine.
        Dim Value As Integer = 0                                                                    'Variable to temporarly store values being checked.

        If TXT.Version = "" Then                                                                    'If the Version property of the TXT object is set to nothing.

            Validation_Error = "Error in version field: No version"                                 'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf TXT.Version < Version Then                                                           'Else if the Version property of the TXT object is smaller then the Version variable.

            Validation_Error = "Error in version field: Version too old"                            'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Version check OK", "Main", "Green")                                      'Sends a message to the console subroutine saying the version check was ok.

        If TXT.Inputs.Count <= 0 Then                                                               'If the number of items in the Inputs property list of the TXT object is smaller or equal to zero.

            Validation_Error = "Error in input count"                                               'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf TXT.Outputs.Count <= 0 Then                                                          'If the number of items in the Outputs property list of the TXT object is smaller or equal to zero.

            Validation_Error = "Error in output count"                                              'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Input output count OK", "Main", "Green")                                 'Sends a message to the console subroutine saying the input output count was ok.

        For i = 0 To TXT.Outputs.Count - 1                                                          'For loop from i equals 0 to the number of items in the Outputs property list of the TXT object minus 1.

            If TXT.Outputs(i).Value <= 0 Then                                                       'If the Value property of the item at index i of the Outputs property list of the TXT object is smaller then or equal to 0. 

                Validation_Error = "Error in value: Value too small"                                'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            ElseIf TXT.Outputs(i).Value > 2500000000 Then                                           'Else if the Value property of the item at index i of the outputs property list of the TXT object is greater then 2500000000. 

                Validation_Error = "Error in value: Value too large"                                'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

            Outputs_Total = Outputs_Total + TXT.Outputs(i).Value                                    'The Outputs_Total variable equals itself plus the Value property of the item at index i of the Outputs list property of the TXT object.

        Next i

        Sub_Console_Print("Output value check OK", "Main", "Green")                                 'Sends a message to the console subroutine saying the output value check was ok.

        If Outputs_Total <= 0 Then                                                                  'If the Outputs_Total variable is smaller then or equal to 0.

            Validation_Error = "Error in total: Output total to small"                              'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf Outputs_Total > 2500000000 Then                                                      'Else if the Outputs_Total variable is greater then 2500000000.

            Validation_Error = "Error in total: Output total too large"                             'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Output total value check OK", "Main", "Green")                           'Sends a message to the console subroutine saying the output total value check was ok.

        For i = 0 To TXT.Inputs.Count - 1                                                           'For loop from i equals 0 to the number of items in the Inputs list property of the TXT object minus 1.

            If TXT.Inputs(i).TXT_Hash = "Coinbase" Then                                             'If the TXT_Hash property of the item at index i of the Inputs property list of the TXT object is equal to the string "Coinbase".

                Validation_Error = "Error in format: Transaction from the coinbase"                 'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            ElseIf TXT.Inputs(i).TXT_Hash = "" Then                                                 'Else if the TXT_Hash property of the item at index i of the Inputs property list of the TXT object is equal to nothing.

                Validation_Error = "Error in format: No hash found"                                 'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

        Next i

        Sub_Console_Print("Input check OK", "Main", "Green")                                        'Sends a message to the console subroutine saying the input check was ok.

        For i = 0 To Memory_Pool.Count - 1                                                          'For loop from i equals 0 to the number of items in the Memory_Pool list minus 1.

            If Memory_Pool(i).Func_Transaction_Hash = TXT.Func_Transaction_Hash Then                'If the result of the Func_Transaction_Hash method of the item at index i of the Memory_Pool list is equal to the Func_Transaction_Hash method of the TXT object.

                Validation_Error = "Error: Transaction already in memory pool"                      'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

        Next i

        Sub_Console_Print("Already in pool check OK", "Main", "Green")                              'Sends a message to the console subroutine saying the already in memory pool check was ok.

        For i = 0 To Memory_Pool.Count - 1                                                          'For loop from i equals 0 to the number of items in the Memory_Pool list minus 1.

            For i2 = 0 To Memory_Pool(i).Inputs.Count - 1                                           'For loop from i2 equals 0 to the number of items in the Inputs list property of the item at index i of the Memory_Pool list minus 1.

                For i3 = 0 To TXT.Inputs.Count - 1                                                  'For loop from i3 equals 0 to the number of items in the Inputs list property of the TXT object minus 1.

                    If Memory_Pool(i).Inputs(i2).TXT_Hash = TXT.Inputs(i3).TXT_Hash Then            'If the TXT_Hash property of the item at index i2 of the Inputs list property of the item at index i of the Memory_Pool list is equal to the TXT_Hash property of the item at index i3 of the Inputs property of the TXT object.

                        If Memory_Pool(i).Inputs(i2).Index = TXT.Inputs(i3).Index Then              'If the Index property of the item at index i2 of the Inputs list property of the item at index i of the Memory_Pool list is equal to the Index property of the item at index i3 of the Inputs property of the TXT object.

                            Validation_Error = "Error with outputs: Already used by transactions in memory pool" 'Sets the Validation_Error variable to a string containing information about the error in validation.
                            Return False                                                            'Returns the boolean result false.

                        End If

                    End If

                Next i3

            Next i2

        Next i

        Sub_Console_Print("Outputs used in pool transactions check OK", "Main", "Green")            'Sends a message to the console subroutine saying the outputs already used in pool check was ok.

        'Check if transaction exists for all input references.

        For i = 0 To TXT.Inputs.Count - 1                                                           'For loop from i equals 0 to the number of items in the Inputs list property of the TXT object minus 1.

            For i2 = 0 To UTXO_Hash_List.Count - 1                                                  'For loop from i2 equals 0 to the number of items in the UTXO_Hash_List minus 1.

                If UTXO_Hash_List(i2) = TXT.Inputs(i).TXT_Hash Then                                 'If the item at index i2 of the UTXO_Hash_List equals the TXT_Hash property of the item at index i of the Inputs list property of the TXT object.

                    If UTXO_Index_List(i2) = TXT.Inputs(i).Index Then                               'If the item at index i2 of the UTXO_Index_List equals the Index property of the item at index i of the Inputs list property of the TXT object.

                        Check_Counter = Check_Counter + 1                                           'The Check_Counter variable equals itself plus 1.
                        Exit For                                                                    'Exits the for loop.

                    End If

                End If

            Next i2

        Next i

        If Check_Counter = TXT.Inputs.Count Then                                                    'If the Check_Counter variable equals the number of items in the Inputs list property of the TXT object. 

        Else

            Validation_Error = "Error with outputs: UTXO's do not exist/have already been spent"    'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Outputs exist in UTXO pool check OK", "Main", "Green")                   'Sends a message to the console subroutine saying the outputs exist check was ok.

        For i = 0 To TXT.Inputs.Count - 1                                                           'For loop from i equals 0 to the number of items in the Inputs list property of the TXT object minus 1.

            Value = Func_Input_Value(TXT.Inputs(i).TXT_Hash, TXT.Inputs(i).Index)                   'The Value variable equals the result of the Func_Input_Value function with the TXT_Hash property of the the item at index i of the Inputs list property of the TXT object and the Index property of the item at index i of the Inputs property of the TXT object as parameters.

            If Value <= 0 Then                                                                      'If the Value variable is smaller the or equal to 0.

                Validation_Error = "Error in value: Value too small"                                'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            ElseIf Value > 2500000000 Then                                                          'Else if the Value variable is greater then 2500000000.

                Validation_Error = "Error in value: Value too large"                                'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

            Inputs_Total = Inputs_Total + Value                                                     'The Inputs_Total variable is set equal to itself plus the Value variable.

        Next i

        Sub_Console_Print("Inputs value check OK", "Main", "Green")                                 'Sends a message to the console subroutine saying the input value check was ok.

        If Inputs_Total <= 0 Then                                                                   'If the Inputs_Total variable is smaller then or equal to 0.

            Validation_Error = "Error in total: Total too small"                                    'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf Inputs_Total > 2500000000 Then                                                       'Else if the Inputs_Total variable is greater then 2500000000.

            Validation_Error = "Error in total: Total too large"                                    'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Inputs total value check OK", "Main", "Green")                           'Sends a message to the console subroutine saying the inputs total check was ok.

        If Inputs_Total < Outputs_Total Then                                                        'If the Inputs_Total variable is smaller then the Outputs_Total variable.

            Validation_Error = "Error in total: Outputs exceed inputs"                              'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Outputs smaller then inputs check OK", "Main", "Green")                  'Sends a message to the console subroutine saying the outputs greater then inputs check was ok..

        If Inputs_Total - Outputs_Total < 10 Then                                                   'If the Inputs_Total variable minus the Outputs_Total variable is smaller then 10.

            Validation_Error = "Error with fee: Minimum transaction fee requirment not met"         'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Transaction fee check OK", "Main", "Green")                              'Sends a message to the console subroutine saying the transaction fee check was ok.

        For i = 0 To TXT.Inputs.Count - 1                                                           'For loop from i equals 0 to the number of items in the Inputs list property of the TXT object minus 1.

            For i2 = 0 To UTXO_Hash_List.Count - 1                                                  'For loop from i2 equals 0 to the nuber of items in the UTXO_Hash_List minus 1.

                If UTXO_Hash_List(i2) = TXT.Inputs(i).TXT_Hash Then                                 'If the item at index i2 of the UTXO_Hash_List equals the TXT_hash property of the item at index i of the Inputs property of the TXT object.

                    If UTXO_Index_List(i2) = TXT.Inputs(i).Index Then                               'If the item at index i2 of the UTXO_Index_List equals the Index property of the item at index i of the Inputs list property of the TXT object.

                        Script_Result = Func_Script_Validation(TXT.Inputs(i).Unlocking_Script, UTXO_Script_List(i2)) 'The Script_Result variable is set equal to the result returned when executing the Func_Script_Validation function passing the Ulocking_Script property of the item at index i of the Inputs list property of the TXT object and the item at index i2 of the UTXO_Script_List as parameters.

                        If Script_Result <> True Then                                               'If the Script_Result variable is not equal to true.

                            Validation_Error = "Error in script execution: Execution failure"       'Sets the Validation_Error variable to a string containing information about the error in validation.
                            Return False                                                            'Returns the boolean result false.

                        Else

                            Exit For                                                                'Exits the for loop

                        End If

                    End If

                End If

            Next i2

        Next i

        Sub_Console_Print("Script validation OK", "Main", "Green")                                  'Sends a message to the console subroutine saying the script execution was successful.
        Sub_Console_Print("Transaction validation complete", "Main", "Green")                       'Sends a message to the console subroutine saying the transaction validation was complete.

        Return True                                                                                 'Returns the boolean result true.

    End Function

    Public Function Func_Validate_Block(ByVal Block As Block)                                       'Function to validate inbound blocks. Takes a Block object as a parameter.

        Sub_Console_Print("Validating block...", "Main", "black")                                   'Sends a message to the console subroutine saying the block is being validated.

        Validation_Error = ""                                                                       'Sets the Validation_Error variable equal to nothing.
        Validation_Message = "Block header error: "                                                 'Sets the Validation_Message variable equal to the string "Block header error: ".
        Dim String_List As New List(Of String)                                                      'Variable to store a list of strings.

        Dim Zero_String As String = ""                                                              'Variable to store the leading zeros needed to check if the block hash is valid.
        Dim Merkle_Test As String = ""                                                              'Variable to store the calculated Merkle Root of the transactions stored in the block.
        Dim Inputs_Running_Total As Integer = 0                                                     'Variable to store the running total of all the inputs contained within the transactions in the block.
        Dim Outputs_Running_Total As Integer = 0                                                    'Variable to store the running total of all the outputs contained within the transactions in the block.
        Dim Index As Integer = 0                                                                    'Variable to store indexes of inputs, outputs and transactions.
        Dim Expected_Reward As Integer = 0                                                          'Variable to store the expected reward generated by the block.
        Dim Claimed_Reward As Integer = 0                                                           'Variable to store the reward field within the block.
        Dim Value As Integer = 0                                                                    'Variable to temporarly store values being checked.

        If Block.Version = "" Then                                                                  'If the Version property of the Block object equals nothing.

            Validation_Error = "Error in version field: No version"                                 'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf Block.Version < Version Then                                                         'Else if Version property of the Block object is smaller then the Version variable.

            Validation_Error = "Error in version field: Version too old"                            'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Version check OK", "Main", "Green")                                      'Sends a message to the console subroutine saying the version check was ok.

        If Block.Transactions.Count <= 0 Then                                                       'If the number of items in the Transactions list property of the Block object is less then or equal to 0.

            Validation_Error = "Error in transaction list: No transactions found"                   'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Transaction list check OK", "Main", "Green")                             'Sends a message to the console subroutine saying the transaction list check was ok.

        For i = 0 To Block.Difficulty - 1                                                           'For loop from i equals 0 to the Difficulty property of the Block object minus 1.

            Zero_String = Zero_String & "0"                                                         'Sets the Zero_String variable equal to itself with "0" appened to it.

        Next i

        If Block.Func_Header_Hash.substring(0, Block.Difficulty) <> Zero_String Then                'If the substring of the result of the Func_Header_Hash method of the Block object starting at index 0 with length equal to the Difficulty property of the Block object is not equal to Zero_String.

            Validation_Error = "Error with header hash: Invalid proof of work"                      'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Proof of work with claimed difficulty is valid", "Main", "Green")        'Sends a message to the console subroutine saying the proof of work check was valid.

        If Block.Time > Func_Unix_Time() + 3600 Then                                                'If the Time property of the Block object is greater then the current Unix time plus 3600.

            Validation_Error = "Error with timestamp: Block creation time cannot be more then 1 hour ahead of current time."    'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Timestamp check OK", "Main", "Green")                                    'Sends a message to the console subroutine saying the timestamp check was ok.
        Validation_Message = "Transaction error: "                                                  'Sets the Validation_Message variable equal to the string "Transaction error: ".

        If Block.Transactions(0).Inputs(0).TXT_Hash <> "Coinbase" Then                              'If the TXT_Hash property of the item at index 0 of the Inputs list property of the item at index 0 of the Transaction list propery of the Block object is not equal to the string "Coinbase".

            Validation_Error = "Error with generation transaction: No coinbase found in hash field." 'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf Block.Transactions(0).Inputs(0).Index <> 0 Then                                      'Else if the Index property of the item at index 0 of the Inputs list property of the item at index 0 of the Transaction list propery of the Block object is not equal to 0.

            Validation_Error = "Error with generation transaction: No coinbase found in index field." 'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf Block.Transactions(0).Inputs.Count > 1 Then                                          'Else if the number of items in Inputs list property of the item at index 0 of the Transaction list propery of the Block object is greater then 1.

            Validation_Error = "Error with generation transaction: Too many inputs."                'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf Block.Transactions(0).Outputs.Count > 1 Then                                         'Else if the number of items in Outputs list property of the item at index 0 of the Transaction list propery of the Block object is greater then 1.

            Validation_Error = "Error with generation transaction: Too many outputs."               'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Generation transaction check OK", "Main", "Green")                       'Sends a message to the console subroutine saying the generation transaction check was ok.

        For i = 1 To Block.Transactions.Count - 1                                                   'For loop from i equals 1 to the number of items in the Transaction list property of the Block object minus 1.

            For i2 = 0 To Block.Transactions(i).Inputs.Count - 1                                    'For loop from i2 equals 0 to the number of items in the Inputs list property of the item at index i of the Transaction list property of the Block object minus 1.

                If Block.Transactions(i).Inputs(i2).TXT_Hash = "Coinbase" Then                      'If the TXT_Hash property of the item at index i2 of the Inputs list property of the item at index i of the Transactions list property of the Block object equals the string "Coinbase".

                    Validation_Message = "Transaction index " & i & ": "                            'Sets the Validation_Message variable equal to the string "Transaction index" with i appened to it.
                    Validation_Error = "Error with transactions: Duplicate coinbase."               'Sets the Validation_Error variable to a string containing information about the error in validation.
                    Return False                                                                    'Returns the boolean result false.

                End If

            Next i2

        Next i

        Sub_Console_Print("Duplicate coinbase check OK", "Main", "Green")                           'Sends a message to the console subroutine saying the duplicate coinbase transaction check was ok.
        Validation_Message = "Block header error: "                                                 'Sets the Validation_Message variable equal to the string "Block header error: ".

        For i = 0 To Block.Transactions.Count - 1                                                   'For loop from i equals 0 to the number of items in the Transactions list property of the Block object minus 1.

            String_List.Add(Block.Transactions(i).Func_Transaction_Hash)                            'Adds the result of the Func_Transaction_Hash method of item at index i in the Transaction list property of the Block object to the String_List.

        Next i

        Merkle_Test = Func_Merkle_Tree(String_List)                                                 'Sets the Merkle_Test variable equal to the result of the Func_Merkle_Tree function passing the String_List as the parameter.

        If Merkle_Test <> Block.Merkle_Root Then                                                    'If the Merkle_Test variable is not equal to the Merkle_Root property of the Block object.

            Validation_Error = "Error in Merkle tree: Invalid Merkle root."                         'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Merkle root check OK", "Main", "Green")                                  'Sends a message to the console subroutine saying the Merkle Root check was ok.

        Dim Previous_Block As Block = Func_Block_Read(Func_Calculate_Height)                        'Object variable to store the previous block initilized as the object returned from the Func_Block_Read function with the current blockchain height passes as the parameter.

        If Block.Func_Header_Hash = Previous_Block.Func_Header_Hash Then                            'If the result of the Func_Header_Hash method of the Block object is equal to the result of the Func_Header_Hash method of the Previous_Block object.

            Validation_Error = "Error with block: Duplicate block."                                 'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Duplicate block check OK", "Main", "Green")                              'Sends a message to the console subroutine saying the duplicate block check was ok.

        If Block.Previous_Hash <> Previous_Block.Func_Header_Hash Then                              'If the Previous_Hash property of the Block object is not equal to the result of the Func_Header_Hash method of the Previous_Block object.

            Validation_Error = "Error in header hash: Orphan block does not link to main chain."    'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Is orphan check OK", "Main", "Green")                                    'Sends a message to the console subroutine saying the orphan block check is ok.

        If Func_Difficulty_Calibration() = 0 Then                                                   'If the result of the function Func_Difficulty_Calibration is equal to 0.

            If Block.Difficulty <> Previous_Block.Difficulty Then                                   'If the Difficulty property of the Block object is not equal to the Difficulty property of the Previous_Block object.

                Validation_Error = "Error in difficulty: Difficulty does not match previous block target." 'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

        Else

            If Block.Difficulty <> Mine_Difficulty Then                                             'If the Difficulty property of the Block object is not equal the Mine_Difficulty variable.

                Validation_Error = "Error in difficulty: Difficulty does not match calculated target." 'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

        End If

        Sub_Console_Print("Difficulty target check OK", "Main", "Green")                            'Sends a message to the console subroutine saying the difficulty check was ok.

        For i = 1 To Block.Transactions.Count - 1                                                   'For loop from i equals 1 to the number of items in the Transactions list property of the Block object minus 1.

            If Func_Validate_Block_Transaction(Block.Transactions(i)) = False Then                  'If the result of the Func_Validate_Block_Transaction function when passing the item at index i of the Transactions list property of Block object is equal to false.

                Validation_Message = "Transaction index " & i & ": "                                'Sets the Validation_Message variable equal to the string "Transaction index" with i appened to it.
                Validation_Error = "Error with transaction: Transaction did not validate."          'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

        Next i

        Sub_Console_Print("Individual transaction validation OK", "Main", "Green")                  'Sends a message to the console subroutine saying individual transaction validation was ok.
        Validation_Message = "Block header error: "                                                 'Sets the Validation_Message variable equal to the string "Block header error: ".

        For i = 1 To Block.Transactions.Count - 1                                                   'For loop from i equals 1 to the number of items in the Transactions list property of the Block object minus 1.

            For i2 = 0 To Block.Transactions(i).Inputs.Count - 1                                    'For loop from i2 equals 0 to the number of items in the Inputs list property of the item at index i of the Transactions list property of the Block object minus 1. 

                Value = Func_Input_Value(Block.Transactions(i).Inputs(i2).TXT_Hash, Block.Transactions(i).Inputs(i2).Index) 'The Value variable equals the result of the Func_Input_Value function with the TXT_Hash property of the the item at index i2 of the Inputs list property of the item at index i of the Transactions list property of the Block object and the Index property of the item at index i2 of the Inputs list property of the item at index i of the Transactions list property of the Block object as parameters.

                Inputs_Running_Total = Inputs_Running_Total + Value                                 'The Inputs_Running_Total variable is set equal to itself plus the Value variable.

            Next i2

            For i3 = 0 To Block.Transactions(i).Outputs.Count - 1                                   'For loop from i3 equals 0 to the number of items in the Outputs list property of the item at index i of the Transactions list property of the Block object minus 1. 

                Outputs_Running_Total = Outputs_Running_Total + Block.Transactions(i).Outputs(i3).Value 'The Outputs_Running_Total variable is set equal to itself plus the Value property of the item at index i3 of the Outputs list property of the item at index i in the Transactions list property of the Block object.

            Next i3

        Next i

        Expected_Reward = Func_Reward_Calculation() + (Inputs_Running_Total - Outputs_Running_Total) 'The Expected_Rewards variable is set equal to the result of the Func_Reward_Calculation function plus the Inputs_Running_Total variable minus the Outputs_Running_Total variable.
        Claimed_Reward = Block.Transactions(0).Outputs(0).Value                                     'Sets the Claimed_Reward variable equal to the Value property of the item at index 0 in the Outputs list property of the item at index 0 of the Transactions list property of the Block object.

        If Claimed_Reward <> Expected_Reward Then                                                   'If the Claimed_Reward variable is not equal to the Expected_Reward variable.

            Validation_Error = "Error in reward calculation: Reward not equal to the calculated reward plus fees." 'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Return True                                                                                 'Returns the boolean result true.

    End Function

    Public Function Func_Validate_Block_Transaction(ByVal TXT As Transaction)                       'Function for validating transactions inside new blocks. Takes a Transaction object as a parameter.

        Sub_Console_Print("Validating transaction...", "Main", "black")                             'Sends a message to the console subroutine saying the transaction is being validated.

        Validation_Error = ""                                                                       'Sets the Validation_Error variable equal to nothing.

        Dim Outputs_Total As Integer = 0                                                            'Variable to store the total of all of the outputs.
        Dim Check_Counter As Integer = 0                                                            'Variable to store the number of inputs checked against the UTXO database.
        Dim Index As Integer = 0                                                                    'Variable to store indexes of inputs, outputs and transactions.
        Dim Inputs_Total As Integer = 0                                                             'Variable to store the total of all the inputs.
        Dim Script_Result As Boolean = False                                                        'Variable to store the result of the script execution engine.
        Dim Value As Integer = 0                                                                    'Variable to temporarly store values being checked.

        If TXT.Version = "" Then                                                                    'If the Version property of the TXT object is set to nothing.

            Validation_Error = "Error in version field: No version"                                 'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf TXT.Version < Version Then                                                           'Else if the Version property of the TXT object is smaller then the Version variable.

            Validation_Error = "Error in version field: Version too old"                            'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Version check OK", "Main", "Green")                                      'Sends a message to the console subroutine saying the version check was ok.

        If TXT.Inputs.Count <= 0 Then                                                               'If the number of items in the Inputs property list of the TXT object is smaller or equal to zero.

            Validation_Error = "Error in input count"                                               'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf TXT.Outputs.Count <= 0 Then                                                          'If the number of items in the Outputs property list of the TXT object is smaller or equal to zero.

            Validation_Error = "Error in output count"                                              'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Input output count OK", "Main", "Green")                                 'Sends a message to the console subroutine saying the input output count check was ok.

        For i = 0 To TXT.Outputs.Count - 1                                                          'For loop from i equals 0 to the number of items in the Outputs property list of the TXT object minus 1.

            If TXT.Outputs(i).Value <= 0 Then                                                       'If the Value property of the item at index i of the outputs property list of the TXT object is smaller then or equal to 0. 

                Validation_Error = "Error in value: Value too small"                                'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            ElseIf TXT.Outputs(i).Value > 2500000000 Then                                           'Else if the Value property of the item at index i of the outputs property list of the TXT object is greater then 2500000000. 

                Validation_Error = "Error in value: Value too large"                                'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

            Outputs_Total = Outputs_Total + TXT.Outputs(i).Value                                    'The Outputs_Total variable equals itself plus the Value property of the item at index i of the Outputs list property of the TXT object.

        Next i

        Sub_Console_Print("Output value check OK", "Main", "Green")                                 'Sends a message to the console subroutine saying output count check was ok.

        If Outputs_Total <= 0 Then                                                                  'If the Outputs_Total variable is smaller then or equal to 0.

            Validation_Error = "Error in total: Output total to small"                              'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf Outputs_Total > 2500000000 Then                                                      'Else if the Outputs_Total variable is greater then 2500000000.

            Validation_Error = "Error in total: Output total too large"                             'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Output total value check OK", "Main", "Green")                           'Sends a message to the console subroutine saying the output total check was ok.

        For i = 0 To TXT.Inputs.Count - 1                                                           'For loop from i equals 0 to the number of items in the Inputs list property of the TXT object minus 1.

            For i2 = 0 To UTXO_Hash_List.Count - 1                                                  'For loop from i2 equals 0 to the number of items in the UTXO_Hash_List minus 1.

                If UTXO_Hash_List(i2) = TXT.Inputs(i).TXT_Hash Then                                 'If the item at index i2 of the UTXO_Hash_List equals the TXT_Hash property of the item at index i of the Inputs list property of the TXT object.

                    If UTXO_Index_List(i2) = TXT.Inputs(i).Index Then                               'If the item at index i2 of the UTXO_Index_List equals the Index property of the item at index i of the Inputs list property of the TXT object.

                        Check_Counter = Check_Counter + 1                                           'The Check_Counter variable equals itself plus 1.
                        Exit For                                                                    'Exits the for loop.

                    End If

                End If

            Next i2

        Next i

        If Check_Counter = TXT.Inputs.Count Then                                                    'If the Check_Counter variable equals the number of items in the Inputs list property of the TXT object. 

        Else

            Validation_Error = "Error with outputs: UTXO's do not exist/have already been spent"    'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Outputs exist in UTXO pool check OK", "Main", "Green")                   'Sends a message to the console subroutine saying the outputs existing in the UTXO pool check was ok.

        For i = 0 To TXT.Inputs.Count - 1                                                           'For loop from i equals 0 to the number of items in the Inputs list property of the TXT object minus 1.

            Value = Func_Input_Value(TXT.Inputs(i).TXT_Hash, TXT.Inputs(i).Index)                   'The Value variable equals the result of the Func_Input_Value function with the TXT_Hash property of the the item at index i of the Inputs list property of the TXT object and the Index property of the item at index i of the Inputs property of the TXT object as parameters.

            If Value <= 0 Then                                                                      'If the Value variable is smaller then or equal to 0.

                Validation_Error = "Error in value: Value too small"                                'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            ElseIf Value > 2500000000 Then                                                          'If the Value variable is greater then 2500000000.

                Validation_Error = "Error in value: Value too large"                                'Sets the Validation_Error variable to a string containing information about the error in validation.
                Return False                                                                        'Returns the boolean result false.

            End If

            Inputs_Total = Inputs_Total + Value                                                     'The Inputs_Total variable is set equal to itself plus the Value variable.

        Next i

        Sub_Console_Print("Inputs value check OK", "Main", "Green")                                 'Sends a message to the console subroutine saying the input value check was ok.

        If Inputs_Total <= 0 Then                                                                   'If the Inputs_Total variable is smaller then or equal to 0.

            Validation_Error = "Error in total: Total too small"                                    'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        ElseIf Inputs_Total > 2500000000 Then                                                       'Else if the Inputs_Total variable is greater then 2500000000.

            Validation_Error = "Error in total: Total too large"                                    'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Inputs total value check OK", "Main", "Green")                           'Sends a message to the console subroutine saying the inputs total check was ok.

        If Inputs_Total < Outputs_Total Then                                                        'If the Inputs_Total variable is smaller then the Outputs_Total variable.

            Validation_Error = "Error in total: Outputs exceed inputs"                              'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Outputs smaller then inputs check OK", "Main", "Green")                  'Sends a message to the console subroutine saying outputs smaller then inputs check was ok.

        If Inputs_Total - Outputs_Total < 10 Then                                                   'If the Inputs_Total variable minus the Outputs_Total variable is smaller then 10.

            Validation_Error = "Error with fee: Minimum transaction fee requirment not met"         'Sets the Validation_Error variable to a string containing information about the error in validation.
            Return False                                                                            'Returns the boolean result false.

        End If

        Sub_Console_Print("Transaction fee check OK", "Main", "Green")                              'Sends a message to the console subroutine saying the transaction fee check was ok.

        For i = 0 To TXT.Inputs.Count - 1                                                           'For loop from i equals 0 to the number of items in the Inputs list property of the TXT object minus 1.

            For i2 = 0 To UTXO_Hash_List.Count - 1                                                  'For loop from i2 equals 0 to the nuber of items in the UTXO_Hash_List minus 1.

                If UTXO_Hash_List(i2) = TXT.Inputs(i).TXT_Hash Then                                 'If the item at index i2 of the UTXO_Hash_List equals the TXT_hash property of the item at index i of the Inputs property of the TXT object.

                    If UTXO_Index_List(i2) = TXT.Inputs(i).Index Then                               'If the item at index i2 of the UTXO_Index_List equals the Index property of the item at index i of the Inputs list property of the TXT object.

                        Script_Result = Func_Script_Validation(TXT.Inputs(i).Unlocking_Script, UTXO_Script_List(i2)) 'The Script_Result variable is set equal to the result returned when executing the Func_Script_Validation function passing the Ulocking_Script property of the item at index i of the Inputs list property of the TXT object and the item at index i2 of the UTXO_Script_List as parameters.

                        If Script_Result <> True Then                                               'If the Script_Result variable is not equal to true.

                            Validation_Error = "Error in script execution: Execution failure"       'Sets the Validation_Error variable to a string containing information about the error in validation.
                            Return False                                                            'Returns the boolean result false.

                        Else

                            Exit For                                                                'Exits the for loop

                        End If

                    End If

                End If

            Next i2

        Next i

        Sub_Console_Print("Script validation OK", "Main", "Green")                                  'Sends a message to the console subroutine saying the script validation was ok.
        Sub_Console_Print("Transaction validation complete", "Main", "Green")                       'Sends a message to the console subroutine saying the transaction validation is complete.

        Return True                                                                                 'Returns the boolean result true.

    End Function

    Public Function Func_Script_Validation(ByVal Unlocking_Script As String, ByVal Locking_Script As String) 'Function for validating transaction scripts. Takes Unlocking_Script and Locking_Script by value as strings as parameters.

        Dim Script As String = Unlocking_Script & Locking_Script                                    'Variable to store (and initilized with) the Unlocking_Script and Locking_Script variables.
        Dim Script_Stack As New Stack(Of String)                                                    'Stack variable to hold data and script commands.

        Try                                                                                         'Sets up a try and catch block.

            Dim Next_Data As String = ""                                                            'Variable to store the next item in the script.
            Dim Index As String = ""                                                                'Variable to store the index of the next comma seperator in the script.

            While Script <> ""                                                                      'While the Script variable is not empty.

                Index = Script.IndexOf(",")                                                         'Makes the Index variable equal to the index of the next comma in the Script Variable.
                Next_Data = Script.Substring(0, Index)                                              'Makes the Next_Data variable equal a substring of the Script variable starting at index 0 with length equal to the Index variable.
                Script = Script.Substring(Index + 1, (Script.Length - Index - 1))                   'Makes the Script variable equal to a substring of the Script variable starting at index Index plus 1 with length equal to the itself minus the Index variable minus 1.

                Select Case True                                                                    'Selects the case that is true.

                    Case Next_Data = "Op_Check_Salt"                                                'Case the Next_Data variable equals the string "Op_Check_Salt".

                        Dim Lock_Address As String = Script_Stack.Pop                               'Variable to store the address the output is locked to, initilized by poping the Script_Stack.
                        Dim Lock_Public_Key As String = Script_Stack.Pop                            'Variable to store the public key the output is locked to, initilized by poping the Script_Stack.
                        Dim Test_Salt As String = Script_Stack.Pop                                  'Variable to store the salt needed to unlock the output, initilized by poping the Script_Stack.
                        Dim Test_Address As String = sha256(Lock_Public_Key & Test_Salt)            'Variable to store the result of the script to see if the salt is valid, initilized by hashing the Lock_Public_Key and Test_Salt variables appened together.

                        If Test_Address = Lock_Address.Substring(0, 64) Then                        'If the Test_Address variable equals a substring of the Lock_Address variable starting at index 0 with length 64.

                            Script_Stack.Push("True")                                               'Push the string "True" onto the Script_Stack.

                        End If

                    Case Else                                                                       'Case that will execute if the data is not an instruction.

                        Script_Stack.Push(Next_Data)                                                'Push the Next_Data variable onto the Script_Stack.

                End Select

            End While

            If Script_Stack.Count = 1 Then                                                          'If the number of items on the stack is equal to 1.

                If Script_Stack.Pop = "True" Then                                                   'If the string returned when poping the item from the stack equals "True".

                Else

                    Return False                                                                    'Returns false.

                End If

            Else

                Return False                                                                        'Returns false.

            End If

        Catch EX As Exception                                                                       'Catches EX as an exception.

            Sub_Console_Print("Script execution failure: " & EX.Message, "Main", "black")           'Sends a message to the console subroutine saying the script engine failed execution and presents the error.
            Return False                                                                            'Returns false.

        End Try

        Return True                                                                                 'Returns true.

    End Function

#End Region

#Region "Blockchain"                                                                                    'Region containing code for accessing the blockchain, UTXO database and hash database.

    Public Function Func_Calculate_Height()                                                             'Function for calculating the hight of the local blockchain.

        Return System.IO.File.ReadAllLines(Blockchain_Directory).Length                                 'Retrives the number of lines in the file at the Blockchain_Directory location and returns it.

    End Function

    Public Function Func_Calculate_Balance()                                                            'Function for calculating the balance of the users account (Without using the UTXO database).

        Dim Temp_Balance As Integer = 0                                                                 'Variable to store the balance temporarly as it is added up.
        Dim Item_Index As Integer = 0                                                                   'Variable to store the index of items within the lists.
        Dim Hash_List As New List(Of String)                                                            'List to store the hash of UTXO transactions.
        Dim Index_List As New List(Of Integer)                                                          'List to store the index of UTXO's.
        Dim Value_List As New List(Of Integer)                                                          'List to store the value of UTXO's.
        Dim Copy_Hash_List As New List(Of String)                                                       'Copy of the list to store the hash of UTXO's.
        Dim Copy_Index_List As New List(Of Integer)                                                     'Copy of the list to store the index of UTXO's.
        Dim Copy_Value_List As New List(Of Integer)                                                     'Copy of the list to store the value of UTXO's.
        Dim Inspect_Script As String = ""                                                               'Variable to store the locking script of UTXO's.
        Dim Inspect_Key As String = ""                                                                  'Variable to store the public key extracted from the UTXO's locking scripts.

        For Count_Block = 1 To Func_Calculate_Height()                                                  'For loop from Count_Block equals 1 to the local blockchain height.

            Dim Scan_Block As Block = Func_Block_Read(Count_Block)                                      'Object variable Block to hold the block currently being scaned for UTXO's. Initilized by the block returned from the function Func_Block_Read with parameter Count_Block.

            For Count_Transaction = 0 To Scan_Block.Transactions.Count - 1                              'For loop from Count_Transaction equals 0 to the number of items in the Transactions list property of the Scan_Block object minus 1.

                For Count_Output = 0 To Scan_Block.Transactions(Count_Transaction).Outputs.Count - 1    'For loop from Count_Output equals 0 to the number of items in the Outputs list property of the currently selected transaction in the Transactios list property of the Scan_Block object minus 1.

                    Inspect_Script = Scan_Block.Transactions(Count_Transaction).Outputs(Count_Output).Locking_Script 'The Inspect_Script variable equals the Locking_Script property of the item at index Count_Output of the Outputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object.
                    Inspect_Key = Inspect_Script.Substring(0, Inspect_Script.IndexOf(","))              'The Inspect_Key variable equals a substring of the Inspect_Script variable starting at index 0 with length equal to the index of the string "," in the Inspect_Script variable.

                    If Inspect_Key = Public_Key Then                                                    'If the Inspect_Key variable equals the Public_Key variable.

                        Temp_Balance = Temp_Balance + Scan_Block.Transactions(Count_Transaction).Outputs(Count_Output).Value 'The Temp_Balance variable equals itself plus the value property of the item at index Count_Output of the Outputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object.

                        Hash_List.Add(Scan_Block.Transactions(Count_Transaction).Func_Transaction_Hash) 'Adds the result of the Func_Transaction_Hash method of the item at index Count_Transaction of the Transactions list property of the Scan_Block object to the Hash_List.
                        Index_List.Add(Count_Output)                                                    'Adds the Count_Output variable to the Index_List.
                        Value_List.Add(Scan_Block.Transactions(Count_Transaction).Outputs(Count_Output).Value) 'Adds the value property of the item at index Count_Output of the Outputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object to the Value_List.

                    End If

                Next Count_Output

            Next Count_Transaction

        Next Count_Block

        For i = 0 To Hash_List.Count - 1                                                                'For loop from i equals 0 to the number of items in the Hash_List minus 1.

            Copy_Hash_List.Add(Hash_List(i))                                                            'Adds the item at index i of the Hash_List to the Copy_Hash_List.
            Copy_Index_List.Add(Index_List(i))                                                          'Adds the item at index i of the Index_List to the Copy_Index_List.
            Copy_Value_List.Add(Value_List(i))                                                          'Adds the item at index i of the Value_List to the Copy_Value_List.

        Next i

        For Count_Block = 1 To Func_Calculate_Height()                                                  'For loop from Count_Block equals 1 to the local blockchain height.

            Dim Scan_Block As Block = Func_Block_Read(Count_Block)                                      'Object variable Block to hold the block currently being scaned for UTXO's. Initilized by the block returned from the function Func_Block_Read with parameter Count_Block.

            For Count_Transaction = 0 To Scan_Block.Transactions.Count - 1                              'For loop from Count_Transaction equals 0 to the number of items in the Transactions list property of the Scan_Block object minus 1.

                For Count_Input = 0 To Scan_Block.Transactions(Count_Transaction).Inputs.Count - 1      'For loop from Count_Input equals 0 to the number of items in the Inputs list property of the currently selected transaction in the Transactios list property of the Scan_Block object minus 1.

                    For Hash_Index = 0 To Hash_List.Count - 1                                           'For loop from Hash_Index equals 0 to the number of items in the Hash_List minus 1.

                        If Scan_Block.Transactions(Count_Transaction).Inputs(Count_Input).TXT_Hash = Hash_List(Hash_Index) Then 'If the TXT_Hash property of the item at index Count_Input of the Inputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object is equal to the item at index Hash_Index of the Hash_List.

                            If Scan_Block.Transactions(Count_Transaction).Inputs(Count_Input).Index = Index_List(Hash_Index) Then 'If the Index property of the item at index Count_Input of the Inputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object is equal to the item at index Hash_Index of the Index_List.

                                Temp_Balance = Temp_Balance - Value_List(Hash_Index)                    'The Temp_Balance variable equals itself minus the item at index Hash_Index of the Value_List.

                                Item_Index = Copy_Hash_List.IndexOf(Hash_List(Hash_Index))              'The Item_Index variable equals the index of the item in the Copy_Hash_List that equals the item at index Hash_Index of the Hash_List.
                                Copy_Hash_List.RemoveAt(Item_Index)                                     'Removes the item at index Item_Index from the Copy_Hash_List.

                                Item_Index = Copy_Index_List.IndexOf(Index_List(Hash_Index))            'The Item_Index variable equals the index of the item in the Copy_Index_List that equals the item at index Hash_Index of the Index_List.
                                Copy_Index_List.RemoveAt(Item_Index)                                    'Removes the item at index Item_Index from the Copy_Index_List.

                                Item_Index = Copy_Value_List.IndexOf(Value_List(Hash_Index))            'The Item_Index variable equals the index of the item in the Copy_Value_List that equals the item at index Hash_Index of the Value_List.
                                Copy_Value_List.RemoveAt(Item_Index)                                    'Removes the item at index Item_Index from the Copy_Value_List.

                            End If

                        End If

                    Next Hash_Index

                Next Count_Input

            Next Count_Transaction

        Next Count_Block

        Balance = Temp_Balance                                                                          'The Balance variable equals the Temp_Balance variable.

        Return Balance                                                                                  'Returns the Balance variable.

    End Function

    Public Function Func_Unconfirmed_Balance()                                                          'Subroutine for calculating pending funds.

        Dim Inspect_Script As String = ""                                                               'Variable to store the locking script of UTXO's.
        Dim Inspect_Key As String = ""                                                                  'Variable to store the public key extracted from the UTXO's locking scripts.

        Unconfirmed_Balance = 0                                                                         'Sets the Unconfirmed_Balance to 0.

        For i = 0 To Memory_Pool.Count - 1                                                              'For loop from i equals 0 to the number of items in the Memory_Pool minus 1.

            For i2 = 0 To Memory_Pool(i).Outputs.Count - 1                                              'For loop from i2 equals 0 to the number of items in the Outputs list property of the item at index i of the Memory_Pool. 

                Inspect_Script = Memory_Pool(i).Outputs(i2).Locking_Script                              'Sets the Inspect_Script variable equal to the Locking_Script property of the item at index i2 of the Outputs list property of the item at index i of the Memory_Pool.
                Inspect_Key = Inspect_Script.Substring(0, Inspect_Script.IndexOf(","))                  'Sets the Inspect_Key variable equal to a substring of the Inspect_Script variable starting at index 0 with length equal to the index of ",".

                If Inspect_Key = Public_Key Then                                                        'If the Inspect_Key variable equals the Public_Key variable.

                    Unconfirmed_Balance = Unconfirmed_Balance + Memory_Pool(i).Outputs(i2).Value        'The Unconfimed_Balance variable equals itself plus the Value property of the item at index i2 of the Outputs list property of the item at index i of the Memory_Pool.

                End If

            Next i2

        Next i

        Dim Inspect_Hash As String = ""                                                                 'Clears the Inspect_Hash variable.
        Dim Inspect_Index As Integer = 0                                                                'Clears the Inspect_Index variable.

        For i = 0 To Memory_Pool.Count - 1                                                              'For loop from i equals 0 to the number of items in the Memory_Pool minus 1.

            For i2 = 0 To Memory_Pool(i).Inputs.Count - 1                                               'For loop from i2 equals 0 to the number of items in the Inputs list property of the item at index i of the Memory_Pool minus 1.

                Inspect_Hash = Memory_Pool(i).Inputs(i2).TXT_Hash                                       'Sets the Inspect_Hash variable equal to the TXT_Hash property of the item at index i2 of the Inputs list property of the item at index i of the Memory_Pool.
                Inspect_Index = Memory_Pool(i).Inputs(i2).Index                                         'Sets the Inspect_Index variable equal to the Index property of the item at index i2 of the Inputs list property of the item at index i of the Memory_Pool.

                For i3 = 0 To UTXO_Hash_List.Count - 1                                                  'For loop from i3 equals 0 to the number of items in the UTXO_Hash_List minus 1.

                    If UTXO_Hash_List(i3) = Inspect_Hash Then                                           'If the item at index i3 of the UTXO_Hash_List is equal to the Inspect_Hash variable.

                        If UTXO_Index_List(i3) = Inspect_Index Then                                     'If the item at index i3 of the UTXO_Index_List is equal to the Inspect_Index variable.

                            Inspect_Script = UTXO_Script_List(i3)                                       'If the item at index i3 of the UTXO_Script_List equals the Inspect_Script variable.
                            Inspect_Key = Inspect_Script.Substring(0, Inspect_Script.IndexOf(","))      'Sets the Inspect_Key variable equal to a substring of the Inspect_Script variable starting at index 0 with length equal to the index of ",".

                            If Inspect_Key = Public_Key Then                                            'If the Inspect_Key variable equals the Public_Key variable.

                                Unconfirmed_Balance = Unconfirmed_Balance - UTXO_Value_List(i3)         'The Unconfirmed_Balance variable equals itself plus the item at index i3 of the UTXO_Value_List.

                            End If

                        End If

                    End If

                Next i3

            Next i2

        Next i

        Return Unconfirmed_Balance                                                                      'Returns the Unconfirmed_Balance variable.

    End Function

    Public Sub Sub_UTXO_Load()                                                                          'Subroutine to load the UTXO database into RAM.

        UTXO_Hash_List.Clear()                                                                          'Clears the UTXO_Hash_List.
        UTXO_Index_List.Clear()                                                                         'Clears the UTXO_Index_List.
        UTXO_Value_List.Clear()                                                                         'Clears the UTXO_Value_List.
        UTXO_Script_List.Clear()                                                                        'Clears the UTXO_Script_List.

        Dim UTXO_Reader As New System.IO.StreamReader(UTXO_Directory)                                   'Opens a stream to read data from the directory stored in the variable UTXO_Directory.

        Dim Data_Line As String = ""                                                                    'Variable to store the line read from the file.
        Dim Data_Chunk As String = ""                                                                   'Variable to store one piece of data extracted from 1 line.
        Dim Index As Integer = 0                                                                        'Variable to store the index of the "#" seperator.

        Do While UTXO_Reader.Peek() <> -1                                                               'DSo while loop whilst there is still data in the file to be read.

            Data_Line = UTXO_Reader.ReadLine()                                                          'Puts the line being read into the variable Data_Line.

            Index = Data_Line.IndexOf("#")                                                              'The Index variable equals the index of the "#" in the Data_Line variable.
            Data_Chunk = Data_Line.Substring(0, Index)                                                  'The Data_Chunk variable equals a substring of the Data_Line variable starting at index 0 with length Index.
            Data_Line = Data_Line.Substring(Index + 1, Data_Line.Length - Index - 1)                    'The Data_Line variable equals a substring of itself starting at index Index plus 1 with length equal to itself minus Index minus 1.
            UTXO_Hash_List.Add(Data_Chunk)                                                              'Adds the variable Data_Chunk to the UTXO_Hash_List.

            Index = Data_Line.IndexOf("#")                                                              'The Index variable equals the index of the "#" in the Data_Line variable.
            Data_Chunk = Data_Line.Substring(0, Index)                                                  'The Data_Chunk variable equals a substring of the Data_Line variable starting at index 0 with length Index.
            Data_Line = Data_Line.Substring(Index + 1, Data_Line.Length - Index - 1)                    'The Data_Line variable equals a substring of itself starting at index Index plus 1 with length equal to itself minus Index minus 1.
            UTXO_Index_List.Add(Data_Chunk)                                                             'Adds the variable Data_Chunk to the UTXO_Index_List.

            Index = Data_Line.IndexOf("#")                                                              'The Index variable equals the index of the "#" in the Data_Line variable.
            Data_Chunk = Data_Line.Substring(0, Index)                                                  'The Data_Chunk variable equals a substring of the Data_Line variable starting at index 0 with length Index.
            Data_Line = Data_Line.Substring(Index + 1, Data_Line.Length - Index - 1)                    'The Data_Line variable equals a substring of itself starting at index Index plus 1 with length equal to itself minus Index minus 1.
            UTXO_Value_List.Add(Data_Chunk)                                                             'Adds the variable Data_Chunk to the UTXO_Value_List.

            Index = Data_Line.IndexOf("#")                                                              'The Index variable equals the index of the "#" in the Data_Line variable.
            Data_Chunk = Data_Line.Substring(0, Index)                                                  'The Data_Chunk variable equals a substring of the Data_Line variable starting at index 0 with length Index.
            Data_Line = Data_Line.Substring(Index + 1, Data_Line.Length - Index - 1)                    'The Data_Line variable equals a substring of itself starting at index Index plus 1 with length equal to itself minus Index minus 1.
            UTXO_Script_List.Add(Data_Chunk)                                                            'Adds the variable Data_Chunk to the UTXO_Script_List.


        Loop

        UTXO_Reader.Close()                                                                             'Closes the data stream to the UTXO database file.

    End Sub

    Public Sub Sub_UTXO(ByVal Start_Index As Integer)                                                   'Subroutine for populating and updating the UTXO database. Takes Start_Index by value as an integer as a parameter.

        Dim Item_Index As Integer = 0                                                                   'Variable to store the index of items within the lists.
        Dim Copy_Hash_List As New List(Of String)                                                       'Copy of the list to store the hash of UTXO transactions.
        Dim Copy_Index_List As New List(Of Integer)                                                     'Copy of the list to store the index of UTXO's.
        Dim Copy_Value_List As New List(Of Integer)                                                     'Copy of the list to store the value of UTXO's
        Dim Copy_Script_List As New List(Of String)                                                     'Copy of the list to store the locking script of UTXO's.

        For Count_Block = (Start_Index) To Func_Calculate_Height()                                      'For loop from Count_Block equals Start_Index to the local blockchain height.

            Dim Scan_Block As Block = Func_Block_Read(Count_Block)                                      'Object variable Block to hold the block currently being scaned for UTXO's. Initilized by the block returned from the function Func_Block_Read with parameter Count_Block.

            For Count_Transaction = 0 To Scan_Block.Transactions.Count - 1                              'For loop from Count_Transaction equals 0 to the number of items in the Transactions list property of the Scan_Block object minus 1.

                For Count_Output = 0 To Scan_Block.Transactions(Count_Transaction).Outputs.Count - 1    'For loop from Count_Output equals 0 to the number of items in the Outputs list property of the currently selected transaction in the Transactios list property of the Scan_Block object minus 1.

                    UTXO_Hash_List.Add(Scan_Block.Transactions(Count_Transaction).Func_Transaction_Hash) 'Adds the result of the Func_Transaction_Hash method of the item at index Count_Transaction of the Transactions list property of the Scan_Block object to the UTXO_Hash_List.
                    UTXO_Index_List.Add(Count_Output)                                                    'Adds the Count_Output variable to the UTXO_Index_List.
                    UTXO_Value_List.Add(Scan_Block.Transactions(Count_Transaction).Outputs(Count_Output).Value) 'Adds the value property of the item at index Count_Output of the Outputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object to the UTXO_Value_List.
                    UTXO_Script_List.Add(Scan_Block.Transactions(Count_Transaction).Outputs(Count_Output).Locking_Script) 'Adds the Locking_Script property of the item at index Count_Output of the Outputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object to the UTXO_Script_List.

                Next Count_Output

            Next Count_Transaction

        Next Count_Block

        For i = 0 To UTXO_Hash_List.Count - 1                                                           'For loop from i equals 0 to the number of items in the UTXO_Hash_List.

            Copy_Hash_List.Add(UTXO_Hash_List(i))                                                       'Adds the item at index i of the UTXO_Hash_List to the Copy_Hash_List.
            Copy_Index_List.Add(UTXO_Index_List(i))                                                     'Adds the item at index i of the UTXO_Index_List to the Copy_Index_List.
            Copy_Value_List.Add(UTXO_Value_List(i))                                                     'Adds the item at index i of the UTXO_Value_List to the Copy_Value_List.
            Copy_Script_List.Add(UTXO_Script_List(i))                                                   'Adds the item at index i of the UTXO_Script_List to the Copy_Script_List.

        Next i

        For Count_Block = (Start_Index) To Func_Calculate_Height()                                      'For loop from Count_Block equals Start_Index to the local blockchain height.

            Dim Scan_Block As Block = Func_Block_Read(Count_Block)                                      'Object variable Block to hold the block currently being scaned for UTXO's. Initilized by the block returned from the function Func_Block_Read with parameter Count_Block.

            For Count_Transaction = 0 To Scan_Block.Transactions.Count - 1                              'For loop from Count_Transaction equals 0 to the number of items in the Transactions list property of the Scan_Block object minus 1.

                For Count_Input = 0 To Scan_Block.Transactions(Count_Transaction).Inputs.Count - 1      'For loop from Count_Input equals 0 to the number of items in the Inputs list property of the currently selected transaction in the Transactios list property of the Scan_Block object minus 1.

                    For Hash_Index = 0 To Copy_Hash_List.Count - 1                                      'For loop from Hash_Index equals 0 to the number of items in the Copy_Hash_List minus 1.

                        If Scan_Block.Transactions(Count_Transaction).Inputs(Count_Input).TXT_Hash = Copy_Hash_List(Hash_Index) Then    'If the TXT_Hash property of the item at index Count_Input of the Inputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object is equal to the item at index Hash_Index of the Copy_Hash_List.

                            If Scan_Block.Transactions(Count_Transaction).Inputs(Count_Input).Index = Copy_Index_List(Hash_Index) Then  'If the Index property of the item at index Count_Input of the Inputs list property of the item at index Count_Transaction of the Transactions list property of the Scan_Block object is equal to the item at index Hash_Index of the Copy_Index_List.

                                Item_Index = UTXO_Hash_List.IndexOf(Copy_Hash_List(Hash_Index))         'The Item_Index variable equals the index of the item in the UTXO_Hash_List that equals the item at index Hash_Index of the Copy_Hash_List.
                                UTXO_Hash_List.RemoveAt(Item_Index)                                     'Removes the item at index Item_Index from the UTXO_Hash_List.

                                Item_Index = UTXO_Index_List.IndexOf(Copy_Index_List(Hash_Index))       'The Item_Index variable equals the index of the item in the UTXO_Index_List that equals the item at index Hash_Index of the Copy_Index_List.
                                UTXO_Index_List.RemoveAt(Item_Index)                                    'Removes the item at index Item_Index from the UTXO_Index_List.

                                Item_Index = UTXO_Value_List.IndexOf(Copy_Value_List(Hash_Index))       'The Item_Index variable equals the index of the item in the UTXO_Value_List that equals the item at index Hash_Index of the Copy_Value_List.
                                UTXO_Value_List.RemoveAt(Item_Index)                                    'Removes the item at index Item_Index from the UTXO_Value_List.

                                Item_Index = UTXO_Script_List.IndexOf(Copy_Script_List(Hash_Index))     'The Item_Index variable equals the index of the item in the UTXO_Script_List that equals the item at index Hash_Index of the Copy_Script_List.
                                UTXO_Script_List.RemoveAt(Item_Index)                                   'Removes the item at index Item_Index from the UTXO_Script_List.

                            End If

                        End If

                    Next Hash_Index

                Next Count_Input

            Next Count_Transaction

        Next Count_Block

        Dim UTXO_Data As String = ""                                                                    'Variable to store the UTXO data to be saved to the UTXO database file.

        My.Computer.FileSystem.WriteAllText(UTXO_Directory, "", False)                                  'Clears the contents of the UTXO database file.

        For i = 0 To UTXO_Hash_List.Count - 1                                                           'For loop from i equals 0 to the number of items in the UTXO_Hash_List minus 1.

            UTXO_Data = UTXO_Hash_List(i) & "#" & UTXO_Index_List(i) & "#" & UTXO_Value_List(i) & "#" & UTXO_Script_List(i) & "#" & vbNewLine   'The UTXO_Data variable equals the items at index i of the UTXO_Hash_List, UTXO_Index_List, UTXO_Value_List and the UTXO_Script_List, all appened together with a line break with "#" between each item.

            My.Computer.FileSystem.WriteAllText(UTXO_Directory, UTXO_Data, True)                        'Appends the UTXO_Data variable to the UTXO database file .

        Next i

        txtUTXONum.Text = System.IO.File.ReadAllLines(UTXO_Directory).Length                            'Sets the txtUTXONum text box to the number of lines in the UTXO database.
        txtUTXOSize.Text = FileLen(UTXO_Directory)                                                      'Sets the txtUTXOSize text box to the file size in bytes of the UTXO database.

    End Sub

    Public Function Func_Balance_UTXO()                                                                 'Function to calculate the balance of the user account using the UTXO database.

        Dim Temp_Balance As Integer = 0                                                                 'Variable to store the balance temporarly as it is added up.
        Dim Inspect_Script As String = ""                                                               'Variable to store the locking script of UTXO's.
        Dim Inspect_Key As String = ""                                                                  'Variable to store the public key extracted from the UTXO's locking scripts.

        For i = 0 To UTXO_Hash_List.Count - 1                                                           'For loop from i equals 0 to the number of items in the UTXO_Hash_List minus 1.

            Inspect_Script = UTXO_Script_List(i)                                                        'The Inspect_Script variable equals the item at index i of the UTXO_Script_List.
            Inspect_Key = Inspect_Script.Substring(0, Inspect_Script.IndexOf(","))                      'The Inspect_Key variable equals a substring of the Inspect_Script variable starting at index 0 with length equal to the index of "," of the Inspect_Script variable.

            If Inspect_Key = Public_Key Then                                                            'If the Inspect_Key variable equals the Public_Key variable.

                Temp_Balance = Temp_Balance + UTXO_Value_List(i)                                        'The Temp_Balance variable equals itself plus the item at index i of the UTXO_Value_List.

            End If

        Next i

        Return Temp_Balance                                                                             'Returns the Temp_Balance variable.

    End Function

    Public Function Func_Input_Value(ByVal Hash As String, ByVal Index As Integer)                      'Function for finding the value of a transaction input. Takes hash and index by value as strings as parameters.

        For i = 0 To UTXO_Hash_List.Count - 1                                                           'For loop from i equals 0 to the nuber of items in the UTXO_Hash_List minus 1.

            If UTXO_Hash_List(i) = Hash Then                                                            'If the item at index i of the UTXO_Hash_List equals the Hash variable.

                If UTXO_Index_List(i) = Index Then                                                      'If the item at index i of the UTXO_Index_List equals the Index variable.

                    Return UTXO_Value_List(i)                                                           'Returns the item at index i of the UTXO_Value_List.

                End If

            End If

        Next i

        Return 0                                                                                        'Returns 0.

    End Function

    Public Function Func_Block_Write(ByVal Block As Object)                                             'Function for writing to the blockchain. Takes a block by value as an object as a parameter.

        Try                                                                                             'Sets up a try and catch block.

            Dim Serial_Stream As New MemoryStream                                                       'Object variable to store a memory stream.
            Dim Byte_Formatter As New BinaryFormatter                                                   'Object variable to store a binary formatter.

            Byte_Formatter.Serialize(Serial_Stream, Block)                                              'Serializes the Block object variable to the Serial_Stream memory stream.

            Dim Serial_String As String = System.Convert.ToBase64String(Serial_Stream.ToArray)          'Converts the Serial_Stream memory stream to base 64 and stores in the string variable Serial_String.
            Serial_Stream.Close()                                                                       'Closes the Serial_Stream memory stream.

            My.Computer.FileSystem.WriteAllText(Blockchain_Directory, Serial_String & vbNewLine, True)  'Appends the Serial_String to the blockchain file.

            txtLocalHeight.Text = Func_Calculate_Height()                                               'Sets the txtLocalHeight text box to the local blockchain height.
            txtBlockSize.Text = FileLen(Blockchain_Directory)                                           'Sets the txtBlockSize text box to the file size in bytes of the blockchain.

            Return "Success"                                                                            'Returns the string "Success".

        Catch EX As Exception                                                                           'Catches EX as an exception.

            Sub_Console_Print("Error in writing to blockchain: " & EX.Message, "Main", "red")           'Sends a message to the console subroutine saying there was an error writing to the blockchain.
            Return "Error"                                                                              'Returns the string "Error".

        End Try

    End Function

    Public Function Func_Block_Read(ByVal Height As Integer)                                            'Function for reading the blochchain. Takes the height by value as an integer as a parameter.

        Try                                                                                             'Sets up a try and catch block.

            Dim Byte_Formatter As New BinaryFormatter                                                   'Object variable to store a binary formatter.
            Dim Serial_Reader As New System.IO.StreamReader(Blockchain_Directory)                       'Opens a stream to read data from the directory stored in the variable Blockchain_Directory.
            Dim Serial_String As String = ""                                                            'Variable to store the current line being read from the blockchain.

            For i = 1 To Height                                                                         'For loop from i equal 1 to the height variable.

                Serial_String = Serial_Reader.ReadLine()                                                'Puts the line being read from the blockchain into the variable Serial_String.

            Next i

            Serial_Reader.Close()                                                                       'Closes the Serial_Reader stream.

            Dim Byte_Data() As Byte = System.Convert.FromBase64String(Serial_String).ToArray            'Converts the Serial_String line from base 64 and saves it into the Byte_Data variable.
            Dim Serial_Stream As New MemoryStream(Byte_Data)                                            'Object variable to store a memory stream, loads the Byte_Data variable into it.

            Dim Temp_Block As Object = Byte_Formatter.Deserialize(Serial_Stream)                        'Object variable block to store the block being read, initialized with the data deserialized from the memory stream.
            Serial_Stream.Close()                                                                       'Closes the Serial_Stream.
            Return Temp_Block                                                                           'Returns the Temp_Block object.

        Catch EX As Exception                                                                           'Catches EX as an exception.

            Sub_Console_Print("Error in reading blockchain: " & EX.Message, "Main", "red")              'Sends a message to the console subroutine saying there was an error reading from the blockchain.
            Return "Error"                                                                              'Returns the string "Error".

        End Try

    End Function

    Public Sub Sub_Hash_Database_Add(ByVal Start_Index As Integer)                                      'Subroutine for adding hashes to the hash database. Takes a start index by value as integer as a parameter.

        For i = Start_Index To Func_Calculate_Height()                                                  'For loop from i equals the Start_Index variable to the local blockchain height.

            Dim Hash_Data As String = ""                                                                'Variable to store the hash being added to the database.

            Dim Scan_Block As Block = Func_Block_Read(i)                                                'Object variable block to store the block at height i in the blockchain.
            Hash_Database.Add(Scan_Block.Func_Header_Hash)                                              'Adds the result of the Func_Header_Hash of the Scan_Block object to the Hash_Database.

        Next i

        My.Computer.FileSystem.WriteAllText(Hash_Database_Directory, "", False)                         'Clears the hash database file.

        For i = 0 To Hash_Database.Count - 1                                                            'For loop from i equals 0 to the number of items in the Hash_Database.

            My.Computer.FileSystem.WriteAllText(Hash_Database_Directory, Hash_Database(i) & vbNewLine, True) 'Appends the item at index i of the Hash_Database along with a line break to the hash databse file.

        Next i

    End Sub

#End Region

#Region "Mining"                                                                                    'Region containing code for creating new blocks and mining them.

    Public Function Func_Merkle_Tree(ByVal Pool As List(Of String))                                 'Function for calculating the merkle root of a set of transactions. Takes Pool as list of transaction objects as a parameter.

        Dim Temp_Pool As New List(Of String)                                                        'Object variable list to store hash pairs temporarly.

        Do                                                                                          'Do loop.

            If Pool.Count Mod 2 > 0 Then                                                            'If the number of items in the Pool list Mod 2 is greater then 0.

                Pool.Add(Pool(Pool.Count - 1))                                                      'Duplicate the last item in the Pool list and adds it to the Pool list.

            End If

            For i = 0 To Pool.Count - 1 Step 2                                                      'For loop from i equals 0 to the number of items in the Pool list minus 1 with a step of +2.

                Temp_Pool.Add(sha256(Pool(i) & Pool(i + 1)))                                        'Adds the hash of the item at index i of the Pool list and the item at the index i plus 1 of the Pool list concatonated together to the Temp_Pool list.

            Next i

            Pool.Clear()                                                                            'Clear the Pool list.

            For i = 0 To Temp_Pool.Count - 1                                                        'For loop from i equals 0 to the number of items in the Temp_Pool minus 1.

                Pool.Add(Temp_Pool(i))                                                              'Add item at index i from the Temp_Pool list to the Pool list.

            Next i

            Temp_Pool.Clear()                                                                       'Clear the Temp_Pool list.

            If Pool.Count = 1 Then                                                                  'If the number of items in the Pool list is equal to 1.

                Exit Do                                                                             'Exits the do loop.

            End If

        Loop

        Return Pool(0)                                                                              'Return the item at index 0 of the Pool list.

    End Function

    Public Function Func_Difficulty_Calibration()                                                   'Function for calibrating the difficulty of the mining process.

        If Func_Calculate_Height() Mod 576 <> 0 Then                                                'If the height of the local blockchain Mod 576 is not equal to 0.

            Return 0                                                                                'Returns 0.

        Else

            Dim Block_Test_A As Block = Func_Block_Read(Func_Calculate_Height)                      'Object variable Block to store (and initilized as) the highest block in the local blockchain.
            Dim Block_Test_B As Block = Func_Block_Read(Func_Calculate_Height() - 575)              'Object variable Block to store (and initilized as) the block 576 blocks below the highest block. 

            Dim Time_Difference As Integer = Block_Test_A.Time - Block_Test_B.Time                  'Variable to store the time difference between Block_Test_A and Block_Test_B. Initilized by minusing the Time property of Block_Test_B from the Time property of Block_Test_A.

            If Time_Difference < 86400 Then                                                         'If the variable Time_Difference is smaller then 86400.

                Mine_Difficulty = Block_Test_A.Difficulty + 1                                       'The Mine_Difficulty variable is set equal to the Difficulty property of Block_Test_A plus 1.

            ElseIf Time_Difference > 86400 Then                                                     'Else if the variable Time_Difference is bigger then 86400.

                Mine_Difficulty = Block_Test_A.Difficulty - 1                                       'The Mine_Difficulty variable is set equal to the Difficulty property of Block_Test_A minus 1.

            Else

                Mine_Difficulty = Block_Test_A.Difficulty                                           'The Mine_Difficulty variable is set equal to the Difficulty property of Block_Test_A.

            End If

            If Mine_Difficulty <= 0 Then                                                            'If the Mine_Difficulty variable is less then or equal to 0.

                Mine_Difficulty = 1                                                                 'The Mine_Difficulty variable is set to 1.

            End If

            Return Mine_Difficulty                                                                  'Returns the Mine_Difficulty variable.

        End If

    End Function

    Public Function Func_Reward_Calculation()                                                       'Function for calculating the block reward for successfuly mining a new block.

        Dim Divisor As Integer = 0                                                                  'Variable for storing the division result.
        Dim Dividing_Reward As Integer = 65536                                                      'Variable for storing the reward given for the first block cycle.

        Divisor = Func_Calculate_Height() \ 17280                                                   'The Divisor variable equals the height of the local blockchain integer divided by 17280.

        If Divisor >= 17 Then                                                                       'If the Divisor variable is greater then or equal to 17.

            Dividing_Reward = 0                                                                     'The dividing reward equals 0.
            Return Dividing_Reward                                                                  'Returns the Dividing_Reward variable.

        End If

        For i = 1 To Divisor                                                                        'For loop from i equals 1 to the Divisor variable.

            Dividing_Reward = Dividing_Reward \ 2                                                   'The Dividing_Reward variable equals itself integer divided by 2.

        Next i

        Return Dividing_Reward                                                                      'Returns the Dividing_Reward variable.

    End Function

    Public Function Func_Fee_Calculation(ByVal Pool As List(Of Transaction))                        'Function for calculating the total transaction fee reward for successfuly mining a new block. Takes pool list of transaction objects as a parameter.

        Dim Fee_Value As Integer = 0                                                                'Variable to store the total amount of transaction fees earned from the list of transactions.
        Dim Running_Input As Integer = 0                                                            'Variable to store a running total of transaction inputs.
        Dim Running_Output As Integer = 0                                                           'Variable to store a running total of transaction outputs.
        Dim Index As Integer = 0

        For i = 0 To Pool.Count - 1                                                                 'For loop from i equals 0 to the number of items in the Pool list minus 1.

            For i2 = 0 To Pool(i).Inputs.Count - 1                                                  'For loop from i2 equals 0 to the number of items in the Inputs list property of the item at index i of the Pool list minus 1.

                Running_Input = Running_Input + Func_Input_Value(Pool(i).Inputs(i2).TXT_Hash, Pool(i).Inputs(i2).Index) 'The Running_Input variable equals itself plus the value returned from the Func_Input_Value function passing the TXT_Hash property of the item at index i2 of the Inputs list property of the item at index i of the Pool list and the Index property of the item at index i2 of the Inputs list property of the item at index i of the Pool list as parameters.

            Next i2

            For i3 = 0 To Pool(i).Outputs.Count - 1                                                 'For loop from i3 equals 0 to the number of items in the Outputs list property of the item at index i of the Pool list minus 1.

                Running_Output = Running_Output + Pool(i).Outputs(i3).Value                         'The Running_Output variable equals itself plus the Value property of the item at index i3 of the Output list property of the item at index i of the Pool list.

            Next i3

        Next i

        Fee_Value = Running_Input - Running_Output                                                  'The Fee_Value variable equals the Running_Input variable minus the Running_Output variable.

        Return Fee_Value                                                                            'Returns the Fee_Value variable.

    End Function

    Public Sub Sub_Start_Mining()                                                                   'Subroutine to start the mining process.

        Block_Missed = False                                                                        'Sets the Block_Missed variable to false.
        Abort = False                                                                               'Sets the Abort variable to false.
        Winning_Nonce = 1                                                                           'Sets the Winning_Nonce variable to 1.

        If Func_Validate(cmbCores.Text, "Core") = False Then                                        'If the result of the Func_Validate function when passing the cmbCores combo box and the string "Core" as parameters is false.

            Exit Sub                                                                                'Exits the subroutine.

        End If

        btnStartMine.Enabled = False                                                                'Disables the btnStartMine button.
        Core_Select = cmbCores.Text                                                                 'Sets the Core_Select variable equal to the cmbCores text box.

        Sub_Console_Print("Starting mining process", "Mine", "Green")                               'Sends a message to the console subroutine saying the mining process is starting.
        Sub_Console_Print("Current height: " & Func_Calculate_Height(), "Mine", "Black")            'Sends a message to the console subroutine displaying the local blockchain height.
        Sub_Console_Print("New block height: " & Func_Calculate_Height() + 1, "Mine", "Black")      'Sends a message to the console subroutine displaying the height of the candidate block.
        Sub_Console_Print("Creating candidate block...", "Mine", "Black")                           'Sends a message to the console subroutine saying the candidate block is being created.

        Dim Previous_Block As Block = Func_Block_Read(Func_Calculate_Height)                        'Object variable block to store (and initilized with) the last block on the block chain. 

        Sub_Console_Print("Creating block header...", "Mine", "Black")                              'Sends a message to the console subroutine saying the block header is being created.

        Candidate_Block = New Block                                                                 'Resets the Candidate_Block object.

        Candidate_Block.Version = Version                                                           'Makes the Version property of the candidate block equal to the variable Version.
        Candidate_Block.Previous_Hash = Previous_Block.Func_Header_Hash                             'Makes the Previous_Hash property of the candidate block equal to the result of the Func_Header_Hash method of the Previous_Block object.
        Candidate_Block.Time = Func_Unix_Time()                                                     'Makes the Time property of the candidate block equal to the current Unix time.

        Sub_Console_Print("Applying difficulty calibration...", "Mine", "Black")                    'Sends a message to the console subroutine saying the difficulty target is being calibrated.

        If Func_Difficulty_Calibration() = 0 Then                                                   'If the result of the function Func_Difficulty_Calibration is equal to 0.

            Mine_Difficulty = Previous_Block.Difficulty                                             'The Mine_Difficulty variable is set equal to the difficulty property of the Previous_Block object.
            Sub_Console_Print("Difficulty unchanged", "Mine", "Black")                              'Sends a message to the console subroutine saying the difficulty is unchanged.
            Sub_Console_Print("Difficulty: " & Mine_Difficulty, "Mine", "Black")                    'Sends a message to the console subroutine displaying the Mine_Difficulty variable.

        Else

            Mine_Difficulty = Func_Difficulty_Calibration()                                         'The Mine_Difficulty variable equals the result of the Func_Difficulty_Calibration function.
            Sub_Console_Print("Difficulty changed", "Mine", "Black")                                'Sends a message to the console subroutine saying the difficulty has changed.
            Sub_Console_Print("Difficulty: " & Mine_Difficulty, "Mine", "Black")                    'Sends a message to the console subroutine displaying the Mine_Difficulty variable.

        End If

        Candidate_Block.Difficulty = Mine_Difficulty                                                'The Difficulty property of the candidate block is set to the variable Mine_Difficulty.

        Sub_Console_Print("Generating coinbase transaction...", "Mine", "Black")                    'Sends a message to the console subroutine saying the coinbase transaction is now being generated.

        Dim Coinbase_Transaction As New Transaction                                                 'Object variable Transaction to store the coinbase transaction to deliver the block reward to the user.
        Dim Coinbase_Input As New TXT_Input                                                         'Object variable TXT_Input to store input data for the coinbase transaction.
        Dim Coinbase_Output As New TXT_Output                                                       'Object variable TXT_Output to store output data for the coinbase transaction.

        Coinbase_Input.TXT_Hash = "Coinbase"                                                        'Sets the TXT_Hash property of the Coinbase_Input object to the string "Coinbase".
        Coinbase_Input.Index = 0                                                                    'Sets the Index property of the Coinbase_Input object to 0.
        Coinbase_Input.Unlocking_Script = ""                                                        'Sets the Unlocking_Script property of the Coinbase_Input object to nothing.

        Sub_Console_Print("Calculating block reward...", "Mine", "Black")                           'Sends a message to the console subroutine saying the block reward is being calculated.

        Block_Reward = Func_Reward_Calculation()                                                    'Block_Reward variable equals the result of the Func_Reward_Calculation function.

        If Memory_Pool.Count <> 0 Then                                                              'If the Memory_Pool list contains items.

            Fee_Reward = Func_Fee_Calculation(Memory_Pool)                                          'Fee_Reward variable equals the result of the Func_Fee_Calculation function passing the Memory_Pool variable as the parameter. 

        Else

            Fee_Reward = 0                                                                          'Sets the Fee_Reward variable equal to 0.

        End If

        Total_Reward = Block_Reward + Fee_Reward                                                    'Total_Reward variable equals the Block_Reward and Fee_Reward variables summed.

        Sub_Console_Print("Total reward calculated", "Mine", "green")                               'Sends a message to the console subroutine saying the total reward has been calculated.
        Sub_Console_Print("Block reward: " & Block_Reward, "Mine", "black")                         'Sends a message to the console subroutine displaying the Block_Reward variable.
        Sub_Console_Print("Fee reward: " & Fee_Reward, "Mine", "black")                             'Sends a message to the console subroutine displaying the Fee_Reward varible.
        Sub_Console_Print("Total reward: " & Total_Reward, "Mine", "green")                         'Sends a message to the console subroutine displaying the Total_Reward variable.

        Func_New_Address()                                                                          'Executes the Func_New_Address function to create a new address for the user account.

        Coinbase_Output.Value = Total_Reward                                                        'Sets the Value property of the Coinbase_Ouput object equal to the Total_Reward variable.
        Coinbase_Output.Locking_Script = Public_Key & "," & Address & "," & "Op_Check_Salt,"        'Sets the Locking_Script property of the Coinbase_Ouput object equal to the variable and strings appened together: Public_Key "," Address "," "Op_Check_Salt,".

        Coinbase_Transaction.Version = Version                                                      'Sets the Version property of the Coinbase_Transaction object equal to the Version variable.
        Coinbase_Transaction.Inputs.Add(Coinbase_Input)                                             'Adds the Coinbase_Input object to the Inputs list property of the Coinbase_Transaction object.
        Coinbase_Transaction.Outputs.Add(Coinbase_Output)                                           'Adds the Coinbase_Output object to the Outputs list property of the Coinbase_Transaction object.

        Candidate_Block.Transactions.Add(Coinbase_Transaction)                                      'Adds the Coinbase_Transaction object to the Transactions list property of the Candidate_Block object.

        Sub_Console_Print("Coinbase transaction complete", "Mine", "green")                         'Sends a message to the console subroutine saying the coinbase transaction is complete.

        For i = 0 To Memory_Pool.Count - 1                                                          'For loop from i equals 0 to the number of items in the Memory_Pool object variable minus 1.

            Candidate_Block.Transactions.Add(Memory_Pool(i))                                        'Adds the item at index i of the Memory_Pool list to the Transactions list property of the Candidate_Block object.

        Next i

        Dim String_List As New List(Of String)                                                      'Variable to store a list of strings.

        For i = 1 To Candidate_Block.Transactions.Count                                             'For loop from i equals 1 to the number of items in the Transactions list property of the Candidate_Block object.

            String_List.Add(Candidate_Block.Transactions(i - 1).Func_Transaction_Hash)              'Adds the result of the Func_Transaction_Hash method of item at index i minus 1 in the Transaction list property of the Candidate_Block object to the String_List variable.

        Next i

        Candidate_Block.Merkle_Root = Func_Merkle_Tree(String_List)                                 'Sets the Merkle_Root property of the Candidate_Block equal to the result of the Func_Merkle_Tree function when passed the String_List as parameter.

        Sub_Console_Print("Version: " & Candidate_Block.Version, "Mine", "Black")                   'Sends a message to the console subroutine displaying the Version property of the Candidate_Block object.
        Sub_Console_Print("Previous block hash: " & Candidate_Block.Previous_Hash, "Mine", "Black") 'Sends a message to the console subroutine displaying the Previous_Hash property of the Candidate_Block object.
        Sub_Console_Print("Merkle root: " & Candidate_Block.Merkle_Root, "Mine", "Black")           'Sends a message to the console subroutine displaying the Merkle_Root property of the Candidate_Block object.
        Sub_Console_Print("Time: " & Candidate_Block.Time, "Mine", "Black")                         'Sends a message to the console subroutine displaying the Time property of the Candidate_Block object.
        Sub_Console_Print("Difficulty: " & Candidate_Block.Difficulty, "Mine", "Black")             'Sends a message to the console subroutine displaying the Difficulty property of the Candidate_Block object.
        Sub_Console_Print("Block header initilized", "Mine", "green")                               'Sends a message to the console subroutine saying that the block header has been initilized.
        Sub_Console_Print("Starting hash process", "Mine", "black")                                 'Sends a message to the console subroutine saying the hash process is starting.

        Mine_Zeros = ""                                                                             'Clears the Mine_Zeros variable.

        For i = 1 To Mine_Difficulty                                                                'For loop from i equals 1 to the Mine_Difficulty variable.

            Mine_Zeros = Mine_Zeros & "0"                                                           'Sets the Mine_Zeros variable equal to itself with "0" appened to the end.

        Next i

        Header_Data = Candidate_Block.Version & Candidate_Block.Previous_Hash & Candidate_Block.Merkle_Root & Candidate_Block.Time & Candidate_Block.Difficulty 'Sets the Header_Data variable equal to all the properties of the Candidate_Block appeneded together.

        Sub_Console_Print("Starting timer", "Mine", "black")                                        'Sends a message to the console subroutine saying the timer is starting.
        Tick = 0                                                                                    'Sets the Tick variable to 0.
        Clock.Start()                                                                               'Starts the Clock timer.

        If txtHashRate.Text = "" Then                                                               'If there is nothing in the txtHashRate text box.

            txtHashRate.Text = "Calculating..."                                                     'The string "Calculating..." is put into the txtHashRate text box.

        End If

        Select Case Core_Select                                                                     'Selects the case depending on what the Core_Select variable equals.

            Case Is = 1                                                                             'Case where the Core_Select variable equals 1.

                Thread_Worker01.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker01 background worker to start its selected subroutine.

            Case Is = 2                                                                             'Case where the Core_Select variable equals 2.

                Thread_Worker01.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker01 background worker to start its selected subroutine.
                Thread_Worker02.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker02 background worker to start its selected subroutine.

            Case Is = 3                                                                             'Case where the Core_Select variable equals 3.

                Thread_Worker01.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker01 background worker to start its selected subroutine.
                Thread_Worker02.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker02 background worker to start its selected subroutine.
                Thread_Worker03.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker03 background worker to start its selected subroutine.

            Case Is = 4                                                                             'Case where the Core_Select variable equals 4.

                Thread_Worker01.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker01 background worker to start its selected subroutine.
                Thread_Worker02.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker02 background worker to start its selected subroutine.
                Thread_Worker03.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker03 background worker to start its selected subroutine.
                Thread_Worker04.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker04 background worker to start its selected subroutine.

            Case Is = 5                                                                             'Case where the Core_Select variable equals 5.

                Thread_Worker01.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker01 background worker to start its selected subroutine.
                Thread_Worker02.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker02 background worker to start its selected subroutine.
                Thread_Worker03.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker03 background worker to start its selected subroutine.
                Thread_Worker04.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker04 background worker to start its selected subroutine.
                Thread_Worker05.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker05 background worker to start its selected subroutine.

            Case Is = 6                                                                             'Case where the Core_Select variable equals 6.

                Thread_Worker01.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker01 background worker to start its selected subroutine.
                Thread_Worker02.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker02 background worker to start its selected subroutine.
                Thread_Worker03.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker03 background worker to start its selected subroutine.
                Thread_Worker04.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker04 background worker to start its selected subroutine.
                Thread_Worker05.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker05 background worker to start its selected subroutine.
                Thread_Worker06.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker06 background worker to start its selected subroutine.

            Case Is = 7                                                                             'Case where the Core_Select variable equals 7.

                Thread_Worker01.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker01 background worker to start its selected subroutine.
                Thread_Worker02.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker02 background worker to start its selected subroutine.
                Thread_Worker03.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker03 background worker to start its selected subroutine.
                Thread_Worker04.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker04 background worker to start its selected subroutine.
                Thread_Worker05.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker05 background worker to start its selected subroutine.
                Thread_Worker06.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker06 background worker to start its selected subroutine.
                Thread_Worker07.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker07 background worker to start its selected subroutine.

            Case Is = 8                                                                             'Case where the Core_Select variable equals 8.

                Thread_Worker01.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker01 background worker to start its selected subroutine.
                Thread_Worker02.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker02 background worker to start its selected subroutine.
                Thread_Worker03.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker03 background worker to start its selected subroutine.
                Thread_Worker04.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker04 background worker to start its selected subroutine.
                Thread_Worker05.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker05 background worker to start its selected subroutine.
                Thread_Worker06.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker06 background worker to start its selected subroutine.
                Thread_Worker07.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker07 background worker to start its selected subroutine.
                Thread_Worker08.RunWorkerAsync()                                                    'Creates a new thread and tells the Thread_Worker08 background worker to start its selected subroutine.

            Case Else

                Sub_Console_Print("ERROR: Something went wrong with the number of cores, mining aborted", "Mine", "red") 'Sends a message to the console subroutine saying the mining process was aborted due to an error.
                Exit Sub                                                                            'Exits the subroutine.

        End Select

        Sub_Console_Print("Process running...", "Mine", "green")                                    'Sends a message to the console subroutine saying the mining process is running.

    End Sub

    Public Sub Sub_Mine01()                                                                         'Subroutine for the Thread_Worker01 background worker to execute the proof of work algorithm.

        Hash_Attempt(1) = ""                                                                        'Clears the value at index 1 of the Hash_Attempt array.
        Mine_Nonce(1) = 0                                                                           'Sets the value at index 1 of the Mine_Nonce array variable to 0.

        Do                                                                                          'Do loop.

            If Thread_Worker01.CancellationPending = True Then                                      'If the thread cancel flag is set to true.

                Exit Sub                                                                            'Exit the subroutine.

            End If

            Hash_Attempt(1) = sha256(Header_Data & Mine_Nonce(1))                                   'Index 1 of the Hash_Attempt array equals the hash of the Header_Data and index 1 of the Mine_Nonce array appened.

            If Hash_Attempt(1).Substring(0, Mine_Difficulty) = Mine_Zeros Then                      'If the substring of index 1 of the Hash_Attempt variable starting at index 0 with equal to the length Mine_Difficulty variable equals the Mine_Zeros variable.

                Winning_Nonce = 1                                                                   'Sets the Winning_Nonce variable to 1.
                Exit Sub                                                                            'Exits the subroutine.

            End If

            Mine_Nonce(1) = Mine_Nonce(1) + Nonce_Step                                              'Index 1 of the Mine_Nonce array equals itself plus the Nonce_Step variable.

        Loop

    End Sub

    Public Sub Sub_Mine02()                                                                         'Subroutine for the Thread_Worker02 background worker to execute the proof of work algorithm.

        Hash_Attempt(2) = ""                                                                        'Clears the value at index 2 of the Hash_Attempt array.
        Mine_Nonce(2) = 1                                                                           'Sets the value at index 2 of the Mine_Nonce array variable to 1.

        Do                                                                                          'Do loop.

            If Thread_Worker02.CancellationPending = True Then                                      'If the thread cancel flag is set to true.

                Exit Sub                                                                            'Exit the subroutine.

            End If

            Hash_Attempt(2) = sha256(Header_Data & Mine_Nonce(2))                                   'Index 2 of the Hash_Attempt array equals the hash of the Header_Data and index 2 of the Mine_Nonce array appened.

            If Hash_Attempt(2).Substring(0, Mine_Difficulty) = Mine_Zeros Then                      'If the substring of index 2 of the Hash_Attempt variable starting at index 0 with equal to the length Mine_Difficulty variable equals the Mine_Zeros variable.

                Winning_Nonce = 2                                                                   'Sets the Winning_Nonce variable to 2.
                Exit Sub                                                                            'Exits the subroutine.

            End If

            Mine_Nonce(2) = Mine_Nonce(2) + Nonce_Step                                              'Index 2 of the Mine_Nonce array equals itself plus the Nonce_Step variable.

        Loop

    End Sub

    Public Sub Sub_Mine03()                                                                         'Subroutine for the Thread_Worker03 background worker to execute the proof of work algorithm.

        Hash_Attempt(3) = ""                                                                        'Clears the value at index 3 of the Hash_Attempt array.
        Mine_Nonce(3) = 2                                                                           'Sets the value at index 3 of the Mine_Nonce array variable to 2.

        Do                                                                                          'Do loop.

            If Thread_Worker03.CancellationPending = True Then                                      'If the thread cancel flag is set to true.

                Exit Sub                                                                            'Exit the subroutine.

            End If

            Hash_Attempt(3) = sha256(Header_Data & Mine_Nonce(3))                                   'Index 3 of the Hash_Attempt array equals the hash of the Header_Data and index 3 of the Mine_Nonce array appened.

            If Hash_Attempt(3).Substring(0, Mine_Difficulty) = Mine_Zeros Then                      'If the substring of index 3 of the Hash_Attempt variable starting at index 0 with equal to the length Mine_Difficulty variable equals the Mine_Zeros variable.

                Winning_Nonce = 3                                                                   'Sets the Winning_Nonce variable to 3.
                Exit Sub                                                                            'Exits the subroutine.

            End If

            Mine_Nonce(3) = Mine_Nonce(3) + Nonce_Step                                              'Index 2 of the Mine_Nonce array equals itself plus the Nonce_Step variable.

        Loop

    End Sub

    Public Sub Sub_Mine04()                                                                         'Subroutine for the Thread_Worker04 background worker to execute the proof of work algorithm.

        Hash_Attempt(4) = ""                                                                        'Clears the value at index 4 of the Hash_Attempt array.
        Mine_Nonce(4) = 3                                                                           'Sets the value at index 4 of the Mine_Nonce array variable to 3.

        Do                                                                                          'Do loop.

            If Thread_Worker04.CancellationPending = True Then                                      'If the thread cancel flag is set to true.

                Exit Sub                                                                            'Exit the subroutine.

            End If

            Hash_Attempt(4) = sha256(Header_Data & Mine_Nonce(4))                                   'Index 4 of the Hash_Attempt array equals the hash of the Header_Data and index 4 of the Mine_Nonce array appened.

            If Hash_Attempt(4).Substring(0, Mine_Difficulty) = Mine_Zeros Then                      'If the substring of index 4 of the Hash_Attempt variable starting at index 0 with equal to the length Mine_Difficulty variable equals the Mine_Zeros variable.

                Winning_Nonce = 4                                                                   'Sets the Winning_Nonce variable to 4.
                Exit Sub                                                                            'Exits the subroutine.

            End If

            Mine_Nonce(4) = Mine_Nonce(4) + Nonce_Step                                              'Index 4 of the Mine_Nonce array equals itself plus the Nonce_Step variable.

        Loop

    End Sub

    Public Sub Sub_Mine05()                                                                         'Subroutine for the Thread_Worker05 background worker to execute the proof of work algorithm.

        Hash_Attempt(5) = ""                                                                        'Clears the value at index 5 of the Hash_Attempt array.
        Mine_Nonce(5) = 4                                                                           'Sets the value at index 5 of the Mine_Nonce array variable to 4.

        Do                                                                                          'Do loop.

            If Thread_Worker05.CancellationPending = True Then                                      'If the thread cancel flag is set to true.

                Exit Sub                                                                            'Exit the subroutine.

            End If

            Hash_Attempt(5) = sha256(Header_Data & Mine_Nonce(5))                                   'Index 5 of the Hash_Attempt array equals the hash of the Header_Data and index 5 of the Mine_Nonce array appened.

            If Hash_Attempt(5).Substring(0, Mine_Difficulty) = Mine_Zeros Then                      'If the substring of index 5 of the Hash_Attempt variable starting at index 0 with equal to the length Mine_Difficulty variable equals the Mine_Zeros variable.

                Winning_Nonce = 5                                                                   'Sets the Winning_Nonce variable to 5.
                Exit Sub                                                                            'Exits the subroutine.

            End If

            Mine_Nonce(5) = Mine_Nonce(5) + Nonce_Step                                              'Index 5 of the Mine_Nonce array equals itself plus the Nonce_Step variable.

        Loop

    End Sub

    Public Sub Sub_Mine06()                                                                         'Subroutine for the Thread_Worker06 background worker to execute the proof of work algorithm.

        Hash_Attempt(6) = ""                                                                        'Clears the value at index 6 of the Hash_Attempt array.
        Mine_Nonce(6) = 5                                                                           'Sets the value at index 6 of the Mine_Nonce array variable to 5.

        Do                                                                                          'Do loop.

            If Thread_Worker06.CancellationPending = True Then                                      'If the thread cancel flag is set to true.

                Exit Sub                                                                            'Exit the subroutine.

            End If

            Hash_Attempt(6) = sha256(Header_Data & Mine_Nonce(6))                                   'Index 6 of the Hash_Attempt array equals the hash of the Header_Data and index 6 of the Mine_Nonce array appened.

            If Hash_Attempt(6).Substring(0, Mine_Difficulty) = Mine_Zeros Then                      'If the substring of index 6 of the Hash_Attempt variable starting at index 0 with equal to the length Mine_Difficulty variable equals the Mine_Zeros variable.

                Winning_Nonce = 6                                                                   'Sets the Winning_Nonce variable to 6.
                Exit Sub                                                                            'Exits the subroutine.

            End If

            Mine_Nonce(6) = Mine_Nonce(6) + Nonce_Step                                              'Index 6 of the Mine_Nonce array equals itself plus the Nonce_Step variable.

        Loop

    End Sub

    Public Sub Sub_Mine07()                                                                         'Subroutine for the Thread_Worker07 background worker to execute the proof of work algorithm.

        Hash_Attempt(7) = ""                                                                        'Clears the value at index 7 of the Hash_Attempt array.
        Mine_Nonce(7) = 6                                                                           'Sets the value at index 7 of the Mine_Nonce array variable to 6.

        Do                                                                                          'Do loop.

            If Thread_Worker07.CancellationPending = True Then                                      'If the thread cancel flag is set to true.

                Exit Sub                                                                            'Exit the subroutine.

            End If

            Hash_Attempt(7) = sha256(Header_Data & Mine_Nonce(7))                                   'Index 7 of the Hash_Attempt array equals the hash of the Header_Data and index 7 of the Mine_Nonce array appened.

            If Hash_Attempt(7).Substring(0, Mine_Difficulty) = Mine_Zeros Then                      'If the substring of index 7 of the Hash_Attempt variable starting at index 0 with equal to the length Mine_Difficulty variable equals the Mine_Zeros variable.

                Winning_Nonce = 7                                                                   'Sets the Winning_Nonce variable to 7.
                Exit Sub                                                                            'Exits the subroutine.

            End If

            Mine_Nonce(7) = Mine_Nonce(7) + Nonce_Step                                              'Index 7 of the Mine_Nonce array equals itself plus the Nonce_Step variable.

        Loop

    End Sub

    Public Sub Sub_Mine08()                                                                         'Subroutine for the Thread_Worker08 background worker to execute the proof of work algorithm.

        Hash_Attempt(8) = ""                                                                        'Clears the value at index 8 of the Hash_Attempt array.
        Mine_Nonce(8) = 7                                                                           'Sets the value at index 8 of the Mine_Nonce array variable to 7.

        Do                                                                                          'Do loop.

            If Thread_Worker08.CancellationPending = True Then                                      'If the thread cancel flag is set to true.

                Exit Sub                                                                            'Exit the subroutine.

            End If

            Hash_Attempt(8) = sha256(Header_Data & Mine_Nonce(8))                                   'Index 8 of the Hash_Attempt array equals the hash of the Header_Data and index 8 of the Mine_Nonce array appened.

            If Hash_Attempt(8).Substring(0, Mine_Difficulty) = Mine_Zeros Then                      'If the substring of index 8 of the Hash_Attempt variable starting at index 0 with equal to the length Mine_Difficulty variable equals the Mine_Zeros variable.

                Winning_Nonce = 8                                                                   'Sets the Winning_Nonce variable to 8.
                Exit Sub                                                                            'Exits the subroutine.

            End If

            Mine_Nonce(8) = Mine_Nonce(8) + Nonce_Step                                              'Index 8 of the Mine_Nonce array equals itself plus the Nonce_Step variable.

        Loop

    End Sub

    Public Sub Sub_Mine_Complete()                                                                  'Subroutine that executes when the mining process is complete or canceled.

        Thread_Worker01.CancelAsync()                                                               'Cancels the Thread_Worker01 background worker.
        Thread_Worker02.CancelAsync()                                                               'Cancels the Thread_Worker02 background worker.
        Thread_Worker03.CancelAsync()                                                               'Cancels the Thread_Worker03 background worker.
        Thread_Worker04.CancelAsync()                                                               'Cancels the Thread_Worker04 background worker.
        Thread_Worker05.CancelAsync()                                                               'Cancels the Thread_Worker05 background worker.
        Thread_Worker06.CancelAsync()                                                               'Cancels the Thread_Worker06 background worker.
        Thread_Worker07.CancelAsync()                                                               'Cancels the Thread_Worker07 background worker.
        Thread_Worker08.CancelAsync()                                                               'Cancels the Thread_Worker08 background worker.

        Thread_Shutdown_Count = Thread_Shutdown_Count + 1                                           'Sets the Thread_Shutdown_Count variable equal to itself plus 1.

        If Thread_Shutdown_Count < Core_Select Then                                                 'If the Thread_Shutdown_Count variable is smaller then the Core_Select variable.

            Exit Sub                                                                                'Exits the subroutine.

        End If

        Thread_Shutdown_Count = 0                                                                   'Sets the Thread_Shutdown_Count to 0.

        Clock.Stop()                                                                                'Stops the Clock timer.
        txtTime.Text = 0                                                                            'Clears the txtTime text box.

        If Tick = 0 Then                                                                            'If the Tick variavle equals 0.

            Tick = 1                                                                                'The Tick variable is set to 1.

        End If

        Hash_Rate = Mine_Nonce(Winning_Nonce) \ Tick                                                'The Hash_Rate variable is equal to the Mine_Nonce variable integer divided by the Tick variable.
        txtHashRate.Text = Hash_Rate                                                                'The txtHashRate text box is set equal to the Hash_Rate variable.

        If Abort = True Then                                                                        'If the Abort boolean variable equals true.

            If Block_Missed = False Then                                                            'If the Block_Missed variable equals false.

                Sub_Console_Print("Mining aborted", "Mine", "red")                                  'Sends a message to the console subroutine saying the mining process has been aborted.
                Sub_Console_Print("Time elapsed: " & Tick & " seconds", "Mine", "black")            'Sends a message to the console subroutine displaying the time elapsed.
                Abort = False                                                                       'Sets the Abort variable to false.
                Exit Sub                                                                            'Exits the subroutine.

            Else

                Block_Missed = False                                                                'Sets the Abort variable to false.
                Sub_Console_Print("Block miss, attempting new block", "Mine", "red")                'Sends a message to the console subroutine saying there was a block miss.
                Sub_Console_Print("Time elapsed: " & Tick & " seconds", "Mine", "black")            'Sends a message to the console subroutine displaying the time elapsed.
                txtBlocksMissed.Text = txtBlocksMissed.Text + 1                                     'Sets the value of the txtBlocksMissed text box to itself plus 1.
                Sub_Start_Mining()                                                                  'Executes the Sub_Start_Mining subroutine.
                Exit Sub                                                                            'Exits the subroutine.

            End If

        End If

        Sub_Console_Print("Hash creation successful with nonce: " & Mine_Nonce(Winning_Nonce), "Mine", "green") 'Sends a message to the console subroutine saying the mining process was a success and displays the valid nonce value.
        Sub_Console_Print("Header hash: " & Hash_Attempt(Winning_Nonce), "Mine", "black")           'Sends a message to the console subroutine displaying the valid header hash.
        Sub_Console_Print("Time elapsed: " & Tick & " seconds", "Mine", "black")                    'Sends a message to the console subroutine displaying the time elapsed.

        Candidate_Block.Nonce = Mine_Nonce(Winning_Nonce)                                           'Sets the Nonce property of the Candidate_Block object equal to the item at index Winning_Nonce of the Mine_Nonce array.
        Func_Block_Write(Candidate_Block)                                                           'Writes the Candidate_Block object to the blockchain by executing the Func_Block_Write function.
        Sub_Hash_Database_Add(Func_Calculate_Height)                                                'Executes the Sub_Hash_Database_Add subroutine passing the local blockchain height as the parameter.
        txtBlocksMined.Text = txtBlocksMined.Text + 1                                               'Sets the value of the txtBlocksMined text box to itself plus 1.

        Dim Index As Integer = 0                                                                    'Variable to store the index of transactions in lists.
        Dim Remove_List As New List(Of Transaction)                                                 'Object variable list to store transactions that need to be removed from the Memory_Pool.

        For i = 1 To Candidate_Block.Transactions.Count - 1                                         'For loop from i equals 1 to the number of items in the Transactions list property of the Candidate_Block object minus 1.

            Index = Memory_Pool.IndexOf(Candidate_Block.Transactions(i))                            'Sets the Index variable equal to the index of the item at index i of the Transactions list property of the the Candidate_Block object in the Memory_Pool. 

            If Index = -1 Then                                                                      'If the Index variable is equal to -1.

            Else

                Remove_List.Add(Candidate_Block.Transactions(i))                                    'Adds the item at index i in the Transactions list property of the Candidate_Block object to the Remove_List.

            End If

        Next i

        For i = 0 To Remove_List.Count - 1                                                          'For loop from i equals 0 to the number of items in the Remove_List minus 1.

            Memory_Pool.Remove(Remove_List(i))                                                      'Removes the item at index i of the Remove_List from the Memory_Pool.

        Next i

        lblMemPool1.Text = Memory_Pool.Count                                                        'Sets the lblMemPool1 label to the number of items in the Memory_Pool.
        Sub_UTXO(Func_Calculate_Height())                                                           'Executes the Sub_UTXO subroutine passing the height of the local blockchain as the parameter.
        Balance = Func_Balance_UTXO()                                                               'Balance variable is set equal to the result of the Func_Balance_UTXO function.
        txtBal.Text = Balance                                                                       'Sets the txtBal text box equal to the Balance variable.
        Unconfirmed_Balance = Func_Unconfirmed_Balance()                                            'Sets the Unconfirmed_Balance variable equal to the result of the Func_Unconfirmed_Balance function.
        txtUnBal.Text = Unconfirmed_Balance                                                         'Sets the txtUnBal text box equal to the Unconfirmed_Balance variable.

        For i = 0 To Active_Node_List.Count - 1                                                     'For loop from i equals 0 to the number of items in the Active_Node_List.

            Sub_P2P_Block_Hash(Active_Node_List(i))                                                 'Executes the Sub_P2P_Block_Hash subroutine passing the item at index i of the Active_Node_List as the parameter.

        Next i

        For i = 0 To Child_Node_List.Count - 1                                                      'For loop from i equals 0 to the number of items in the Child_Node_List.

            Sub_P2P_Block_Hash(Child_Node_List(i))                                                  'Executes the Sub_P2P_Block_Hash subroutine passing the item at index i of the Child_Node_List as the parameter.

        Next i

        If Single_Mine = True Then                                                                  'If the Single_Mine variable equals true.

            btnStartMine.Enabled = True                                                             'Enables the btnStartMine button.
            Exit Sub                                                                                'Exits the subroutine.

        End If

        Sub_Start_Mining()                                                                          'Executes the Sub_Start_Mining subroutine.


    End Sub

    Private Sub Clock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clock.Tick 'Subroutine executed every time the Clock timer ticks.

        Tick = Tick + 1                                                                             'Sets the Tick variable equals itself plus 1.
        txtTime.Text = Tick                                                                         'Sets the value of the txtTime text box to the Tick variable.
        txtTotalTime.Text = CInt(txtTotalTime.Text) + 1                                             'Sets the value of the txtTotalTime text box equal to the integer value of itself plus 1.

    End Sub

#End Region

#Region "Blockchain Explorer"                                                                                               'Region containing code for the block explorer.

    Private Sub btnBlockGetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlockGetData.Click 'Subroutine for extracting data from a chosen block stored on the Blockchain.

        If Func_Validate(txtXBB.Text, "Height") = False Then                        'If the result of the Func_Validate function when passing the text within txtXBB and the string "Height" as parameters is false.

            Exit Sub                                                                'Exits the subroutine.

        End If

        txtXTI.Clear()                                                              'Clears the txtXTI text box.
        txtXTV.Clear()                                                              'Clears the txtXTV text box.
        lstXTI.Items.Clear()                                                        'Clears the lstXTI list box.
        lstXTO.Items.Clear()                                                        'Clears the lstXTO list box.
        txtXIT.Clear()                                                              'Clears the txtXIT text box.
        txtXII.Clear()                                                              'Clears the txtXII text box.
        txtXIS.Clear()                                                              'Clears the txtXTS text box.
        txtXOV.Clear()                                                              'Clears the txtXOV text box.
        txtXOS.Clear()                                                              'Clears the txtXOS text box.
        txtXTH.Clear()                                                              'Clears the txtXTHtext box.

        Dim Selected_Block As Integer = txtXBB.Text                                 'Variable to store the selected block entered by the user. Initilized as the value of the txtXBB text box.

        Explore_Block = Func_Block_Read(Selected_Block)                             'The Explore_Block object is set equal to the block returned from the function Func_Block_Read with the Selected_Block variable as the parameter.

        txtXBH.Text = Explore_Block.Func_Header_Hash                                'Sets the txtXBH text box equal to the result of the Func_Header_Hash method of the Explore_Block object.
        txtXBV.Text = Explore_Block.Version                                         'Sets the txtXBV text box equal to the Version property of the Explore_Block object.
        txtXBT.Text = Explore_Block.Time                                            'Sets the txtXBT text box equal to the Time property of the Explore_Block object.
        txtXBP.Text = Explore_Block.Previous_Hash                                   'Sets the txtXBP text box equal to the Previous_Hash property of the Explore_Block object.
        txtXBM.Text = Explore_Block.Merkle_Root                                     'Sets the txtXBM text box equal to the Merkle_Root property of the Explore_Block object.
        txtXBD.Text = Explore_Block.Difficulty                                      'Sets the txtXBD text box equal to the Difficulty property of the Explore_Block object.
        txtXBN.Text = Explore_Block.Nonce                                           'Sets the txtXBN text box equal to the Nonce property of the Explore_Block object.
        lstXBT.Items.Clear()                                                        'Clears the lstXBT list box.

        For i = 1 To Explore_Block.Transactions.Count                               'For loop from i equals 1 to the number of items in the Transactions list property of the Explore_Block object.

            lstXBT.Items.Add(i)                                                     'Adds i to the list box lstXBT.

        Next i

    End Sub

    Private Sub btnTransGetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransGetData.Click 'Subroutine for extracting data about a chosen transaction.

        If lstXBT.SelectedIndex = -1 Then                                           'If no item in the lstXBT list box is selected.

            MsgBox("Please select an item from the transactions list box.")         'Displays a message box asking the user to select an item.
            Exit Sub                                                                'Exits the subroutine.

        End If

        txtXIT.Clear()                                                              'Clears the txtXIT textbox.
        txtXII.Clear()                                                              'Clears the txtXII textbox.
        txtXIS.Clear()                                                              'Clears the txtXTS textbox.
        txtXOV.Clear()                                                              'Clears the txtXOV textbox.
        txtXOS.Clear()                                                              'Clears the txtXOS textbox.

        Transaction_Index = lstXBT.SelectedIndex                                    'The Transaction_Index variable is set equal to the index of the currently selected row in the lstXBT list box.

        txtXTH.Text = Explore_Block.Transactions(Transaction_Index).Func_Transaction_Hash 'Sets the txtXTH text box equal to the result of the Func_Transaction_Hash of the item at index Transaction_Index of the Transactions list property of the Explore_Block object. 
        txtXTI.Text = Transaction_Index                                             'Sets the txtXTI text box equal to the Transaction_Index variable.
        txtXTV.Text = Explore_Block.Transactions(Transaction_Index).Version         'Sets the txtXTV text box equal to Version property of the object at index Transaction_Index of the Transactions list property of the Explore_Block object.

        lstXTI.Items.Clear()                                                        'Clears the lstXTI list box.
        lstXTO.Items.Clear()                                                        'Clears the lstXTO list box.

        For i = 1 To Explore_Block.Transactions(Transaction_Index).Inputs.Count     'For loop from i equals 1 to the number of items in the Inputs list property of the item at index Transaction_Index of the Transactions list property of the Explore_Block object.

            lstXTI.Items.Add(i)                                                     'Adds i to the lstXTI list box.

        Next i

        For i = 1 To Explore_Block.Transactions(Transaction_Index).Outputs.Count    'For loop from i equals 1 to the number of items in the Output list property of the item at index Transaction_Index of the Transactions list property of the Explore_Block object.

            lstXTO.Items.Add(i)                                                     'Adds i to the lstXTO list box.

        Next i

    End Sub

    Private Sub btnINGetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnINGetData.Click 'Subroutine for extracting data from a chosen input.

        If lstXTI.SelectedIndex = -1 Then                                           'If no item in the lstXIT list box is selected.

            MsgBox("Please select an item from the inputs list box.")               'Displays a message box asking the user to select an item.
            Exit Sub                                                                'Exits the subroutine.

        End If

        Input_Index = lstXTI.SelectedIndex                                          'The Input_Index variable is set equal to the index of the currently selected row in the lstXTI list box.

        txtXIT.Text = Explore_Block.Transactions(Transaction_Index).Inputs(Input_Index).TXT_Hash 'Sets the txtXIT text box equal to the TXT_Hash property of the item at index Input_Index of the Inputs list property of the item at index Transaction_Index of the Transactions list property of the Explore_Block object.
        txtXII.Text = Explore_Block.Transactions(Transaction_Index).Inputs(Input_Index).Index 'Sets the txtXII text box equal to the Index property of the item at index Input_Index of the Inputs list property of the item at index Transaction_Index of the Transactions list property of the Explore_Block object.
        txtXIS.Text = Explore_Block.Transactions(Transaction_Index).Inputs(Input_Index).Unlocking_Script 'Sets the txtXIS text box equal to the Unlocking_Script property of the item at index Input_Index of the Inputs list property of the item at index Transaction_Index of the Transactions list property of the Explore_Block object.

    End Sub

    Private Sub btnOutGetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutGetData.Click 'Subroutine for extracting data from a chosen output.

        If lstXTO.SelectedIndex = -1 Then                                           'If no item in the lstXTO list box is selected.

            MsgBox("Please select an item from the outputs list box.")              'Displays a message box asking the user to select an item.
            Exit Sub                                                                'Exits the subroutine.

        End If

        Output_Index = lstXTO.SelectedIndex                                         'The Output_Index variable is set equal to the index of the currently selected row in the lstXTO list box.

        txtXOV.Text = Explore_Block.Transactions(Transaction_Index).Outputs(Output_Index).Value 'Sets the txtXOV text box equal to the Value property of the item at index Output_Index of the Outputs list property of the item at index Transaction_Index of the Transactions list property of the Explore_Block object.
        txtXOS.Text = Explore_Block.Transactions(Transaction_Index).Outputs(Output_Index).Locking_Script 'Sets the txtXOS text box equal to the Locking_Script property of the item at index Output_Index of the Outputs list property of the item at index Transaction_Index of the Transactions list property of the Explore_Block object.

    End Sub

    Private Sub btnDataPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataPlus.Click 'Subroutine for getting data about the next block.

        If Func_Validate(txtXBB.Text + 1, "Height") = False Then                    'If the result of the Func_Validate function is false when passing the text within txtXBB plus 1 and the string "Height" as parameters.

            Exit Sub                                                                'Exit the subroutine.

        End If

        txtXBB.Text = txtXBB.Text + 1                                               'The value of the txtXBB text box equals itself plus 1.
        Me.btnBlockGetData.PerformClick()                                           'Clicks the btnBlockGetData button.

    End Sub

    Private Sub btnDataMinus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataMinus.Click 'Subroutine for getting data about the previous block.

        If Func_Validate(txtXBB.Text - 1, "Height") = False Then                    'If the result of the Func_Validate is false when passing the text within txtXBB minus 1 and the string "Height" as parameters.

            Exit Sub                                                                'Exit the subroutine.

        End If

        txtXBB.Text = txtXBB.Text - 1                                               'The value of the txtXBB text box equals itself minus 1.
        Me.btnBlockGetData.PerformClick()                                           'Clicks the btnBlockGetData button.

    End Sub

#End Region

End Class