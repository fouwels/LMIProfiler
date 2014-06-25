// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using combob.Models.Responses;

namespace combob
{
	/// <summary>
	///     An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ResultsPage : Page
	{
		public ResultsPage()
		{
			InitializeComponent();
		}

		/// <summary>
		///     Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">
		///     Event data that describes how this page was reached.
		///     This parameter is typically used to configure the page.
		/// </param>
		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			NumericalResults.Visibility = Visibility.Collapsed;
			BasicResults.Visibility = Visibility.Visible;
			//NumericalResults.Visibility = Visibility.Visible;
			//BasicResults.Visibility = Visibility.Collapsed;

			foreach (Skill x in ((e.Parameter as OptimalResponse).skills).GetRange(0, 1))
			{
				var Name = new TextBlock
				{
					FontSize = 20,
					Text = await Helpers.GetFriendlyName(x.soc),
				};
				var Power = new TextBlock
				{
					FontSize = 12,
					Text = "Confidence: " + x.prediction
				};
				BasicResults.Children.Add(Name);
				BasicResults.Children.Add(Power);
			}

			//oh god
			var datas = e.Parameter as OptimalResponse;
			t0.Text = datas.optimum[0];
			t0.Text = datas.optimum[0];
			t1.Text = datas.optimum[1];
			t2.Text = datas.optimum[2];
			t3.Text = datas.optimum[3];
			t4.Text = datas.optimum[4];
			t5.Text = datas.optimum[5];
			t6.Text = datas.optimum[6];
			t7.Text = datas.optimum[7];
			t8.Text = datas.optimum[8];
			t9.Text = datas.optimum[9];
			t10.Text = datas.optimum[10];
			t11.Text = datas.optimum[11];
			t12.Text = datas.optimum[12];
			t13.Text = datas.optimum[13];
			t14.Text = datas.optimum[14];
			t15.Text = datas.optimum[15];
			t16.Text = datas.optimum[16];
			t17.Text = datas.optimum[17];
			t18.Text = datas.optimum[18];
			t19.Text = datas.optimum[19];
			t20.Text = datas.optimum[20];
			t21.Text = datas.optimum[21];
			t22.Text = datas.optimum[22];
			t23.Text = datas.optimum[23];
			t24.Text = datas.optimum[24];
			t25.Text = datas.optimum[25];
			t26.Text = datas.optimum[26];
			t27.Text = datas.optimum[27];
			t28.Text = datas.optimum[28];
			t29.Text = datas.optimum[29];
			t30.Text = datas.optimum[30];
			t31.Text = datas.optimum[31];
			t32.Text = datas.optimum[32];
			t33.Text = datas.optimum[33];
			t34.Text = datas.optimum[34];
		}

		private void UIElement_OnTapped(object sender, RoutedEventArgs routedEventArgs)
		{
			NumericalResults.Visibility = NumericalResults.Visibility == Visibility.Visible
				? Visibility.Collapsed
				: Visibility.Visible;
			BasicResults.Visibility = BasicResults.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}
	}
}