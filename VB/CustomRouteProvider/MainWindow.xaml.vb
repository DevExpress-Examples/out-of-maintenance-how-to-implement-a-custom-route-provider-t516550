Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace CustomRouteProvider
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            Dim provider As New RouteProvider()
            infoLayer.DataProvider = provider
            provider.CalculateRoute(New GeoPoint(70, 60), New GeoPoint(-60, -70))
        End Sub
    End Class
    Public Class RouteProvider
        Inherits InformationDataProviderBase

        Protected Shadows ReadOnly Property Data() As RouteData
            Get
                Return CType(MyBase.Data, RouteData)
            End Get
        End Property
        Public ReadOnly Property Route() As IEnumerable(Of GeoPoint)
            Get
                Return Data.Route
            End Get
        End Property
        Public Overrides ReadOnly Property IsBusy() As Boolean
            Get
                Return False
            End Get
        End Property
        Protected Overrides Function CreateData() As IInformationData
            Return New RouteData()
        End Function
        Public Sub CalculateRoute(ByVal point1 As GeoPoint, ByVal point2 As GeoPoint)
            Data.CalculateRoute(point1, point2)
        End Sub

        Public Overrides Sub Cancel()
            Throw New NotImplementedException()
        End Sub

        Protected Overrides Function CreateObject() As MapDependencyObject
            Return New RouteProvider()
        End Function
    End Class
    Public Class RouteData
        Implements IInformationData


        Private ReadOnly route_Renamed As New List(Of GeoPoint)()
        Public ReadOnly Property Route() As List(Of GeoPoint)
            Get
                Return route_Renamed
            End Get
        End Property
        Public Event OnDataResponse As EventHandler(Of RequestCompletedEventArgs) Implements IInformationData.OnDataResponse
        Private Function CreateEventArgs() As RequestCompletedEventArgs
            Dim items(2) As MapItem
            items(1) = New MapPushpin() With { _
                .Location = route_Renamed(0), _
                .Text = "A", _
                .Information = route_Renamed(0).ToString() _
            }
            items(2) = New MapPushpin() With { _
                .Location = route_Renamed(route_Renamed.Count - 1), _
                .Text = "B", _
                .Information = route_Renamed(route_Renamed.Count - 1).ToString() _
            }
            Dim polyline As New MapPolyline() With { _
                .IsGeodesic = True, _
                .Stroke = New SolidColorBrush() With {.Color = Colors.Red}, _
                .StrokeStyle = New StrokeStyle() With {.Thickness = 4} _
            }
            For i As Integer = 0 To route_Renamed.Count - 1
                polyline.Points.Add(route_Renamed(i))
            Next i
            items(0) = polyline
            Return New RequestCompletedEventArgs(items, Nothing, False, Nothing)
        End Function
        Protected Sub RaiseChanged()
            RaiseEvent OnDataResponse(Me, CreateEventArgs())
        End Sub
        Public Sub CalculateRoute(ByVal point1 As GeoPoint, ByVal point2 As GeoPoint)
            CalculateRouteCore(point1, point2)
            RaiseChanged()
        End Sub
        Private Sub CalculateRouteCore(ByVal point1 As GeoPoint, ByVal point2 As GeoPoint)
            Me.route_Renamed.Clear()
            route_Renamed.Add(point1)
            route_Renamed.Add(point2)
        End Sub
    End Class
End Namespace
