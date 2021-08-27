<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128571582/16.1.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T516550)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/CustomRouteProvider/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/CustomRouteProvider/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/CustomRouteProvider/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/CustomRouteProvider/MainWindow.xaml.vb))
<!-- default file list end -->
# How to: Implement a Custom Route Provider


This example demonstrates how create a custom route provider and use it to calculate a route between two geographical points.


<h3>Description</h3>

To do this, design a class inheriting the&nbsp; <a href="https://documentation.devexpress.com/#WPF/clsDevExpressXpfMapInformationDataProviderBasetopic">InformationDataProviderBase</a>&nbsp;abstract class and implement its <strong>CreateData</strong>&nbsp;method. Then, design a class inheriting the&nbsp;<a href="https://documentation.devexpress.com/#wpf/clsDevExpressXpfMapIInformationDatatopic">IInformationData</a>&nbsp;interface and&nbsp;override&nbsp;its&nbsp;<a href="https://documentation.devexpress.com/#wpf/DevExpressXpfMapIInformationData_OnDataResponsetopic">OnDataResponse</a>&nbsp;event. Implement the <strong>CalculateRouteCore</strong> method to provide custom route calculations.

<br/>


