// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using combob.Data;
using combob.Models;
using combob.Models.Responses;
using Newtonsoft.Json;

namespace combob
{
	/// <summary>
	///     An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public List<Jobdat> JobdatSubmitTray;

		private List<Jobdat> _alJobdatsList;
		private Jobdat _currentJobdat;
		private int _currentJobdatsListIndex;
		private Dictionary<string, string> onetLookup;

		public MainPage()
		{
			InitializeComponent();
			_currentJobdat = new Jobdat();

			Socs.all.Shuffle();

			NavigationCacheMode = NavigationCacheMode.Required;
		}

		public Jobdat CurrentJobdat
		{
			get { return _currentJobdat; }
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			YesButton.IsEnabled = false;
			NoButton.IsEnabled = false;
			JobdatSubmitTray = new List<Jobdat>();
			_alJobdatsList = await GetJobdatsFromExternal();
			foreach (Jobdat jobdat in _alJobdatsList)
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
			foreach (string soc in Socs.all.GetRange(0, 20)) //ANCHOR char42
			{
				try
				{
					var y =
						JsonConvert.DeserializeObject<Soc2Onet>(
							await Helpers.HttpGet("http://api.lmiforall.org.uk/api/v1/o-net/soc2onet/" + soc));
					onetLookup.Add(y.onetCodes[0].code, soc);
				}
				catch (Exception ex)
				{
					//throw;
				}
			}
			Debug.WriteLine("onet count: " + onetLookup.Count);
			var jobDats = new List<Jobdat>();
			foreach (string onet in onetLookup.Keys)
			{
				try
				{
					var y =
						JsonConvert.DeserializeObject<Jobdat>(
							await Helpers.HttpGet("http://api.lmiforall.org.uk/api/v1/o-net/skills/" + onet));
					if (y.scales.Count > 0)
					{
						jobDats.Add(y);
					}
				}
				catch (Exception ex)
				{
					//throw;
				}
			}
			Debug.WriteLine("jobDat count: " + jobDats.Count);
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
			Debug.WriteLine("Card left in batch: " + (_alJobdatsList.Count - _currentJobdatsListIndex));
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

			List<List<string>> pl = JobdatSubmitTray.Select(jobdat => new List<string>
			{
				jobdat.onetcode,
				jobdat.acceptionValue.ToString()
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

			var z = JsonConvert.DeserializeObject<OptimalResponse>(y);

			Frame.Navigate(typeof (ResultsPage), z);
		}
	}
}