using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomRouteProvider {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            RouteProvider provider = new RouteProvider();
            infoLayer.DataProvider = provider;
            provider.CalculateRoute(new GeoPoint(70, 60), new GeoPoint(-60, -70));
        }
    }
    public class RouteProvider : InformationDataProviderBase {
        protected new RouteData Data { get { return (RouteData)base.Data; } }
        public IEnumerable<GeoPoint> Route { get { return Data.Route; } }
        public override bool IsBusy {
            get {
                return false;
            }
        }
        protected override IInformationData CreateData() {
            return new RouteData();
        }
        public void CalculateRoute(GeoPoint point1, GeoPoint point2) {
            Data.CalculateRoute(point1, point2);
        }

        public override void Cancel() {
            throw new NotImplementedException();
        }

        protected override MapDependencyObject CreateObject() {
            return new RouteProvider();
        }
    }
    public class RouteData : IInformationData {
        readonly List<GeoPoint> route = new List<GeoPoint>();
        public List<GeoPoint> Route { get { return route; } }
        public event EventHandler<RequestCompletedEventArgs> OnDataResponse;
        RequestCompletedEventArgs CreateEventArgs() {
            MapItem[] items = new MapItem[3];
            items[1] = new MapPushpin() { Location = route[0], Text = "A", Information = route[0].ToString() };
            items[2] = new MapPushpin() { Location = route[route.Count - 1], Text = "B", Information = route[route.Count - 1].ToString() };
            MapPolyline polyline = new MapPolyline() {
                IsGeodesic = true,
                Stroke = new SolidColorBrush() { Color = Colors.Red },
                StrokeStyle = new StrokeStyle() { Thickness = 4 }
            };
            for (int i = 0; i < route.Count; i++)
                polyline.Points.Add(route[i]);
            items[0] = polyline;
            return new RequestCompletedEventArgs(items, null, false, null);
        }
        protected void RaiseChanged() {
            if (OnDataResponse != null)
                OnDataResponse(this, CreateEventArgs());
        }
        public void CalculateRoute(GeoPoint point1, GeoPoint point2) {
            CalculateRouteCore(point1, point2);
            RaiseChanged();
        }
        void CalculateRouteCore(GeoPoint point1, GeoPoint point2) {
            this.route.Clear();
            route.Add(point1);
            route.Add(point2);
        }
    }
}
