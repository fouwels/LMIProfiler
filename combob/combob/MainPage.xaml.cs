using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using combob.Data;
using combob.Models;

namespace combob
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{

		private Jobdat _currentJobdat;
		public Jobdat CurrentJobdat { get { return _currentJobdat; } }
		public List<Jobdat> JobdatSubmitTray;

		private Dictionary<string, string> onetLookup; 
		private List<Jobdat> _alJobdatsList;
		private int _currentJobdatsListIndex;
		public MainPage()
		{
			this.InitializeComponent();
			_currentJobdat = new Jobdat();
			
			Socs.all.Shuffle();

			this.NavigationCacheMode = NavigationCacheMode.Required;
		}
		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			YesButton.IsEnabled = false;
			NoButton.IsEnabled = false;
			JobdatSubmitTray = new List<Jobdat>();
			_alJobdatsList = await GetJobdatsFromExternal();
			foreach (var jobdat in _alJobdatsList)
			{
				jobdat.friendlyName = jobdat.onetcode;
				jobdat.skillsClean = jobdat.scales[0].skills;
				jobdat.skillsPreppedForSending = jobdat.skillsClean.OrderBy(x => x.name).Select(x => x.value).ToList(); // <3 linq
				jobdat.friendlyName = await Helpers.GetFriendlyName(onetLookup[jobdat.onetcode]);
			}

			_currentJobdatsListIndex = 0;
			_currentJobdat = _alJobdatsList.ElementAt(_currentJobdatsListIndex);
			Card.Text = _currentJobdat.friendlyName;
			NoButton.IsEnabled = true;
			YesButton.IsEnabled = true;
		}

		private async Task<List<Jobdat>> GetJobdatsFromExternal()
		{
			onetLookup = new Dictionary<string, string>();
			foreach (var soc in Socs.all.GetRange(0, 20)) //ANCHOR char42
			{
				try
				{
					var y = JsonConvert.DeserializeObject<Soc2Onet>(await Helpers.HttpGet("http://api.lmiforall.org.uk/api/v1/o-net/soc2onet/" + soc));
					onetLookup.Add(y.onetCodes[0].code, soc);
				}
				catch (Exception ex)
				{
					//throw;
				}

				
			}
			Debug.WriteLine("onet count: " + onetLookup.Count.ToString());
			var jobDats = new List<Jobdat>();
			foreach (var onet in onetLookup.Keys)
			{
				try
				{
					var y = JsonConvert.DeserializeObject<Jobdat>(await Helpers.HttpGet("http://api.lmiforall.org.uk/api/v1/o-net/skills/" + onet));
					if (y.scales.Count > 0)
					{
						jobDats.Add(y);	
					}
					
				}
				catch (Exception ex )
				{
					//throw;
				}
			}
			Debug.WriteLine("jobDat count: " + jobDats.Count.ToString());
			return jobDats;

		}

		private void Yes_OnTapped(object sender, TappedRoutedEventArgs e)
		{
			CurrentJobdat.acceptionValue = 1;
			JobdatSubmitTray.Add(CurrentJobdat);
			IncrementJobdatView();
		}

		private void No_OnTapped(object sender, TappedRoutedEventArgs e)
		{
			CurrentJobdat.acceptionValue = 1;
			JobdatSubmitTray.Add(CurrentJobdat);
			IncrementJobdatView();
		}

		private void IncrementJobdatView()
		{
			_currentJobdatsListIndex += 1;
			Debug.WriteLine("Card left in batch: " + (_alJobdatsList.Count - _currentJobdatsListIndex).ToString());
			if (_currentJobdatsListIndex > (_alJobdatsList.Count - 1))
			{
				//Card.Text = "Out of cards :(";
				Card.Text = "";
				SendToRegresser();
			}
			else
			{
				_currentJobdat = _alJobdatsList.ElementAt(_currentJobdatsListIndex);
				Card.Text = CurrentJobdat.friendlyName;
			}
		}
		private async void SendToRegresser()
		{
			//var payload = JobdatSubmitTray.Select(x => x.skillsPreppedForSending).ToList();

			//var payload2 = new List<List<double>>();

			//foreach (var item in JobdatSubmitTray)
			//{
			//	var y = item.skillsPreppedForSending;
			//	y.Add(item.acceptionValue);
			//	payload2.Add(y);
			//}
			//HttpGet("http://172.16.169.165:8080/?data=" + payload2);

			var pl = JobdatSubmitTray.Select(jobdat => new List<string>
			{
				jobdat.onetcode, jobdat.acceptionValue.ToString()
			}).ToList();

			string y;
			try
			{
				y = await Helpers.HttpGet("http://172.16.170.59:8080/?data=" + JsonConvert.SerializeObject(pl));
			}
			catch (Exception webException)
			{
				Debug.WriteLine(webException.Message);
				throw;
			}

			var z = JsonConvert.DeserializeObject<Models.Responses.OptimalResponse>(y);

			Frame.Navigate(typeof(ResultsPage), z);
		}
	}

}

