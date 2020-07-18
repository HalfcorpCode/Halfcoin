'© 2016-2017 Gianluca Cantone (Raw code)

Public Class Splash                                                                                                 'Splash screen code.

    Dim Count As Integer = 1                                                                                        'Variable to track how many ticks of the timer have passed.

    Private Sub Splash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load         'Subroutine that executes when the program first starts up.

        tmrSplash.Start()                                                                                           'Starts the tmrSplash timer.

    End Sub

    Private Sub tmrSplash_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSplash.Tick   'Subroutine that executes when the timer ticks.

        If Count = 4 Then                                                                                           'If the Count variable equals 4.

            tmrSplash.Stop()                                                                                        'Stops the tmrSplash timer.
            Halfcoin.Show()                                                                                         'Displays the Halfcoin form.
            Me.Hide()                                                                                               'Hides the splash screen form.

        Else

            lblSplashLoad.Text = lblSplashLoad.Text & "."                                                           'The label lblSplashLoad equals itself with "." appended to it.
            Count = Count + 1                                                                                       'The Count variable equals itself plus 1.

        End If

    End Sub

End Class