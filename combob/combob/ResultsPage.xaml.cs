using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using combob.Models.Responses;

namespace combob
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ResultsPage : Page
	{
		public ResultsPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			NumericalResults.Visibility = Visibility.Collapsed;
			BasicResults.Visibility = Visibility.Visible;
			//NumericalResults.Visibility = Visibility.Visible;
			//BasicResults.Visibility = Visibility.Collapsed;

			foreach (var x in ((e.Parameter as OptimalResponse).skills).GetRange(0, 1))
			{
				var Name = new TextBlock
				{
					FontSize = 20,
					Text = await Helpers.GetFriendlyName(x.soc),
				};
				var Power = new TextBlock
				{
					FontSize = 12,
					Text = "Confidence: " +  x.prediction
				};
				BasicResults.Children.Add(Name);
				BasicResults.Children.Add(Power);
			}


			var datas = e.Parameter as Models.Responses.OptimalResponse;
			t0.Text = datas.optimum[0].ToString();
			t0.Text = datas.optimum[0].ToString();
			t1.Text = datas.optimum[1].ToString();
			t2.Text = datas.optimum[2].ToString();
			t3.Text = datas.optimum[3].ToString();
			t4.Text = datas.optimum[4].ToString();
			t5.Text = datas.optimum[5].ToString();
			t6.Text = datas.optimum[6].ToString();
			t7.Text = datas.optimum[7].ToString();
			t8.Text = datas.optimum[8].ToString();
			t9.Text = datas.optimum[9].ToString();
			t10.Text = datas.optimum[10].ToString();
			t11.Text = datas.optimum[11].ToString();
			t12.Text = datas.optimum[12].ToString();
			t13.Text = datas.optimum[13].ToString();
			t14.Text = datas.optimum[14].ToString();
			t15.Text = datas.optimum[15].ToString();
			t16.Text = datas.optimum[16].ToString();
			t17.Text = datas.optimum[17].ToString();
			t18.Text = datas.optimum[18].ToString();
			t19.Text = datas.optimum[19].ToString();
			t20.Text = datas.optimum[20].ToString();
			t21.Text = datas.optimum[21].ToString();
			t22.Text = datas.optimum[22].ToString();
			t23.Text = datas.optimum[23].ToString();
			t24.Text = datas.optimum[24].ToString();
			t25.Text = datas.optimum[25].ToString();
			t26.Text = datas.optimum[26].ToString();
			t27.Text = datas.optimum[27].ToString();
			t28.Text = datas.optimum[28].ToString();
			t29.Text = datas.optimum[29].ToString();
			t30.Text = datas.optimum[30].ToString();
			t31.Text = datas.optimum[31].ToString();
			t32.Text = datas.optimum[32].ToString();
			t33.Text = datas.optimum[33].ToString();
			t34.Text = datas.optimum[34].ToString();

		}

		private void UIElement_OnTapped(object sender, RoutedEventArgs routedEventArgs)
		{
			NumericalResults.Visibility = NumericalResults.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			BasicResults.Visibility = BasicResults.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}
	}
}
